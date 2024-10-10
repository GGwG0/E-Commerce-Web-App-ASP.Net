using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class Global : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        public static string GetEmailByID(int id)
        {
            String email = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("select email from [User] where userID = @id;", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                email = command.ExecuteScalar().ToString();

            }
            return email;
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
        public static int GetUserIDByUsername(string username)
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
        public static string GetUsernameByUserId(int userid)
        {
            string result = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT username FROM [dbo].[User] WHERE userID = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", userid);

                    result = command.ExecuteScalar().ToString();



                }
            }
            return result;

        }
        public static string GetNameByUsername(string username)
        {
            string result = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT name FROM [dbo].[User] WHERE username = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", username);

                    result = command.ExecuteScalar().ToString();



                }
            }
            return result;

        }
        public static int AddNotification(string action, int requestID, int to, int from)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();  
            SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
            command2.Parameters.AddWithValue("@action", action);
            command2.Parameters.AddWithValue("@to", to);
            command2.Parameters.AddWithValue("@from", from);
            command2.Parameters.AddWithValue("@requestID", requestID);
            command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
            int notificationSucess = command2.ExecuteNonQuery();

            return notificationSucess;
        }
        public static void InsertAuditRecord(int req_ID, string action, int userID, int companyID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = "INSERT INTO [dbo].[Audit] (req_ID, Action, date, UserID, CompanyID) " +
                           "VALUES (@req_ID, @Action, @date, @UserID, @CompanyID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@req_ID", req_ID);
                    command.Parameters.AddWithValue("@Action", action);
                    command.Parameters.AddWithValue("@date", DateTime.Now);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CompanyID", companyID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception
                        Console.WriteLine($"Error: {ex.Message}");
                        // You might want to throw an exception or handle it according to your application's requirements
                    }
                }
            }
        }
        public static int GetCompanyID(int userid)
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

    }
}