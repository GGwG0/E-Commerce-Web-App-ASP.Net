using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.Services.Description;

namespace DemoAssignment
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["problem"] != null)
                {
                    string message = "You have been banned. Please contact the administrator for more information.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                }
            }

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = Login1.UserName;
            string password = Login1.Password;



            if (Membership.ValidateUser(username, password) && Roles.IsUserInRole(username, "Admin"))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect("~/AuthenticatedUser/Admin/AdminDashboard.aspx"); // Redirect to the admin home page
            }
            else if (Membership.ValidateUser(username, password))
            {

                (int id, int status) = getIdByName(username);

                MembershipUser user = Membership.GetUser(username);

                if (status == 1)
                {
                    Session["user_id"] = id;
                    Session["Username"] = Login1.UserName;

                    if (Session["RequireResetPassword"] != null && Session["RequireResetPassword"].ToString() == Login1.UserName)
                    {
                        Session["RequireResetPassword"] = null;
                        Response.Redirect("PasswordRecovery.aspx");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else if (status == 2)
                {
                    string message = "You have been banned. Please contact the administrator for more information.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "'); window.location.href='Login.aspx';", true);
                    Response.Redirect("Login.aspx?problem=banned");

                }
                else if (user.IsLockedOut)
                {

                    String message = "You have fail to login too many times! Please contact the admin!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                }



            }
            else
            {
                lblMessage.Text = "Invalid username or password";
            }

        }
        protected void unlockAccount()
        {
            // Get the MembershipUser object for the user
            MembershipUser user = Membership.GetUser(Login1.UserName);

            if (user != null && user.IsLockedOut)
            {
                user.UnlockUser();
            }

        }
        private (int,int) getIdByName(string name)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT id, status FROM [User] WHERE login_name = @name;", con);
            command.Parameters.AddWithValue("@name", name);

            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            int id = 0;
            int status = 0;
            if (reader.Read())
            {
                id = Convert.ToInt32(reader["id"]);
                status = Convert.ToInt32(reader["status"]);
            }
            con.Close();

            return (id, status);

        }

    }
}