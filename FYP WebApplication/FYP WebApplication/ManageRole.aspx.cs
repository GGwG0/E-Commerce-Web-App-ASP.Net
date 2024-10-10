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
    public partial class ManageRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                //Session["currentRoleName"] = FYP_Project.RequestDashboard.GetRole(Convert.ToInt32(Session["userid"]));

                //if (Session["currentRoleName"] == null)
                //{
                //    Response.Redirect("Login.aspx");
                //    return;
                //}
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

        private string determineCurrentRole(string currentRoleName)
        {
            string roleName = null;
            if (currentRoleName == "client admin")
            {
                roleName = "client user";
            }
            else if (currentRoleName == "cosec admin")
            {
                roleName = "cosec user";
            }
            else if (currentRoleName == "service admin")
            {
                roleName = "service user";
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");

            }
            return roleName;
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
                string roleID = e.CommandArgument.ToString();
                Response.Redirect($"DisplayRoleDetail.aspx?id={roleID}");

            }
            else if (e.CommandName == "DeleteItem")
            {
                string roleString = e.CommandArgument.ToString();

                int roleID = Convert.ToInt32(roleString);


                DeleteRecord(roleID);


            }
        }
        protected void DeleteRecord(int requestID)
        {

            // delete user role 
            // delete role ID

            // service admin - manage role = see what role in db
            // cosec admin - can see role = 

            //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand("Delete request set status = @status where requestID = @requestID", connection);
            //    command.Parameters.AddWithValue("@status", "deleted");
            //    command.Parameters.AddWithValue("@requestID", requestID);

            //    connection.Open();
            //    int success = command.ExecuteNonQuery();

            //    String deleteNotification = "deleted a request";

            //    int cosecId = FindCurrentCosecUser(Convert.ToInt32(Session["userid"]));

            //    if (success > 0)
            //    {
            //        SqlCommand command2 = new SqlCommand("Insert into notification values (@action, @from, @to, @requestID, @performedDate); ", connection);
            //        command2.Parameters.AddWithValue("@action", deleteNotification);
            //        command2.Parameters.AddWithValue("@to", cosecId);
            //        command2.Parameters.AddWithValue("@from", Session["userid"]);
            //        command2.Parameters.AddWithValue("@requestID", requestID);
            //        command2.Parameters.AddWithValue("@performedDate", DateTime.Now);
            //        int notificationSucess = command2.ExecuteNonQuery();

            //        if (notificationSucess > 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Successfully deleted the request.\");", true);
            //            BindGridView();
            //        }

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Failed to delete the request. Please try again later.\");", true);

            //    }

            //}
        }


        protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selectedValue = ddlStatus.SelectedValue;
            //string status = null;
            //string selectQuery = null;
            //string currentRoleName = Session["currentRoleName"].ToString();
            //string roleName = determineCurrentRole(currentRoleName);

            //if (selectedValue == "Active")
            //{

            //    status = "active";
            //    selectQuery = "SELECT  U.userID,U.username, U.name, U.position, U.profilePicture, U.email, C.companyID,U.[status],\r\nC.comName, STRING_AGG(R.roleName, ', ') AS RoleNames FROM \r\n[User] U LEFT JOIN User_Role UR ON U.userID = UR.userID LEFT JOIN\r\n[Role] R ON UR.roleID = R.roleID LEFT JOIN Company C ON U.companyID = C.companyID GROUP BY  U.userID,\r\nU.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status]\r\nHAVING MAX(CASE WHEN R.roleName LIKE '%'+ @usertype +'%' THEN 1 ELSE 0 END) = 1 AND U.[status] = @status ORDER BY U.userID;\r\n";

            //}
            //else if (selectedValue == "Deactive")
            //{
            //    status = "deactive";
            //    selectQuery = "SELECT  U.userID,U.username, U.name, U.position, U.profilePicture, U.email, C.companyID,U.[status],\r\nC.comName, STRING_AGG(R.roleName, ', ') AS RoleNames FROM \r\n[User] U LEFT JOIN User_Role UR ON U.userID = UR.userID LEFT JOIN\r\n[Role] R ON UR.roleID = R.roleID LEFT JOIN Company C ON U.companyID = C.companyID GROUP BY  U.userID,\r\nU.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status]\r\nHAVING MAX(CASE WHEN R.roleName LIKE '%'+ @usertype +'%' THEN 1 ELSE 0 END) = 1 AND U.[status] = @status ORDER BY U.userID;\r\n";
            //}
            //else
            //{
            //    selectQuery = "SELECT U.userID,U.username, U.name, U.position, U.profilePicture, U.email, C.companyID,U.[status],\r\n    C.comName, STRING_AGG(R.roleName, ', ') AS RoleNames\r\nFROM \r\n    [User] U\r\nLEFT JOIN \r\n    User_Role UR ON U.userID = UR.userID\r\nLEFT JOIN \r\n    [Role] R ON UR.roleID = R.roleID\r\nLEFT JOIN \r\n    Company C ON U.companyID = C.companyID\r\nGROUP BY  U.userID,\r\n    U.username, U.name, U.position, U.profilePicture, U.email, C.comName, C.companyID, U.[status]\r\nHAVING \r\n    MAX(CASE WHEN R.roleName LIKE '%' + @userType + '%' THEN 1 ELSE 0 END) = 1 \r\nORDER BY \r\n    U.userID;";

            //}

            //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(selectQuery, connection))
            //    {
            //        command.Parameters.AddWithValue("@userType", roleName);
            //        command.Parameters.AddWithValue("@status", status);

            //        SqlDataAdapter adapter = new SqlDataAdapter(command);
            //        DataTable table = new DataTable();
            //        adapter.Fill(table);

            //        DataColumn base64Column = new DataColumn("Base64ProfilePicture", typeof(string));
            //        table.Columns.Add(base64Column);

            //        foreach (DataRow row in table.Rows)
            //        {
            //            byte[] imageBytes = row["profilePicture"] as byte[];
            //            string image = Convert.ToBase64String(imageBytes);

            //            if (imageBytes != null && imageBytes.Length > 0)
            //            {
            //                string base64String = "data:image/jpg;base64," + image;
            //                row["Base64ProfilePicture"] = base64String;
            //            }
            //        }

            //        GridView1.DataSource = table;
            //        GridView1.DataBind();

            //        Label lblNoRecords = GridView1.Controls.Count > 0 ? (Label)GridView1.Controls[0].Controls[0].FindControl("lblNoRecords") : null;
            //        if (lblNoRecords != null)
            //        {
            //            lblNoRecords.Visible = table.Rows.Count == 0;
            //        }
            //    }
            //}

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "SELECT * FROM Role WHERE (roleID LIKE @searchTerm OR roleName LIKE @searchTerm OR roleDesc LIKE @searchTerm);";

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

        protected DataTable GetDataFromTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "SELECT * From Role;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
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

    }
}
