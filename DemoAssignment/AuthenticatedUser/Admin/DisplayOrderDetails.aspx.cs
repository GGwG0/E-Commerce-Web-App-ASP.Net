using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayOrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource2.EnableCaching = false;
        }
        protected string GetStatusText(object status)
        {
            if (status != null && status.ToString() == "1")
            {
                return "Packaging";
            }
            else if (status != null && status.ToString() == "2")
            {
                return "Shipping";
            }
            else
            {
                return "Completed";
            }
        }

        protected void SqlDataSource2_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            var ddlStatus = (DropDownList)DetailsView1.FindControl("ddlStatus");
            var statusParameter = e.Command.Parameters["@status"];
            statusParameter.Value = ddlStatus.SelectedValue;
        }


        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)sender;
            DetailsView dvOrder = (DetailsView)ddlStatus.NamingContainer;
            int orderID = Convert.ToInt32(dvOrder.DataKey.Value);

            if (ddlStatus.SelectedValue == "3")
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    connection.Open();

                    // Retrieve delivery ID 
                    string selectDeliveryIDQuery = "SELECT delivery_id FROM [Order] WHERE id = @OrderID";
                    using (SqlCommand selectDeliveryIDCommand = new SqlCommand(selectDeliveryIDQuery, connection))
                    {
                        selectDeliveryIDCommand.Parameters.AddWithValue("@OrderID", orderID);
                        int deliveryID = Convert.ToInt32(selectDeliveryIDCommand.ExecuteScalar());

                        string updateDeliveredDatetimeQuery = "UPDATE Delivery SET deliveredDatetime = GETDATE() WHERE id = @DeliveryID";
                        using (SqlCommand updateDeliveredDatetimeCommand = new SqlCommand(updateDeliveredDatetimeQuery, connection))
                        {
                            updateDeliveredDatetimeCommand.Parameters.AddWithValue("@DeliveryID", deliveryID);
                            updateDeliveredDatetimeCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }



    }
}