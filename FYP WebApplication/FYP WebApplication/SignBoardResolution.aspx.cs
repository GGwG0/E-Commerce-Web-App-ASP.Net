using iTextSharp.text.pdf.security;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = iTextSharp.text.Image;
using System.Data;
using iText.Commons.Utils;
using Org.BouncyCastle.Ocsp;
using System.Runtime.ConstrainedExecution;
using iText.IO.Colors;
using System.Windows.Controls;
using iTextSharp.text;
using System.Windows.Media.Media3D;
using System.Web.Security;
using static System.Net.WebRequestMethods;
using System.EnterpriseServices.Internal;

namespace FYP_WebApplication
{
    public partial class SignBoardResolution : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //20 = client user
                // 3 = cosec user

                //Session["userid"] = 12;
                //Session["currentRole"] = RequestDashboard.GetRole(12);

                string role = null;
                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Session["currentRole"].ToString() == "client user")
                {
                    if (Session["cosecId"] == null)
                    {
                        Session["cosecId"] = TwoFactorAuthentication.GetCosecIdFromCompany(Convert.ToInt32(Session["userid"]));
                        role = Session["cosecId"].ToString();
                    }
                }

                LoadInfo();
                BindDataToLabels(Convert.ToInt32(Request.QueryString["id"]));
                byte[] pdfByte = LoadDocumentFromDatabase(Convert.ToInt32(Request.QueryString["id"]));
                DisplayPdf(pdfByte);

            }

        }

        protected void LoadInfo()
        {
            // verification 
            // client - edit
            // cosec - publish, fix

            // published
            // client - approve, otp
            // cosec -
            string status = GetStatusByRequestID(Convert.ToInt32(Request.QueryString["reqID"]));
            string role = Session["currentRole"].ToString();
            int boardReID = Convert.ToInt32(Request.QueryString["id"]);

            if (status == "verification") // if request is in verification status
            {

                if (role == "client user")
                {
                    btnPublish.Visible = false;
                    btnEdit.Visible = false;
                }
                else if (role == "cosec user")
                {
                  
                }
                btnApprove.Visible = false;
                containerOTP.Visible = false;
                title.InnerText = "Verifying Board Resolution";
            }
            else if (status == "Published") // if board resolution already published
            {
                // check if all users have signed 
                // yes = move to completed
                // no =
                // current user: client user
                // 
                // current user: cosec
                // notify that user?? sign before due date

                if (role == "client user")
                {
                    Boolean checkUserInSignature = CheckUserInSignTable();
                    if (checkUserInSignature) // already sign
                    {
                        btnApprove.Visible = false;
                        containerOTP.Visible = false;
                    }

                }
                title.InnerText = "Passing Board Resolution";
                btnEdit.Visible = false;
                btnPublish.Visible = false;

            }
            else
            {
                title.InnerText = "Completed Board Resolution";
                btnEdit.Visible = false;
                btnPublish.Visible = false;
                btnApprove.Visible = false;
                containerOTP.Visible = false;
            }

            requestID.Text = Request.QueryString["reqID"];
            statusLabel.Text = GetStatusByRequestID(Convert.ToInt32(Request.QueryString["reqID"]));
        }

        protected Boolean CheckUserInSignTable()
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("CheckUserInSignature", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["userid"]));
                    cmd.Parameters.AddWithValue("@boardReID", Convert.ToInt32(Request.QueryString["id"]));
                    cmd.Parameters.AddWithValue("@requestID", Convert.ToInt32(Request.QueryString["reqID"]));

                    // Add the output parameter
                    SqlParameter outputParameter = new SqlParameter("@isAuthorizedOutput", SqlDbType.Bit);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    // Execute the stored procedure
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    bool isAuthorized = Convert.ToBoolean(outputParameter.Value);

                    // Now 'isAuthorized' contains the result
                    if (isAuthorized)
                    {
                        return true; //in signature 
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public int GetCreatedBy(int requestID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the SQL query to retrieve the "created by" user
                string query = "SELECT [createdBy] FROM [dbo].[Request] WHERE [requestID] = @requestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the query
                    command.Parameters.AddWithValue("@requestID", requestID);

                    // Execute the query and get the result
                    object createdBy = command.ExecuteScalar();

                    // Check if the result is not null
                    if (createdBy != null && createdBy != DBNull.Value)
                    {
                        // Return the "created by" as an int
                        return Convert.ToInt32(createdBy);
                    }
                }
            }
            return 0;
        }

        public void BindDataToLabels(int boardReID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [dbo].[BoardResolution] WHERE [boardReID] = @BoardReID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BoardReID", boardReID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Bind values to the labels
                            createdDate.Text = reader["created_date"] is DBNull ? string.Empty : ((DateTime)reader["created_date"]).ToString("d MMMM yyyy");
                            titleLabel.Text = reader["title"] is DBNull ? string.Empty : (string)reader["title"];
                            descLabel.Text = reader["description"] is DBNull ? string.Empty : (string)reader["description"];
                        }
                    }
                }
            }
        }

        public string GetStatusByRequestID(int requestID)
        {
            string status = null; // Default value if not found or an error occurs

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve the status for the specified requestID
                string query = "SELECT status FROM [Request] WHERE requestID = @requestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@requestID", requestID);

                    // Execute the query
                    object result = command.ExecuteScalar();

                    // Check if the result is not null
                    if (result != null && result != DBNull.Value)
                    {
                        status = result.ToString();
                    }
                }
            }

            return status;
        }

        protected int AddRecordToSignatureDB()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection

            SqlCommand command2 = new SqlCommand("INSERT INTO Signature (createdDate, userID, boardReID) VALUES (@createdDate, @userID, @boardReID);", connection);
            command2.Parameters.AddWithValue("@createdDate", DateTime.Now);
            command2.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["userid"]));
            command2.Parameters.AddWithValue("@boardReID", Convert.ToInt32(Request.QueryString["id"]));
            int success = command2.ExecuteNonQuery();

            return success;
        }

        protected void btnSign_Click(object sender, EventArgs e)
        {

            int success = AddRecordToSignatureDB();

            Boolean checkSignNumCompleted = ExecuteStoredProcedure(GetCompanyID(Convert.ToInt32(Session["userid"])), Convert.ToInt32(Request.QueryString["id"]));

            if (checkSignNumCompleted)
            {
                AddNotification("Board Resolution Has been Passed", Convert.ToInt32(Request.QueryString["reqID"]), Convert.ToInt32(Session["cosecId"]), Convert.ToInt32(Session["userid"]));
                signProcess();
                UpdateRequestStatusToCompleted(Convert.ToInt32(Request.QueryString["reqID"]));
                LoadInfo();
                BindDataToLabels(Convert.ToInt32(Request.QueryString["id"]));
                byte[] pdfByte = LoadDocumentFromDatabase(Convert.ToInt32(Request.QueryString["id"]));
                DisplayPdf(pdfByte);
            }

           
        }
        protected int AddNotification(string action, int requestID, int to, int from)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
            connection.Open();
            command2.Parameters.AddWithValue("@action", action);
            command2.Parameters.AddWithValue("@to", to);
            command2.Parameters.AddWithValue("@from", from);
            command2.Parameters.AddWithValue("@requestID", requestID);
            command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
            int notificationSucess = command2.ExecuteNonQuery();

            return notificationSucess;
        }

        protected void SavePDFToDB(byte[] pdf)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Update BoardResolution SET attachment = @attachment where boardReID = @boardReID;", conn))
                {
                    cmd.Parameters.AddWithValue("@attachment", pdf);
                    cmd.Parameters.AddWithValue("@boardReID", Convert.ToInt32(Request.QueryString["id"]));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void signProcess()
        {
            string query = "SELECT U.username, U.graphicPic, U.signaturePic, U.userID FROM Signature S LEFT JOIN [User] U ON S.userID = U.userID WHERE S.boardReID = @boardReID";

            byte[] pdfBytes = LoadDocumentFromDatabase(Convert.ToInt32(Request.QueryString["id"]));
            int totalPage = GetNumberOfPages(pdfBytes);
            int newPage = totalPage + 1;
            pdfBytes = AddNewPageToPdf(pdfBytes);

            float[] offsetXArray = new float[] { 70, 350, 70, 350, 70, 350, 70, 350, 70, 350 };
            float[] offsetYArray = new float[] { 650, 650, 500, 500, 350, 350, 200, 200, 50, 50 };

            // offsetX, offsetY
            // cert1 - 70,      650,           
            // cert2 - 70,      500            
            // cert3 - 70,      350             
            // cert4 - 70,      200           
            // cert5 - 70,      50             
            // cert6 - 350,     650            
            // cert7 - 350,     500            
            // cert8 - 350,     350             
            // cert9 - 350,     200            
            // cert10 - 350,    50  


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@boardReID", Convert.ToInt32(Request.QueryString["id"]));
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int userIndex = 0;
                        while (reader.Read())
                        {
                            byte[] cert = reader["signaturePic"] as byte[];
                            byte[] graphic = reader["graphicPic"] as byte[];
                            string username = reader["username"] as string;

                            // Adjust the parameters as needed
                            int gWidth = 250;
                            int dWidth = 150;
                            int height = 100;

                            // Apply signature to PDF
                            pdfBytes = ApplySignature(pdfBytes, cert, graphic, offsetXArray[userIndex], offsetYArray[userIndex], gWidth, dWidth, height, username, username, newPage);

                            // Move to the next user
                            userIndex++;

                            // Check if the user count exceeds 8 and increment the page
                            if (userIndex > 9)
                            {
                                pdfBytes = AddNewPageToPdf(pdfBytes);
                                newPage++;
                                userIndex = 0;
                            }
                        }
                    }
                }
            }

            SavePDFToDB(pdfBytes);
        }

        private int GetNumberOfPages(byte[] pdfBytes)
        {
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            {
                using (PdfReader reader = new PdfReader(pdfStream))
                {
                    return reader.NumberOfPages;
                }
            }
        }

        protected void DisplayPdf(byte[] pdfBytes)
        {
            // Convert the byte array to a Base64-encoded string
            string base64Pdf = Convert.ToBase64String(pdfBytes);

            // Create an iframe and set its source to display the PDF content
            iframePdfViewer.Attributes["src"] = "data:application/pdf;base64," + base64Pdf;
        }
        protected byte[] LoadDocumentFromDatabase(int boardReID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            byte[] pdfData = null;
            // Replace "YourConnectionString" with your actual database connection string.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string sql = "SELECT attachment FROM BoardResolution WHERE boardReID = @BoardReID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BoardReID", boardReID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pdfData = (byte[])reader["attachment"];
                            string base64Pdf = Convert.ToBase64String(pdfData);

                            // Embed the PDF in the iframe.
                        }
                    }
                }

            }
            return pdfData;
        }

        private byte[] AddNewPageToPdf(byte[] pdfBytes)
        {
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            using (MemoryStream outputMemoryStream = new MemoryStream())
            {
                // Create a new stamper
                using (PdfStamper stamper = new PdfStamper(new PdfReader(pdfStream), outputMemoryStream))
                {
                    // Add a new page
                    stamper.InsertPage(stamper.Reader.NumberOfPages + 1, PageSize.A4);

                    // Close the stamper
                    stamper.Close();
                }

                // Get the updated PDF as bytes
                byte[] updatedPdfBytes = outputMemoryStream.ToArray();

                return updatedPdfBytes;
            }
        }

        protected byte[] ApplySignature(byte[] pdfBytes, byte[] certBytes, byte[] graphicSign, float offsetX, float offsetY, int gWidth, int dWidth, int height, string signatureFieldName, string userName, int targetPage)
        {

            // Step 1: Load the PDF document from pdfBytes
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            {
                // Step 2: Load the certificate from certBytes and certificatePassword
                X509Certificate2 certificate = new X509Certificate2(certBytes, "1234", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
                X509CertificateParser parser = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate bouncyCastleCertificate = parser.ReadCertificate(certificate.GetRawCertData());


                IExternalSignature signature = new X509Certificate2Signature(certificate, "SHA-1");

                // Step 4: Apply the digital signature to the PDF document
                using (PdfReader reader = new PdfReader(pdfStream))
                using (MemoryStream outputMemoryStream = new MemoryStream())
                using (PdfStamper stamper = PdfStamper.CreateSignature(reader, outputMemoryStream, '\0'))
                {
                    // Graphic Signature  
                    iTextSharp.text.Image image = Image.GetInstance(graphicSign);
                    image.ScaleToFit(gWidth, image.Height);
                    image.SetAbsolutePosition(offsetX, offsetY);
                    var overContent = stamper.GetOverContent(targetPage);
                    overContent.AddImage(image);

                    //Digital Signature
                    PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                    //appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(50, 100, 250, 150), 1, signatureFieldName);
                    appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(offsetX, offsetY, offsetX + dWidth, offsetY + height), targetPage, signatureFieldName);

                    PdfSignature signField = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
                    signField.Date = new PdfDate(appearance.SignDate);
                    appearance.CryptoDictionary = signField;

                    appearance.Certificate = bouncyCastleCertificate;

                    // Set the certificate's chain
                    X509Chain chain = new X509Chain();
                    chain.Build(certificate);
                    ICollection<Org.BouncyCastle.X509.X509Certificate> chainCertificates = new List<Org.BouncyCastle.X509.X509Certificate>();
                    foreach (X509ChainElement chainElement in chain.ChainElements)
                    {
                        chainCertificates.Add(DotNetUtilities.FromX509Certificate(chainElement.Certificate));
                    }

                    // Sign the PDF document
                    MakeSignature.SignDetached(appearance, signature, chainCertificates, null, null, null, 0, CryptoStandard.CADES);

                    // Close the stamper and get the signed PDF as bytes
                    stamper.Close();
                    byte[] signedPdfBytes = outputMemoryStream.ToArray();

                    return signedPdfBytes;
                }
            }
        }

        protected void verifybtn_Click(object sender, EventArgs e)
        {
            UpdateRequestStatusToPublished(Convert.ToInt32(Request.QueryString["reqID"]));
            int createdBy = GetCreatedBy(Convert.ToInt32(Request.QueryString["reqID"]));

            // notify creator that the board resolution has been published 
            Global.InsertAuditRecord(Convert.ToInt32(Request.QueryString["reqID"]), "Published Board Resolution for pass : " + Request.QueryString["reqID"], Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
            AddNotification("Board resolution has been published and waiting for completion of signatures", Convert.ToInt32(Request.QueryString["reqID"]), createdBy, Convert.ToInt32(Session["userid"]));
            int companyID = GetCompanyID(createdBy);
            NotifyAllBOD(companyID);
            String role = Session["currentRole"].ToString();
            LoadInfo();


        }
        public void UpdateRequestStatusToCompleted(int requestID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the status to 'verification' for the specified requestID
                string query = "UPDATE [Request] SET status = 'Completed' WHERE requestID = @requestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@requestID", requestID);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateRequestStatusToPublished(int requestID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the status to 'verification' for the specified requestID
                string query = "UPDATE [Request] SET status = 'Published' WHERE requestID = @requestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter
                    command.Parameters.AddWithValue("@requestID", requestID);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        protected Boolean ExecuteStoredProcedure(int companyID, int boardReID)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CheckBODAndSignatoryCounts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@companyID", companyID);
                    command.Parameters.AddWithValue("@boardReID", boardReID);

                    // Add output parameter
                    SqlParameter resultParameter = new SqlParameter("@result", SqlDbType.Bit);
                    resultParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParameter);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();

                    // Get the result from the output parameter
                    Boolean result = (Boolean)resultParameter.Value;

                    // Use the result as needed

                    return result;
                }
            }


        }

        public int GetCompanyID(int userid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the SQL query to retrieve the company ID
                string query = "SELECT [companyID] FROM [dbo].[User] WHERE [userID] = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the query
                    command.Parameters.AddWithValue("@userID", userid);

                    // Execute the query and get the result
                    object companyId = command.ExecuteScalar();

                    // Check if the result is not null
                    if (companyId != null && companyId != DBNull.Value)
                    {
                        // Return the company ID as an int
                        return Convert.ToInt32(companyId);
                    }
                }
            }

            return 0;
        }

        public void NotifyAllBOD(int companyID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the SQL query
                string query = "SELECT * FROM [dbo].[User] WHERE [companyID] = @companyID AND [position] = 'BOD'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query
                    command.Parameters.AddWithValue("@companyID", companyID);

                    // Execute the query and read the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            MailMessage message = new MailMessage();

                            // Set the sender, recipient, subject, and body of the email
                            String to = reader["email"].ToString();

                            message.From = new MailAddress("awieling0777@gmail.com");
                            message.To.Add(new MailAddress(to));
                            message.Subject = "Board Resolution Required Passed";
                            message.Body = "Dear Mr./Mrs.  " + reader["name"].ToString() + "\n" +
                                "We would like to remind sir/madam to passed a board resolution of \n" +
                                "Request ID: " + Request.QueryString["reqID"] + "\n"
                                + "Board Resolution ID: " + Request.QueryString["id"] + "\n"
                                + "Please visit the platform to view more details, Thank You";

                            // Create a new SmtpClient object
                            SmtpClient client = new SmtpClient();
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            // Configure the SMTP client with your email server settings
                            client.Host = "smtp.gmail.com";
                            client.Port = 587;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential("awieling0777@gmail.com", "vpiaoqahiewobkxu");
                            client.EnableSsl = true;

                            // Send the email message
                            client.Send(message);
                        }
                    }
                }
            }

        }

        public string GetPosition(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Execute the SQL query to retrieve the position
                string query = "SELECT [position] FROM [dbo].[User] WHERE [userID] = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter to the query
                    command.Parameters.AddWithValue("@userID", userID);

                    // Execute the query and get the result
                    object position = command.ExecuteScalar();

                    // Check if the result is not null
                    if (position != null && position != DBNull.Value)
                    {
                        // Return the position as a string
                        return position.ToString();
                    }
                }
            }
            return "";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditBoardResolutionForm.aspx?id=" + Request.QueryString["id"] + "&reqID=" + Request.QueryString["reqID"]);
        }
    }
}