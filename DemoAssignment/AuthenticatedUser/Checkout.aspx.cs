using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using DemoAssignment.AuthenticatedUser;

namespace DemoAssignment
{
    public partial class Checkout : System.Web.UI.Page
    {
        int[] cartIdsArray;
        protected void Page_Init(object sender, EventArgs e)
        {
            cartIdsArray = (int[])Session["CartIds"];
            BindCartData(cartIdsArray);
            Label totalLabel = (Label)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("totalLabel");
            totalLabel.Text = (String)Session["totalPrice"];

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindCartData(int[] cartID)
        {





            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cartIdList = string.Join(",", cartID);

                SqlCommand command = new SqlCommand($"SELECT p.id AS ProductID, c.id AS CartItemId, p.product_image_1 AS ImageUrl, p.product_name AS ProductName, pv.variation_name, pv.price AS UnitPrice, c.purchase_qty AS Quantity, pv.price * c.purchase_qty AS Price FROM Cart c INNER JOIN Product_Variation pv ON c.variation_id = pv.id INNER JOIN Product p ON pv.product_id = p.id WHERE c.user_id = @UserId AND c.id IN ({cartIdList})", connection);
                command.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["user_id"]));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Repeater1.DataSource = dataTable;
                Repeater1.DataBind();
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void CheckOut_Click(object sender, EventArgs e)
        {



            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            int payemntid = 0;
            int deliveryid = 0;
            int orderid = 0;
            bool problem = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (int cartId in cartIdsArray)
                {
                    SqlCommand command = new SqlCommand("SELECT variation_id, purchase_qty FROM Cart WHERE id = @cartId", connection);
                    command.Parameters.AddWithValue("@cartId", cartId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        int variationId = reader.GetInt32(0);
                        int purchaseQty = reader.GetInt32(1);

                        reader.Close();

                        SqlCommand command2 = new SqlCommand("SELECT stock_quantity FROM Product_Variation WHERE id = @variationId", connection);
                        command2.Parameters.AddWithValue("@variationId", variationId);

                        SqlDataReader reader2 = command2.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            reader2.Read();

                            int stockQuantity = reader2.GetInt32(0);

                            reader2.Close();

                            if (purchaseQty > stockQuantity && stockQuantity != 0)
                            {
                                SqlCommand command3 = new SqlCommand("UPDATE Cart SET purchase_qty = @total WHERE id = @cartId", connection);
                                command3.Parameters.AddWithValue("@total", stockQuantity);
                                command3.Parameters.AddWithValue("@cartId", cartId);
                                command3.ExecuteNonQuery();
                                problem = true;
                            }
                            else if (stockQuantity == 0)
                            {
                                SqlCommand command4 = new SqlCommand("DELETE FROM [dbo].[Cart] WHERE id = @cartId", connection);
                                command4.Parameters.AddWithValue("@cartId", cartId);
                                command4.ExecuteNonQuery();
                                problem = true;
                            }
                        }
                    }
                }
            }

