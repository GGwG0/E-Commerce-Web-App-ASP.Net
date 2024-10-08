using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class AddProduct : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }

        }

        protected void ddlProdCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList pdCategory = (DropDownList)sender;
            DropDownList pdType = ddlProdType;
            pdType.Items.Clear();

            switch (pdCategory.SelectedValue)
            {

                case "cat1":
                    pdType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    pdType.Items.Add(new ListItem("External Hard Drive"));
                    pdType.Items.Add(new ListItem("External SSD"));
                    pdType.Items.Add(new ListItem("Flash SSD"));
                    pdType.Items.Add(new ListItem("Internal Hard Drives"));

                    break;

                case "cat2":
                    pdType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    pdType.Items.Add(new ListItem("Curved Monitors"));
                    pdType.Items.Add(new ListItem("Smart Monitors"));
                    pdType.Items.Add(new ListItem("Gaming Monitors"));
                    break;

                case "cat3":
                    pdType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    pdType.Items.Add(new ListItem("Cooling Pads and Stands"));
                    pdType.Items.Add(new ListItem("Keyboard & Mouse"));
                    pdType.Items.Add(new ListItem("Projector"));

                    break;

                case "cat4":
                    pdType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    pdType.Items.Add(new ListItem("Network Adapters"));
                    pdType.Items.Add(new ListItem("Range Extender / Powerline"));
                    pdType.Items.Add(new ListItem("Smart Home & IP Cameras"));

                    break;

                case "cat5":
                    pdType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    pdType.Items.Add(new ListItem("Ink Cartridges & Toners"));
                    pdType.Items.Add(new ListItem("Printers & Scanners"));

                    break;

                case "cat6":
                    pdType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    pdType.Items.Add(new ListItem("Mobile Accessories"));
                    pdType.Items.Add(new ListItem("Mobile Phones"));
                    pdType.Items.Add(new ListItem("Tablets"));
                    break;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            //int stockqty = Convert.ToInt32(quantity1.Text) + Convert.ToInt32(quantity2.Text) + Convert.ToInt32(quantity3.Text);

            string productName = txtProductName.Text;
            string description = txtDesc.Text;
            string variation1Value = variation1.Text;
            string variation2Value = variation2.Text;
            string variation3Value = variation3.Text;
            string price1Value = price1.Text;
            string price2Value = price2.Text;
            string price3Value = price3.Text;




            if (string.IsNullOrEmpty(productName))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Product Name is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                txtProductName.Text = string.Empty;
                return;
            }
            else if (productName.Length > 30)
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Product Name should not exceed 30 characters!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                txtProductName.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(description))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Description is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }
            else if (description.Length > 30)
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Description should not more than 30 characters!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }

            if (string.IsNullOrEmpty(variation3Value) && string.IsNullOrEmpty(variation2Value) && string.IsNullOrEmpty(variation1Value))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'At least 1 Variation is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }
            if (string.IsNullOrEmpty(price3Value) && string.IsNullOrEmpty(price2Value) && string.IsNullOrEmpty(price1Value))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'At least 1 Price is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }


            int quantity1, quantity2, quantity3;
            TextBox quantityTextBox = (TextBox)form1.FindControl("quantity1");
            TextBox quantityTextBox2 = (TextBox)form1.FindControl("quantity2");
            TextBox quantityTextBox3 = (TextBox)form1.FindControl("quantity3");
            if (int.TryParse(quantityTextBox.Text, out quantity1) && int.TryParse(quantityTextBox2.Text, out quantity2) && int.TryParse(quantityTextBox3.Text, out quantity3))
            {
                if (quantity1 == 0 && quantity2 == 0 && quantity3 == 0)
                {
                    string script = "Swal.fire({ title: 'Invalid', text: 'Quantity  cannot be zero! ', icon: 'error' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
                else if (quantity1 == 0 && quantity2 == 0 && quantity3 == 0)
                {
                    string script = "Swal.fire({ title: 'Invalid', text: 'Quantity  cannot be negative!', icon: 'error' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    return;
                }
            }

            FileUpload fileUpload1 = (FileUpload)form1.FindControl("FileUpload1");
            FileUpload fileUpload2 = (FileUpload)form1.FindControl("FileUpload2");
            FileUpload fileUpload3 = (FileUpload)form1.FindControl("FileUpload3");

            if (fileUpload1.HasFile && !IsImageValid(fileUpload1))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Please upload a JPG or PNG image for File 1!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }
            if (fileUpload2.HasFile && !IsImageValid(fileUpload2))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Please upload a JPG or PNG image for File 2!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }
            if (fileUpload3.HasFile && !IsImageValid(fileUpload3))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Please upload a JPG or PNG image for File 3!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                return;
            }







            //if (Convert.ToInt32(stockqty) < 0)
            //{

            //    string message = "Unable to add because the stock is less than 0";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
            //}

            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Product] (product_name,product_category, product_type,description,stock_status, status, product_image_1, product_image_2, product_image_3) VALUES (@product_name,@product_category,@product_type,@description,@stock_status,@status, @product_image_1,@product_image_2, @product_image_3);", con);
                command.Parameters.AddWithValue("@product_name", txtProductName.Text);
                command.Parameters.AddWithValue("@product_category", ddlProdCat.SelectedItem.Text);
                command.Parameters.AddWithValue("@product_type", ddlProdType.SelectedItem.Text);
                command.Parameters.AddWithValue("@description", txtDesc.Text);
                command.Parameters.AddWithValue("@stock_status", ddlStockStatus.SelectedIndex);
                command.Parameters.AddWithValue("@status", ddlStatus.SelectedIndex);

                for (int i = 1; i <= 3; i++)
                {
                    FileUpload fileUpload = FindControl("FileUpload" + i) as FileUpload;
                    if (fileUpload.HasFile)
                    {
                        string fileName1 = fileUpload.FileName.ToString();
                        string newFileName1 = Server.MapPath("~/assets/images/products/") + fileName1;
                        fileUpload.PostedFile.SaveAs(newFileName1);
                        command.Parameters.AddWithValue("@product_image_" + i, fileName1);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@product_image_" + i, "default_product.png");

                    }

                }
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand command2 = new SqlCommand("SELECT TOP 1 * FROM Product ORDER BY id DESC;", con);
                int product_id = (int)command2.ExecuteScalar();
                con.Close();

                for (int i = 1; i <= 3; i++)
                {
                    TextBox variation = FindControl("variation" + i) as TextBox;
                    TextBox price = FindControl("price" + i) as TextBox;
                    TextBox quantity = FindControl("quantity" + i) as TextBox;

                    if (!string.IsNullOrEmpty(variation.Text) && !string.IsNullOrEmpty(price.Text) && !string.IsNullOrEmpty(quantity.Text))
                    {
                        SqlCommand command3 = new SqlCommand("INSERT INTO [dbo].[Product_Variation] (variation_name, price, stock_quantity, product_id) VALUES (@variation_name,@price,@stock_quantity, @product_id)", con);
                        command3.Parameters.AddWithValue("@variation_name", variation.Text);
                        command3.Parameters.AddWithValue("@price", price.Text);
                        command3.Parameters.AddWithValue("@stock_quantity", quantity.Text);
                        command3.Parameters.AddWithValue("@product_id", product_id);
                        con.Open();
                        command3.ExecuteNonQuery();
                        con.Close();
                    }
                }




                string message = "Successfully added product!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
            }

        }

        private bool IsImageValid(FileUpload fileUpload)
        {
            // Check if the file has content
            if (fileUpload.HasFile)
            {
                // Get the file extension
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                // Check if the file extension is either JPG or PNG
                if (fileExtension == ".jpg" || fileExtension == ".png")
                {
                    return true;
                }
            }

            return false;
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            //txtProductName.Text = "";
            ////txtStock.Text = "0";
            //txtPrice.Text = "0";
            //txtDesc.Text = "";
            //ddlProdCat.SelectedIndex = 0;
            //ddlStock.SelectedIndex = 0;

        }


    }
}