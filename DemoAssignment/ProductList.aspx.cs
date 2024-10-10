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
using DemoAssignment.AuthenticatedUser;

namespace DemoAssignment
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // loaded for the first time
            {

                if (Request.QueryString["search"] != null)
                {
                    RetrieveSearchProduct();
                }
                else if (Request.QueryString["cat"] != null && Request.QueryString["type"] != null)
                {
                    SelectProductDetail(Convert.ToInt32(Request.QueryString["cat"]), Convert.ToInt32(Request.QueryString["type"]));
                    Session["product_category"] = Request.QueryString["cat"];
                    Session["product_type"] = Request.QueryString["type"];
                }
                else
                {
                    RetrieveProductDetail();
                }

            }
        }

        protected void RetrieveSearchProduct()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.id AS product_id, p.product_name,p.product_category,COALESCE(SUM(v.stock_quantity), 0) AS total_stock_quantity,p.product_image_1, p.product_image_2, COALESCE(AVG(r.rating_num), 0) AS avg_rating,COALESCE(MIN(v.price), 0) AS min_price, COALESCE(MAX(v.price), 0) AS max_price FROM Product p JOIN product_variation v ON p.id = v.product_id LEFT JOIN product_rating r ON v.id = r.variation_id WHERE p.status = 1 and p.product_name LIKE '%' + @search_term + '%' GROUP BY p.id,p.product_name, p.product_category, p.product_image_1, p.product_image_2;", con);

                cmd.Parameters.AddWithValue("@search_term", Request.QueryString["search"].ToString());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

        }

        protected void SelectProductDetail(int cat, int type)
        {
            String product_cat = "", product_type = "";
            switch (cat)
            {
                case 1:
                    {
                        product_cat = "Data Storage";
                        switch (type)
                        {
                            case 1: product_type = "External Hard Drives"; break;
                            case 2: product_type = "External SSD"; break;
                            case 3: product_type = "Flash Drives"; break;
                            case 4: product_type = "Internal Hard Drives"; break;
                            default: break;
                        }
                        RetriveSpecificProduct(product_cat, product_type);

                    }
                    break;
                case 2:
                    {
                        product_cat = "Monitors";
                        switch (type)
                        {
                            case 1: product_type = "Curved Monitors"; break;
                            case 2: product_type = "Flat Monitors"; break;
                            case 3: product_type = "Gaming Monitors"; break;
                            default: break;
                        }
                        RetriveSpecificProduct(product_cat, product_type);

                    }
                    break;
                case 3:
                    {
                        product_cat = "Computer Accessories";
                        switch (type)
                        {
                            case 1: product_type = "Cooling Pads & Stands"; break;
                            case 2: product_type = "Keyboard & Mouse"; break;
                            case 3: product_type = "Projector"; break;
                            default: break;
                        }
                    }
                    RetriveSpecificProduct(product_cat, product_type);

                    break;
                case 4:
                    {
                        product_cat = "Network Components & IP Cameras";
                        switch (type)
                        {
                            case 1: product_type = "Network Adapters"; break;
                            case 2: product_type = "Range Extender / Powerline"; break;
                            case 3: product_type = "Smart Home & IP Cameras"; break;
                            default: break;
                        }
                    }
                    RetriveSpecificProduct(product_cat, product_type);

                    break;
                case 5:
                    {
                        product_cat = "Printer & Ink";
                        switch (type)
                        {
                            case 1: product_type = "Ink Cartridges & Toners"; break;
                            case 2: product_type = "Printer & Scanner"; break;
                            default: break;
                        }
                        RetriveSpecificProduct(product_cat, product_type);

                    }
                    break;
                case 6:
                    {
                        product_cat = "Mobile & Tablets";
                        switch (type)
                        {
                            case 1: product_type = "Mobile Accessories"; break;
                            case 2: product_type = "Mobile Phones"; break;
                            case 3: product_type = "Tablets"; break;
                            default: break;
                        }
                        RetriveSpecificProduct(product_cat, product_type);
                    }
                    break;
                default:
                    RetrieveProductDetail();
                    break;
            }
        }

        protected void RetriveSpecificProduct(String cat, String type)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT p.id AS product_id, p.product_name,p.product_category,COALESCE(SUM(v.stock_quantity), 0) AS total_stock_quantity,p.product_image_1, p.product_image_2, COALESCE(AVG(r.rating_num), 0) AS avg_rating,COALESCE(MIN(v.price), 0) AS min_price, COALESCE(MAX(v.price), 0) AS max_price FROM Product p JOIN product_variation v ON p.id = v.product_id LEFT JOIN product_rating r ON v.id = r.variation_id WHERE p.status = 1 and p.product_category=@category and p.product_type=@type GROUP BY p.id,p.product_name, p.product_category, p.product_image_1, p.product_image_2;", con);
            command.Parameters.AddWithValue("@category", cat);
            command.Parameters.AddWithValue("@type", type);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void RetrieveProductDetail()
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("SELECT p.id AS product_id, p.product_name,p.product_category,COALESCE(SUM(v.stock_quantity), 0) AS total_stock_quantity,p.product_image_1, p.product_image_2, COALESCE(AVG(r.rating_num), 0) AS avg_rating,COALESCE(MIN(v.price), 0) AS min_price, COALESCE(MAX(v.price), 0) AS max_price FROM Product p JOIN product_variation v ON p.id = v.product_id LEFT JOIN product_rating r ON v.id = r.variation_id WHERE p.status = 1 GROUP BY p.id,p.product_name, p.product_category, p.product_image_1, p.product_image_2;", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();



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