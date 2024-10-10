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
    public partial class AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userid"] = 32;
            Session["currentRole"] = "service admin";
            if (Session["userid"] == null || Session["currentRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["currentRole"].ToString() != "service admin")
            {

                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string selectQuery = "Insert into Role values (@roleName, @roleDesc)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@roleName", txtRoleName.Text);
                    command.Parameters.AddWithValue("@roleDesc", txtDescription.Text);

                    int success = command.ExecuteNonQuery();

                    if (success > 0)
                    {
                        string script = "alert('Successfully add role.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                        int a = 0;
                        a++;
                        if (a > 0)
                        {
                            Response.Redirect("ManageRole.aspx");
                        }
                    }
                }

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageRole.aspx");
        }

    }
}