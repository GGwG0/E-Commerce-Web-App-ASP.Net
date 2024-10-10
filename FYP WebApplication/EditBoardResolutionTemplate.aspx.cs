using iText.Html2pdf;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class EditBoardResolutionTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }


                GetUserDetails(Convert.ToInt32(Session["userid"]));
                string roles = Session["currentRole"].ToString();


                if (roles == "cosec user" || roles == "client user")
                {
                    if (roles == "cosec user")
                    {
                        secretaryBar.Visible = false;
                    }


                    adminDashboard.Visible = false;
                    adminDashboard2.Visible = false;
                }
                else if (roles == "service user" || roles == "service admin" || roles == "client admin" || roles == "cosec admin")
                {
                    clientDashboard.Visible = false;
                    secretaryBar.Visible = false;
                }


                // Retrieve the content from the database based on your logic
                string content = RetrieveContentFromDatabase( Convert.ToInt32(Request.QueryString["id"])); // Implement this method to retrieve content from the database

                // Set the retrieved content to the CKEditor
                editor.Text = HttpUtility.HtmlDecode(content);



                DateTime currentDate = DateTime.Now;
                createdDate.Text = currentDate.ToString("dd MMM yyyy");

                LoadDataForBoardResolution( Convert.ToInt32(Request.QueryString["id"]));
            }

        }

        public void GetUserDetails(int userID)
        {
            try
            {
                // Your database connection logic goes here
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the SQL query to retrieve user details
                    string query = "SELECT [name], [profilePicture] FROM [dbo].[User] WHERE [userID] = @userID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter to the query
                        command.Parameters.AddWithValue("@userID", userID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are rows returned
                            if (reader.Read())
                            {

                                //imgProfile.Text = reader["name"].ToString();
                                // Access the columns as needed
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                names.InnerText = name;
                                byte[] profilePicture = reader["profilePicture"] as byte[];

                                if (profilePicture != null && profilePicture.Length > 0)
                                {
                                    string image = Convert.ToBase64String(profilePicture);
                                    string base64String = "data:image/jpg;base64," + image;

                                    imgProfile.ImageUrl = base64String;

                                }
                                else
                                {
                                    imgProfile.ImageUrl = "~/assets/image/profile/default.jpg";

                                }


                            }
                            else
                            {
                                Console.WriteLine($"User with ID {userID} not found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error retrieving user details: {ex.Message}");
            }
        }


        // Method to retrieve content from the database
        private string RetrieveContentFromDatabase(int boardReID)
        {

            string query = "SELECT [content] FROM [dbo].[BoardResolution] WHERE [boardReID] = @BoardReID"; // Adjust the query based on your requirements
            // Replace with the actual boardReID for the record you want to retrieve

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BoardReID", boardReID);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }



            return string.Empty;
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

        protected void editBtn_click(object sender, EventArgs e)
        {

            string test1 = editor.Text;
            DeleteBoardResolutionField( Convert.ToInt32(Request.QueryString["id"]));
            UpdateBoardResolutionData( Convert.ToInt32(Request.QueryString["id"]));

            string content = editor.Text;
            MatchCollection matches = Regex.Matches(content, @"\{.*?\}");
            List<string> variableNames = new List<string>();

            foreach (Match match in matches)
            {
                string variableName = match.Value.Trim('{', '}');
                variableNames.Add(variableName);
                InsertVariableIntoUserInput(variableName,  Convert.ToInt32(Request.QueryString["id"]));
            }

            Global.InsertAuditRecord(0, "Edited Board Resolution Template : " + Request.QueryString["id"], Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
            GenerateAndSavePDF(editor.Text,  Convert.ToInt32(Request.QueryString["id"]));
            Response.Redirect("ViewBoardResolutionTemplate.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
        
        }


        private void DeleteBoardResolutionField(int boardReFieldID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = "DELETE FROM [dbo].[BoardResolutionField] WHERE [boardReID] = @boardReID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@boardReID", boardReFieldID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void UpdateBoardResolutionData(int boardReIDToUpdate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "UPDATE [dbo].[BoardResolution] SET [title] = @Title, [description] = @Description, [content] = @Content WHERE [boardReID] = @BoardReID";

            // Assuming you have TextBox controls with IDs TextBox1, TextBox2, and TextBox3 for title, description, and content respectively
            string title = Name.Text;
            string description = Description.Text;
            string content = editor.Text;
            // Replace with the actual boardReID you want to update

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@BoardReID", boardReIDToUpdate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        protected void InsertVariableIntoUserInput(string variableName, int boardReId)
        {
            // Replace the following with your actual ADO.NET code to insert the data into the "UserInput" table
            string insertQuery = "INSERT INTO [dbo].[BoardResolutionField] ([boardReID], [label_name]) VALUES (@BoardReID, @label_name)";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@label_name", variableName);
                    cmd.Parameters.AddWithValue("@BoardReID", boardReId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GenerateAndSavePDF(string content, int boardReId)
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
                        cmd.Parameters.AddWithValue("@boardReId", boardReId);
                        cmd.Parameters.AddWithValue("@pdf", renamedPdfBytes);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBoardResolutionTemplate.aspx?id=" + Request.QueryString["id"]);
        }
    }
}