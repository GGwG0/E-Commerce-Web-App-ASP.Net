using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class BoardResolutionTemplateList : System.Web.UI.Page
    {
        protected List<int> selectedRows = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {

         
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Assume you have a SELECT query to retrieve data from the database
                string selectQuery = "SELECT * FROM BoardResolution WHERE type = 'T' ";

                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                {
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

        protected DataTable GetDataTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "SELECT * FROM BoardResolution WHERE type = 'T' ";
            DataTable table = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                   
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    table = new DataTable();
                    adapter.Fill(table);

                }
            }

            return table;
        }




        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected row index
            int selectedIndex = GridView1.SelectedIndex;
            // You can use this index to retrieve data or perform actions on the selected row
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                string boardReID = e.CommandArgument.ToString();
                DeleteRecords(Convert.ToInt32(boardReID));
                BindGridView();


            }
            else if (e.CommandName == "ViewItem")
            {
                string BoardRID = e.CommandArgument.ToString();
                Response.Redirect($"ViewBoardResolutionTemplate.aspx?id={BoardRID}");
            }
        }

        protected void DeleteRecords(int boardReID)
        {
            // Connection string to your SQL Server database
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use a transaction to ensure both DELETE statements succeed or fail together
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Delete records from BoardResolutionField
                        string deleteFieldQuery = "DELETE FROM [dbo].[BoardResolutionField] WHERE [boardReID] = @boardReID";

                        using (SqlCommand fieldCommand = new SqlCommand(deleteFieldQuery, connection, transaction))
                        {
                            fieldCommand.Parameters.AddWithValue("@boardReID", boardReID);
                            fieldCommand.ExecuteNonQuery();
                        }

                        // 2. Delete record from BoardResolution
                        string deleteResolutionQuery = "DELETE FROM [dbo].[BoardResolution] WHERE [boardReID] = @boardReID";

                        using (SqlCommand resolutionCommand = new SqlCommand(deleteResolutionQuery, connection, transaction))
                        {
                            resolutionCommand.Parameters.AddWithValue("@boardReID", boardReID);
                            int rowsAffected = resolutionCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Record deleted successfully
                                transaction.Commit();
                                string script = "alert('Records deleted successfully.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);

                            }
                            else
                            {
                                // No record found with the specified boardReID in BoardResolution
                                transaction.Rollback();
                                string script = "alert('Record not found or already deleted.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);

                             
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur during the deletion
                        transaction.Rollback();
                        Response.Write($"Error deleting records: {ex.Message}");
                    }
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string requestID = GridView1.DataKeys[e.Row.RowIndex]["requestID"].ToString();
            //    // Find the checkbox control in the row
            //    CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
            //    if (chkSelect != null)
            //    {
            //        // Set the checkbox value based on row selection
            //        chkSelect.Checked = ((GridView1.SelectedRow != null) && (e.Row.RowIndex == GridView1.SelectedRow.RowIndex));

            //        // Add JavaScript to enable selection by clicking the checkbox
            //        chkSelect.Attributes["onclick"] = string.Format("SelectRow(this, {0});", e.Row.RowIndex);

            //    }
            //}

        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("CheckBox1");
                if (chkSelect.Checked)
                {
                    int requestID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex]["requestID"]);
                    selectedRows.Add(requestID);

                }

            }

            string selectedRowsMessage = string.Join(", ", selectedRows); // Convert List to comma-separated string

            // Trigger a JavaScript alert with the selected rows
            ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteConfirmation", $"alert('Selected rows: {selectedRowsMessage}');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteConfirmation", "if (confirm('Are you sure you want to delete?')) { alert('Deleted!'); } else { alert('Deletion canceled.'); }", true);


        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the data source
            DataTable dataTable = GetDataTable(); // Replace with your actual data retrieval method

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "SELECT * FROM BoardResolution WHERE type = 'T' AND (title LIKE @searchTerm OR description LIKE @searchTerm OR CONVERT(VARCHAR, due_date, 23) LIKE @searchTerm OR CONVERT(VARCHAR, created_date, 23) LIKE @searchTerm OR requestID LIKE @searchTerm OR createdBy LIKE @searchTerm);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");

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
    }
}