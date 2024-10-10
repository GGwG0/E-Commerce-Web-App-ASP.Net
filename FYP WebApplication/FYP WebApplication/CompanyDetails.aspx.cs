using iText.IO.Colors;
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
    public partial class CompanyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session["currentRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            LoadDataFromTable();
        }
        protected void LoadDataFromTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "Select * from Company where companyID = @id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Request.QueryString["id"]));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            lblComID.Text = reader["companyID"].ToString();
                            lblAddress.Text = reader["address"].ToString();
                            lblAdmin.Text = reader["cosecId"].ToString();
                            lblRegNum.Text = reader["comRegNum"].ToString();
                            lblContactNum.Text = reader["contactNum"].ToString();
                            lblComName.Text = reader["comName"].ToString();


                            byte[] imageBytes = reader["comlogo"] as byte[];


                            if (imageBytes != null && imageBytes.Length > 0)
                            {
                                string image = Convert.ToBase64String(imageBytes);
                                string base64String = "data:image/jpg;base64," + image;
                                imgProfile.ImageUrl = base64String;
                            }
                            else
                            {
                                imgProfile.ImageUrl = "~/assets/image/logo.jpeg";
                            }



                        }
                    }

                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyList.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            Response.Redirect("EditCompanyDetails.aspx?id=" + Request.QueryString["id"].ToString());
        }
    }
}