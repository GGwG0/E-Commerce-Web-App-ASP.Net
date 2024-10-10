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

            
                lblCreatedBy.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["createdByID"]));
                lblAssignedTo.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["assignedTo"]));
                

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
                    requestTypeChoice.Text = row["type"].ToString();
                }


            }

        }
        protected void ValidateDueDate(object source, ServerValidateEventArgs args)
        {
            DateTime dueDate;

            if (DateTime.TryParse(txtDate.Text, out dueDate))
            {
                // Check if the due date is less than today
                args.IsValid = dueDate >= DateTime.Today;
            }
            else
            {
                // If the date entered is not a valid date, consider it invalid
                args.IsValid = false;
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

                string query = "SELECT title, description, dueDate, createdDate, type FROM [dbo].[Request] WHERE requestID = @RequestId";

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
                               "SET  title = @title, description = @description, " +
                               "attachment = @attachment, dueDate = @dueDate, filename = @filename " +
                               "WHERE requestID = @RequestId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestId", requestId);
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
            int notifiedSuccess = AddNotification("edited a request", Convert.ToInt32(Request.QueryString["reqID"]));

            Response.Redirect("ViewRequestDetail.aspx?id=" + Request.QueryString["reqID"]);
        }



        protected int AddNotification(string action, int requestID)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
                command2.Parameters.AddWithValue("@action", action);
                command2.Parameters.AddWithValue("@to", Convert.ToInt32(Global.GetUserIDByUsername(lblCreatedBy.Text)));
                command2.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
                command2.Parameters.AddWithValue("@requestID", requestID);
                command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
                int notificationSucess = command2.ExecuteNonQuery();
                return notificationSucess;
            }
           
        }



        protected void editbtn_Click(object sender, EventArgs e)
        {
            UpdateRequest(Convert.ToInt32(Request.QueryString["reqID"]));
           

        }


        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewRequestDetail.aspx?id=" + Request.QueryString["reqID"]);
        }
    }
}