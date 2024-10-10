using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Retrieve attachment details from the database based on requestID
                AttachmentDetails details = GetAttachmentDetails(Convert.ToInt32(Request.QueryString["id"]));

                // Send the file to the user for download
                SendFileToClient(details.FileName, details.Attachment);
                Response.Redirect("ViewRequestDetail.aspx?id=" + Request.QueryString["id"]);

            }
        }

        private void SendFileToClient(string fileName, byte[] fileData)
        {
            Response.Clear();

            // Determine content type and disposition based on file extension
            string contentType = GetContentType(fileName);
            string contentDisposition = IsImage(contentType) ? "inline" : "attachment";

            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", $"{contentDisposition}; filename={fileName}");
            Response.BinaryWrite(fileData);
            Response.End();
        }

        private string GetContentType(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName).ToLower();

            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".pdf":
                    return "application/pdf";
                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".csv":
                    return "text/csv";
                case ".zip":
                    return "application/zip";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".txt":
                    return "text/plain";
                default:
                    return "application/octet-stream";
            }

        }

        private bool IsImage(string contentType)
        {
            return contentType.StartsWith("image/");
        }

        private AttachmentDetails GetAttachmentDetails(int requestID)
        {
            // Implement your logic to retrieve attachment details from the database
            // For example, query the BoardResolution table using the requestID

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            AttachmentDetails details = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT filename, attachment FROM [dbo].[Request] WHERE  requestID = @requestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@requestID", requestID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            details = new AttachmentDetails
                            {
                                FileName = reader["filename"].ToString(),
                                Attachment = (byte[])reader["attachment"]
                            };
                        }
                    }
                }
            }

            return details;
        }
    }

    public class AttachmentDetails
    {
        public string FileName { get; set; }
        public byte[] Attachment { get; set; }
        // Add other properties as needed
    }
}