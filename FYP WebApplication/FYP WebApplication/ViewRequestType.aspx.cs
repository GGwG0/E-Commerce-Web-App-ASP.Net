using Org.BouncyCastle.Asn1.Ocsp;
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
    public partial class ViewRequestType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int test1 = Convert.ToInt32(Request.QueryString["id"]);
                LoadDataForRequestType(test1);

            }

        }

        private void LoadDataForRequestType(int requestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = "SELECT [title], [description], [createdDate] FROM [dbo].[Request] WHERE [RequestID] = @RequestID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Bind data to the controls
                        Name.Text = reader["title"].ToString();
                        Description.Text = reader["description"].ToString();
                        createdDate.Text = reader["createdDate"].ToString();
                    }

                    reader.Close();
                }
            }
        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestTypeList.aspx");
        }

        protected void addbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"EditRequestType.aspx?id=" + Request.QueryString["id"]);

        }
    }
}