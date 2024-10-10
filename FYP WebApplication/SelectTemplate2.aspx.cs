using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class SelectTemplate2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBoardResolutionData2();
            }
        }

        private void BindBoardResolutionData2()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "SELECT * FROM [dbo].[BoardResolution] where [type] = 't'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    DataColumn base64Column = new DataColumn("Base64Attachment", typeof(string));
                    dataTable.Columns.Add(base64Column);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        byte[] pdfData = row["attachment"] as byte[];

                        if (pdfData != null && pdfData.Length > 0)
                        {
                            string base64String = "data:application/pdf;base64," + Convert.ToBase64String(pdfData);
                            row["Base64Attachment"] = base64String;
                        }
                    }

                    repeater1.DataSource = dataTable.DefaultView;
                    repeater1.DataBind();
                }
            }
        }



        private void BindBoardResolutionData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "SELECT * FROM [dbo].[BoardResolution]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                    dataTable.Columns.Add(base64Column);


                    foreach (DataRow row in dataTable.Rows)
                    {
                        byte[] imageBytes = row["attachment"] as byte[];

                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string base64String = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                            row["Base64ProfilePicture"] = base64String;
                        }

                    }

                    repeater1.DataSource = dataTable.DefaultView;
                    repeater1.DataBind();


                }
            }
        }

        protected void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Handle the ItemDataBound event to set the image source for each item in the repeater
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dataRow = e.Item.DataItem as DataRowView;
                if (dataRow != null)
                {
                    byte[] imageData = dataRow["attachment"] as byte[];
                    if (imageData != null)
                    {
                        Image imgDisplay = e.Item.FindControl("imgProfile") as Image;
                        if (imgDisplay != null)
                        {
                            // Convert to Base64
                            string base64String = Convert.ToBase64String(imageData);
                            imgDisplay.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }
                }
            }
        }
    }
}