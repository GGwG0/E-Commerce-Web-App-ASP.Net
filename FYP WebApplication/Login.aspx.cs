using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace FYP_WebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }

        protected bool VerifyUser()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            //  string query = "SELECT * FROM [User] WHERE username = @username AND PasswordHash = HASHBYTES('SHA2_256', @input_password);";
            string query = "SELECT * FROM [User] WHERE username = @username AND password = @password;";
            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming the column storing the Google Authenticator key is named 'googleAuthKey'

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    command.Parameters.AddWithValue("@password", HashPassword(txtPassword.Text));

                    result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Authenticate the user based on username and password (add your logic here)

            // For simplicity, assume the user is authenticated
            bool isAuthenticated = VerifyUser();



            if (isAuthenticated)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string status = null;
                string mfa = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT [status] FROM [dbo].[User] WHERE username = @username";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        // ExecuteScalar returns the first column of the first row
                        object result = command.ExecuteScalar();

                        status = result.ToString();

                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT [mfa] FROM [dbo].[User] WHERE username = @username";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        // ExecuteScalar returns the first column of the first row
                        object result = command.ExecuteScalar();

                        mfa = result.ToString();

                    }
                }

                if (status == "waiting")
                {
                    // Redirect to the Two-Factor Authentication page
                    Response.Redirect("FirstTimeLogin.aspx?username=" + username + "&googleAuthKey=" + GetUserGoogleAuthKey(username));
                }
                else if (status == "active")
                {
                    if (mfa == "yes")
                    {
                        // Redirect to the Two-Factor Authentication page
                        Response.Redirect("TwoFactorAuthentication.aspx?username=" + username + "&googleAuthKey=" + GetUserGoogleAuthKey(username));

                    }
                    else
                    {
                        int uid = Global.GetUserIDByUsername(username);
                        string role = Global.GetRole(uid);
                        Session["userid"] = uid;
                        Session["currentRole"] = role;
                        Global.InsertAuditRecord(0, "Logged In", Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

                        if (role == "cosec user" || role == "client user")
                        {
                            if (role == "client user")
                            {
                                Session["cosecId"] = TwoFactorAuthentication.GetCosecIdFromCompany(uid);
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

                }
                else
                {
                    string script = "alert('Your account is being deactivated, please contact admin for more');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                }



            }
            else
            {
                //error page
                string script = "alert('Your username/ password is incorrect.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
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

        private string GetUserGoogleAuthKey(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            // Replace the connection string with your actual connection string

            object result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming the column storing the Google Authenticator key is named 'googleAuthKey'
                string sql = "SELECT googleAuthKey FROM [dbo].[User] WHERE username = @username";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    result = command.ExecuteScalar();
                }
            }

            return result.ToString();
        }

    }
}