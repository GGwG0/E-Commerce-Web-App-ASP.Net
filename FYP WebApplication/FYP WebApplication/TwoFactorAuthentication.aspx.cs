using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class TwoFactorAuthentication : System.Web.UI.Page
    {
        string randomNumber = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string GetRole(int userid)
        {
            String roleName = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("select \r\nR.roleName from [User] U  \r\nleft join \r\n\t[User_Role] UR \r\n\ton U.userID = UR.userID \r\nleft join \r\n\t[Role] R \r\n\ton UR.roleID = R.roleID \r\nwhere U.userID = @userId;", connection);
                command.Parameters.AddWithValue("@userId", userid);
                connection.Open();
                roleName = command.ExecuteScalar().ToString();

            }
            return roleName;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            if (txtUsername.Text == "")
            {
                string username = Request.QueryString["username"];
                int uid = GetUserIDByUsername(username);
                string role = GetRole(uid);
                randomNumber = GetOTPByUserId(uid);
                if (txtPassword.Text.Equals(randomNumber))
                {

                    Session["userid"] = uid;
                    Session["currentRole"] = role;
                    Global.InsertAuditRecord(0, "Logged In", Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));


                    if (role == "cosec user" || role == "client user")
                    {
                        if (role == "client user")
                        {
                            Session["cosecId"] = GetCosecIdFromCompany(uid);
                        }

                        Response.Redirect("RequestDashboard.aspx");
                    }
                    else if (role == "service user" || role == "service admin" || role == "client admin" || role == "cosec admin")
                    {
                        Response.Redirect("ManageUser.aspx");
                    }
                    else
                    {
                        string script = "alert('Role Problem');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                    }
                }
                else
                {
                    string script = "alert('Incorrect Password');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                }
            }
            else
            {

                string username = Request.QueryString["username"];
                string googleAuthKey = Request.QueryString["googleAuthKey"];

                string verificationCode = txtUsername.Text.Trim();

                TwoFactorAuthenticator twoFactorAuthenticator = new TwoFactorAuthenticator();
                bool isValid = twoFactorAuthenticator.ValidateTwoFactorPIN(googleAuthKey, verificationCode, true);



                if (isValid)
                {
                    int uid = GetUserIDByUsername(username);
                    string role = GetRole(uid);

                    Session["userid"] = uid;
                    Session["currentRole"] = role;
                    Global.InsertAuditRecord(0, "Logged In", Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

                    if (role == "cosec user" || role == "client user")
                    {
                        if (role == "client user")
                        {
                            Session["cosecId"] = GetCosecIdFromCompany(uid);
                            string cosecid = Session["cosecId"].ToString();
                        }

                        Response.Redirect("RequestDashboard.aspx");
                    }
                    else if (role == "service user" || role == "service admin" || role == "client admin" || role == "cosec admin")
                    {
                        Response.Redirect("ManageUser.aspx");
                    }
                    else
                    {
                        string script = "alert('Your account is inactive. Please contact your admin to activate it.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                    }

                }
                else
                {
                    string script = "alert('Incorrect OTP');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                }
            }



        }

        public static int GetCosecIdFromCompany(int userId)
        {
            int cosecId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "Select C.cosecId from [User] U  LEFT JOIN Company C ON U.companyID = C.companyID  where userID = @userId;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        cosecId = Convert.ToInt32(result);
                    }
                }
            }
            return cosecId;
        }

        protected int GetUserIDByUsername(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            int userID = -1; // Default value if user not found

            // Create a SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Use the SqlConnection object when creating the SqlCommand
                using (SqlCommand command = new SqlCommand("SELECT [userID] FROM [dbo].[User] WHERE [username] = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // User found, retrieve the userID
                            userID = Convert.ToInt32(reader["userID"]);
                        }
                    }
                }
            }

            return userID;
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string username = Request.QueryString["username"];
            int uid = GetUserIDByUsername(username);
            string randomNumbers = GenerateRandomOTP();
            UpdateOTP(uid, randomNumbers);
            GetEmailsByUserId(uid, randomNumbers);

        }

        private string GetOTPByUserId(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the SQL query
                string query = "SELECT [OTP] FROM [dbo].[User] WHERE [userID] = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter for the user ID
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Execute the query
                    object result = command.ExecuteScalar();

                    // Check if there is a result and return it as a string
                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }

        private string GetEmailsByUserId(int userId, string randomNumbers)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the SQL query
                string query = "SELECT * FROM [dbo].[User] WHERE [userID] = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter for the user ID
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Execute the query
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are rows returned
                        if (reader.HasRows)
                        {
                            // Read the first row (assuming there's only one row for the user ID)
                            reader.Read();

                            MailMessage message = new MailMessage();

                            // Set the sender, recipient, subject, and body of the email
                            String to = reader["email"].ToString();

                            message.From = new MailAddress("awieling0777@gmail.com");
                            message.To.Add(new MailAddress(to));
                            message.Subject = "Login OTP";
                            message.Body = "Dear Mr./Mrs.  " + reader["name"].ToString() + "\n" +
                                "The Code for the OTP is: \n" + randomNumbers;

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
                    }
                }
            }

            // Return an empty string if no emails are found
            return string.Empty;
        }

        private string GenerateRandomOTP()
        {
            // Create an instance of Random class
            Random random = new Random();

            // Generate a 6-digit random number
            int randomNumber = random.Next(100000, 1000000);

            // Convert the random number to a string and return it
            return randomNumber.ToString();
        }

        private void UpdateOTP(int userId, string newOTPValue)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a command to execute the SQL query
                string query = "UPDATE [dbo].[User] SET [OTP] = @NewOTP WHERE [userID] = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for the new OTP value and user ID
                    command.Parameters.AddWithValue("@NewOTP", newOTPValue);
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}