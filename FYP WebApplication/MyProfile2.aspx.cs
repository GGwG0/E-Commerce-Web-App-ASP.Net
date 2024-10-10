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
    public partial class MyProfile2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userid = Convert.ToInt32(Session["cosecid"]);

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






                        }
                    }

                }
            }
        }



    }
}