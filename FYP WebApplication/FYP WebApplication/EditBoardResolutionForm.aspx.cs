using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Kernel.Pdf;

namespace FYP_WebApplication
{
    public partial class EditBoardResolutionForm : System.Web.UI.Page
    {

        int totalrow = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void page_init(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

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

            DataTable dt = GetInputData(Convert.ToInt32(Request.QueryString["id"]));

            foreach (DataRow row in dt.Rows)
            {
                totalrow++;
                string labelName = row["label_name"].ToString();
                string content = row["value_text"].ToString();

                // Create a Label control for each row
                Label label = new Label();
                label.Text = labelName;
                label.CssClass = "labelClass"; // Optional: You can add a CSS class for styling

                // Create a TextBox control for each row
                TextBox textBox = new TextBox();
                textBox.ID = labelName; // Use the LabelName as the TextBox ID
                textBox.Text = content;
                textBox.CssClass = "TextBox"; // Optional: You can add a CSS class for styling

                // Add the controls to the placeholder
                placeholder1.Controls.Add(label);
                placeholder1.Controls.Add(textBox);

                // Add a line break after each Label and TextBox

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            
             

          



        }


        protected void submit_Click(object sender, EventArgs e)
        {
            
            // Generate the 2D array based on the dynamically generated rows
            string[,] dataArray = new string[totalrow, 2];

            // Populate the 2D array with placeholders and user inputs
            int index = 0;
            string labelText = string.Empty;
            
            int numberOfControls = placeholder1.Controls.Count;

            foreach (Control control in placeholder1.Controls)
            {
                if (control is Label)
                {
                    Label label = (Label)control;
                    labelText = label.Text; // Get the label text and remove the trailing colon
                }
                else if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    dataArray[index, 0] = "{" + labelText + "}"; // Placeholder ID (label text without the colon)
                    dataArray[index, 1] = textBox.Text; // User input for the corresponding TextBox
                
                
                   
                    index++;

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {

                        using (SqlCommand cmd = new SqlCommand("UPDATE BoardResolutionField SET value_text = @value_text WHERE boardReId = @boardReId AND label_name = @label ", conn))
                        {
                            cmd.Parameters.AddWithValue("@boardReId", Convert.ToInt32(Request.QueryString["id"]));
                            cmd.Parameters.AddWithValue("@label", labelText);
                            cmd.Parameters.AddWithValue("@value_text", textBox.Text);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }





                }
            }

            // Step 3: Replace placeholders in the PDF content with user inputs based on the 2D array
            string pdfContent = string.Empty;



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [content] FROM [dbo].[BoardResolution] WHERE [boardReId] = @BoardReId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BoardReId", Convert.ToInt32(Request.QueryString["id"]));
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        pdfContent = (string)result;
                    }
                }
            }

