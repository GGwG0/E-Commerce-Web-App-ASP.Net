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
    public partial class DisplayRoleDetail : System.Web.UI.Page
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
                            lblRoleName.Text = reader.GetString(reader.GetOrdinal("roleName"));
                            lblDescription.Text = reader.GetString(reader.GetOrdinal("roleDesc"));

                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditRoleDetail.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
        }

    }
}