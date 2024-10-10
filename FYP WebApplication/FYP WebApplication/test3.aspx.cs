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
    public partial class test3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("test2.aspx?id=" + GenerateGoogleAuthKey());
        }

        protected string GetGoogleAuthKey(int userID)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [googleAuthKey] FROM [dbo].[User] WHERE [userID] = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();

                    var result = command.ExecuteScalar();

                    // Check if the result is not null before returning
                    return result != null ? result.ToString() : null;
                }
            }
        }

        private string GenerateGoogleAuthKey()
        {
            // Implement your logic to generate a unique key
            // For simplicity, let's assume you generate a random 16-character string
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}