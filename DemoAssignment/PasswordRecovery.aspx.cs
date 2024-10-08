using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Services.Description;
using System.Text.RegularExpressions;

namespace DemoAssignment
{
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String name = Session["Username"].ToString();
               // String name = HttpContext.Current.User.Identity.Name;
                ChangePassword1.UserName = name;

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
           Session.Clear();
            Response.Redirect("~/Index.aspx");
        }

        protected void ChangePassword1_ChangedPassword(object sender, EventArgs e)
        {
               
            String userName = ChangePassword1.UserName;
            String newPassword = ChangePassword1.NewPassword;

            // Get the user object from the membership system
            MembershipUser user = Membership.GetUser(userName);

            // Change the user's password
            bool passwordChanged = Membership.Provider.ChangePassword(userName, ChangePassword1.CurrentPassword, newPassword);

            // Save the changes to the membership system
            Membership.UpdateUser(user);
            // Redirect the user to the success page

            if(passwordChanged == false)
            {
            
                // Display an error message
                ChangePassword1.ChangePasswordFailureText = "Unable to change password. Please try again.";
            }
            else
            {
                
            }
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            //Session["RequireResetPassword"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}