using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["userid"] = 42;
                Session["currentRole"] = "client user";

                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
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

        protected DataTable GetDataFromTable()
        {
            string query = null;

            if (Session["currentRole"].ToString() == "client user" || Session["currentRole"].ToString() == "cosec user" || Session["currentRole"].ToString() == "service admin" || Session["currentRole"].ToString() == "service user")
            {
                query = "SELECT A.*, U.username, C.comName FROM Audit A LEFT JOIN [User] U ON A.UserID = U.userID LEFT JOIN Company C on A.CompanyID =C.companyID  WHERE A.UserID = @userid ORDER BY date DESC;";

            }
            else if (Session["currentRole"].ToString() == "cosec admin" || Session["currentRole"].ToString() == "client admin")
            {
                query = "SELECT A.*, U.username, C.comName FROM Audit LEFT JOIN [User] U ON A.UserID = U.userID LEFT JOIN Company C on A.CompanyID =C.companyID  WHERE A.UserID = @userid AND A.CompanyID = @CompanyID ORDER BY date DESC;";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userid", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@CompanyID",Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = null;

            if (Session["currentRole"].ToString() == "client user" || Session["currentRole"].ToString() == "cosec user" || Session["currentRole"].ToString() == "service admin" || Session["currentRole"].ToString() == "service user")
            {
                query = "SELECT * FROM Audit WHERE UserID = @userid AND (title LIKE @searchTerm OR description LIKE @searchTerm OR CONVERT(VARCHAR, createdDate, 23) LIKE @searchTerm OR requestID LIKE @searchTerm OR createdBy LIKE @searchTerm) ORDER BY date DESC;";

            }
            else if (Session["currentRole"].ToString() == "cosec admin" || Session["currentRole"].ToString() == "client admin")
            {
                query = "SELECT * FROM Audit WHERE UserID = @userid AND CompanyID = @CompanyID AND (title LIKE @searchTerm OR description LIKE @searchTerm OR CONVERT(VARCHAR, createdDate, 23) LIKE @searchTerm OR requestID LIKE @searchTerm OR createdBy LIKE @searchTerm) ORDER BY date DESC;";
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + txtSearch.Text + "%");
                    command.Parameters.AddWithValue("@userid", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@CompanyID", Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

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