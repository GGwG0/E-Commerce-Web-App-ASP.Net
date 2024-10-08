using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser
{
    public partial class Cart : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            BindCartData(Convert.ToInt32(Session["user_id"]));


        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void BindCartData(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT p.id AS ProductID,  c.id AS CartItemId, p.product_image_1 AS ImageUrl, p.product_name AS ProductName,pv.variation_name , pv.price AS UnitPrice, c.purchase_qty AS Quantity, pv.price * c.purchase_qty AS Price \r\nFROM Cart c \r\nInner JOIN Product_Variation pv ON c.variation_id = pv.id \r\nINNER JOIN Product p ON pv.product_id = p.id \r\nWHERE c.user_id = @UserId\r\n", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    CartGridView.DataSource = table;
                    CartGridView.DataBind();

                    // Loop through each row in the CartGridView and get the ProductID
                    foreach (GridViewRow row in CartGridView.Rows)
                    {
                        String image = table.Rows[row.RowIndex]["ImageUrl"].ToString();
                        int productID = Convert.ToInt32(table.Rows[row.RowIndex]["ProductID"]);

                        // Get the variations for the current product using the GetProductVariations() method
                        List<string> variations = GetVariationsForProduct(productID);

                        // Find the DropDownList control in the current row of the GridView
                        DropDownList ddlVariation = (DropDownList)row.FindControl("VariationDropDown");

                        // Bind the DropDownList to the variations list
                        ddlVariation.DataSource = variations;
                        ddlVariation.DataBind();

                        // Set the selected value to the current variation name from the table
                        string selectedVariation = table.Rows[row.RowIndex]["variation_name"].ToString();
                        ddlVariation.Items.FindByValue(selectedVariation).Selected = true;
                    }
                }
                else
                {
                    // Display an alert box and redirect the user back to cart.aspx
                    string message = "Your cart is empty,go add to cart now";
                    string script = "<script>alert('" + message + "'); window.location.href='../Index.aspx';</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }


            }
        }

        protected void CartGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int cartItemId = 0;
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                cartItemId = Convert.ToInt32(CartGridView.DataKeys[rowIndex].Value);

                // rest of the code for deleting the cart item using the cartItemId
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Cart WHERE id = @CartItemId", connection);
                command.Parameters.AddWithValue("@CartItemId", cartItemId);
                connection.Open();
                command.ExecuteNonQuery();
            }

            BindCartData(Convert.ToInt32(Session["user_id"])); // rebind the data after deleting the record
        }

        protected void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            double totalPrice = 0;
            CheckBox headerCheckBox = (CheckBox)CartGridView.HeaderRow.FindControl("HeaderCheckBox");
            bool finalcheck = true;


            foreach (GridViewRow gridViewRow in CartGridView.Rows)
            {
                CheckBox cb = (CheckBox)gridViewRow.FindControl("CheckBox");
                if (cb.Checked)
                {
                    Label lblPrice = (Label)gridViewRow.FindControl("PriceLabel");
                    double rowPrice = double.Parse(lblPrice.Text);
                    totalPrice += rowPrice;
                }
                else
                {
                    headerCheckBox.Checked = false;
                    finalcheck = false;
                }
            }

            if (finalcheck == true)
            {
                headerCheckBox.Checked = true;
            }

            results.Text = string.Format("{0:N2}", totalPrice);
        }



        protected void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (CheckBox)CartGridView.HeaderRow.FindControl("HeaderCheckBox");
            foreach (GridViewRow row in CartGridView.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("CheckBox");
                checkBox.Checked = headerCheckBox.Checked;
            }

            double totalPrice = 0;




            foreach (GridViewRow gridViewRow in CartGridView.Rows)
            {
                CheckBox cb = (CheckBox)gridViewRow.FindControl("CheckBox");
                if (cb.Checked)
                {
                    Label lblPrice = (Label)gridViewRow.FindControl("PriceLabel");
                    double rowPrice = double.Parse(lblPrice.Text);
                    totalPrice += rowPrice;
                }
            }
            results.Text = string.Format("{0:N2}", totalPrice);
        }



        private void UpdateCartQuantity(int cartItemId, int quantity)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Cart SET purchase_qty = @Quantity WHERE id = @CartItemId", connection);
                command.Parameters.AddWithValue("@CartItemId", cartItemId);
                command.Parameters.AddWithValue("@Quantity", quantity);
                connection.Open();
                command.ExecuteNonQuery();
            }
            results.Text = "0.00";
            BindCartData(Convert.ToInt32(Session["user_id"]));
        }

        protected void Quantity_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            string commandName = button.CommandName;
            int cartItemId = Convert.ToInt32(button.CommandArgument);
            int Stockquantity = 0;
            // Find the QuantityLabel control for the row corresponding to the button clicked
            Label quantityLabel = (Label)button.NamingContainer.FindControl("QuantityLabel");
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            int currentQuantity = Convert.ToInt32(quantityLabel.Text);
            int newQuantity = currentQuantity;

            if (commandName == "Increase")
            {
                newQuantity = currentQuantity + 1;
            }
            else if (commandName == "Decrease")
            {
                newQuantity = currentQuantity - 1;
            }

            if (newQuantity < 1)
            {
                string message = "The selected quantity is not available. Please choose a higher quantity.";
                string script = "<script>alert('" + message + "');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

            }
            else
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand("SELECT pv.stock_quantity\r\nFROM Cart c\r\nINNER JOIN Product_Variation pv ON c.variation_id = pv.id\r\nWHERE c.id = @cartID;\r\n", connection);
                    command.Parameters.AddWithValue("@cartID", cartItemId);
                    // Open the connection and execute the command to retrieve the quantity
                    connection.Open();
                    Stockquantity = (int)command.ExecuteScalar();

                }

                if (Stockquantity >= newQuantity && newQuantity > 0 && Stockquantity != 0)
                {
                    // Update cart quantity by calling UpdateCartQuantity() method
                    UpdateCartQuantity(cartItemId, newQuantity);



                }
                else if (Stockquantity == 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string deleteCartSql = "DELETE FROM [dbo].[Cart] WHERE id = @cartId";
                        SqlCommand deleteCartCommand = new SqlCommand(deleteCartSql, connection);
                        deleteCartCommand.Parameters.AddWithValue("@cartId", cartItemId);
                        deleteCartCommand.ExecuteNonQuery();
                        connection.Close();
                        // Display an alert box and redirect the user back to cart.aspx
                        string message = "Sorry The Product You Choose Is Out Of Stock";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                }
                else
                {
                    // Display an alert box and redirect the user back to cart.aspx
                    string message = "The selected quantity is not available. Please choose a lower quantity.";
                    string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
            }





        }

       protected List<string> GetVariationsForProduct(int productId)
{
    List<string> variations = new List<string>();
    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT variation_name FROM Product_Variation WHERE product_id = @ProductId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string variationName = reader.GetString(reader.GetOrdinal("variation_name"));
                        variations.Add(variationName);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle the exception here
        // You can log the error, display a user-friendly message, or perform any other necessary actions
        // For example:
        Console.WriteLine("An error occurred while getting the variations for the product: " + ex.Message);
    }
    return variations;
}

        protected void CartGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the dropdown list control in the row
                DropDownList VariationDropDown = (DropDownList)e.Row.FindControl("VariationDropDown");

                if (VariationDropDown != null)
                {
                    // Get the product ID from the data row
                    int productId = (int)CartGridView.DataKeys[e.Row.RowIndex].Value;

                    // Bind the dropdown list to product variations list for the product ID
                    VariationDropDown.DataSource = GetVariationsForProduct(productId);
                    VariationDropDown.DataBind();

                    // Set the selected value of the dropdown list
                    string variationName = DataBinder.Eval(e.Row.DataItem, "variation_name") as string;
                    VariationDropDown.SelectedValue = variationName;
                }
            }
        }


        protected void VariationDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlVariation = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlVariation.NamingContainer;
            int cartItemId = Convert.ToInt32(CartGridView.DataKeys[row.RowIndex].Value);
            int productId = GetProductIdFromCart(cartItemId);
            string variationName = ddlVariation.SelectedValue;
            int variationId = GetVariationId(productId, variationName);
            int currentPurchaseQty = 0;

            int stockQty = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT id, purchase_qty  FROM Cart WHERE variation_id = @variationId AND user_id = @userId", connection);
                command.Parameters.AddWithValue("@variationId", variationId);
                command.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["user_id"]));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    reader.Read();
                    int cartId = reader.GetInt32(0);
                    int purchaseQty = reader.GetInt32(1);
                    reader.Close();

                    SqlCommand command2 = new SqlCommand("SELECT purchase_qty FROM Cart WHERE id = @cartId", connection);
                    command2.Parameters.AddWithValue("@cartId", cartItemId);
                    SqlDataReader reader2 = command2.ExecuteReader();
                    // do something with cartId           

                    if (reader2.HasRows)
                    {
                        reader2.Read();
                        currentPurchaseQty = reader2.GetInt32(0);
                        // do something with currentPurchaseQty
                        reader2.Close();
                    }
                    int total = purchaseQty + currentPurchaseQty;

                    SqlCommand command5 = new SqlCommand("SELECT stock_quantity FROM Product_Variation WHERE id = @variationid", connection);
                    command5.Parameters.AddWithValue("@variationid", variationId);
                    SqlDataReader reader5 = command5.ExecuteReader();
                    reader5.Read();
                    stockQty = reader5.GetInt32(0);
                    reader5.Close();
                    if (stockQty < total && stockQty != 0)
                    {
                        total = stockQty;

                        SqlCommand command3 = new SqlCommand("UPDATE Cart SET purchase_qty = @total WHERE id = @cartId", connection);
                        command3.Parameters.AddWithValue("@total", total);
                        command3.Parameters.AddWithValue("@cartId", cartId);
                        command3.ExecuteNonQuery();

                        SqlCommand command4 = new SqlCommand("DELETE FROM Cart WHERE id = @cartId", connection);
                        command4.Parameters.AddWithValue("@cartId", cartItemId);
                        command4.ExecuteNonQuery();


                        string message = "Product Variation You Choosen have been lowered the quantity since having limited size";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                    else if (stockQty == 0)
                    {

                        SqlCommand command3 = new SqlCommand("DELETE FROM Cart WHERE id = @cartId", connection);
                        command3.Parameters.AddWithValue("@cartId", cartId);
                        command3.ExecuteNonQuery();


                        SqlCommand command4 = new SqlCommand("DELETE FROM Cart WHERE id = @cartId", connection);
                        command4.Parameters.AddWithValue("@cartId", cartItemId);
                        command4.ExecuteNonQuery();

                        string message = "Product Variation You Choosen have been out of stock,sorry for any inconvieniecce";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                    }
                    else
                    {
                        SqlCommand command3 = new SqlCommand("UPDATE Cart SET purchase_qty = @total WHERE id = @cartId", connection);
                        command3.Parameters.AddWithValue("@total", total);
                        command3.Parameters.AddWithValue("@cartId", cartId);
                        command3.ExecuteNonQuery();

                        SqlCommand command4 = new SqlCommand("DELETE FROM Cart WHERE id = @cartId", connection);
                        command4.Parameters.AddWithValue("@cartId", cartItemId);
                        command4.ExecuteNonQuery();

                        string message = "Product Variation Update Sucessfully";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }




                }
                else
                {
                    reader.Close();
                    SqlCommand command6 = new SqlCommand("SELECT stock_quantity FROM Product_Variation WHERE id = @variationid", connection);
                    command6.Parameters.AddWithValue("@variationid", variationId);
                    SqlDataReader reader6 = command6.ExecuteReader();
                    reader6.Read();
                    stockQty = reader6.GetInt32(0);
                    reader6.Close();

                    SqlCommand command7 = new SqlCommand("SELECT purchase_qty FROM Cart WHERE id = @cartId", connection);
                    command7.Parameters.AddWithValue("@cartId", cartItemId);
                    SqlDataReader reader7 = command7.ExecuteReader();
                    // do something with cartId           

                    if (reader7.HasRows)
                    {
                        reader7.Read();
                        currentPurchaseQty = reader7.GetInt32(0);
                        // do something with currentPurchaseQty
                        reader7.Close();
                    }

                    if (currentPurchaseQty > stockQty && stockQty != 0)
                    {

                        SqlCommand command3 = new SqlCommand("UPDATE Cart SET purchase_qty = @total WHERE id = @cartId", connection);
                        command3.Parameters.AddWithValue("@total", stockQty);
                        command3.Parameters.AddWithValue("@cartId", cartItemId);
                        command3.ExecuteNonQuery();
                        UpdateCartVariationId(cartItemId, variationId);
                        results.Text = "0.00";
                        BindCartData(Convert.ToInt32(Session["user_id"])); // Refresh the GridView with updated data
                                         // Display an alert box and redirect the user back to cart.aspx
                        string message = "New Variation You Selected Have Not Much the quantity you selected previously,therefore,we assign the value of max current variation.";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                    else if (stockQty == 0)
                    {


                        SqlCommand command4 = new SqlCommand("DELETE FROM Cart WHERE id = @cartId", connection);
                        command4.Parameters.AddWithValue("@cartId", cartItemId);
                        command4.ExecuteNonQuery();
                        results.Text = "0.00";

                        string message = "Product Variation You Choosen have been out of stock,sorry for any inconvieniecce";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                    }
                    else
                    {
                        UpdateCartVariationId(cartItemId, variationId);
                        results.Text = "0.00";

                        string message = "Product Variation Sucessfully Changed";
                        string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                    }



                }
            }


        }

