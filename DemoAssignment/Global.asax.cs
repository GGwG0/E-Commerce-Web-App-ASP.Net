using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DemoAssignment
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            if (Membership.FindUsersByName("admin").Count == 0)
            {
                Membership.CreateUser("admin", "admin");

            }

            if (!Roles.IsUserInRole("admin", "Admin"))
                Roles.AddUserToRole("admin", "Admin");


            
        }
       
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}