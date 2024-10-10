using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class CreateRequestType : System.Web.UI.Page
    {
        int BRID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime currentDate = DateTime.Now;
                createdDate.Text = currentDate.ToString("dd MMM yyyy");
            }

        }

        protected void addbtn_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define your SQL query to insert a new row into the table
                string query = @"INSERT INTO [dbo].[Request] ([title], [type], [description], [createdDate])
                             VALUES (@title, @type, @description, @created_Date);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set the parameter values for the query
                    command.Parameters.AddWithValue("@title", Name.Text);
                    command.Parameters.AddWithValue("@type", 'T');
                    command.Parameters.AddWithValue("@created_Date", DateTime.Now);
                    command.Parameters.AddWithValue("@description", Description.Text);

                    connection.Open();
                    // Execute the query and get the newly created ID using SCOPE_IDENTITY()
                    BRID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            Global.InsertAuditRecord(BRID, "Created a New Request Type: " + BRID, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

            string script = "if(confirm('Successfully created request type')){ window.location.href = 'RequestTypeList.aspx'; }";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientScript", script, true);

        }

        protected void InsertVariableIntoUserInput(string variableName, int boardReId)
        {
            // Replace the following with your actual ADO.NET code to insert the data into the "UserInput" table
            string insertQuery = "INSERT INTO [dbo].[RequestTypeField] ([requestID], [label_name]) VALUES (@requestID, @label_name)";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@label_name", variableName);
                    cmd.Parameters.AddWithValue("@requestID", boardReId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestTypeList.aspx");
        }
    }
}