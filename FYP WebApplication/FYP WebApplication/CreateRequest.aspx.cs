using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Kernel.Pdf;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FYP_WebApplication
{
    public partial class CreateRequest : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                if (Session["cosecId"] == null)
                {
                    Session["cosecId"] = TwoFactorAuthentication.GetCosecIdFromCompany(Convert.ToInt32(Session["userid"]));
                }

                createdDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                lblCreatedBy.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["userid"]));
                lblAssignedTo.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["cosecId"]));
                BindRequests();

            }

        }

        private void BindRequests()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve data where type is 'T'
                string query = "SELECT requestID, title FROM [dbo].[Request] WHERE type = 'T'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Bind data to the DropDownList
                        ddlRequests.DataSource = reader;
                        ddlRequests.DataTextField = "title";
                        ddlRequests.DataValueField = "title";
                        ddlRequests.DataBind();
                    }
                }
            }
        }




        public void createNewRequest()
        {
            byte[] fileBytes = fileUploadAttachment.FileBytes;
            int cosecClientID = Convert.ToInt32(Session["cosecId"]);
            int newRequestId = 0;
            string fileName = Path.GetFileName(fileUploadAttachment.PostedFile.FileName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [Request] (type, title, description, attachment, assignedTo, status, createdDate, createdBy, dueDate, filename) " +
                "VALUES (@type, @title, @description, @attachment, @assignedTo, @status, @createdDate, @createdBy, @dueDate, @filename); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@type", ddlRequests.SelectedItem.Text);
                    command.Parameters.AddWithValue("@title", TextBox1.Text);
                    command.Parameters.AddWithValue("@description", TextBox2.Text);
                    command.Parameters.AddWithValue("@attachment", fileBytes);
                    command.Parameters.AddWithValue("@assignedTo", cosecClientID);
                    command.Parameters.AddWithValue("@status", "New");
                    command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                    command.Parameters.AddWithValue("@createdBy", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@dueDate", txtDate.Text);
                    command.Parameters.AddWithValue("@filename", fileName);


                    newRequestId = Convert.ToInt32(command.ExecuteScalar());

                    if (newRequestId != 0)
                    {
                        int notifiedSuccess = AddNotification("created a request", newRequestId, connection);
                        if (notifiedSuccess > 0)
                        {
                            Global.InsertAuditRecord(newRequestId, "Created a New Request: " + newRequestId, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

                            ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Successfully create request.\");", true);
                            Response.Redirect($"ViewRequestDetail.aspx?id={newRequestId}");

                        }
                    }
                }
            }
        }

        protected int AddNotification(string action, int requestID, SqlConnection connection)
        {


            SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
            command2.Parameters.AddWithValue("@action", action);
            command2.Parameters.AddWithValue("@to", Convert.ToInt32(Session["cosecId"]));
            command2.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
            command2.Parameters.AddWithValue("@requestID", requestID);
            command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
            int notificationSucess = command2.ExecuteNonQuery();

            return notificationSucess;
        }


        protected void ddlRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            requestTypeChoice.Text = ddlRequests.SelectedItem.Text;

        }


        protected void addbtn_Click1(object sender, EventArgs e)
        {
            createNewRequest();



        }


        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestDashboard.aspx");
        }
    }
}