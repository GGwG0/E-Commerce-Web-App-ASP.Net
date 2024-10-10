using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace FYP_WebApplication
{
    public partial class ViewRequestDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int requestID = Convert.ToInt32(Request.QueryString["id"]);

               
                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                Attachment.NavigateUrl = $"~/Download.aspx?id=" + Request.QueryString["id"];


                string status = GetRequestStatus(requestID);

                if (status != "New")
                {

                    btnBR.Visible = false;

                    if (Session["currentRole"].ToString() == "cosec user" || Session["currentRole"].ToString() == "client user")
                    {
                        btnBR.Text = "View Board Resolution";
                        btnBR.Visible = true;
                        btnBR.PostBackUrl = $"SignBoardResolution.aspx?id={RequestDashboard.FindBoardResoByRequestID(requestID)}" + "&reqID=" + requestID;
                    }
                }

                BindRequestData(requestID);
                BindComment(requestID, Convert.ToInt32(Session["userid"]));

            }
        }

        private string GetRequestStatus(int requestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [status] FROM [dbo].[Request] WHERE [requestID] = @RequestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID);

                    // Execute the query and retrieve the status
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        // Handle the case where the requestID does not exist
                        return "Request not found";
                    }
                }
            }
        }


        private void BindRequestData(int requestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Select R.*, C.username AS createdByName, U.username AS assignedToName from request R left join [User] U on R.assignedTo = U.userID LEFT JOIN [User] C ON R.createdBy = C.userID Where R.requestID = @id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", requestID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Session["createdByID"] = reader["createdBy"];
                        Session["assignedTo"] = reader["assignedTo"];
                        lblAssignedTo.Text = reader["assignedToName"].ToString();
                        lblCreatedBy.Text = reader["createdByName"].ToString();
                        DateTime createdDate = Convert.ToDateTime(reader["createdDate"]);
                        lblCreatedDate.Text = createdDate.ToString("dd MMM yyyy");
                        lblDescription.Text = reader["description"].ToString();
                        DateTime dueDate = Convert.ToDateTime(reader["dueDate"]);
                        lblDueDate.Text = dueDate.ToString("dd MMM yyyy");
                        lblStatus.Text = reader["status"].ToString();
                        lblTitle.Text = reader["title"].ToString();
                        lblType.Text = reader["type"].ToString();
                        lblTimeTaken.Text = FormatTime(createdDate);
                        lblRequestID.Text = reader["requestID"].ToString();
                        lblProgressPercent.Text = CalPercentage() + "%";
                        byte[] fileBytes = reader["attachment"] as byte[];
                        Attachment.Text = reader["filename"].ToString();
                        if (lblStatus.Text == "Completed" || lblStatus.Text == "Published")
                        {
                            ImageButton1.Visible = false;
                        }
                    }

                }

            }
            //    ProgressBar progressBar = Page.FindControl("progress") as ProgricControl;

            if (progress != null)
            {
                progress.Style["height"] = "5px";
                progress.Style["background-color"] = "#4caf50";
                progress.Style["transition"] = "width 0.6s ease";
                progress.Style["width"] = lblProgressPercent.Text;
            }
        }

        protected int CalPercentage()
        {
            string status = lblStatus.Text;
            float percentage = 0;

            switch (status.ToLower())
            {
                case "deleted":
                    percentage = 1;
                    break;
                case "new":
                    percentage = 0.25f;
                    break;
                case "verification":
                    percentage = 0.5f;
                    break;
                case "published":
                    percentage = 0.75f;
                    break;
                case "completed":
                    percentage = 1;
                    break;
                default: return Convert.ToInt32(percentage);

            }
            return Convert.ToInt32(percentage * 100.0);
        }

        protected string FormatTime(DateTime date)
        {
            TimeSpan timeDifference = DateTime.Now - date;

            if (timeDifference.TotalMinutes < 1)
            {
                return "just now";
            }
            else if (timeDifference.TotalHours < 1)
            {
                int minutesAgo = (int)timeDifference.TotalMinutes;
                return $"{minutesAgo} {(minutesAgo == 1 ? "minute" : "minutes")}";
            }
            else if (timeDifference.TotalDays < 1)
            {
                int hoursAgo = (int)timeDifference.TotalHours;
                int minutesRemaining = (int)timeDifference.TotalMinutes % 60;

                if (minutesRemaining > 0)
                {
                    return $"{hoursAgo} {(hoursAgo == 1 ? "hour" : "hours")} {minutesRemaining} {(minutesRemaining == 1 ? "minute" : "minutes")}";
                }
                else
                {
                    return $"{hoursAgo} {(hoursAgo == 1 ? "hour" : "hours")}";
                }
            }
            else
            {
                int daysAgo = (int)timeDifference.TotalDays;
                int hoursRemaining = (int)timeDifference.TotalHours % 24;

                if (hoursRemaining > 0)
                {
                    return $"{daysAgo} {(daysAgo == 1 ? "day" : "days")} {hoursRemaining} {(hoursRemaining == 1 ? "hour" : "hours")}";
                }
                else
                {
                    return $"{daysAgo} {(daysAgo == 1 ? "day" : "days")}";
                }
            }
        }

        private void BindComment(int requestID, int userid)
        {
            string currentUsername = GetCurrentLoginUsername(userid);
            Session["currentUsername"] = currentUsername;

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Select C.*, U.profilePicture, U.username from Comment C left join [User] U ON C.userID = U.userID where requestID = @requestID Order By writtenDate Asc;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@requestID", requestID);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                DataColumn formattedDate = new DataColumn("FormattedDate", typeof(string));
                DataColumn commentUserId = new DataColumn("commentUserId", typeof(string));
                dataTable.Columns.Add(base64Column);
                dataTable.Columns.Add(formattedDate);

                foreach (DataRow row in dataTable.Rows)
                {
                    byte[] imageBytes = row["profilePicture"] as byte[];

                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        string base64String = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                        row["Base64ProfilePicture"] = base64String;
                    }
                    else
                    {
                        row["Base64ProfilePicture"] = "~/assets/image/profile/default.jpg";

                    }

                    DateTime writtenDate = (DateTime)row["writtenDate"];
                    row["FormattedDate"] = FormatTimeAgo(writtenDate);

                }

                RepeaterMessages.DataSource = dataTable.DefaultView;
                RepeaterMessages.DataBind();
            }
        }
        protected string FormatTimeAgo(DateTime performedDate)
        {
            TimeSpan timeDifference = DateTime.Now - performedDate;

            if (timeDifference.TotalMinutes < 1)
            {
                return "just now";
            }
            else if (timeDifference.TotalHours < 1)
            {
                int minutesAgo = (int)timeDifference.TotalMinutes;
                return $"{minutesAgo} {(minutesAgo == 1 ? "minute" : "minutes")} ago";
            }
            else if (timeDifference.TotalDays < 1)
            {
                int hoursAgo = (int)timeDifference.TotalHours;
                return $"{hoursAgo} {(hoursAgo == 1 ? "hour" : "hours")} ago";
            }
            else if (timeDifference.TotalDays <= 29)
            {
                int daysAgo = (int)timeDifference.TotalDays;
                return $"{daysAgo} {(daysAgo == 1 ? "day" : "days")} ago";
            }
            else
            {
                return performedDate.ToString("dd MMM yyyy");
            }
        }

        //class='<%# Eval("PersonName") == "awie"?"profile-img-cont ongoing" : "profile-img-cont incoming" %>'
        private string GetCurrentLoginUsername(int userId)
        {
            string username = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Select username from [User] where userID=@userId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);
                connection.Open();

                username = command.ExecuteScalar().ToString();


            }
            return username;

        }

        protected void RepeaterMessages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = e.Item.DataItem as DataRowView;

                if (row != null)
                {

                    string currentUser = Session["currentUsername"]?.ToString();
                    string commenter = row["username"].ToString();

                    HtmlGenericControl messageStyle = e.Item.FindControl("messageStyle") as HtmlGenericControl;
                    HtmlGenericControl profileContent = e.Item.FindControl("profileContent") as HtmlGenericControl;

                    // if currentuser = username, right side

                    if (currentUser == commenter)
                    {
                        messageStyle.Attributes["class"] = "profile-img-cont incoming"; //left

                    }
                    else
                    {

                        messageStyle.Attributes["class"] += "profile-img-cont ongoing";//right
                        profileContent.Attributes["style"] += "margin-right:10px;";
                    }
                }
            }
        }


        protected void btnSend_Click(object sender, ImageClickEventArgs e)
        {

            int userid = Convert.ToInt32(Session["userid"]);
            int requestID = Convert.ToInt32(Request.QueryString["id"]);
            int to = 0;
            int from = 0;

            if (Session["currentRole"].ToString() == "cosec user")
            {
                to = Convert.ToInt32(Session["createdByID"]);
                from = Convert.ToInt32(Session["assignedTo"]);
            }
            else if (Session["currentRole"].ToString() == "client user")
            {
                to = Convert.ToInt32(Session["assignedTo"]);
                from = Convert.ToInt32(Session["createdByID"]);
            }


            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Insert into Comment(description, userID, requestID, writtenDate) values (@description, @userID, @requestID, @writtenDate);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@description", txtMessage.Text);
                command.Parameters.AddWithValue("@userID", userid);
                command.Parameters.AddWithValue("@requestID", requestID);
                command.Parameters.AddWithValue("@writtenDate", DateTime.Now);
                connection.Open();

                int sucess = command.ExecuteNonQuery();

                if (sucess > 0)
                {
                    SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
                    command2.Parameters.AddWithValue("@action", "send a comment");
                    command2.Parameters.AddWithValue("@to", to);
                    command2.Parameters.AddWithValue("@from", from);
                    command2.Parameters.AddWithValue("@requestID", requestID);
                    command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
                    int notificationSucess = command2.ExecuteNonQuery();

                    if (notificationSucess > 0)
                    {
                        txtMessage.Text = "";
                        BindComment(requestID, userid);
                    }


                }

            }
        }

        public static int FindBoardResoByRequestID(int requestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command2 = new SqlCommand("Select boardReID from BoardResolution where requestID = @id;", connection);
                command2.Parameters.AddWithValue("@id", requestID);
                connection.Open();
                int boardID = Convert.ToInt32(command2.ExecuteScalar());
                return boardID;
            }

        }
        protected void DeleteRecord(int requestID, string status)
        {
            int boardID = FindBoardResoByRequestID(requestID);

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (status == "Verification")
                {
                    SqlCommand command2 = new SqlCommand("Delete from BoardResolutionField where boardReID = @id; ", connection);
                    command2.Parameters.AddWithValue("@id", boardID);


                    int success = command2.ExecuteNonQuery();

                    if (success > 0)
                    {
                        SqlCommand command3 = new SqlCommand("Delete from BoardResolution where boardReID = @id; ", connection);
                        command3.Parameters.AddWithValue("@id", boardID);

                        success = command3.ExecuteNonQuery();
                    }
                }


                SqlCommand command = new SqlCommand("Update request set status = @status where requestID = @requestID", connection);
                command.Parameters.AddWithValue("@status", "Deleted");
                command.Parameters.AddWithValue("@requestID", requestID);


                int success2 = command.ExecuteNonQuery();

                String deleteNotification = "deleted a request";

                string role = Session["currentRole"].ToString();
                int to = 0;
                int from = 0;
                if (role == "client user")
                {
                    to = Convert.ToInt32(Session["cosecId"]);
                    from = Convert.ToInt32(Session["userid"]);
                }
                else
                {
                    to = Convert.ToInt32(Session["userid"]);
                    from = Convert.ToInt32(Session["cosecId"]);
                }



                if (success2 > 0)
                {
                    SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
                    command2.Parameters.AddWithValue("@action", deleteNotification);
                    command2.Parameters.AddWithValue("@to", Convert.ToInt32(Session["cosecId"]));
                    command2.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
                    command2.Parameters.AddWithValue("@requestID", requestID);
                    command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
                    int notificationSucess = command2.ExecuteNonQuery();

                    if (notificationSucess > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Successfully deleted the request.\");", true);
                        Response.Redirect("RequestDashboard.aspx");
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Failed to delete the request. Please try again later.\");", true);

                }

            }
        }

        protected void ImageButton1_Click1(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Completed" || lblStatus.Text == "Published")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Cannot delete this item because it's published/ completed.\");", true);
            }
            else
            {
                DeleteRecord(Convert.ToInt32(lblRequestID.Text), lblStatus.Text);
            }
        }

        protected void btnBR_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectTemplate.aspx?reqID=" + Request.QueryString["id"]);
        }

        protected void Editbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditRequest.aspx?reqID=" + Request.QueryString["id"]);
        }
    }
}