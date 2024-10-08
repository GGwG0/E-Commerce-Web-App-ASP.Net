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
using System.Web.Security;

namespace DemoAssignment
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // loaded for the first time
            {
              
                if (Request.QueryString["command"] != null && Request.QueryString["command"] == "logout")
                {
                    Session.Clear();
                    FormsAuthentication.SignOut();
                }
               
                RetrieveTopNewProduct();
                RetrieveTopTrendingProduct();
                RetrieveTopRatedProduct();
                RetrieveProductDetail();
           
            }
        }
      
        protected void RetrieveTopNewProduct()
        {
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT TOP 3 p.id AS product_id, p.product_name, p.product_category, p.product_type, p.product_image_1, COALESCE(MIN(v.price), 0) AS min_price FROM Product p JOIN product_variation v ON p.id = v.product_id WHERE p.status = 1 GROUP BY p.id,p.product_name, p.product_category, p.product_type,product_image_1 ORDER BY p.id DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();

        }
        protected void RetrieveTopTrendingProduct()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT TOP 3 p.id AS product_id, p.product_name, p.product_category, p.product_type, p.product_image_1, COALESCE(SUM(O.purchase_qty), 0) AS total_purchased_qty , COALESCE(MIN(v.price), 0) AS min_price FROM Product p RIGHT JOIN product_variation v ON p.id = v.product_id RIGHT JOIN Order_Item O ON O.variation_id = V.id WHERE p.status = 1 GROUP BY p.id, p.product_name, p.product_category, p.product_type, p.product_image_1 ORDER BY total_purchased_qty DESC\r\n", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Repeater3.DataSource = dt;
            Repeater3.DataBind();

        }
        protected void RetrieveTopRatedProduct()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT TOP 3 p.id AS product_id, p.product_name, p.product_category, p.product_type, p.product_image_1, COALESCE(AVG(r.rating_num), 0) AS avg_rating , COALESCE(MIN(v.price), 0) AS min_price FROM Product p RIGHT JOIN Product_Variation v ON p.id = v.product_id RIGHT JOIN Product_Rating r ON R.variation_id = V.id WHERE p.status = 1 GROUP BY p.id, p.product_name, p.product_category, p.product_type, p.product_image_1 ORDER BY avg_rating DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Repeater4.DataSource = dt;
            Repeater4.DataBind();

        }
        protected void RetrieveProductDetail()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT p.id AS product_id, p.product_name,p.product_category,COALESCE(SUM(v.stock_quantity), 0) AS total_stock_quantity,p.product_image_1, p.product_image_2, COALESCE(AVG(r.rating_num), 0) AS avg_rating,COALESCE(MIN(v.price), 0) AS min_price, COALESCE(MAX(v.price), 0) AS max_price FROM Product p JOIN product_variation v ON p.id = v.product_id LEFT JOIN product_rating r ON v.id = r.variation_id WHERE p.status = 1 GROUP BY p.id,p.product_name, p.product_category, p.product_image_1, p.product_image_2;\r\n", con);
            //command.Parameters.AddWithValue("@emailEntered", txtEmail.Text);       
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();


        }

        protected (int, int) SelectProductDetail(String cat, String type)
        {
            int product_cat = 0, product_type = 0;
            switch (cat)
            {
                case "Data Storage":
                    {
                        product_cat = 1;
                        switch (type)
                        {
                            case "External Hard Drives": product_type = 1; break;
                            case "External SSD": product_type = 2; break;
                            case "Flash Drives": product_type = 3; break;
                            case "Internal Hard Drives": product_type = 4; break;
                            default: break;
                        }
                        //RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                case "Monitors":
                    {
                        product_cat = 2;
                        switch (type)
                        {
                            case "Curved Monitors": product_type = 1; break;
                            case "Flat Monitors": product_type = 2; break;
                            case "Gaming Monitors": product_type = 3; break;
                            default: break;
                        }
                        //RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                case "Computer Accessories":
                    {
                        product_cat = 3;
                        switch (type)
                        {
                            case "Cooling Pads & Stands": product_type = 1; break;
                            case "Keyboard & Mouse": product_type = 2; break;
                            case "Projector": product_type = 3; break;
                            default: break;
                        }
                        //RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                case "Network Components & IP Cameras":
                    {
                        product_cat = 4;
                        switch (type)
                        {
                            case "Network Adapters": product_type = 1; break;
                            case "Range Extender / Powerline": product_type = 2; break;
                            case "Smart Home & IP Cameras": product_type = 3; break;
                            default: break;
                        }
                        //RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                case "Printer & Ink":
                    {
                        product_cat = 5;
                        switch (type)
                        {
                            case "Ink Cartridges & Toners": product_type = 1; break;
                            case "Printer & Scanner": product_type = 2; break;
                            default: break;
                        }
                        //RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                case "Mobile & Tablets":
                    {
                        product_cat = 6;
                        switch (type)
                        {
                            case "Mobile Accessories": product_type = 1; break;
                            case "Mobile Phones": product_type = 2; break;
                            case "Tablets": product_type = 3; break;
                            default: break;
                        }
                        //RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                default:
                    product_cat = 0;
                    product_type = 0;
                    //RetrieveProductDetail();
                    break;
            }

            return (product_cat,product_type);
        }
       

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litStars = (Literal)e.Item.FindControl("litStars");

                decimal avgRating = 0;
                if (DataBinder.Eval(e.Item.DataItem, "avg_rating").ToString() == "")
                {
                    avgRating = 0;

                }
                else
                {
                    avgRating = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "avg_rating"));

                }



                //generate the HTML for the star icons
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

                    litStars.Text = sb.ToString();
                }
                else
                {
                    litStars.Text = "<ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon><ion-icon name=\"star-outline\"></ion-icon>";
                }
            }
        }
    }
}