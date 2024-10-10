using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DemoAssignment
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }
        protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
        {
            string userName = PasswordRecovery1.UserName;
            Session["RequireResetPassword"] = userName;
            Session["PasswordAnswer"] = PasswordRecovery1.Answer;
            string passwordResetUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/Login.aspx") ;
            e.Message.Body += "Please reset your password after login:";
            e.Message.Body += passwordResetUrl;
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PasswordRecovery.aspx");
        }
        protected void PasswordRecovery1_UserNameTemplateContainer_Load(object sender, EventArgs e)
        {
            string userName = ((TextBox)PasswordRecovery1.UserNameTemplateContainer.FindControl("UserName")).Text;

            // Check if the user is locked out
            //if (Membership.GetUser(userName).IsLockedOut)
            //{
            //    Membership.GetUser(userName).UnlockUser();
            //    // Display an error message
            //    //Label errorMessage = (Label)PasswordRecovery1.UserNameTemplateContainer.FindControl("ErrorMessage");
            //    //errorMessage.Text = "Your account is locked out. Please contact the administrator to unlock your account.";
            //}
        }

    }
}