using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {

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







        protected void page_init(object sender, EventArgs e)
        {
            string script = @"
        var svgElement = document.getElementById('salesSVG');
        var circleElement = document.getElementById('salesCircle');
        
        if (circleElement) {
            // Calculate the desired stroke-dashoffset and stroke-dasharray values for 50% coverage
            var circleRadius = 36;
            var circumference = 2 * Math.PI * circleRadius;
            var desiredCoverage = 0; // Desired coverage percentage
            var dasharray = (desiredCoverage / 100) * circumference;
            var dashoffset = circumference - dasharray;
            
            // Set the calculated values to the circle element's style attribute
            circleElement.setAttribute('style', 'stroke-dashoffset: ' + dashoffset + '; stroke-dasharray: ' + dasharray + ';');
        }
    ";


            // Register the startup script
            ScriptManager.RegisterStartupScript(this, GetType(), "circleScript", script, true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // total sales within 24 hours
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT SUM(payment_amount) as total_sales" +
                " FROM[Order] INNER JOIN Payment ON[Order].payment_id = Payment.id" +
                " WHERE DATEDIFF(hour, payment_datetime, GETDATE()) <= 24", con);

                con.Open();
                Decimal totalSalesWithin24 = Convert.ToDecimal(command.ExecuteScalar());
                con.Close();
                Decimal targetSales = 1000000.00m;
                Decimal percentages = Decimal.Multiply(Decimal.Divide(totalSalesWithin24, targetSales), 100);

                lblTotalSalesWithin24.Text = totalSalesWithin24.ToString("0.00");
                lblPercentage.Text = percentages.ToString("0");

                // total expenses within 24 hours
                SqlCommand command2 = new SqlCommand("SELECT SUM((ov.price * oi.purchase_qty)) + SUM(pv.product_cost) AS total_expenses\r\nFROM [dbo].[Order] o JOIN [dbo].[Order_Item] oi ON oi.order_id = o.id\r\nJOIN [dbo].[Product_Variation] ov ON ov.id = oi.variation_id\r\nJOIN (\r\n    SELECT p.id, SUM((pv.price * pv.stock_quantity)) AS product_cost\r\n    FROM [dbo].[Product] p\r\n    JOIN [dbo].[Product_Variation] pv ON pv.product_id = p.id\r\n    GROUP BY p.id\r\n) pv ON pv.id = ov.product_id\r\nWHERE o.orderDatetime >= DATEADD(hour, -24, GETDATE())", con);

                con.Open();
                Decimal totalExpense = Convert.ToDecimal(command2.ExecuteScalar());
                con.Close();

                Decimal targetExpense = 500000m;

                lblExpense.Text = totalExpense.ToString("0.00");

                lblExpensePercent.Text = Decimal.Multiply(Decimal.Divide(totalExpense, targetExpense), 100).ToString("0");


                // total income within 24 hours
                Decimal totalIncome = Decimal.Subtract(totalExpense, totalSalesWithin24);
                lblIncome.Text = totalIncome.ToString("0.00");
                Decimal targetIncome = 900990m;
                lblIncomePercent.Text = Decimal.Multiply(Decimal.Divide(totalIncome, targetIncome), 100).ToString("0");



                //string script = @" var svgElement = document.getElementById('svgSales');" +
                //                   "var circleElement = document.getElementById('circleSales'); if (circleElement) {var circleRadius = 36.0; var circumference = 2.0 * Math.PI * circleRadius;"
                //                    + "var desiredCoverage = " + percentages.ToString("0") + "; var dasharray = (desiredCoverage / 100.0) * circumference;"
                //                    + "var dashoffset = circumference - dasharray;"
                //                    + "circleElement.setAttribute('style', 'stroke-dashoffset: ' + dashoffset + '; stroke-dasharray: ' + dasharray + ';');}";


                //ClientScript.RegisterStartupScript(this.GetType(), "CustomScript", script, true);


            }

        }

    }
}