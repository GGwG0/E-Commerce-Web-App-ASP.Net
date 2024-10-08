using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)GridView1.SelectedDataKey.Value;
            string url = "DisplayCustomerDetails.aspx?id=" + id;
            Response.Redirect(url);
        }
    }
}