            if (problem == true)
            {
                // Display an alert box and redirect the user back to cart.aspx
                string message = "Some Cart Checked Out is out of stock,sorry for any inconvenience";
                string script = "<script>alert('" + message + "'); window.location.href='Cart.aspx';</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (int cartId in cartIdsArray)
                    {
                        SqlCommand command5 = new SqlCommand("SELECT variation_id, purchase_qty FROM Cart WHERE id = @cartId", connection);
                        command5.Parameters.AddWithValue("@cartId", cartId);

                        SqlDataReader reader3 = command5.ExecuteReader();

                        if (reader3.HasRows)
                        {
                            reader3.Read();

                            int variationId = reader3.GetInt32(0);
                            int purchaseQty = reader3.GetInt32(1);

                            reader3.Close();

                            SqlCommand command2 = new SqlCommand("UPDATE Product_Variation SET stock_quantity = stock_quantity - @purchaseQty WHERE id = @variationId", connection);
                            command2.Parameters.AddWithValue("@purchaseQty", purchaseQty);
                            command2.Parameters.AddWithValue("@variationId", variationId);
                            command2.ExecuteNonQuery();
                        }

                    }
                }


                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sql = "INSERT INTO [dbo].[Payment] (payment_datetime, payment_method, payment_amount) OUTPUT INSERTED.id VALUES (@paymentDatetime, @paymentMethod, @paymentAmount)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@paymentDatetime", DateTime.Now);
                    command.Parameters.AddWithValue("@paymentMethod", "Credit Card");
                    decimal paymentAmount = Convert.ToDecimal(Session["totalPrice"]);
                    command.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                    connection.Open();
                    payemntid = (int)command.ExecuteScalar();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Delivery] (address, estimateDeliverDate, deliveredDatetime, contactPerson, phone_num) OUTPUT INSERTED.id VALUES (@address, @estimateDeliverDate, @deliveredDatetime, @contactPerson, @phone_num)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@address", address.Text);
                    command.Parameters.AddWithValue("@estimateDeliverDate", DateTime.Today.AddDays(8));
                    command.Parameters.AddWithValue("@deliveredDatetime", DBNull.Value);
                    command.Parameters.AddWithValue("@contactPerson", name.Text);
                    command.Parameters.AddWithValue("@phone_num", contact_no.Text);
                    connection.Open();
                    deliveryid = (int)command.ExecuteScalar();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO [dbo].[Order] (payment_id, delivery_id, user_id, status, orderDatetime) VALUES (@paymentId, @deliveryId, @userId, @status, @orderDatetime)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@paymentId", payemntid);
                    command.Parameters.AddWithValue("@deliveryId", deliveryid);
                    command.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["user_id"]));
                    command.Parameters.AddWithValue("@status", 1);
                    command.Parameters.AddWithValue("@orderDatetime", DateTime.Now);
                    connection.Open();
                    command.ExecuteNonQuery();

                    sql = "SELECT MAX(id) FROM [dbo].[Order]";
                    command.CommandText = sql;
                    orderid = (int)command.ExecuteScalar();


                }






                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    foreach (int cartId in cartIdsArray)
                    {
                        // Retrieve cart data to transfer
                        string selectCartSql = "SELECT user_id, variation_id, purchase_qty FROM [dbo].[Cart] WHERE id = @cartId";
                        SqlCommand selectCartCommand = new SqlCommand(selectCartSql, connection, transaction);
                        selectCartCommand.Parameters.AddWithValue("@cartId", cartId);
                        SqlDataReader cartReader = selectCartCommand.ExecuteReader();
                        if (!cartReader.Read())
                        {
                            throw new Exception($"Cart with id {cartId} not found.");
                        }

                        // Insert cart data into Order_Item table

                        int userId = (int)cartReader["user_id"];
                        int variationId = (int)cartReader["variation_id"];
                        int purchaseQty = (int)cartReader["purchase_qty"];

                        // Close the data reader before executing the insert command
                        cartReader.Close();

                        string insertOrderItemSql = "INSERT INTO [dbo].[Order_Item] (order_id, variation_id, purchase_qty) VALUES (@orderId, @variationId, @purchaseQty)";
                        SqlCommand insertOrderItemCommand = new SqlCommand(insertOrderItemSql, connection, transaction);
                        insertOrderItemCommand.Parameters.AddWithValue("@orderId", orderid);

                        insertOrderItemCommand.Parameters.AddWithValue("@variationId", variationId);
                        insertOrderItemCommand.Parameters.AddWithValue("@purchaseQty", purchaseQty);
                        insertOrderItemCommand.ExecuteNonQuery();

                        // Delete corresponding cart row
                        string deleteCartSql = "DELETE FROM [dbo].[Cart] WHERE id = @cartId";
                        SqlCommand deleteCartCommand = new SqlCommand(deleteCartSql, connection, transaction);
                        deleteCartCommand.Parameters.AddWithValue("@cartId", cartId);
                        deleteCartCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();


                }

                String email = "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectCartSql = "SELECT email FROM [User] WHERE id = @userid";
                    SqlCommand selectCartCommand = new SqlCommand(selectCartSql, connection);
                    selectCartCommand.Parameters.AddWithValue("@userid", Convert.ToInt32(Session["user_id"]));
                    SqlDataReader cartReader = selectCartCommand.ExecuteReader();
                   
                    if (!cartReader.Read())
                    {
                        throw new Exception($"User with id {Session["user_id"].ToString()} not found.");
                    }
                    email = cartReader["email"].ToString();

                    connection.Close();

                }

                
                MailMessage message = new MailMessage();
                String creditcardNumber = card_credit_number.Text;
              
                // Set the sender, recipient, subject, and body of the email
                message.From = new MailAddress("awieling0777@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Check Out SucessFull";
                message.Body = "You Sucessfully Purchase with WIll Nett with amount of : RM " + Session["totalPrice"].ToString() + " With Order ID : " + orderid +
                    "\n" + "With Payment Credit Card Methods Card Number " + creditcardNumber + 
                "\n" + "Please Contact Admin If Your are not making any payment";

                // Create a new SmtpClient object
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
              
                // Configure the SMTP client with your email server settings
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("awieling0777@gmail.com", "vpiaoqahiewobkxu");
                client.EnableSsl = true;

                // Send the email message
                client.Send(message);


                string redirectUrl = "PaymentSuccessful.aspx?orderId=" + orderid + "&Amount=" + Session["totalPrice"].ToString();
                Response.Redirect(redirectUrl);

            }
        }
    }
}