using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String name = Session["Username"].ToString();
                // String name = HttpContext.Current.User.Identity.Name;
                ChangePassword1.UserName = name;


                String username = Session["Username"].ToString();


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                // check if the product is in cart
                SqlCommand command1 = new SqlCommand("Select id, email, phone_num, address, name, dob, status, profile from [User] where login_name = @login_name;", con);
                command1.Parameters.AddWithValue("@login_name", Session["Username"].ToString());
                con.Open();
                SqlDataReader reader = command1.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                   
                    imgProfile.ImageUrl = "~/assets/images/profile/" + reader["profile"].ToString();

                }
                lblName.Text = name;
            }

        
        
        }
       

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ChangePassword1.ConfirmNewPasswordLabelText = "";
            ChangePassword1.NewPasswordLabelText = "";
            
            //FormsAuthentication.SignOut();
            //Session.Clear();
            //Response.Redirect("~/Index.aspx");
        }

        protected void ChangePassword1_ChangedPassword(object sender, EventArgs e)
        {

            String userName = ChangePassword1.UserName;
            String newPassword = ChangePassword1.NewPassword;

            // Get the user object from the membership system
            MembershipUser user = Membership.GetUser(userName);

            // Change the user's password
            bool passwordChanged = Membership.Provider.ChangePassword(userName, ChangePassword1.CurrentPassword, newPassword);

            // Save the changes to the membership system
            Membership.UpdateUser(user);
            // Redirect the user to the success page

            if (passwordChanged == false)
            {

                // Display an error message
                ChangePassword1.ChangePasswordFailureText = "Unable to change password. Please try again.";
            }
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("ViewProfile.aspx");
        }
    }
}