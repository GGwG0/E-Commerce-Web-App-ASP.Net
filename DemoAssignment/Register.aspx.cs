using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
        protected void CreateUserWizard1_NextButtonClick(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[User] (email,login_name, phone_num,address,name,user_type,dob) VALUES (@email,@login_name, @phone_num,@address,@name,@user_type,@dob);", con);
            TextBox dobTextBox = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("DOB");
            TextBox phoneNumTextBox = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("MobileNumber");
            TextBox addressTextBox = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Address");
            TextBox nameTextBox = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Name");

           

            command.Parameters.AddWithValue("@email", CreateUserWizard1.Email);
    
            command.Parameters.AddWithValue("@login_name", CreateUserWizard1.UserName);
            command.Parameters.AddWithValue("@phone_num", phoneNumTextBox.Text);
            command.Parameters.AddWithValue("@address", addressTextBox.Text);
            command.Parameters.AddWithValue("@name", nameTextBox.Text);
            command.Parameters.AddWithValue("@user_type", "customer");
            command.Parameters.AddWithValue("@dob", dobTextBox.Text);

            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}