using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class FirstTimeLogin2 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            title2f.InnerText = Request.QueryString["username"];
        }

        protected void logins_Click(object sender, EventArgs e)
        {


            Global.InsertAuditRecord(0, "First Time Login: " + Request.QueryString["username"], Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
            //after click i understand
            Session["userid"] = GetUserIdByUsername(Request.QueryString["username"]);

            String roleName = GetRole(Convert.ToInt32(Session["userid"]));
            Session["currentRole"] = roleName;
            if(roleName == "client user")
            {
                Session["cosecId"] = TwoFactorAuthentication.GetCosecIdFromCompany(Convert.ToInt32(Session["userid"]));
            }
            if (roleName == "cosec user" || roleName == "client user")
            {
                Response.Redirect("RequestDashboard.aspx");
            }
            else if (roleName == "service user" || roleName == "service admin" || roleName == "client admin" || roleName == "cosec admin")
            {
                Response.Redirect("ManageUser.aspx");
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        private int GetUserIdByUsername(string username)
        {
            int userId = -1; // Default value indicating failure

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT userID FROM [dbo].[User] WHERE username = @username";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    // ExecuteScalar returns the first column of the first row
                    object result = command.ExecuteScalar();

                    // Check if the result is not null before converting to int
                    if (result != null && int.TryParse(result.ToString(), out userId))
                    {

                        // Successfully retrieved user ID
                    }
                    else
                    {
                        // Handle the case where the username does not exist or the userID is not a valid integer
                    }
                }
            }

            return userId;
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

    }
}