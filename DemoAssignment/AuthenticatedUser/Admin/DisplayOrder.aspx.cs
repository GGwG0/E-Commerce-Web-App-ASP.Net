using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayOrder : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected string GetStatusText(object status)
        {
            if (status != null && status.ToString() == "1")
            {
                return "Packaging";
            }
            else if (status != null && status.ToString() == "2")
            {
                return "Shipping";
            }
            else
            {
                return "Completed";
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)GridView1.SelectedDataKey.Value;
            string url = "DisplayOrderDetails.aspx?id=" + id;
            Response.Redirect(url);
        }
    }
}