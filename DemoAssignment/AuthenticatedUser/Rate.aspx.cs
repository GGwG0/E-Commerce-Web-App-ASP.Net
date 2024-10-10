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
    public partial class Rate : System.Web.UI.Page
    {

        int cardids;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cartID = Request.QueryString["cartItemId"];
            string productName = Request.QueryString["productName"];
            string variationName = Request.QueryString["variationName"];
            string imageUrl = Request.QueryString["imageUrl"];
            string variationid = Request.QueryString["variationID"];
            // Add click event handlers to the close button and the background overlay of the modal
            cardids = Convert.ToInt32(cartID);
            header.InnerHtml = "<b>" + productName + "</b>";
            variation.InnerText = "variation: " + variationName;
            ProductImage.ImageUrl = "~/assets/images/products/" + imageUrl;
            modalOverlay.Attributes.Add("onclick", "document.getElementById('myModal').style.display='none';return false;");


        }

        protected void ChangeStar(object sender, EventArgs e)
        {
            string buttonId = ((LinkButton)sender).ID;

            switch (buttonId)
            {
                case "stars1":
                    // Code to execute when stars1 is clicked
                    SetStarRating(1);
                    break;
                case "stars2":
                    // Code to execute when stars2 is clicked
                    SetStarRating(2);
                    break;
                case "stars3":
                    // Code to execute when stars3 is clicked
                    SetStarRating(3);
                    break;
                case "stars4":
                    // Code to execute when stars4 is clicked
                    SetStarRating(4);
                    break;
                case "stars5":
                    // Code to execute when stars5 is clicked
                    SetStarRating(5);
                    break;

            }

            myModal.Attributes["style"] = "display: block;";
        }

        public void SetStarRating(int rating)
        {
            // Set all stars to black
            star_1.Attributes["style"] = "color: grey;";
            star_2.Attributes["style"] = "color: grey;";
            star_3.Attributes["style"] = "color: grey;";
            star_4.Attributes["style"] = "color: grey;";
            star_5.Attributes["style"] = "color: grey;";

            // Set the selected number of stars to gold
            switch (rating)
            {
                case 1:
                    star_1.Attributes["style"] = "color: gold;";
                    numberrating.Value = "1";
                    break;
                case 2:
                    star_1.Attributes["style"] = "color: gold;";
                    star_2.Attributes["style"] = "color: gold;";

                    numberrating.Value = "2";
                    break;
                case 3:
                    star_1.Attributes["style"] = "color: gold;";
                    star_2.Attributes["style"] = "color: gold;";
                    star_3.Attributes["style"] = "color: gold;";
                    numberrating.Value = "3";
                    break;
                case 4:
                    star_1.Attributes["style"] = "color: gold;";
                    star_2.Attributes["style"] = "color: gold;";
                    star_3.Attributes["style"] = "color: gold;";
                    star_4.Attributes["style"] = "color: gold;";
                    numberrating.Value = "4";
                    break;
                case 5:
                    star_1.Attributes["style"] = "color: gold;";
                    star_2.Attributes["style"] = "color: gold;";
                    star_3.Attributes["style"] = "color: gold;";
                    star_4.Attributes["style"] = "color: gold;";
                    star_5.Attributes["style"] = "color: gold;";
                    numberrating.Value = "5";
                    break;
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseHistory.aspx");
        }

        protected void rate_Click(object sender, EventArgs e)
        {
            int newRatingID = 0;
            // Get the connection string from the Web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // Create a new SQL connection using the connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Create a SQL command to insert a new product rating into the Product_Rating table
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Product_Rating (rating_num, comment, variation_id, created_at, user_id)\r\nOUTPUT INSERTED.ID\r\nVALUES (@RatingNum, @Comment, @variation_id, @CreatedAt, @UserId)\r\n";
                string value1 = numberrating.Value;
                cmd.Parameters.AddWithValue("@RatingNum", Convert.ToInt32(value1));
                cmd.Parameters.AddWithValue("@Comment", comment.Text);
                cmd.Parameters.AddWithValue("@variation_id", Request.QueryString["variationID"]);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["user_id"]));

                // Open the database connection and execute the SQL command
                conn.Open();
                newRatingID = (int)cmd.ExecuteScalar();

                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Order_Item\r\nSET rating_id = @ratingId\r\nWHERE id = @cartId\r\n";
                cmd.Parameters.AddWithValue("@ratingId", newRatingID);
                cmd.Parameters.AddWithValue("@cartId", cardids);


                try
                {

                    cmd.ExecuteNonQuery();

                    // Show an alert dialog indicating that the rating was added successfully
                    string script = "alert('Rating added successfully.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                    // Redirect the user to the purchase_history.aspx page
                    string redirectUrl = "PurchaseHistory.aspx";
                    string script2 = "window.location = '" + redirectUrl + "';";
                    ScriptManager.RegisterStartupScript(this, GetType(), "RedirectScript", script2, true);

                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the database operation
                    Response.Write("Error: " + ex.Message);
                }
            }


        }
    }
}