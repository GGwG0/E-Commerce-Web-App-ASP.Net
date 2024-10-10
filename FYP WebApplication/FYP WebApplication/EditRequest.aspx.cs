using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{

    public partial class EditRequest : System.Web.UI.Page
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

            
                lblCreatedBy.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["userid"]));
                lblAssignedTo.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["cosecId"]));
                BindRequests();

                // Fetch data from the database based on the request ID
                DataTable dt = GetRequestData(Convert.ToInt32(Request.QueryString["reqID"]));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Bind data to controls
                    TextBox1.Text = row["title"].ToString();
                    TextBox2.Text = row["description"].ToString();
                    txtDate.Text = row["dueDate"] is DBNull ? string.Empty : ((DateTime)row["dueDate"]).ToString("yyyy-MM-dd");
                    createdDate.Text = row["createdDate"] is DBNull ? string.Empty : ((DateTime)row["createdDate"]).ToString("yyyy-MM-dd");
                }


            }

        }

        private DataTable GetRequestData(int requestId)
        {
            DataTable dt = new DataTable();

            // Use your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT title, description, dueDate, createdDate FROM [dbo].[Request] WHERE requestID = @RequestId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void BindRequests()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve data where type is 'T'
                string query = "SELECT requestID, title, createdDate FROM [dbo].[Request] WHERE type = 'T'";

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




     
        private void UpdateRequest(int requestId)
        {
            byte[] fileBytes = fileUploadAttachment.FileBytes;
            string fileName = Path.GetFileName(fileUploadAttachment.PostedFile.FileName);
            // Use your connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE [dbo].[Request] " +
                               "SET type = @type, title = @title, description = @description, " +
                               "attachment = @attachment, dueDate = @dueDate, filename = @filename " +
                               "WHERE requestID = @RequestId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestId", requestId);
                    command.Parameters.AddWithValue("@type", ddlRequests.SelectedItem.Text);
                    command.Parameters.AddWithValue("@title", TextBox1.Text);
                    command.Parameters.AddWithValue("@description", TextBox2.Text);
                    command.Parameters.AddWithValue("@attachment", fileBytes);
                    command.Parameters.AddWithValue("@dueDate", txtDate.Text);
                    command.Parameters.AddWithValue("@filename", fileName);

                    // Execute the update command
                    command.ExecuteNonQuery();
                }
                Global.InsertAuditRecord(requestId, "Edited Request Details : " + requestId, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
            }
        }



        protected int AddNotification(string action, int requestID)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
                command2.Parameters.AddWithValue("@action", action);
                command2.Parameters.AddWithValue("@to", Convert.ToInt32(Session["cosecId"]));
                command2.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
                command2.Parameters.AddWithValue("@requestID", requestID);
                command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
                int notificationSucess = command2.ExecuteNonQuery();
                return notificationSucess;
            }
           
        }


        protected void ddlRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            requestTypeChoice.Text = ddlRequests.SelectedItem.Text;

        }


        protected void editbtn_Click(object sender, EventArgs e)
        {
            UpdateRequest(Convert.ToInt32(Request.QueryString["reqID"]));
            int notifiedSuccess = AddNotification("edited a request", Convert.ToInt32(Request.QueryString["reqID"]));


        }


        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewRequestDetail.aspx?id=" + Request.QueryString["reqID"]);
        }
    }
}