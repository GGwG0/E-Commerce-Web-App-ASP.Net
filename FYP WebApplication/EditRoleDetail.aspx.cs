using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP_WebApplication
{
    public partial class EditRoleDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int roleId = Convert.ToInt32(Request.QueryString["id"]);
                LoadRoleDetail(roleId);
            }
        }
        protected void LoadRoleDetail(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Select * from Role where roleID = @id;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access data using reader
                            txtRoleName.Text = reader["roleName"].ToString();
                            txtDescription.Text = reader["roleDesc"].ToString();
                            HiddenField1.Value = reader["roleID"].ToString();
                        }
                    }
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int roleId = Convert.ToInt32(HiddenField1.Value);
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Update Role set roleName = @roleName , roleDesc = @roleDesc where roleID = @id;";
            string script = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@roleName", txtRoleName.Text);
                    command.Parameters.AddWithValue("@roleDesc", txtDescription.Text);
                    command.Parameters.AddWithValue("@id", roleId);

                    int success = command.ExecuteNonQuery();
                    if (success > 0)
                    {
                        Global.InsertAuditRecord(0, "Edited Role Details : " + roleId, Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
                        script = "alert('Successfully update role.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                        Response.Redirect("DisplayRoleDetail.aspx?id=" + roleId);
                    }

                    script = "alert('Fail to update role.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);


                }
            }
        }

    }
}