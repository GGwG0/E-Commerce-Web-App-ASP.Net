using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeaterData();
                BindRepeaterData2();
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    div2.Visible = false;
                    div1.Visible = true;

                }
                else
                {
                    div1.Visible = false;
                    div2.Visible = true;
                }
            }

        }



        private void BindRepeaterData()
        {
            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL query
            string query = "SELECT TOP 10 P.id, P.product_name AS ProductName, P.product_image_1 AS ImageUrl, V.id AS variation_id, V.variation_name AS variation_name, " +
                           "OI.total_qty, V.price, (OI.total_qty * V.price) AS total_price " +
                           "FROM Product P " +
                           "INNER JOIN Product_Variation V ON P.id = V.product_id " +
                           "INNER JOIN ( " +
                           "SELECT DISTINCT variation_id, SUM(purchase_qty) AS total_qty " +
                           "FROM Order_Item " +
                           "GROUP BY variation_id " +
                           ") OI ON V.id = OI.variation_id " +
                           "ORDER BY OI.total_qty DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Open the database connection
                    connection.Open();

                    // Create a data adapter to fill the dataset
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    // Fill the dataset with the query results
                    adapter.Fill(dataSet);

                    // Set the dataset as the data source for the repeater control
                    Repeater1.DataSource = dataSet;
                    Repeater1.DataBind();

                    // Close the database connection
                    connection.Close();
                }
            }
        }
        private void BindRepeaterData2()
        {
            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL query
            string query = "SELECT p.id as product_id, p.product_name, " +
                "p.product_image_1, v.id as variation_id, v.variation_name, v.stock_quantity " +
                "FROM Product p INNER JOIN Product_Variation v ON v.product_id = p.id " +
                "WHERE p.status = 1 ORDER BY v.stock_quantity ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();

                    // Create a data adapter to fill the dataset
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    // Fill the dataset with the query results
                    adapter.Fill(dataSet);

                    // Set the dataset as the data source for the repeater control
                    Repeater2.DataSource = dataSet;
                    Repeater2.DataBind();

                    // Close the database connection
                    connection.Close();
                }
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int stockQuantity = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "stock_quantity"));
                Label lblStockQuantity = (Label)e.Item.FindControl("lblStockQuantity");
                if (stockQuantity <= 10)
                {
                    lblStockQuantity.ForeColor = System.Drawing.Color.Red;
                }
                else if (stockQuantity < 30 && stockQuantity > 10)
                {

                    lblStockQuantity.ForeColor = System.Drawing.Color.Orange;

                }
                else
                {
                    lblStockQuantity.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                div2.Visible = false;
                div1.Visible = true;

            }
            else
            {
                div1.Visible = false;
                div2.Visible = true;
            }
        }
    }
}