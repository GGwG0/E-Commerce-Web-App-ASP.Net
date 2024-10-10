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
    public partial class ViewBoardResolutionTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                LoadDataForBoardResolution(Convert.ToInt32(Request.QueryString["id"]));

                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                // Replace "YourConnectionString" with your actual database connection string.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int boardReID = Convert.ToInt32(Request.QueryString["id"]); // Replace with the actual boardReID you want to retrieve.

                    string sql = "SELECT attachment FROM BoardResolution WHERE boardReID = @BoardReID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@BoardReID", boardReID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] pdfData = (byte[])reader["attachment"];
                                string base64Pdf = Convert.ToBase64String(pdfData);

                                // Embed the PDF in the iframe.
                                iframePdfViewer.Src = $"data:application/pdf;base64,{base64Pdf}";
                            }
                        }
                    }
                }
            }

        }
        private void LoadDataForBoardResolution(int boardReID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = "SELECT [title], [description], [created_date] FROM [dbo].[BoardResolution] WHERE [boardReID] = @BoardReID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BoardReID", boardReID);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Bind data to the controls
                        Name.Text = reader["title"].ToString();
                        Description.Text = reader["description"].ToString();
                        createdDate.Text = reader["created_date"].ToString();
                    }

                    reader.Close();
                }
            }
        }

        protected void editBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"EditBoardResolutionTemplate.aspx?id=" + Request.QueryString["id"]);
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"BoardResolutionTemplateList.aspx");
        }
    }
}