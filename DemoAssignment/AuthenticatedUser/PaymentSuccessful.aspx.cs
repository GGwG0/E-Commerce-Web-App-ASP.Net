using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser
{
    public partial class PaymentSuccessful : System.Web.UI.Page
    {

        protected void Page_init(object sender, EventArgs e)
        {
            orderidDisplay.Text = Request.QueryString["orderId"];
            OrderStatus.Text = "Packaging";
            orderdate.Text = DateTime.Now.ToString();
            estimatedate.Text = DateTime.Today.AddDays(8).ToString();
            paymentdate.Text = DateTime.Now.ToString();
            paymentamount.Text = Request.QueryString["Amount"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}