using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayRatingDetails : System.Web.UI.Page
    {

        protected void page_init(object sender, EventArgs e)
        {
            SqlDataSource2.SelectParameters["id"].DefaultValue = Request.QueryString["id"];
            SqlDataSource2.UpdateParameters["id"].DefaultValue = Request.QueryString["id"];
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}