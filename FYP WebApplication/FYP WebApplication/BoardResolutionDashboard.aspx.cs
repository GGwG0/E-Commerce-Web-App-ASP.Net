using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FYP_WebApplication
{
    public partial class BoardResolutionDashboard : System.Web.UI.Page
    {
        protected List<int> selectedRows = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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
                BindGridView();
            }
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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");

                if (Session["currentRole"].ToString() != "client user")
                {
                    btnDelete.Visible = false;
                }
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandArgument = e.CommandArgument.ToString();
            string[] arguments = commandArgument.Split(',');


            if (e.CommandName == "DeleteItem")
            {
                int boardID = Convert.ToInt32(arguments[0]);
                int requestID = Convert.ToInt32(arguments[1]);
                DeleteRecord(boardID, requestID);

            }
            else if (e.CommandName == "ViewItem")
            {
                int boardID = Convert.ToInt32(arguments[0]);
                int requestID = Convert.ToInt32(arguments[1]);
                Response.Redirect($"SignBoardResolution.aspx?id={boardID}&reqID={requestID}");
            }
        }
        protected DataTable GetDataFromTable()
        {
            string query = null;

            if (Session["currentRole"].ToString() == "client user")
            {
                ddlStatus.Items.Remove("Deleted");
                ddlStatus.DataBind();
                query = "SELECT * FROM BoardResolution WHERE type = 'Board Resolution Request'";

            }
            else if (Session["currentRole"].ToString() == "cosec user")
            {
                query = "SELECT * FROM BoardResolution B LEFT JOIN Request R ON B.requestID = R.requestID WHERE B.type = 'Board Resolution Request' and R.assignedTo = @id;";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        protected void DeleteRecord(int boardID, int requestID)
        {
            string query = "Select R.status from BoardResolution B left join Request R on B.requestID = R.requestID where boardReID = @id";

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", boardID);

                connection.Open();
                string status = command.ExecuteScalar().ToString();

                if (status.ToLower() != "completed" && status.ToLower() != "published")
                {
                    SqlCommand command2 = new SqlCommand("Delete from BoardResolutionField where boardReID = @id", connection);
                    command2.Parameters.AddWithValue("@id", boardID);

                    int success = command2.ExecuteNonQuery();
                    SqlCommand command3 = new SqlCommand("Delete from BoardResolution where boardReID = @id", connection);
                    command3.Parameters.AddWithValue("@id", boardID);

                    success = command3.ExecuteNonQuery();

                    if (success > 0)
                    {
                        String deleteNotification = "Deleted a board resolution (id: " + boardID + ")";

                        int cosecId = FindCurrentCosecUser(Convert.ToInt32(Session["userid"]));

                        if (success > 0)
                        {
                            SqlCommand command4 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
                            command4.Parameters.AddWithValue("@action", deleteNotification);
                            command4.Parameters.AddWithValue("@to", cosecId);
                            command4.Parameters.AddWithValue("@from", Convert.ToInt32(Session["userid"]));
                            command4.Parameters.AddWithValue("@requestID", requestID);
                            command4.Parameters.AddWithValue("@performedDate", DateTime.Now);

                            int notificationSucess = command4.ExecuteNonQuery();

                            if (notificationSucess > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Successfully deleted the board resolution.\");", true);
                                BindGridView();
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Failed to delete the board resolution. Please try again later.\");", true);
                        }

                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Cannot delete this board resolution due to it is completed/ published\");", true);
                }
            }
        }
        public static int FindCurrentCosecUser(int userId)
        {
            int cosecID = 0; // Use nullable int to handle possible null values
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command3 = new SqlCommand("SELECT C.cosecId FROM [User] U LEFT JOIN Company C ON U.companyID = C.companyID WHERE U.userID = @userID", connection);
                command3.Parameters.AddWithValue("@userID", userId);

                object result = command3.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    cosecID = Convert.ToInt32(result);
                }
            }

            return cosecID;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = null;

            if (Session["currentRole"].ToString() == "client user")
            {
                ddlStatus.Items.Remove("Deleted");
                ddlStatus.DataBind();
                query = "SELECT * FROM BoardResolution WHERE type = 'Board Resolution Request' and createdBy = @id and (title LIKE @searchTerm OR description LIKE @searchTerm OR CONVERT(VARCHAR, due_date, 23) LIKE @searchTerm OR CONVERT(VARCHAR, created_date, 23) LIKE @searchTerm OR requestID LIKE @searchTerm OR createdBy LIKE @searchTerm);";
            }
            else if (Session["currentRole"].ToString() == "cosec user")
            {
                query = "SELECT * FROM BoardResolution B LEFT JOIN Request R ON B.requestID = R.requestID WHERE B.type = 'Board Resolution Request' and R.assignedTo = @id and (B.type LIKE @searchTerm or B.title LIKE @searchTerm OR B.description LIKE @searchTerm OR CONVERT(VARCHAR, B.due_date, 23) LIKE @searchTerm OR CONVERT(VARCHAR, B.created_date, 23) LIKE @searchTerm OR B.requestID LIKE @searchTerm OR B.createdBy LIKE @searchTerm);";
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        GridView1.DataSource = table;
                        GridView1.DataBind();
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
                        query = "SELECT * FROM BoardResolution WHERE type = 'Board Resolution Request' and createdBy = @id;";
                    }
                    else
                    {
                        query = "SELECT * FROM BoardResolution B LEFT JOIN Request R ON B.requestID = R.requestID WHERE B.type = 'Board Resolution Request' and B.createdBy = @id and R.status = @status;";
                    }
                }
                else if (roleName == "cosec user")
                {
                    // Select all for cosec user
                    if (selectedValue == "All")
                    {
                        // Select all 
                        query = "SELECT * FROM BoardResolution B LEFT JOIN Request R ON B.requestID = R.requestID WHERE B.type = 'Board Resolution Request' and R.assignedTo = @id;";
                    }
                    else
                    {
                        // Select based on the chosen
                        query = "SELECT * FROM BoardResolution B LEFT JOIN Request R ON B.requestID = R.requestID WHERE B.type = 'Board Resolution Request' and R.assignedTo =@id and R.status = @status;";
                    }
                }
                else
                {
                    Response.Redirect("ErrorPage.aspx");
                    return;
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selectedValue != "all")
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