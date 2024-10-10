using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using WebApplication2;

namespace DemoAssignment
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["id"] != null)
                    {
                        string encryptedProdId = Request.QueryString["id"];
                        string decryptProdId = WebCustomControl1.DecryptQueryString(encryptedProdId);

                        int product_id = Convert.ToInt32(decryptProdId);
                        RetrieveProductDetail(product_id);


                        RetriveSpecificProductRating(product_id);
                        RetrieveAvgRate(product_id);
                        RetrieveRatingBar(product_id);

                        //SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }


        }

        protected void RetrieveProductDetail(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT p.id as product_id, p.product_name, p.product_category, p.product_type, p.description, p.stock_status, p.product_image_1, p.product_image_2, p.product_image_3, v.id as variation_id, v.variation_name, v.price, v.stock_quantity FROM  Product p INNER JOIN Product_Variation v ON p.id = v.product_id WHERE p.id =@product_id and v.status =1 GROUP BY  p.id, p.product_name,p.product_category, p.product_type, p.description, p.stock_status,p.product_image_1, p.product_image_2,p.product_image_3,v.id,v.variation_name,v.price, v.stock_quantity", con);
                command.Parameters.AddWithValue("@product_id", id);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                int totalStockQty = 0;

                if (reader.HasRows)
                {
                    //product_id = (int)reader["product_id"];

                    while (reader.Read())
                    {
                        lblProdName.Text = reader["product_name"].ToString();
                        Session["product_cat"] = reader["product_category"].ToString();
                        Session["product_type"] = reader["product_type"].ToString();
                        lblProdDescription.Text = reader["description"].ToString();
                        //stockStatusLabel.Text = reader["stock_status"].ToString();
                        mainImage.ImageUrl = "./assets/images/products/" + reader["product_image_1"].ToString();
                        optionImage1.ImageUrl = "./assets/images/products/" + reader["product_image_1"].ToString();
                        optionImage2.ImageUrl = "./assets/images/products/" + reader["product_image_2"].ToString();
                        optionImage3.ImageUrl = "./assets/images/products/" + reader["product_image_3"].ToString();



                        ListItem listItem = new ListItem();
                        if ((int)reader["stock_quantity"] == 0 || reader["stock_quantity"] == null)
                        {
                            listItem.Enabled = false;

                        }
                        totalStockQty += (int)reader["stock_quantity"];
                        listItem.Text = reader["variation_name"].ToString() + " :stock: " + reader["stock_quantity"].ToString();
                        listItem.Value = reader["variation_id"].ToString();


                        rblVariations.Items.Add(listItem);
                    }


                }
                //txtQuantity.MaxLength = totalStockQty;
                reader.Close();
                con.Close();

                if (totalStockQty == 0)
                {
                    rblVariations.Enabled = false;
                    txtQuantity.Enabled = false;
                    btnAddToBag.Enabled = false;
                    btnAddToBag.Text = "Out Of Stock";
                }

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }



            // SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

        }
        protected void RetrieveRatingBar(int product_id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT rating_num, COUNT() as total_count, ROUND(CAST(COUNT() AS float) / (SELECT COUNT(*) FROM Product_Rating WHERE rating_num IN (1, 2, 3, 4, 5) and variation_id IN (SELECT id FROM Product_Variation WHERE product_id = 9)) * 100, 2) as percentage FROM Product_Rating R LEFT JOIN Product_Variation V ON R.variation_id = V.id LEFT JOIN Product P ON V.product_id = P.id WHERE rating_num IN (1, 2, 3, 4, 5) and P.id = @product_id GROUP BY rating_num;", con);
                command.Parameters.AddWithValue("@product_id", product_id);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    decimal ratingNum = reader.GetDecimal(0);
                    if (Decimal.Compare(ratingNum, (decimal)1.00) == 0)
                    {
                        progress_line_1.Style["width"] = reader["percentage"].ToString() + "%";
                        lblPercent1.Text = reader["percentage"].ToString();



                    }
                    else if (Decimal.Compare(ratingNum, (decimal)2.00) == 0)
                    {
                        progress_line_2.Style["width"] = reader["percentage"].ToString() + "%";
                        lblPercent2.Text = reader["percentage"].ToString();


                    }
                    else if (Decimal.Compare(ratingNum, (decimal)3.00) == 0)
                    {
                        progress_line_3.Style["width"] = reader["percentage"].ToString() + "%";
                        lblPercent3.Text = reader["percentage"].ToString();



                    }
                    else if (Decimal.Compare(ratingNum, (decimal)4.00) == 0)
                    {
                        progress_line_4.Style["width"] = reader["percentage"].ToString() + "%";
                        lblPercent4.Text = reader["percentage"].ToString();


                    }
                    else if (Decimal.Compare(ratingNum, (decimal)5.00) == 0)
                    {
                        progress_line_5.Style["width"] = reader["percentage"].ToString() + "%";
                        lblPercent5.Text = reader["percentage"].ToString();


                    }

                    int totalCount = reader.GetInt32(1);

                    // Do something with the ratingNum and totalCount values
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }


        }
        protected void RetriveSpecificProductRating(int product_id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("Select V.variation_name, R.rating_num as rating_num, R.comment, R.adminReply, CONVERT(VARCHAR(12), R.created_at, 107) AS created_at,U.name, R.variation_id from Product_Rating R Right Join Product_Variation V ON R.variation_id = V.id RIGHT JOIN Product P ON P.id = V.product_id RIGHT JOIN [User] U ON R.user_id = U.id where P.id = @product_id;", con);
                command.Parameters.AddWithValue("@product_id", product_id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                lblProductName.Text = lblProdName.Text;
                lblProdDes.Text = lblProdDescription.Text;
                lblProdCatRate.Text = Session["product_cat"].ToString();

                if (dt.Rows.Count > 0)
                {


                    lblRateCount.Text = dt.Rows.Count.ToString();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    Repeater1.Visible = true;
                    lblText.Text = "";
                }
                else
                {
                    Repeater1.Visible = false;
                    lblText.Text = "No ratings yet";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message);
            }



        }
        protected void RetrieveAvgRate(int product_id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("Select COALESCE(AVG(r.rating_num), 0) AS avg_rating From Product_Rating R LEFT JOIN Product_Variation V ON R.variation_id = V.id LEFT JOIN Product P ON V.product_id = P.id Where P.id = @product_id", con);
                command.Parameters.AddWithValue("@product_id", product_id);
                con.Open();
                decimal avgRating = Convert.ToDecimal(command.ExecuteScalar());
                lblAvgRating.Text = avgRating.ToString("0.0");


                if (avgRating > 0)
                {

                    int fullStars = (int)Math.Floor(avgRating);
                    int halfStars = (int)Math.Floor((avgRating - fullStars) * 2);
                    int emptyStars = 5 - fullStars - halfStars;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < fullStars; i++)
                    {
                        sb.Append("<ion-icon name=\"star\"></ion-icon>");
                    }
                    for (int i = 0; i < halfStars; i++)
                    {
                        sb.Append("<ion-icon name=\"star-half\"></ion-icon>");
                    }
                    for (int i = 0; i < emptyStars; i++)
                    {
                        sb.Append("<ion-icon name=\"star-outline\"></ion-icon>");
                    }

                    litStarsAvg.Text = sb.ToString();
                }
                else
                {
                    litStarsAvg.Text = "<ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon>";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message);
            }

        }
        protected void btnAddToBag_Click(object sender, EventArgs e)
        {
            try
            {
                String message = "";
                if (Session["user_id"] == null)
                {
                    message = "You are not log in! Log in and purchase our items!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                }
                else
                {
                    if (rblVariations.SelectedItem != null)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        // check if the product is in cart
                        SqlCommand command1 = new SqlCommand("Select C.purchase_qty, V.stock_quantity from Cart C LEFT JOIN Product_Variation V ON C.variation_id = V.id where variation_id = @variation_id and user_id = @user_id;", con);
                        command1.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user_id"]));
                        command1.Parameters.AddWithValue("@variation_id", Convert.ToInt32(rblVariations.SelectedValue));
                        con.Open();
                        SqlDataReader reader = command1.ExecuteReader();
                        SqlCommand command;
                        int insert = 0;
                        int purchaseQty = 0;

                        if (reader.HasRows)
                        {
                            message = "You have added this product before! Change it at Cart!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                        }
                        else
                        {
                            con.Close();
                            con.Open();
                            command = new SqlCommand("INSERT INTO [dbo].[Cart] VALUES (@user_id,@variation_id,@purchase_qty);", con);
                            command.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user_id"]));
                            command.Parameters.AddWithValue("@variation_id", Convert.ToInt32(rblVariations.SelectedValue));
                            command.Parameters.AddWithValue("@purchase_qty", Convert.ToInt32(txtQuantity.Text));
                            insert = command.ExecuteNonQuery();
                            con.Close();


                            if (insert == 1)
                            {
                                message = "You have added the product successfully!";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location='" + "AuthenticatedUser/Cart.aspx" + "';", true);


                            }
                            else
                            {
                                message = "Something went wrong! Failed to add to cart!";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                            }
                        }
                    }
                    else
                    {
                        message = "You must select variations  ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message);
            }






        }



        protected void rblVariations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["user_id"] != null)
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    // check if the product is in cart
                    SqlCommand command1 = new SqlCommand("Select stock_quantity, price from Product_Variation where id = @variation_id;", con);
                    command1.Parameters.AddWithValue("@variation_id", Convert.ToInt32(rblVariations.SelectedValue));
                    con.Open();
                    SqlDataReader reader = command1.ExecuteReader();
                    int stockQty = 0;
                    Decimal price = 0.00m;
                    if (reader.Read())
                    {
                        stockQty = (int)reader["stock_quantity"];
                        price = (decimal)reader["price"];
                    }

                    con.Close();



                    Decimal totalPrice = 0.00m;

                    if (txtQuantity.Text.CompareTo("1") < 0) // if quantity less than 1
                    {
                        txtQuantity.Text = "1";
                        String message = "The selected variation quantity less than 1";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                    }
                    else if (int.Parse(txtQuantity.Text) > stockQty)
                    {
                        txtQuantity.Text = "1";
                        String message = "The selected variation quantity exceeds the stock";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                    }

                    totalPrice = Decimal.Multiply(Convert.ToDecimal(txtQuantity.Text), price);

                    txtTotalPrice.Text = totalPrice.ToString();
                    //lblPrice.Text = 
                }
                else
                {
                    String message = "You are not log in! Log in and purchase our items!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message);
            }


        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["user_id"] != null)
                {
                    Decimal totalPrice = 0.00m;

                    if (rblVariations.SelectedItem == null)
                    {

                        String message1 = "Please select a variation!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message1 + "');", true);

                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        // check if the product is in cart
                        SqlCommand command1 = new SqlCommand("Select stock_quantity, price from Product_Variation where id = @variation_id;", con);
                        command1.Parameters.AddWithValue("@variation_id", Convert.ToInt32(rblVariations.SelectedValue));
                        con.Open();
                        SqlDataReader reader = command1.ExecuteReader();
                        int stockQty = 0;
                        Decimal price = 0.00m;
                        if (reader.Read())
                        {
                            stockQty = (int)reader["stock_quantity"];
                            price = (decimal)reader["price"];
                        }
                        con.Close();

                        if (txtQuantity.Text.CompareTo("1") < 0) // if quantity less than 1
                        {
                            txtQuantity.Text = "1";
                            String message = "The selected variation quantity less than 1";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                        }
                        else if (int.Parse(txtQuantity.Text) > stockQty)
                        {
                            txtQuantity.Text = "1";
                            String message = "The selected variation quantity exceeds the stock";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                        }

                        totalPrice = Decimal.Multiply(Convert.ToDecimal(txtQuantity.Text), price);

                        txtTotalPrice.Text = totalPrice.ToString();
                    }
                }
                else
                {
                    String message = "You are not log in! Log in and purchase our items!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error" + ex.Message);
            }


        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Footer && Repeater1.Items.Count == 0)
                {
                    // Find the review_per_person div in the footer
                    HtmlGenericControl reviewPerPersonDiv = e.Item.FindControl("user_review") as HtmlGenericControl;
                    HtmlGenericControl divAdminReview = e.Item.FindControl("divAdminReview") as HtmlGenericControl;
                    HtmlGenericControl lblTxtRating = e.Item.FindControl("lblText") as HtmlGenericControl;



                    // Hide the review_per_person div
                    lblTxtRating.InnerText = "No ratings yet";
                    reviewPerPersonDiv.Visible = false;
                    divAdminReview.Visible = false;
                }
                else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataRowView rowView = e.Item.DataItem as DataRowView;
                    string adminReply = rowView["adminReply"] as string;




                    if (string.IsNullOrEmpty(adminReply))
                    {
                        //HtmlControl adminReviewDiv = e.Item.FindControl("admin-review") as HtmlControl;
                        //adminReviewDiv.Style[div"display"] = "none";


                        HtmlControl divAdminReview = (HtmlControl)e.Item.FindControl("divAdminReview");
                        divAdminReview.Attributes["class"] = "admin-review hidden";

                    }



                    Literal litStars = (Literal)e.Item.FindControl("litStars");
                    decimal rating = 0;

                    if (DataBinder.Eval(e.Item.DataItem, "rating_num").ToString() == "")
                    {
                        rating = 0;

                    }
                    else
                    {
                        rating = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "rating_num"));

                    }


                    //generate the HTML for the star icons
                    if (rating > 0)
                    {

                        int fullStars = (int)Math.Floor(rating);
                        int halfStars = (int)Math.Floor((rating - fullStars) * 2);
                        int emptyStars = 5 - fullStars - halfStars;

                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < fullStars; i++)
                        {
                            sb.Append("<ion-icon name=\"star\"></ion-icon>");
                        }
                        for (int i = 0; i < halfStars; i++)
                        {
                            sb.Append("<ion-icon name=\"star-half\"></ion-icon>");
                        }
                        for (int i = 0; i < emptyStars; i++)
                        {
                            sb.Append("<ion-icon name=\"star-outline\"></ion-icon>");
                        }

                        litStars.Text = sb.ToString();
                    }
                    else
                    {
                        litStars.Text = "<ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon>";
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("Error" + ex.Message);
            }

        }
    }


}