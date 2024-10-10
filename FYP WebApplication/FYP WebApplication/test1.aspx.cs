using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    byte[] fileBytes = FileUpload1.FileBytes;
                    // Save the image data into the database
                    SaveImageDataToDatabase(fileBytes);

                    // Optionally, you can display a success message or redirect the user
                    Response.Write("File uploaded successfully!");

                }
                catch (Exception ex)
                {
                    // Handle the exception, display an error message, or log the error
                    Response.Write("Error: " + ex.Message);
                }


            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }

        private void SaveImageDataToDatabase(byte[] imageData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert the image data into the database
                string query = "UPDATE [User] set profilePicture = @image where userID = @userID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@image", imageData);
                    command.Parameters.AddWithValue("@userID", 11);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}