using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace FYP_WebApplication
{
    public partial class CreateCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userid"] == null || Session["currentRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["currentRole"].ToString() != "cosec admin")
            {
                cosecAdminContainer.Visible = false;
            }


            int userid = Convert.ToInt32(Session["userid"]);
            LoadDdlCosecAdminORClientAdmin();
        }
        protected void AddCompany()
        {
            byte[] fileBytes = FileUpload1.FileBytes;
            // int cosecClientID = Convert.ToInt32(Session["cosecId"]);
            int newCompanyId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [Company] (comName, comRegNum, address, contactNum, comlogo, cosecId, createdBy) " +
                "VALUES (@comName, @comRegNum, @address, @contactNum, @comlogo, @cosecId, @createdBy); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@comName", txtCompanyName.Text);
                    command.Parameters.AddWithValue("@comRegNum", txtRegNum.Text);
                    command.Parameters.AddWithValue("@address", txtAddress.Text);
                    command.Parameters.AddWithValue("@contactNum", txtPhone.Text);
                    command.Parameters.AddWithValue("@comlogo", fileBytes);

                    if (Session["currentRole"].ToString() == "service admin")
                    {
                        command.Parameters.AddWithValue("@cosecId", DBNull.Value);
                    }
                    else
                    {
                      command.Parameters.AddWithValue("@cosecId", Convert.ToInt32(ddlCosecAdmin.SelectedValue));
                    }

                   
                    command.Parameters.AddWithValue("@createdBy", Convert.ToInt32(Session["userid"]));

                    newCompanyId = Convert.ToInt32(command.ExecuteScalar());

                    if (newCompanyId != 0)
                    {
                        //int notifiedSuccess = AddNotification("created a request", newRequestId, connection);
                        //if (notifiedSuccess > 0)
                        //{
                        Global.InsertAuditRecord(0, "Created a New Company: " + newCompanyId, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));



                        ScriptManager.RegisterStartupScript(this, this.GetType(), null, "alert(\"Successfully create request.\");", true);
                        Response.Redirect($"CompanyDetails.aspx?id={newCompanyId}");

                        //}
                    }
                }
            }
        }

        protected void LoadDdlCosecAdminORClientAdmin()
        {
            if (Session["currentRole"].ToString() == "cosec admin")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to retrieve data where type is 'T'
                    string query = @"
    SELECT u.[userID], u.[name]
    FROM [dbo].[User] u
    INNER JOIN [dbo].[User_Role] ur ON u.[userID] = ur.[userID]
    WHERE u.[managedBy] = @ManagedByUserID 
    AND ur.[roleID] = 1";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ManagedByUserID", Convert.ToInt32(Session["userid"]));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Bind data to the DropDownList
                            ddlCosecAdmin.DataSource = reader;
                            ddlCosecAdmin.DataTextField = "name";
                            ddlCosecAdmin.DataValueField = "userID";
                            ddlCosecAdmin.DataBind();
                        }
                    }
                }
            }
            else
            {
                // Clear existing items before adding new ones
                ddlCosecAdmin.Items.Clear();

                // Add the default item
                ddlCosecAdmin.Items.Add(new ListItem("No need Cosec", "NULL"));

                // Set the selected item to the default value
                ddlCosecAdmin.SelectedIndex = 0;
            }



         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddCompany();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyList.aspx");
        }
    }
}