using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;
using System.Data.SqlTypes;
using Org.BouncyCastle.Ocsp;

namespace FYP_WebApplication
{
    public partial class RequestDashboard : System.Web.UI.Page
    {
        protected List<int> selectedRows = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


               
                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }


                
                CheckOverdueRequest();
                BindGridView();
                LoadNotification();
                LoadRequestReport();
            }
        }

        protected void CheckOverdueRequest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "UPDATE [dbo].[Request] SET [status] = 'incomplete' WHERE ([status] <> 'completed' AND [status] <> 'deleted') AND CAST(GETDATE() AS DATE) > CAST([dueDate] AS DATE);\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

            }
        }


        private void LoadRequestReport()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = @"
            WITH StatusList AS (
                SELECT 'Completed' AS [status]
                UNION ALL
                SELECT 'Ongoing' -- Combined 'Published' and 'Verification'
                UNION ALL
                SELECT 'New'
                UNION ALL
                SELECT 'Incomplete'
            ),

            ThisWeek AS (
                SELECT
                    CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END AS [status],
                    COUNT(*) AS RequestCount
                FROM
                    [dbo].[Request]
                WHERE
                    [type] <> 'T' AND
                    ([createdBy] = @userid OR [assignedTo] = @userid) AND
                    [createdDate] >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) -- Beginning of this week
                    AND [createdDate] < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) -- Beginning of next week
                GROUP BY
                    CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END
            ),
            LastWeek AS (
                SELECT
                    CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END AS [status],
                    COUNT(*) AS RequestCountLastWeek
                FROM
                    [dbo].[Request]
                WHERE
                    [type] <> 'T' AND
                   ( [createdBy] = @userid OR [assignedTo] = @userid) AND
                    [createdDate] >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0) -- Beginning of last week
                    AND [createdDate] < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) -- Beginning of this week
                GROUP BY
                    CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END
            )


            SELECT
                sl.[status],
                COALESCE(tw.RequestCount, 0) AS RequestCount,
                COALESCE(lw.RequestCountLastWeek, 0) AS RequestCountLastWeek,
                IIF(
                    COALESCE(lw.RequestCountLastWeek, 0) = 0 AND COALESCE(tw.RequestCount, 0) = 0,
                    0,
                    IIF(
                        COALESCE(lw.RequestCountLastWeek, 0) = 0,
                        100,
                        (COALESCE(tw.RequestCount, 0) - COALESCE(lw.RequestCountLastWeek, 0)) * 100.0 / 
                        (COALESCE(lw.RequestCountLastWeek, 0) + COALESCE(tw.RequestCount, 0))
                    )
                ) AS PercentageChange
            FROM
                StatusList sl 
            LEFT JOIN
                ThisWeek tw ON sl.[status] = tw.[status]
            LEFT JOIN
                LastWeek lw ON sl.[status] = lw.[status]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int userId = Convert.ToInt32(Session["userid"]);

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@userid", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string status = reader["status"].ToString();
                        int percent = 0;
                        percent = (int)Math.Round(Convert.ToDouble(reader["PercentageChange"]));
                        switch (status.ToLower())
                        {
                            case "completed":
                                {
                                    lblCompletedNum.InnerText = reader["RequestCount"].ToString();
                                    lblCompletedPercent.InnerText = ' ' + percent.ToString() + '%';
                                    if (percent >= 0)
                                    {
                                        lblCompletedPercent.Attributes["class"] += " fa fa-level-up";
                                        lblCompletedPercent.Style["background-color"] = "#dcebdc;";
                                        lblCompletedPercent.Style["color"] = "green;";
                                    }
                                    else
                                    {
                                        percent = -percent;
                                        lblCompletedPercent.Attributes["class"] += " fa fa-level-down";
                                        lblCompletedPercent.Style["background-color"] = "#fde8e8;";
                                        lblCompletedPercent.Style["color"] = "red;";
                                    }
                                }
                                break;
                            case "ongoing":
                                {
                                    lblVerification.InnerText = reader["RequestCount"].ToString();
                                    lblVerificationPercent.InnerText = ' ' + percent.ToString() + '%';
                                    if (percent >= 0)
                                    {
                                        lblVerificationPercent.Attributes["class"] += " fa fa-level-up";
                                        lblVerificationPercent.Style["background-color"] = "#dcebdc;";
                                        lblVerificationPercent.Style["color"] = "green;";
                                    }
                                    else
                                    {
                                        percent = -percent;
                                        lblVerificationPercent.Attributes["class"] += " fa fa-level-down";
                                        lblVerificationPercent.Style["background-color"] = "#fde8e8;";
                                        lblVerificationPercent.Style["color"] = "red;";
                                    }
                                }
                                break;
                            case "new":
                                {
                                    lblNew.InnerText = reader["RequestCount"].ToString();
                                    lblNewPercent.InnerText = ' ' + percent.ToString() + '%';
                                    if (percent >= 0)
                                    {
                                        lblNewPercent.Attributes["class"] += " fa fa-level-up";
                                        lblNewPercent.Style["background-color"] = "#dcebdc;";
                                        lblNewPercent.Style["color"] = "green;";
                                    }
                                    else
                                    {

                                        percent = -percent;
                                        lblNewPercent.Attributes["class"] += " fa fa-level-down";
                                        lblNewPercent.Style["background-color"] = "#fde8e8;";
                                        lblNewPercent.Style["color"] = "red;";
                                    }
                                }
                                break;
                            case "incomplete":
                                {
                                    lblIncomplete.InnerText = reader["RequestCount"].ToString();
                                    lblIncompletePercent.InnerText = ' ' + percent.ToString() + '%';
                                    if (percent >= 0)
                                    {
                                        lblIncompletePercent.Attributes["class"] += " fa fa-level-up";
                                        lblIncompletePercent.Style["background-color"] = "#dcebdc;";
                                        lblIncompletePercent.Style["color"] = "green;";
                                    }
                                    else
                                    {
                                        percent = -percent;
                                        lblIncompletePercent.Attributes["class"] += " fa fa-level-down";
                                        lblIncompletePercent.Style["background-color"] = "#fde8e8;";
                                        lblIncompletePercent.Style["color"] = "red;";
                                    }
                                }
                                break;

                        }


                    }
                }

            }
        }

        protected void LoadNotification()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int userId = Convert.ToInt32(Session["userid"]);

                SqlCommand command = new SqlCommand("select TOP 5 * from notification  left join [User] on \"from\" = userID where \"to\" = @userId ORDER BY performedDate DESC; ", connection);
                command.Parameters.AddWithValue("@userId", userId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                DataColumn formattedDate = new DataColumn("FormattedDate", typeof(string));
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
                        row["Base64ProfilePicture"] = "assets/image/profile/default.jpg";
                    }
                    DateTime performedDate = (DateTime)row["performedDate"];
                    row["FormattedDate"] = FormatTimeAgo(performedDate);
                }

                Repeater1.DataSource = dataTable.DefaultView;
                Repeater1.DataBind();
            }
        }

        public static string GetRole(int userid)
        {
            String roleName = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("select \r\nR.roleName from [User] U  \r\nleft join \r\n\t[User_Role] UR \r\n\ton U.userID = UR.userID \r\nleft join \r\n\t[Role] R \r\n\ton UR.roleID = R.roleID \r\nwhere U.userID = @userId;", connection);
                command.Parameters.AddWithValue("@userId", userid);
                connection.Open();
                roleName = command.ExecuteScalar().ToString();

            }
            return roleName;

        }
        private void BindGridView()
        {

            DataTable table = GetDataFromTable();

            GridView1.DataSource = table;
            GridView1.DataBind();
            Label lblNoRecords = GridView1.Controls.Count > 0 ? (Label)GridView1.Controls[0].Controls[0].FindControl("lblNoRecords") : null;

            if (lblNoRecords != null)
            {
                lblNoRecords.Visible = table.Rows.Count == 0;
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                string commandArgument = e.CommandArgument.ToString();
                string[] arguments = commandArgument.Split(',');

                int requestID = Convert.ToInt32(arguments[0]);
                string status = arguments[1];

                // Delete Record

                // client side role
                if (status == "Completed" || status == "Published")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Cannot delete this item because it's published/ completed.\");", true);
                }
                else
                {
                    DeleteRecord(requestID, status);
                    
                }

            }
            else if (e.CommandName == "ViewItem")
            {
                string requestID = e.CommandArgument.ToString();
                Response.Redirect($"ViewRequestDetail.aspx?id={requestID}");
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
                        BindGridView();
                        LoadNotification();
                        LoadRequestReport();
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Failed to delete the request. Please try again later.\");", true);

                }

            }
        }

        protected DataTable GetDataFromTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = null;
            string roleName = Session["currentRole"].ToString();

            if (roleName == "client user")
            {
                ddlStatus.Items.Remove("Deleted");
                ddlStatus.DataBind();
                selectQuery = "SELECT R.*,C.companyID, C.comName FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID WHERE R.status <> 'deleted' and R.type <> 'T' and (R.createdBy = @id or R.assignedTo = @id) ORDER BY R.createdDate Desc;";

            }
            else if (roleName == "cosec user")
            {
                // current account is cosec 
                // check request's createdby -> company -> cosecId = cosecId;

                selectQuery = "SELECT R.*,C.companyID, C.comName FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID  WHERE R.type <> 'T' and (R.assignedTo =@id or R.createdBy = @id) ORDER BY R.createdDate Desc";

            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {


                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                if (Session["currentRole"].ToString() != "cosec user")
                {
                    e.Row.Cells[7].Visible = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Check the role and hide the column in data rows.
                if (Session["currentRole"].ToString() != "cosec user")
                {
                    e.Row.Cells[7].Visible = false;
                }
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");

                if (Session["currentRole"].ToString() != "client user")
                {
                    btnDelete.Visible = false;


                }

                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                int requestID = Convert.ToInt32(row["requestID"]);
                if (CheckRequestStatusCompleted(requestID))
                {
                    btnDelete.Visible = false;
                }

            }
        }

        protected Boolean CheckRequestStatusCompleted(int requestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string status = "";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT status FROM [Request] WHERE requestID = @requestID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@requestID", requestID);

                object result = command.ExecuteScalar();

                // Check if the result is not null
                if (result != null && result != DBNull.Value)
                {
                    status = result.ToString();
                    if (status == "Completed")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string roleName = Session["currentRole"].ToString();
            string selectQuery = null;

            if (roleName == "client user")
            {
                selectQuery = "SELECT  R.*,C.companyID, C.comName  FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID  WHERE R.status <> 'deleted' and R.type <> 'T' and (R.createdBy = @id or R.assignedTo = @id) and (R.requestID LIKE @searchTerm  OR R.title LIKE @searchTerm OR R.type LIKE @searchTerm  OR R.status LIKE @searchTerm  OR R.description LIKE @searchTerm OR  CONVERT(VARCHAR, R.createdDate, 23) LIKE @searchTerm OR  CONVERT(VARCHAR, R.dueDate, 23) LIKE @searchTerm )  ORDER BY R.createdDate Desc;";

            }
            else if (roleName == "cosec user")
            {

                selectQuery = "SELECT  R.*,C.companyID, C.comName  FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID WHERE R.type <> 'T' and (R.assignedTo =@id or R.createdBy = @id) and (R.requestID LIKE @searchTerm  OR R.title LIKE @searchTerm OR R.type LIKE @searchTerm  OR R.status LIKE @searchTerm  OR R.description LIKE @searchTerm OR  CONVERT(VARCHAR, R.createdDate, 23) LIKE @searchTerm OR  CONVERT(VARCHAR, R.dueDate, 23) LIKE @searchTerm ) ORDER BY R.createdDate Desc";
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);


                    GridView1.DataSource = table;
                    GridView1.DataBind();

                    Label lblNoRecords = GridView1.Controls.Count > 0 ? (Label)GridView1.Controls[0].Controls[0].FindControl("lblNoRecords") : null;
                    if (lblNoRecords != null)
                    {
                        lblNoRecords.Visible = table.Rows.Count == 0;
                    }


                }
            }

        }

        protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlStatus.SelectedValue;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string roleName = Session["currentRole"].ToString();
                string query = null;

                if (roleName == "client user")
                {
                    if (selectedValue == "All")
                    {
                        // Select all except deleted for client user
                        query = "SELECT R.*,C.companyID, C.comName FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID WHERE R.status <> 'deleted' and R.type <> 'T' and (R.createdBy = @id or R.assignedTo = @id) ORDER BY R.createdDate Desc;";
                    }
                    else
                    {
                        // Select based on the chosen status for client user
                        query = "Select R.*,C.companyID, C.comName FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID WHERE R.status <> 'deleted' and R.type <> 'T' and R.status = @status and (R.createdBy = @id or R.assignedTo = @id) ORDER BY R.createdDate Desc ;";
                    }
                }
                else if (roleName == "cosec user")
                {
                    // Select all for cosec user
                    if (selectedValue == "All")
                    {
                        // Select all except deleted for client user
                        query = "SELECT R.*,C.companyID, C.comName FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID  WHERE R.type <> 'T' and (R.assignedTo =@id or R.createdBy = @id) ORDER BY R.createdDate Desc";


                    }
                    else
                    {
                        // Select based on the chosen status for client user
                        query = "SELECT R.*,C.companyID, C.comName FROM request R LEFT JOIN [User] U ON R.createdBy = U.userID LEFT JOIN Company C ON U.companyID = C.companyID  WHERE R.type <> 'T' and R.status =@status and (R.assignedTo =@id or R.createdBy = @id) ORDER BY R.createdDate Desc";


                    }

                }
                else
                {
                    Response.Redirect("ErrorPage.aspx");
                    return;
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selectedValue != "All")
                    {
                        command.Parameters.AddWithValue("@status", selectedValue);
                    }
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    GridView1.DataSource = table;
                    GridView1.DataBind();
                    Label lblNoRecords = GridView1.Controls.Count > 0 ? (Label)GridView1.Controls[0].Controls[0].FindControl("lblNoRecords") : null;

                    if (lblNoRecords != null)
                    {
                        lblNoRecords.Visible = table.Rows.Count == 0;
                    }
                }
            }
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the data source
            DataTable dataTable = GetDataFromTable(); // Replace with your actual data retrieval method

            if (dataTable != null)
            {
                // Sort the data table based on the column clicked by the user
                dataTable.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                // Rebind the GridView with the sorted data
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted
            string previousColumn = ViewState["SortExpression"] as string;

            if (previousColumn != null)
            {
                // If the same column is clicked, toggle the sort direction
                if (previousColumn == column)
                {
                    string previousDirection = ViewState["SortDirection"] as string;
                    sortDirection = (previousDirection != null && previousDirection == "ASC") ? "DESC" : "ASC";
                }
            }

            // Save new values in ViewState
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}