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
    public partial class MyProfile : System.Web.UI.Page
    {

        string myGoogleKey = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userid"] == null || Session["currentRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userid = Convert.ToInt32(Session["userid"]);

            if (Session["currentRole"].ToString() != "client user")
            {
                signatureDiv.Visible = false;
            }

            LoadDataFromTable(userid);

        }

        protected void LoadDataFromTable(int userid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "SELECT U.mfa, U.userID,U.username, U.name, U.position, U.profilePicture, U.googleAuthKey, U.graphicPic, U.email, C.companyID, " +
                "U.[status], \r\nC.comName, STRING_AGG(R.roleName, ', ') AS RoleNames, U.contactNum, UM.name as ManagerName FROM" +
                " [User] U LEFT JOIN\r\nUser_Role UR ON U.userID = UR.userID  LEFT JOIN   [Role] R ON UR.roleID = R.roleID  LEFT JOIN" +
                " \r\nCompany C ON U.companyID = C.companyID LEFT JOIN [User] UM on U.managedBy = UM.userID WHERE U.userID= @userID GROUP " +
                "BY U.mfa, U.googleAuthKey,  U.userID, U.username, U.name, U.position,\r\nU.profilePicture, U.email, C.comName, U.graphicPic, C.companyID, U.[status], U.contactNum, UM.name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@userID", userid);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            myGoogleKey = reader["googleAuthKey"].ToString();
                            lblUserID.Text = reader["userID"].ToString();
                            lblUsername.Text = reader["username"].ToString();
                            lblStatus.Text = reader["status"].ToString();
                            lblRole.Text = reader["RoleNames"].ToString();
                            lblName.Text = reader["name"].ToString();
                            lblPhoneNum.Text = reader["contactNum"].ToString();
                            lblPosition.Text = reader["position"].ToString();
                            lblEmail.Text = reader["email"].ToString();
                            lblManagedBy.Text = reader["ManagerName"].ToString();
                            lblCompanyName.Text = reader["comName"].ToString();

                            if (reader["mfa"].ToString() == "yes")
                            {
                                mfa.Text = "Enabled";
                                Button2.Text = "Disable MFA";
                            }
                            else
                            {
                                mfa.Text = "Disabled";
                                Button2.Text = "Enable MFA";
                            }

                            byte[] imageBytes = reader["profilePicture"] as byte[];
                            if (imageBytes != null && imageBytes.Length > 0)
                            {

                                string image = Convert.ToBase64String(imageBytes);
                                string base64String = "data:image/jpg;base64," + image;
                                imgProfile.ImageUrl = base64String;
                            }
                            else
                            {
                                imgProfile.ImageUrl = "~/assets/image/profile/default.jpg";
                            }

                            if (Session["currentRole"].ToString() == "client user")
                            {
                                byte[] signByte = reader["graphicPic"] as byte[];
                                imgSignature.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(signByte);

                            }


                        }
                    }

                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["currentRole"].ToString() == "cosec user" || Session["currentRole"].ToString() == "client user")
            {
                Response.Redirect("RequestDashboard.aspx");
            }else
            {
                Response.Redirect("ManageUser.aspx");
            }
        
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");
        }

        protected void btnQR_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewQR.aspx?googleAuthKey=" + myGoogleKey);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string mfa = null;
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT [mfa] FROM [dbo].[User] WHERE userID = @userID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@userID", Convert.ToInt32(Session["userid"]));

                    // ExecuteScalar returns the first column of the first row
                    object result = command.ExecuteScalar();

                    mfa = result.ToString();

                }
            }
            string updateQuery = string.Empty;

            if(mfa == "yes")
            {
                 updateQuery = "UPDATE [dbo].[User] SET mfa = 'no' WHERE userID = @userid";
            }
            else
            {
                 updateQuery = "UPDATE [dbo].[User] SET mfa = 'yes' WHERE userID = @userid";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@userid", Convert.ToInt32(Session["userid"]));
                    command.ExecuteNonQuery();
                                      
                }
            }

            Response.Redirect("MyProfile.aspx");
        }
    }
}