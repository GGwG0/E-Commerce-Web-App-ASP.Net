using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayCustomerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string GetProfileImageUrl(object profile)
        {
            string fileName = profile.ToString();

            string imagePath = "../../assets/images/profile/";

            return imagePath + fileName;
        }
        protected string GetStatusText(object status)
        {
            if (status != null && status.ToString() == "1")
            {
                return "Active";
            }
            else
            {
                return "Inactive";
            }
        }

    }
}