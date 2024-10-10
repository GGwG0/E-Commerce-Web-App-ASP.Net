using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal Literal1 = (Literal)e.Row.FindControl("litStars");
                decimal ratingNum = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rating_num"));


                if (ratingNum > 0)
                {

                    int fullStars = (int)Math.Floor(ratingNum);
                    int halfStars = (int)Math.Floor((ratingNum - fullStars) * 2);
                    int emptyStars = 5 - fullStars - halfStars;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < fullStars; i++)
                    {
                        sb.Append("<ion-icon class=\"stars\" name=\"star\"></ion-icon>");
                    }
                    for (int i = 0; i < halfStars; i++)
                    {
                        sb.Append("<ion-icon class=\"stars\" name=\"star-half\"></ion-icon>");
                    }
                    for (int i = 0; i < emptyStars; i++)
                    {
                        sb.Append("<ion-icon class=\"stars\" name=\"star-outline\"></ion-icon>");
                    }

                    Literal1.Text = sb.ToString();
                }
                else
                {
                    Literal1.Text = "<ion-icon class=\"stars\" name=\"star-outline\"></ion-icon><ion-icon class=\"stars\" name=\"star-outline\"></ion-icon><ion-icon class=\"stars\" name=\"star-outline\"></ion-icon><ion-icon class=\"stars\" name=\"star-outline\"></ion-icon><ion-icon class=\"stars\" name=\"star-outline\"></ion-icon>";
                }
            }
        }

        protected void replyComment_Click(object sender, ImageClickEventArgs e)
        {
            // Get the reference to the ImageButton that was clicked
            ImageButton replyButton = (ImageButton)sender;

            // Get the reference to the parent GridView row
            GridViewRow row = (GridViewRow)replyButton.NamingContainer;

            // Get the necessary data from the row
            string messageId = GridView1.DataKeys[row.RowIndex].Value.ToString();
            // Retrieve additional data using the row's controls if needed

            TextBox replyTextBox = (TextBox)row.FindControl("ReplyTextBox");
            string replyMessage = replyTextBox.Text;

            // Save the reply message to the database
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Desktop\\DemoAssignment\\DemoAssignment\\DemoAssignment\\App_Data\\ComputerHardware.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Prepare the SQL statement to insert the reply message into the database
                string insertQuery = "INSERT INTO Product_Rating (MessageId, adminReply) VALUES (@MessageId, @adminReply)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@MessageId", messageId);
                    command.Parameters.AddWithValue("@adminReply", replyMessage);

                    // Execute the SQL command
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GridView1.SelectedDataKey.Value.ToString();
            string url = "DisplayRatingDetails.aspx?id=" + id;
            Response.Redirect(url);
        }
    }
}