using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DemoAssignment.AuthenticatedUser
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    txtID.Text = reader["id"].ToString();

                    txtEmail.Text = reader["email"].ToString();
                    txtPhoneNum.Text = reader["phone_num"].ToString();
                    txtAddress.Text = reader["address"].ToString();
                    txtName.Text = reader["name"].ToString();
                    txtLoginName.Text = Session["Username"].ToString();
                    DateTime dobValue = DateTime.Parse(reader["dob"].ToString());
                    dob.Text = dobValue.ToString("yyyy-MM-dd");
                    imgProfile.ImageUrl = "~/assets/images/profile/" + reader["profile"].ToString();
                    Session["profile-img"] = reader["profile"];
                }
                lblName.Text = username;
            }

        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            string query = "UPDATE [User] SET email = @email, phone_num = @phone_num, address = @address, name = @name, dob = @dob, profile = @profile WHERE login_name = @login_name";
            SqlCommand command = new SqlCommand(query, con);

            command.Parameters.AddWithValue("@email", txtEmail.Text);
            command.Parameters.AddWithValue("@phone_num", txtPhoneNum.Text);
            command.Parameters.AddWithValue("@address", txtAddress.Text);
            command.Parameters.AddWithValue("@name", txtName.Text);
            command.Parameters.AddWithValue("@dob", DateTime.Parse(dob.Text));
            if (FileUpload1.HasFile)
            {
                string fileName1 = FileUpload1.FileName.ToString();
                string newFileName1 = Server.MapPath("~/assets/images/profile/") + fileName1;
                FileUpload1.PostedFile.SaveAs(newFileName1);
                command.Parameters.AddWithValue("@profile" , fileName1);

            }
            else
            {
                command.Parameters.AddWithValue("@profile", Session["profile-img"].ToString());
            }
      
            command.Parameters.AddWithValue("@login_name", Session["Username"].ToString());

            con.Open();
            int insert = command.ExecuteNonQuery();
            con.Close();

            if(insert > 0)
            {
               
                    MembershipUser user = Membership.GetUser(Session["Username"].ToString());
                    if (user != null)
                    {
                        user.Email = txtEmail.Text;
                        Membership.UpdateUser(user);

                        Response.Redirect("~/AuthenticatedUser/ViewProfile.aspx");
                    }
                    else
                    {
                        String message = "Fail to update profile. Please try again!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

                    }

            }
            else
            {
                String message = "Fail to update profile. Please try again!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);

            }



        }
    }
}