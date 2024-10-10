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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Session["currentRole"].ToString() == "client user")
                {
                    createdDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    lblCreatedBy.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["userid"]));
                    lblAssignedTo.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["cosecId"]));
                    BindRequests();
                    cosecAssignLabel.Visible = false;
              
                    ddlAssigner.Visible = false;

                }
                else
                {
                    createdDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    lblCreatedBy.Text = Global.GetUsernameByUserId(Convert.ToInt32(Session["userid"]));
                    lblAssignedTo.Text = "Haven't Assign Yet.";
                    BindAssigneeDropDownList(Convert.ToInt32(Session["userid"]));
                    BindRequests();
                }
               
                if (Session["currentRole"].ToString() == "cosec user" )
                {
                    // Find the ListItem with the specified value
                    ListItem itemToRemove = ddlRequests.Items.FindByText("Board Resolution Request");

                    // If the item is found, remove it
                    if (itemToRemove != null)
                    {
                        ddlRequests.Items.Remove(itemToRemove);
                    }

                  
                    
                }
                ddlRequests.SelectedIndex = 0;
                requestTypeChoice.Text = ddlRequests.SelectedItem.Text;

                if(ddlRequests.SelectedItem.Text == "Request for Document")
                {
                    lblAttachment.Visible = false;
                    fileUploadAttachment.Visible = false;
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

        protected void BindAssigneeDropDownList(int userId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string combinedQuery = @"
                SELECT U.userID, U.username
                FROM [User] U
                INNER JOIN Company C ON U.companyID = C.companyID
                WHERE C.cosecId = @userId AND U.position != 'client admin'
            ";

                    using (SqlCommand command = new SqlCommand(combinedQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int userIdResult = reader.GetInt32(reader.GetOrdinal("userID"));
                                string username = reader.GetString(reader.GetOrdinal("username"));

                                // Create a ListItem and add it to the DropDownList
                                ListItem listItem = new ListItem(username, userIdResult.ToString());
                                ddlAssigner.Items.Add(listItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
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
              //  ddlRequests.Items.Add(new ListItem("Haven't Selected", "1"));
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
               
                int cosecClientID = Convert.ToInt32(Session["cosecId"]);
                int newRequestId = 0;

                


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                string query = null;
                    if (fileUploadAttachment.Visible == false)
                {
                    query = "INSERT INTO [Request] (type, title, description, assignedTo, status, createdDate, createdBy, dueDate) " +
                  "VALUES (@type, @title, @description, @assignedTo, @status, @createdDate, @createdBy, @dueDate); SELECT SCOPE_IDENTITY();";

                }
                else
                {

                    query = "INSERT INTO [Request] (type, title, description, attachment, assignedTo, status, createdDate, createdBy, dueDate, filename) " +
                    "VALUES (@type, @title, @description, @attachment, @assignedTo, @status, @createdDate, @createdBy, @dueDate, @filename); SELECT SCOPE_IDENTITY();";

                }

                using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        command.Parameters.AddWithValue("@type", ddlRequests.SelectedItem.Text);
                        command.Parameters.AddWithValue("@title", TextBox1.Text);
                        command.Parameters.AddWithValue("@description", TextBox2.Text);

                    if (fileUploadAttachment.Visible == true)
                    {
                        byte[] fileBytes = fileUploadAttachment.FileBytes;
                        string fileName = Path.GetFileName(fileUploadAttachment.PostedFile.FileName);
                        command.Parameters.AddWithValue("@attachment", fileBytes);
                        command.Parameters.AddWithValue("@filename", fileName);

                    }


                    if (Session["currentRole"].ToString() == "client user")
                        {
                            command.Parameters.AddWithValue("@assignedTo", Convert.ToInt32(Session["cosecId"]));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@assignedTo", Convert.ToInt32(ddlAssigner.SelectedValue));
                        }


                        command.Parameters.AddWithValue("@status", "New");
                        command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                        command.Parameters.AddWithValue("@createdBy", Convert.ToInt32(Session["userid"]));
                        command.Parameters.AddWithValue("@dueDate", txtDate.Text);
                  


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
            if (Session["currentRole"].ToString() == "client user")
            {
                command2.Parameters.AddWithValue("@to", Convert.ToInt32(Session["cosecId"]));
                command2.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
            }
            else
            {
                command2.Parameters.AddWithValue("@to", Convert.ToInt32(ddlAssigner.SelectedValue));
                command2.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
            }
          
           
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

        protected void Unnamed_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAssignedTo.Text= ddlAssigner.SelectedItem.Text;
        }
    }
}