            pdfContent = ReplacePlaceholders(pdfContent, dataArray);
            GenerateAndSavePDF(pdfContent, Convert.ToInt32(Request.QueryString["id"]));
            Global.InsertAuditRecord(Convert.ToInt32(Request.QueryString["reqID"]), "Edited Board Resolution Form: " + Request.QueryString["id"], Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

            Global.AddNotification("Board Resolution Edited", Convert.ToInt32(Request.QueryString["reqID"]), GetCreatedBy(Convert.ToInt32(Request.QueryString["reqID"])), Convert.ToInt32(Session["userid"]));

            Response.Redirect("SignBoardResolution.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]) + "&reqID=" + Convert.ToInt32(Request.QueryString["reqID"]));

            //end modify
        }

        public int GetCreatedBy(int requestID)
        {
            int createdBy = -1; // Assuming -1 is an invalid userID, change it based on your requirements.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [createdBy] FROM [dbo].[Request] WHERE [requestID] = @RequestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            createdBy = Convert.ToInt32(reader["createdBy"]);
                        }
                    }
                }
            }

            return createdBy;
        }

        public void UpdateRequestStatusToVerification(int requestID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the status to 'verification' for the specified requestID
                string query = "UPDATE [Request] SET status = 'verification' WHERE requestID = @requestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@requestID", requestID);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        private DataTable GetInputData(int boardReId)
        {
            // Set your connection string here
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [label_name], [value_text] FROM [dbo].[BoardResolutionField] WHERE [BoardReId] = @BoardReId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BoardReId", boardReId);
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private string ReplacePlaceholders(string inputText, string[,] dataArray)
        {
            string pattern = @"\{([^}]+)\}";
            MatchCollection matches = Regex.Matches(inputText, pattern);

            foreach (Match match in matches)
            {
                string placeholder = match.Value;
                for (int i = 0; i < dataArray.GetLength(0); i++)
                {
                    if (dataArray[i, 0] == placeholder)
                    {
                        inputText = inputText.Replace(placeholder, dataArray[i, 1]);
                        break;
                    }
                }
            }

            return inputText;
        }

        public void GenerateAndSavePDF(string content, int NewBoardID)
        {

            // Generate the PDF bytes from the content
            byte[] pdfBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(content, memoryStream, converterProperties);
                pdfBytes = memoryStream.ToArray();
            }

            // Rename the file to test1.pdf
            string renamedFileName = "test1.pdf";
            using (MemoryStream renamedMemoryStream = new MemoryStream())
            {
                // Save the PDF to the renamedMemoryStream with the name test1.pdf
                using (PdfWriter renamedPdfWriter = new PdfWriter(renamedMemoryStream))
                {
                    using (PdfDocument renamedPdfDocument = new PdfDocument(renamedPdfWriter))
                    {
                        PdfArray nameArray = new PdfArray();
                        nameArray.Add(new PdfString(renamedFileName));
                        renamedPdfDocument.GetCatalog().Put(PdfName.Names, nameArray);

                        using (PdfReader pdfReader = new PdfReader(new MemoryStream(pdfBytes)))
                        {
                            PdfDocument pdfDocument = new PdfDocument(pdfReader);
                            pdfDocument.CopyPagesTo(1, pdfDocument.GetNumberOfPages(), renamedPdfDocument);
                        }
                    }
                }


                // Save the renamedMemoryStream to the database
                byte[] renamedPdfBytes = renamedMemoryStream.ToArray();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {



                    using (SqlCommand cmd = new SqlCommand("UPDATE BoardResolution SET attachment = @pdf WHERE boardReId = @boardReId", conn))
                    {
                        cmd.Parameters.AddWithValue("@boardReId", NewBoardID);
                        cmd.Parameters.AddWithValue("@pdf", renamedPdfBytes);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectTemplate.aspx");
        }

        protected int DuplicateBoardResolution(int existingBoardReID)
        {
            // Step 1: Duplicate BoardResolution record
            int newBoardReID = DuplicateBoardResolutionRecord(existingBoardReID);

            // Step 2: Duplicate related BoardResolutionField records
            DuplicateBoardResolutionFieldRecords(existingBoardReID, newBoardReID);

            return newBoardReID;
            // Now, you have a new BoardResolution record with a new boardReID and related BoardResolutionField records.
        }

        private int DuplicateBoardResolutionRecord(int existingBoardReID)
        {
            int newBoardReID = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Duplicate BoardResolution record
                    using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[BoardResolution] (type, title, description, due_date, attachment, created_date, requestID, content, createdBy) SELECT type, title, description, due_date, attachment, GETDATE(), requestID, content, createdBy FROM [dbo].[BoardResolution] WHERE boardReID = @ExistingBoardReID; SELECT SCOPE_IDENTITY();", connection))
                    {
                        command.Parameters.AddWithValue("@ExistingBoardReID", existingBoardReID);

                        newBoardReID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return newBoardReID;
        }

        private void DuplicateBoardResolutionFieldRecords(int existingBoardReID, int newBoardReID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 2: Duplicate related BoardResolutionField records
                    using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[BoardResolutionField] (boardReID, label_name, value_text) SELECT @NewBoardReID, label_name, value_text FROM [dbo].[BoardResolutionField] WHERE boardReID = @ExistingBoardReID;", connection))
                    {
                        command.Parameters.AddWithValue("@NewBoardReID", newBoardReID);
                        command.Parameters.AddWithValue("@ExistingBoardReID", existingBoardReID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or display an error message
            }
        }

    }
}