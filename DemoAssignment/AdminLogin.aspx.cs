using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = Login1.UserName;
            string password = Login1.Password;

            if (Membership.ValidateUser(username, password) && Roles.IsUserInRole(username, "Admin"))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect("~/AuthenticatedUser/Admin/AdminDashboard.aspx"); // Redirect to the admin home page
            }
            else
            {
                lblMessage.Text = "Invalid username or password";
            }


        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("./AuthenticatedUser/Admin/AdminDashboard.aspx");
        }
    }
}