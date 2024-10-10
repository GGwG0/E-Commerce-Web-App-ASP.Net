using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser
{
    public partial class Order_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int ratingID = 0;
            string cartID = Request.QueryString["orderItemID"];
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create a command to execute the query
                SqlCommand command = new SqlCommand("SELECT d.estimateDeliverDate AS estimate, d.deliveredDateTime AS actual , oi.rating_id AS [ratingID] ,o.payment_id AS [paymentID],\r\n       oi.id AS [Order Item Id],\r\n       oi.purchase_qty AS [quantity],\r\n       o.id AS [Order Id],\r\n       o.status AS [Order Status],\r\n       d.contactPerson AS [Contact Person],\r\n       d.phone_num AS [Contact Number],\r\n       d.address AS [Delivery Address],\r\n       o.orderDatetime AS [Order Date],\r\n       p.product_image_1 AS [Product Image URL],\r\n       p.product_name AS [Product Name],\r\n       pv.variation_name AS [Variation Name],\r\n       pv.price AS [Unit Price]\r\nFROM Order_Item oi\r\nINNER JOIN [Order] o ON oi.order_id = o.id\r\nINNER JOIN Delivery d ON o.delivery_id = d.id\r\nLEFT JOIN Product_Variation pv ON oi.variation_id = pv.id\r\nLEFT JOIN Product p ON pv.product_id = p.id\r\nWHERE oi.id = @orderItemId\r\n", connection);

                // add the order item ID parameter to the command
                command.Parameters.AddWithValue("@orderItemId", Convert.ToInt32(cartID));

                // open the connection and execute the query
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // if the query returned a row, display the data on the label
                if (reader.Read())
                {
                    OrderID.InnerHtml = reader["Order Id"].ToString();
                    String statusName = "";
                    int orderStatus = Convert.ToInt32(reader["Order Status"].ToString());

                    switch (orderStatus)
                    {
                        case 1:
                            statusName = "PACKAGING";
                            break;
                        case 2:
                            statusName = "DELIVERED";
                            pipeDelivery.Attributes["style"] = "color: #2dc258;";
                            DeliveryIcon.Attributes["style"] = "color: #2dc258;";
                            break;
                        case 3:
                            statusName = "COMPLETED";
                            pipeDelivery.Attributes["style"] = "color: #2dc258;";
                            DeliveryIcon.Attributes["style"] = "color: #2dc258;";
                            pipeCompleted.Attributes["style"] = "color: #2dc258;";
                            CompletedIcon.Attributes["style"] = "color: #2dc258;";
                            break;
                    }

                    switch (statusName)
                    {
                        case "PACKAGING":
                            break;
                        case "DELIVERED":
                            break;
                        case "COMPLETED":
                            break;
                    }


                    Status.InnerText = statusName;
                    contactName.InnerText = reader["Contact Person"].ToString();
                    contactNo.InnerText = reader["Contact Number"].ToString();
                    addresscontent.InnerHtml = reader["Delivery Address"].ToString();
                    paymentIDs.InnerHtml = reader["paymentID"].ToString();

                    DateTime orderDate = DateTime.Parse(reader["Order Date"].ToString());
                    string formattedOrderDate = orderDate.ToString("dd/MM/yyyy");

                    DateTime orderDate2 = DateTime.Parse(reader["estimate"].ToString());
                    string estimateDate = orderDate2.ToString("dd/MM/yyyy");




                    estimatedLabel.Text = estimateDate;

                    if (!DBNull.Value.Equals(reader["actual"]))
                    {
                        DateTime orderDate3 = DateTime.Parse(reader["actual"].ToString());
                        string actualDate = orderDate3.ToString("dd/MM/yyyy");
                        actualLabel.Text = actualDate;
                    }
                    else
                    {
                        actualLabel.Text = "Haven't Arrive Yet";
                    }


                    orderdate.InnerText = formattedOrderDate;

                    ProductImage.ImageUrl = "~/assets/images/products/" + reader["Product Image URL"].ToString();
                    productname.InnerText = reader["Product Name"].ToString();
                    variationName.InnerText = "variation: " + reader["Variation Name"].ToString();
                    quantity.InnerText = reader["quantity"].ToString();
                    unitprice.InnerText = "RM " + ((decimal)reader["Unit Price"]).ToString("0.00");

                    if (!DBNull.Value.Equals(reader["ratingID"]))
                    {
                        ratingID = Convert.ToInt32(reader["ratingID"]);
                    }


                    double quantitySum = double.Parse(reader["quantity"].ToString());
                    double unitPriceSum = double.Parse(reader["Unit Price"].ToString());
                    double totalSum = quantitySum * unitPriceSum;
                    totalPrice.InnerHtml = "Total Price: RM" + totalSum.ToString("0.00");

                }


                // close the reader and the connection
                reader.Close();
                connection.Close();
            }

            if (ratingID == 0)
            {
                ratingContainer.Visible = false;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("select * from Product_Rating WHERE id = @ratingid", connection);
                    command.Parameters.AddWithValue("@ratingid", ratingID);

                    // open the connection and execute the query
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    ratingIDs.Text = reader["id"].ToString();

                    switch (Convert.ToDouble(reader["rating_num"]))
                    {
                        case 1.00:
                            star_1.Attributes["style"] = "color: gold;";

                            break;
                        case 2.00:
                            star_1.Attributes["style"] = "color: gold;";
                            star_2.Attributes["style"] = "color: gold;";


                            break;
                        case 3.00:
                            star_1.Attributes["style"] = "color: gold;";
                            star_2.Attributes["style"] = "color: gold;";
                            star_3.Attributes["style"] = "color: gold;";

                            break;
                        case 4.00:
                            star_1.Attributes["style"] = "color: gold;";
                            star_2.Attributes["style"] = "color: gold;";
                            star_3.Attributes["style"] = "color: gold;";
                            star_4.Attributes["style"] = "color: gold;";

                            break;
                        case 5.00:
                            star_1.Attributes["style"] = "color: gold;";
                            star_2.Attributes["style"] = "color: gold;";
                            star_3.Attributes["style"] = "color: gold;";
                            star_4.Attributes["style"] = "color: gold;";
                            star_5.Attributes["style"] = "color: gold;";

                            break;
                    }

                    if (DBNull.Value.Equals(reader["comment"]) || reader["comment"].Equals(""))
                    {
                        CustomerComment.Text = "[ Your didn't leave any comment ]";
                    }
                    else
                    {
                        CustomerComment.Text = reader["comment"].ToString();
                    }



                    if (DBNull.Value.Equals(reader["adminReply"]))
                    {
                        AdminComment.Text = "[ Admin Haven't Reply Yet ]";
                    }
                    else
                    {
                        AdminComment.Text = reader["adminReply"].ToString();
                    }


                    reader.Close();
                    connection.Close();
                }
            }

        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseHistory.aspx");
        }
    }
}