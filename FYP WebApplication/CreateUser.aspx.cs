using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace FYP_WebApplication
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                if (Session["currentRole"].ToString() == "client admin")
                {
                    companyCtn.Visible = false;

                }

                rblRole_DataBinding();
                ddlCompany_DataBinding();
            }
        }

        public static string GenerateUniqueUsername(string name)
        {
            string baseUsername = GenerateBaseUsername(name);
            string uniqueUsername = baseUsername;

            // Check if the username already exists in the database
            while (IsUsernameExists(uniqueUsername))
            {
                // If exists, add a random number to make it unique
                uniqueUsername = baseUsername + GenerateRandomNumber();
            }

            return uniqueUsername;
        }
        private static string GenerateBaseUsername(string name)
        {
            // You can customize the logic for generating the base username
            // For example, using the full name or a specific part of it
            return name.ToLower().Replace(" ", "");
        }
        private static string GenerateRandomNumber()
        {
            // Generate a random number to append to the username
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
        private static bool IsUsernameExists(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        protected static string GenerateRandomPassword()
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            Random random = new Random();


            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(chars.Length);
                password.Append(chars[index]);
            }

            return password.ToString();
        }
        private string GenerateGoogleAuthKey()
        {
            // Implement your logic to generate a unique key
            // For simplicity, let's assume you generate a random 16-character string
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string key = GenerateGoogleAuthKey();

            string username = GenerateUniqueUsername(txtName.Text);
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Insert into [User] (username, name, contactNum, position, email, managedBy, companyID, status, password, googleAuthKey) values (@username, @name, @contactNum, @position, @email, @managedBy, @comId, @status,@password, @googleAuthKey); SELECT SCOPE_IDENTITY();";
            string query = "Insert into User_Role values (@userID, @roleID);";
            string password = GenerateRandomPassword();
            int companyID = 0;

            //@name, @contactNum, @position, @email, @managedBy, @comId
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@name", txtName.Text);
                    command.Parameters.AddWithValue("@contactNum", txtPhoneNum.Text);
                    command.Parameters.AddWithValue("@position", txtPosition.Text);
                    command.Parameters.AddWithValue("@email", txtEmail.Text);
                    command.Parameters.AddWithValue("@managedBy", Convert.ToInt32(Session["userid"]));

                    // service user, cosec user, client user
                    //companyID = current user's companyID

                    // role = cosec admin, client admin 
                    // company ID = selectedValue

                    if (rblRole2.SelectedItem.Text == "service user" || rblRole2.SelectedItem.Text == "cosec user" || rblRole2.SelectedItem.Text == "client user")
                    {
                        companyID = GetCompanyID(Convert.ToInt32(Session["userid"]));
                    }
                    else
                    {
                        companyID = Convert.ToInt32(ddlCompany.SelectedValue);
                    }


                    string hashedPassword = HashPassword(password);

                    command.Parameters.AddWithValue("@comId", companyID);
                    command.Parameters.AddWithValue("@status", "waiting");
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@googleAuthKey", key);


                    int id = Convert.ToInt32(command.ExecuteScalar());

                    if (id != 0)
                    {
                        SqlCommand command2 = new SqlCommand(query, connection);
                        command2.Parameters.AddWithValue("@userID", id);
                        command2.Parameters.AddWithValue("@roleID", rblRole2.SelectedValue);
                        int sucess = command2.ExecuteNonQuery();

                        if (sucess > 0)
                        {
                            Global.InsertAuditRecord(id, "Created a New User: " + id, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
                            string script = "alert('Successfully create user');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);

                            sendEmail(username, password);
                            int a = 0;
                            a++;
                            if (a == 1)
                            {
                                Response.Redirect("ManageUser.aspx");
                            }
                        }

                    }
                }
            }



        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the hashed bytes to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        protected void sendEmail(string username, string password)
        {
            MailMessage message = new MailMessage();

            // Set the sender, recipient, subject, and body of the email
            String to = txtEmail.Text;

            message.From = new MailAddress("awieling0777@gmail.com");
            message.To.Add(new MailAddress(to));
            message.Subject = "Account Created for Company Secretary System";
            message.Body = "Dear sir/ madam,  " + "\n" + " Good day! Your account for the company secretary system has been created. " +
                "Please be informed that you have to change the password to activate your account successfully." 
                + "\n\n" + "Username : " + username + "\n"
                + "Password : " + password + "\n" + "Please contact admin if you find any problem.";

            // Create a new SmtpClient object
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // Configure the SMTP client with your email server settings
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("awieling0777@gmail.com", "vpiaoqahiewobkxu");
            client.EnableSsl = true;

            // Send the email message
            client.Send(message);

        }

        protected int getCosecIdFromCompany(int comId)
        {
            int cosecId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "Select cosecId from Company where companyID = @comId;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@comId", comId);
                    cosecId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return cosecId;
        }

        protected void rblRole_DataBinding()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "Select * from Role";

            string roleName = RequestDashboard.GetRole(Convert.ToInt32(Session["userid"]));

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            string text = reader["roleName"].ToString();
                            string id = reader["roleID"].ToString();

                            if (roleName == "service admin")
                            {
                                if (text == "service user" || text == "cosec admin")
                                {
                                    rblRole2.Items.Add(new ListItem(text, id));
                                }
                            }
                            else if (roleName == "cosec admin")
                            {
                                if (text == "cosec user" || text == "client admin")
                                {
                                    rblRole2.Items.Add(new ListItem(text, id));
                                }
                            }
                            else if (roleName == "client admin")
                            {
                                if (text == "client user")
                                {
                                    rblRole2.Items.Add(new ListItem(text, id));
                                    rblRole2.Items[0].Selected = true;
                                    rblRole2.Visible = false;
                                    roleContainer.Visible = false;
                                }
                            }
                            else
                            {
                                if (text == "cosec admin")
                                {
                                    rblRole2.Items.Add(new ListItem(text, id));
                                    rblRole2.Items[0].Selected = true;
                                    rblRole2.Visible = false;
                                    roleContainer.Visible = false;
                                }
                            }
                            //  service admins -service admin, service user, cosec admin
                            //  cosec admins - cosec user, client admins
                            //  client admins -client user
                        }
                    }


                }

            }
        }
        protected void rblRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = rblRole2.SelectedItem.Text;


            if (selectedRole == "service user" || selectedRole == "cosec user" || selectedRole == "client user")
            {
                companyCtn.Visible = false;
            }
            else
            {
                companyCtn.Visible = true;
            }

        }


        protected void ddlCompany_DataBinding()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = string.Empty;
            if (Session["currentRole"].ToString() == "service user" || Session["currentRole"].ToString() == "service admin")
            {
                selectQuery = "SELECT * FROM Company WHERE createdBy = 1 AND NOT EXISTS( SELECT 1 FROM [User] WHERE Company.companyID = [User].companyID)";
            }
            else if (Session["currentRole"].ToString() == "cosec admin")
            {
                selectQuery = "SELECT *\r\nFROM Company\r\nWHERE createdBy = @id\r\n  AND NOT EXISTS (\r\n      SELECT 1\r\n      FROM [User]\r\n      WHERE Company.companyID = [User].companyID\r\n  );";
            }
            else
            {
                selectQuery = "Select * from Company where companyID = @yourCurrentCompanyID; ";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@yourCurrentCompanyID", GetCompanyID(Convert.ToInt32(Session["userid"])));
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {


                        while (reader.Read())
                        {
                            string text = reader["comName"].ToString();
                            string id = reader["companyID"].ToString();
                            ddlCompany.Items.Add(new ListItem(text, id));
                        }
                    }
                }
            }
        }

        public int GetCompanyID(int userid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the SQL query to retrieve the company ID
                string query = "SELECT [companyID] FROM [dbo].[User] WHERE [userID] = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the query
                    command.Parameters.AddWithValue("@userID", userid);

                    // Execute the query and get the result
                    object companyId = command.ExecuteScalar();

                    // Check if the result is not null
                    if (companyId != null && companyId != DBNull.Value)
                    {
                        // Return the company ID as an int
                        return Convert.ToInt32(companyId);
                    }
                }
            }

            return 0;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUser.aspx");
        }


    }
}