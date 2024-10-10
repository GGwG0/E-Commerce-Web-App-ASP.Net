using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                    command.Parameters.AddWithValue("@password", txtPassword.Text);

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

                if (status == "waiting")
                {
                    // Redirect to the Two-Factor Authentication page
                    Response.Redirect("FirstTimeLogin.aspx?username=" + username + "&googleAuthKey=" + GetUserGoogleAuthKey(username));
                }
                else if (status == "active")
                {
                    // Redirect to the Two-Factor Authentication page
                    Response.Redirect("TwoFactorAuthentication.aspx?username=" + username + "&googleAuthKey=" + GetUserGoogleAuthKey(username));
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