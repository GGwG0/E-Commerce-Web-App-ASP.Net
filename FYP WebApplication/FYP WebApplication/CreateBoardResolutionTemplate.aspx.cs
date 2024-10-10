using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public partial class CreateBoardResolutionTemplate : System.Web.UI.Page
    {
        int BRID = 0;

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




                DateTime currentDate = DateTime.Now;
                createdDate.Text = currentDate.ToString("dd MMM yyyy");
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


        protected void addbtn_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define your SQL query to insert a new row into the table
                string query = @"INSERT INTO [dbo].[BoardResolution] ([title], [type], [description], [created_Date])
                             VALUES (@title, @type, @description, @created_Date);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set the parameter values for the query
                    command.Parameters.AddWithValue("@title", Name.Text);
                    command.Parameters.AddWithValue("@type", 'T');
                    command.Parameters.AddWithValue("@created_Date", DateTime.Now);
                    command.Parameters.AddWithValue("@description", Description.Text);

                    connection.Open();
                    // Execute the query and get the newly created ID using SCOPE_IDENTITY()
                    BRID = Convert.ToInt32(command.ExecuteScalar());



                    if (BRID != 0)
                    {
                        addbtn.Text = "Edit/Save Template";

                    }
                }
            }

            string content = editor.Text;
            MatchCollection matches = Regex.Matches(content, @"\{.*?\}");
            List<string> variableNames = new List<string>();

            foreach (Match match in matches)
            {
                string variableName = match.Value.Trim('{', '}');
                variableNames.Add(variableName);
                InsertVariableIntoUserInput(variableName, BRID);
            }

            Global.InsertAuditRecord(0, "Created a BR Template: " + BRID, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
            UpdateBoardResolutionContent(BRID, content);
            GenerateAndSavePDF(content, BRID);

            Response.Redirect("BoardResolutionTemplateList.aspx");

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

        protected void UpdateBoardResolutionContent(int boardReId, string content)
        {
            string updateQuery = "UPDATE [dbo].[BoardResolution] SET [content] = @Content WHERE [boardReId] = @BoardReId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Content", content);
                    cmd.Parameters.AddWithValue("@BoardReId", boardReId);
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
            Response.Redirect("BoardResolutionTemplateList.aspx");
        }
    }
}