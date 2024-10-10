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
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["userid"] == null || Session["currentRole"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                int userid = Convert.ToInt32(Session["userid"]);
                LoadDataFromTable(userid);
            }

        }

        protected void LoadDataFromTable(int userid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "SELECT U.userID,U.username, U.name, U.position, U.profilePicture, U.graphicPic, U.email, C.companyID, " +
                "U.[status], \r\nC.comName, STRING_AGG(R.roleName, ', ') AS RoleNames, U.contactNum, UM.name as ManagerName FROM" +
                " [User] U LEFT JOIN\r\nUser_Role UR ON U.userID = UR.userID  LEFT JOIN   [Role] R ON UR.roleID = R.roleID  LEFT JOIN" +
                " \r\nCompany C ON U.companyID = C.companyID LEFT JOIN [User] UM on U.managedBy = UM.userID WHERE U.userID= @userID GROUP " +
                "BY  U.userID, U.username, U.name, U.position,\r\nU.profilePicture, U.email, C.comName, U.graphicPic, C.companyID, U.[status], U.contactNum, UM.name";

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

                            byte[] signByte = reader["graphicPic"] as byte[];
                            byte[] imageBytes = reader["profilePicture"] as byte[];

                            Session["profile"] = imageBytes;

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
                            imgSignature.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(signByte);



                        }
                    }

                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string selectQuery = "Update [User] set name = @name, contactNum = @contactNum, position=@position, email = @email, profilePicture=@picture where userID =@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@name", lblName.Text);
                    command.Parameters.AddWithValue("@contactNum", lblPhoneNum.Text);

                    command.Parameters.AddWithValue("@position", lblPosition.Text);
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userid"]));
                    command.Parameters.AddWithValue("@email", lblEmail.Text);
                    if (FileUpload1.HasFile)
                    {
                        byte[] fileBytes = FileUpload1.FileBytes;
                        command.Parameters.AddWithValue("@picture", fileBytes);
                    }
                    else
                    {
                        byte[] profile = Session["profile"] as byte[];
                        command.Parameters.AddWithValue("@picture", profile);
                    }


                    int success = command.ExecuteNonQuery();

                    if (success > 0)
                    {
                        Global.InsertAuditRecord(0, "Edited Profile Details : " + Session["userid"], Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));
                        string script = "alert('Successfully update your profile');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ServerScript", script, true);
                        Response.Redirect("MyProfile.aspx");
                    }



                }
            }
        }

    }
}