using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && Session["user_id"] != null)
            {
                // Show the logged-in user links and hide the login/signup links
                hplSignUp.Visible = false;
                hplLogin.Visible = false;
                hplCart.Visible = true;
                account.Visible = true;
            }
            else
            {
                // Show the login/signup links and hide the logged-in user links
                hplSignUp.Visible = true;
                hplLogin.Visible = true;
                hplCart.Visible = false;
                account.Visible = false;
            }
        }
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Index.aspx");
        }
    }
}