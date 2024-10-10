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
    public partial class EditRequestType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int ID = Convert.ToInt32(Request.QueryString["id"]);
                PopulateRequestDetails(ID);

            }

        }

        protected void cancelbtn_Click(object sender, EventArgs e)
        {

            Response.Redirect("RequestTypeList.aspx");
        }

        protected void addbtn_Click(object sender, EventArgs e)
        {
            Global.InsertAuditRecord(Convert.ToInt32(Request.QueryString["id"]), "Edited Request Type Details : " + Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
            UpdateBoardResolutionData(Convert.ToInt32(Request.QueryString["id"]));
            Response.Redirect("RequestTypeList.aspx");
        }

        protected void UpdateBoardResolutionData(int boardReIDToUpdate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string query = "UPDATE [dbo].[Request] SET [title] = @Title, [description] = @Description WHERE [requestID] = @requestID";

            // Assuming you have TextBox controls with IDs TextBox1, TextBox2, and TextBox3 for title, description, and content respectively
            string title = Name.Text;
            string description = Description.Text;
            // Replace with the actual boardReID you want to update

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@requestID", boardReIDToUpdate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void PopulateRequestDetails(int requestID)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = "SELECT * FROM [dbo].[Request] WHERE [RequestID] = @RequestID";

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


    }
}