protected int GetVariationId(int productId, string variationName)
{
    int variationId = 0;
    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id FROM Product_Variation WHERE product_id = @ProductId AND variation_name = @VariationName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@VariationName", variationName);
                variationId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }
    catch (Exception ex)
    {
        // Handle the exception here
        // You can log the error, display a user-friendly message, or perform any other necessary actions
        // For example:
        Console.WriteLine("An error occurred while getting the variation ID: " + ex.Message);
    }
    return variationId;
}


protected int GetProductIdFromCart(int cartItemId)
{
    int productId = 0;
    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT pv.product_id FROM Cart c JOIN Product_Variation pv ON c.variation_id = pv.id WHERE c.id = @CartItemId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CartItemId", cartItemId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productId = reader.GetInt32(reader.GetOrdinal("product_id"));
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle the exception here
        // You can log the error, display a user-friendly message, or perform any other necessary actions
        // For example:
        Console.WriteLine("An error occurred while getting the product ID from the cart: " + ex.Message);
    }
    return productId;
}
        protected void UpdateCartVariationId(int cartItemId, int variationId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Cart SET variation_id = @VariationId WHERE id = @CartItemId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VariationId", variationId);
                    command.Parameters.AddWithValue("@CartItemId", cartItemId);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void checkOutBtn_Click(object sender, EventArgs e)
        {
            int checkedamt = 0;
            List<int> cartIdsList = new List<int>();
            foreach (GridViewRow row in CartGridView.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("CheckBox");
                if (cb.Checked)
                {
                    int cartID = Convert.ToInt32(CartGridView.DataKeys[row.RowIndex].Value);
                    cartIdsList.Add(cartID);
                    checkedamt++;
                }
            }
            if (checkedamt != 0)
            {
                int[] cartIdsArray = cartIdsList.ToArray();
                Session["totalPrice"] = results.Text;
                Session["CartIds"] = cartIdsArray;
                Response.Redirect("Checkout.aspx");
            }
            else
            {
                // Display an alert box and redirect the user back to cart.aspx
                string message = "Please Check Out With at least 1 item";
                string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
        }
    }
}