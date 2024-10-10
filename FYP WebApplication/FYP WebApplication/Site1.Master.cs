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
    public partial class Site1 : System.Web.UI.MasterPage
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


                GetUserDetails(Convert.ToInt32(Session["userid"]));
                string roles = Session["currentRole"].ToString();


                if (roles == "cosec user" || roles == "client user")
                {
                    companyContainer.Visible = false;



                    if (roles == "cosec user")
                    {
                        secretaryBar.Visible = false;
                    }


                    adminDashboard.Visible = false;
                    adminDashboard2.Visible = false;
                }
                else if (roles == "service user" || roles == "service admin" || roles == "client admin" || roles == "cosec admin")
                {
                    if(roles== "service user" || roles == "client admin")
                    {
                        secretaryBar.Visible = false;
                    }

                    if (roles == "service user" || roles == "client admin")
                    {
                        companyContainer.Visible = false;
                    }


                    clientDashboard.Visible = false;
                    secretaryBar.Visible = false;
                }
                

            }
        }

        public void GetUserDetails(int userID)
        {
            try
            {
                // Your database connection logic goes here
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the SQL query to retrieve user details
                    string query = "SELECT [name], [profilePicture] FROM [dbo].[User] WHERE [userID] = @userID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameter to the query
                        command.Parameters.AddWithValue("@userID", userID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are rows returned
                            if (reader.Read())
                            {
                                
                                //imgProfile.Text = reader["name"].ToString();
                                // Access the columns as needed
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                names.InnerText = name;
                                byte[] profilePicture = reader["profilePicture"] as byte[];

                                if (profilePicture != null && profilePicture.Length > 0)
                                {
                                    string image = Convert.ToBase64String(profilePicture);
                                    string base64String = "data:image/jpg;base64," + image;
                                    
                                   imgProfile.ImageUrl = base64String;
                                   
                                }
                                else
                                {
                                    imgProfile.ImageUrl = "~/assets/image/profile/default.jpg";
                                    
                                }


                            }
                            else
                            {
                                Console.WriteLine($"User with ID {userID} not found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error retrieving user details: {ex.Message}");
            }
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            Global.InsertAuditRecord(0, "Logged Out", Convert.ToInt32(Session["userid"]), Global.GetCompanyID(Convert.ToInt32(Session["userid"])));

            Response.Redirect("Login.aspx");

        }
    }
}