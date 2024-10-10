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
    public partial class EditCompanyDetails : System.Web.UI.Page
    {
        protected void page_init(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session["currentRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            LoadDataFromTable();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session["currentRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;

            }
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
                            txtAddress.Text = reader["address"].ToString();
                            txtRegNum.Text = reader["comRegNum"].ToString();
                            txtContactNum.Text = reader["contactNum"].ToString();
                            txtComName.Text = reader["comName"].ToString();


                            byte[] imageBytes = reader["comlogo"] as byte[];
                            Session["comlogo"] = imageBytes;

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "Update [Company] set comName = @comName, comRegNum = @comRegNum, address=@address, contactNum = @contactNum, comlogo=@comlogo where companyID =@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@comName", txtComName.Text);
                    command.Parameters.AddWithValue("@comRegNum", txtRegNum.Text);

                    command.Parameters.AddWithValue("@address", txtAddress.Text);
                    command.Parameters.AddWithValue("@contactNum", txtContactNum.Text);
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Request.QueryString["id"]));

                    if (FileUpload1.HasFile)
                    {
                        byte[] fileBytes = FileUpload1.FileBytes;
                        command.Parameters.AddWithValue("@comlogo", fileBytes);
                    }
                    else
                    {
                        byte[] profile = Session["comlogo"] as byte[];
                        command.Parameters.AddWithValue("@comlogo", profile);
                    }


                    int success = command.ExecuteNonQuery();

                    if (success > 0)
                    {
                        Global.InsertAuditRecord(0, "Edited Company Details : " + Request.QueryString["id"], Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
                        string script = "alert('Successfully update the company');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                        Response.Redirect("CompanyList.aspx");
                    }

                    

                }
            }
        }
    }
}