using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment
{
    public partial class ViewProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
            {

                String username = Session["Username"].ToString();


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                // check if the product is in cart
                SqlCommand command1 = new SqlCommand("Select id, email, phone_num, address, name, dob, status, profile from [User] where login_name = @login_name;", con);
                command1.Parameters.AddWithValue("@login_name", Session["Username"].ToString());
                con.Open();
                SqlDataReader reader = command1.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                  id.Text = reader["id"].ToString();
                  email.Text = reader["email"].ToString(); 
                  phone_num.Text = reader["phone_num"].ToString();
                  address.Text = reader["address"].ToString(); 
                  name.Text = reader["name"].ToString();
                  login_name.Text = Session["Username"].ToString();
                  dob.Text = reader["dob"].ToString();
                   imgProfile.ImageUrl = "~/assets/images/profile/" + reader["profile"].ToString();

                }
                lblName.Text = Session["Username"].ToString();
            }
        }
    }
}