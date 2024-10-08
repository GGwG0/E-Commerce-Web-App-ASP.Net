using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DemoAssignment.AuthenticatedUser
{
    public partial class PurchaseHistory : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            BindCartData(Convert.ToInt32(Session["user_id"]), "All");
            selectionitem.CssClass = "selection-item active";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string status = Request.QueryString["status"];

            if (!string.IsNullOrEmpty(status))
            {
                // Do something based on the selected status
                switch (status)
                {
                    case "to-package":
                        BindCartData(Convert.ToInt32(Session["user_id"]), "to-package");
                        selectionitem2.CssClass = "selection-item active";
                        selectionitem.CssClass = "selection-item ";
                        break;
                    case "to-ship":
                        BindCartData(Convert.ToInt32(Session["user_id"]), "to-ship");
                        selectionitem3.CssClass = "selection-item active";
                        selectionitem.CssClass = "selection-item ";
                        break;
                    case "to-completed":
                        BindCartData(Convert.ToInt32(Session["user_id"]), "to-completed");
                        selectionitem4.CssClass = "selection-item active";
                        selectionitem.CssClass = "selection-item ";
                        break;
                }
            }
            else
            {
                BindCartData(Convert.ToInt32(Session["user_id"]), "All");
                selectionitem.CssClass = "selection-item active";
            }

        }

        private void BindCartData(int userIds, String choice)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            if (choice == "All")
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT \r\n uo.status AS Status, oi.variation_id AS VariationID, oi.rating_id AS RatingID ,oi.order_id AS orderID, \r\n    p.id AS ProductID, \r\n    oi.id AS CartItemId, \r\n    p.product_image_1 AS ImageUrl, \r\n    p.product_name AS ProductName,\r\n    pv.variation_name, \r\n    pv.price AS UnitPrice, \r\n    oi.purchase_qty AS Quantity, \r\n    pv.price * oi.purchase_qty AS Price\r\nFROM \r\n    [Order_Item] oi\r\nJOIN \r\n    [Product_Variation] pv ON oi.variation_id = pv.id\r\nJOIN \r\n    [Product] p ON pv.product_id = p.id\r\nJOIN \r\n    [Order] uo ON oi.order_id = uo.id\r\nWHERE \r\n    uo.user_id = @usd;\r\n", connection);
                    command.Parameters.AddWithValue("@usd", userIds);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();


                }
            }
            else if (choice == "to-package")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT \r\n uo.status AS Status, oi.variation_id AS VariationID,  oi.rating_id AS RatingID ,oi.order_id AS orderID, \r\n    p.id AS ProductID, \r\n    oi.id AS CartItemId, \r\n    p.product_image_1 AS ImageUrl, \r\n    p.product_name AS ProductName,\r\n    pv.variation_name, \r\n    pv.price AS UnitPrice, \r\n    oi.purchase_qty AS Quantity, \r\n    pv.price * oi.purchase_qty AS Price\r\nFROM \r\n    [Order_Item] oi\r\nJOIN \r\n    [Product_Variation] pv ON oi.variation_id = pv.id\r\nJOIN \r\n    [Product] p ON pv.product_id = p.id\r\nJOIN \r\n    [Order] uo ON oi.order_id = uo.id\r\nWHERE \r\n    uo.user_id = @usd AND uo.status = 1 ;\r\n", connection);
                    command.Parameters.AddWithValue("@usd", userIds);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();


                }
            }
            else if (choice == "to-ship")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT \r\n uo.status AS Status, oi.variation_id AS VariationID,  oi.rating_id AS RatingID ,oi.order_id AS orderID, \r\n    p.id AS ProductID, \r\n    oi.id AS CartItemId, \r\n    p.product_image_1 AS ImageUrl, \r\n    p.product_name AS ProductName,\r\n    pv.variation_name, \r\n    pv.price AS UnitPrice, \r\n    oi.purchase_qty AS Quantity, \r\n    pv.price * oi.purchase_qty AS Price\r\nFROM \r\n    [Order_Item] oi\r\nJOIN \r\n    [Product_Variation] pv ON oi.variation_id = pv.id\r\nJOIN \r\n    [Product] p ON pv.product_id = p.id\r\nJOIN \r\n    [Order] uo ON oi.order_id = uo.id\r\nWHERE \r\n    uo.user_id = @usd AND uo.status = 2 ;\r\n", connection);
                    command.Parameters.AddWithValue("@usd", userIds);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();


                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SELECT \r\n uo.status AS Status, oi.variation_id AS VariationID,  oi.rating_id AS RatingID ,oi.order_id AS orderID, \r\n    p.id AS ProductID, \r\n    oi.id AS CartItemId, \r\n    p.product_image_1 AS ImageUrl, \r\n    p.product_name AS ProductName,\r\n    pv.variation_name, \r\n    pv.price AS UnitPrice, \r\n    oi.purchase_qty AS Quantity, \r\n    pv.price * oi.purchase_qty AS Price\r\nFROM \r\n    [Order_Item] oi\r\nJOIN \r\n    [Product_Variation] pv ON oi.variation_id = pv.id\r\nJOIN \r\n    [Product] p ON pv.product_id = p.id\r\nJOIN \r\n    [Order] uo ON oi.order_id = uo.id\r\nWHERE \r\n    uo.user_id = @usd AND uo.status = 3 ;\r\n", connection);
                    command.Parameters.AddWithValue("@usd", userIds);


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();


                }
            }

            foreach (RepeaterItem item in Repeater1.Items)
            {
                Button ratingButton = (Button)item.FindControl("myBtn");
                HiddenField ratingIdField = (HiddenField)item.FindControl("RatingID");
                HtmlTableCell statusCell = (HtmlTableCell)item.FindControl("status");
                HiddenField statusPackage = (HiddenField)item.FindControl("Statusvalue");
                int statusValue = int.Parse(statusPackage.Value);

                if (string.IsNullOrEmpty(ratingIdField.Value))
                {
                    ratingButton.Enabled = true;
                }
                else
                {
                    ratingButton.Enabled = false;
                    ratingButton.Text = "Rated";
                }

                if (statusValue == 1)
                {
                    statusCell.InnerText = "Packaging";

                }
                else if (statusValue == 2)
                {
                    statusCell.InnerText = "Shipping";
                }
                else
                {
                    statusCell.InnerText = "Completed";
                }
            }
        }

        protected void myBtn_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.ToString().Split(',');
            string cartItemId = args[0];
            string productName = args[1];
            string variationName = args[2];
            string imageUrl = args[3];
            string variationID = args[4];


            Response.Redirect("rate.aspx?cartItemId=" + cartItemId + "&productName=" + productName + "&variationName=" + variationName + "&imageUrl=" + imageUrl + "&variationid=" + variationID);



        }

        protected void viewDetails_Click(object sender, EventArgs e)
        {

        }
    }
}