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
    public partial class ManageUser : System.Web.UI.Page
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
            string selectQuery = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(string.Empty, connection))
                {

                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["userid"]));

                    switch (roleName)
                    {
                        case "service admin":
                            selectQuery = GetCommonSelectQuery() + @" GROUP BY U.userID, U.username, U.name, U.position, 
                                            U.profilePicture, U.email, C.comName, C.companyID, U.[status]  HAVING 
                MAX(CASE WHEN R.roleName = 'service user' OR R.roleName = 'cosec admin' THEN 1 ELSE 0 END) = 1 ORDER BY U.userID";
                            break;

                        case "service user":
                            selectQuery = GetCommonSelectQuery() + @"GROUP BY U.userID, U.username, U.name, U.position, 
                                            U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING
                            MAX(CASE WHEN R.roleName = 'cosec admin' THEN 1 ELSE 0 END) = 1 ORDER BY U.userID
                    ";
                            break;

                        case "cosec admin":
                            selectQuery = GetCommonSelectQuery() + @"
                        WHERE 
                            U.managedBy = @ID GROUP BY U.userID, U.username, U.name, U.position,
                        U.profilePicture, U.email, C.comName, C.companyID, U.[status]
                        HAVING
                            MAX(CASE WHEN (R.roleName = 'cosec user' OR R.roleName = 'client admin') THEN 1 ELSE 0 END) = 1 
                        ORDER BY U.userID
                    ";
                            break;

                        default:
                            selectQuery = GetCommonSelectQuery() + @"
                        WHERE 
                            U.managedBy = @ID GROUP BY U.userID, U.username, U.name, U.position,
                        U.profilePicture, U.email, C.comName, C.companyID, U.[status]
                        HAVING
                            MAX(CASE WHEN R.roleName = 'client user' THEN 1 ELSE 0 END) = 1 ORDER BY U.userID
                    ";
                            break;
                    }

                    command.CommandText = selectQuery;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    table = new DataTable();
                    adapter.Fill(table);

                    DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                    table.Columns.Add(base64Column);

                    foreach (DataRow row in table.Rows)
                    {
                        byte[] imageBytes = row["profilePicture"] as byte[];

                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string image = Convert.ToBase64String(imageBytes);
                            string base64String = "data:image/jpg;base64," + image;
                            row["Base64ProfilePicture"] = base64String;
                        }
                        else
                        {
                            row["Base64ProfilePicture"] = "~/assets/image/profile/default.jpg";
                        }
                    }
                }
            }

            return table;
        }

        private string GetCommonSelectQuery()
        {
            return @"
        SELECT
            U.userID,
            U.username,
            U.name,
            U.position,
            U.profilePicture,
            U.email,
            C.companyID,
            U.[status],
            C.comName,
            STRING_AGG(R.roleName, ', ') AS RoleNames
        FROM
            [User] U
        LEFT JOIN
            User_Role UR ON U.userID = UR.userID
        LEFT JOIN
            [Role] R ON UR.roleID = R.roleID
        LEFT JOIN
            Company C ON U.companyID = C.companyID                         
       

    ";
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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView rowView = e.Row.DataItem as DataRowView;
            //    if (rowView != null)
            //    {
            //        string base64ProfilePicture = rowView["Base64ProfilePicture"] as string;
            //        if (!string.IsNullOrEmpty(base64ProfilePicture))
            //        {
            //            Image imgProfile = e.Row.FindControl("imgProfile") as Image;
            //            if (imgProfile != null)
            //            {
            //                imgProfile.ImageUrl = "data:image/png;base64,"+ base64ProfilePicture;
            //            }
            //        }
            //    }
            //}
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewItem")
            {
                string userID = e.CommandArgument.ToString();
                Response.Redirect($"DisplayUserDetail.aspx?id={userID}");
            }
        }
        protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlStatus.SelectedValue;
            string selectQuery = null;
            string roleName = Session["currentRole"].ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["userid"]));
                    if (selectedValue == "Active" || selectedValue == "Inactive" || selectedValue == "Waiting")
                    {
                        command.Parameters.AddWithValue("@status", selectedValue);
                    }
                    switch (roleName)
                    {
                        case "service admin":
                            {
                                if (selectedValue != "All")
                                {
                                    selectQuery = GetCommonSelectQuery() + @"GROUP BY U.userID, U.username, U.name, U.position,
                                                    U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING
                                                    MAX(CASE WHEN R.roleName = 'service user' OR R.roleName = 'cosec admin' THEN 1 ELSE 0 END) = 1 and U.status = @status  ORDER BY U.userID";

                                }
                                else
                                {
                                    selectQuery = GetCommonSelectQuery() + @" GROUP BY U.userID, U.username, U.name, U.position,
                                                    U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING
                                                    MAX(CASE WHEN R.roleName = 'service user' OR R.roleName = 'cosec admin' THEN 1 ELSE 0 END) = 1  ORDER BY U.userID";

                                }

                            }

                            break;

                        case "service user":
                            {
                                if (selectedValue != "All")
                                {
                                    selectQuery = GetCommonSelectQuery() + @"GROUP BY U.userID, U.username, U.name, U.position,
                        U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING MAX(CASE WHEN R.roleName = 'cosec admin' THEN 1 ELSE 0 END) = 1 and status = @status  ORDER BY U.userID";

                                }
                                else
                                {
                                    selectQuery = GetCommonSelectQuery() + @"GROUP BY U.userID, U.username, U.name, U.position,
                        U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING MAX(CASE WHEN R.roleName = 'cosec admin' THEN 1 ELSE 0 END) = 1  ORDER BY U.userID";

                                }
                            }
                            break;

                        case "cosec admin":
                            {
                                if (selectedValue != "All")
                                {
                                    selectQuery = GetCommonSelectQuery() + @" WHERE U.managedBy = @ID GROUP BY U.userID, U.username, U.name, U.position,
                                        U.profilePicture, U.email, C.comName, C.companyID, U.[status]
                                        HAVING MAX(CASE WHEN R.roleName = 'cosec user' OR R.roleName = 'client admin' THEN 1 ELSE 0 END) = 1 and status = @status  ORDER BY U.userID";


                                }
                                else
                                {
                                    selectQuery = GetCommonSelectQuery() + @" WHERE U.managedBy = @ID GROUP BY U.userID, U.username, U.name, U.position,
                                        U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING
                                        MAX(CASE WHEN R.roleName = 'cosec user' OR R.roleName = 'client admin' THEN 1 ELSE 0 END) = 1  ORDER BY U.userID";

                                }
                            }

                            break;

                        default:
                            {
                                if (selectedValue != "All")
                                {
                                    selectQuery = GetCommonSelectQuery() + @" WHERE U.managedBy = @ID GROUP BY U.userID, U.username, U.name, U.position,
                                        U.profilePicture, U.email, C.comName, C.companyID, U.[status]        HAVING
                                        MAX(CASE WHEN R.roleName = 'client user' THEN 1 ELSE 0 END) = 1 and status = @status  ORDER BY U.userID";


                                }
                                else
                                {
                                    selectQuery = GetCommonSelectQuery() + @" WHERE U.managedBy = @ID GROUP BY U.userID, U.username, U.name, U.position,
                                        U.profilePicture, U.email, C.comName, C.companyID, U.[status] HAVING
                                        MAX(CASE WHEN R.roleName = 'client user' THEN 1 ELSE 0 END) = 1  ORDER BY U.userID";

                                }
                            }


                            break;
                    }

                    command.CommandText = selectQuery;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                    table.Columns.Add(base64Column);

                    foreach (DataRow row in table.Rows)
                    {
                        byte[] imageBytes = row["profilePicture"] as byte[];

                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string image = Convert.ToBase64String(imageBytes);
                            string base64String = "data:image/jpg;base64," + image;
                            row["Base64ProfilePicture"] = base64String;
                        }
                        else
                        {
                            row["Base64ProfilePicture"] = "~/assets/image/profile/default.jpg";
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
        private string GetSearchSelectQuery()
        {
            return @"
        SELECT
            U.userID,
            U.username,
            U.name,
            U.position,
            U.profilePicture,
            U.email,
            C.companyID,
            U.[status],
            C.comName,
            STRING_AGG(R.roleName, ', ') AS RoleNames
        FROM
            [User] U
        LEFT JOIN
            User_Role UR ON U.userID = UR.userID
        LEFT JOIN
            [Role] R ON UR.roleID = R.roleID
        LEFT JOIN
            Company C ON U.companyID = C.companyID
        WHERE (U.userID LIKE '%' + @searchTerm + '%' 
        OR U.username LIKE '%' + @searchTerm + '%' 
        OR U.name LIKE '%' +@searchTerm + '%' 
        OR U.position LIKE '%' + @searchTerm + '%' 
        OR U.email LIKE '%' + @searchTerm + '%' 
        OR C.comName LIKE '%' + @searchTerm + '%'
        OR U.status LIKE '%' + @searchTerm + '%')
       ";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string currentRoleName = Session["currentRole"].ToString();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = null;
                string commonSelectQuery = GetSearchSelectQuery();
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    command.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@searchTerm", txtSearch.Text);


                    switch (currentRoleName)
                    {
                        case "service admin":
                            selectQuery = $"{commonSelectQuery} AND EXISTS (SELECT 1 FROM [Role] R2 WHERE UR.roleID = R2.roleID AND (R2.roleName = 'service user' OR R2.roleName = 'cosec admin'))" +
                                $" GROUP BY U.userID, U.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status] ORDER BY U.userID;";
                            break;

                        case "service user":
                            selectQuery = $"{commonSelectQuery} AND EXISTS (SELECT 1 FROM [Role] R2 WHERE UR.roleID = R2.roleID AND R2.roleName = 'cosec admin') " +
                                $" GROUP BY U.userID, U.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status] ORDER BY U.userID;";
                            break;

                        case "cosec admin":
                            selectQuery = $"{commonSelectQuery} AND  U.managedBy = @ID AND EXISTS (SELECT 1 FROM [Role] R2 WHERE UR.roleID = R2.roleID AND R2.roleName = 'cosec user' OR R2.roleName = 'client admin') " +
                                $" GROUP BY U.userID, U.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status] ORDER BY U.userID;";
                            break;
                        default:
                            selectQuery = $"{commonSelectQuery}  AND  U.managedBy = @ID AND  EXISTS (SELECT 1 FROM [Role] R2 WHERE UR.roleID = R2.roleID AND R2.roleName = 'client user') " +
                                $" GROUP BY U.userID, U.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status] ORDER BY U.userID;";
                            break;
                    }

                    command.CommandText = selectQuery;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
                    table.Columns.Add(base64Column);

                    foreach (DataRow row in table.Rows)
                    {
                        byte[] imageBytes = row["profilePicture"] as byte[];

                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string image = Convert.ToBase64String(imageBytes);
                            string base64String = "data:image/jpg;base64," + image;
                            row["Base64ProfilePicture"] = base64String;
                        }
                        else
                        {
                            row["Base64ProfilePicture"] = "~/assets/image/profile/default.jpg";
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}