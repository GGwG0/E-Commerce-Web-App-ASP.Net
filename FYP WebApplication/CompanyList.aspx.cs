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
    public partial class CompanyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

           

                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
               
                BindGridView();
            }
        }

        protected DataTable GetDataTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string roleName = Session["currentRole"].ToString();
            DataTable table = null;
            string selectQuery = "Select * from Company Where createdBy = @userId; ";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["userid"]));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    table = new DataTable();
                    adapter.Fill(table);

                    DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                    table.Columns.Add(base64Column);

                    foreach (DataRow row in table.Rows)
                    {
                        byte[] imageBytes = row["comlogo"] as byte[];

                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string image = Convert.ToBase64String(imageBytes);
                            string base64String = "data:image/jpg;base64," + image;
                            row["Base64ProfilePicture"] = base64String;
                        }
                        else
                        {
                            row["Base64ProfilePicture"] = "~/assets/image/logo.png";
                        }
                    }
                }
            }

            return table;
        }

        private void BindGridView()
        {
            DataTable table = GetDataTable();

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

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            

            if (e.CommandName == "DeleteItem")
            {
                string companyIDs = e.CommandArgument.ToString();
                int companyID = Convert.ToInt32(companyIDs);

                //DeleteRecord(boardID, requestID);

            }
            else if (e.CommandName == "ViewItem")
            {
                string companyIDs = e.CommandArgument.ToString();
                int companyID = Convert.ToInt32(companyIDs);
                Response.Redirect($"CompanyDetails.aspx?id={companyID}");
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string currentRoleName = Session["currentRole"].ToString();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = "Select * from Company Where createdBy = @userId and (companyID LIKE '%' + @searchTerm + '%' OR comName LIKE '%' + @searchTerm + '%' OR comRegNum LIKE '%' +@searchTerm + '%' OR address LIKE '%' + @searchTerm + '%' OR contactNum LIKE '%' + @searchTerm + '%' OR cosecId LIKE '%' + @searchTerm + '%');";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    command.Parameters.AddWithValue("@userId", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@searchTerm", txtSearch.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                    table.Columns.Add(base64Column);

                    foreach (DataRow row in table.Rows)
                    {
                        byte[] imageBytes = row["comlogo"] as byte[];

                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string image = Convert.ToBase64String(imageBytes);
                            string base64String = "data:image/jpg;base64," + image;
                            row["Base64ProfilePicture"] = base64String;
                        }
                        else
                        {
                            row["Base64ProfilePicture"] = "~/assets/image/logo.png";
                        }
                    }

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
            DataTable dataTable = GetDataTable();

            if (dataTable != null)
            {
                dataTable.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

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

    }
}