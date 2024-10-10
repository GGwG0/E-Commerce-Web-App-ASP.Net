using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DrawingImage = System.Drawing.Image;

namespace DemoAssignment.AuthenticatedUser.Admin
{
    public partial class DisplayProductDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisplayProduct.aspx"); // navigate back to the DisplayProduct.aspx page
        }
        protected string GetStatusText(object status)
        {
            if (status != null && status.ToString() == "1")
            {
                return "Listed";
            }
            else
            {
                return "Not Listed";
            }
        }


        protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            DropDownList ddlCategory = (DropDownList)DetailsView1.FindControl("ddlCategory");
            DropDownList ddlProductType = (DropDownList)DetailsView1.FindControl("ddlProductType");

            string productName = e.NewValues["product_name"] as string;
            string description = e.NewValues["description"] as string;
            TextBox quantityTextBox = (TextBox)DetailsView1.FindControl("quantity3");
            int quantity;

            if (string.IsNullOrEmpty(productName))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Product Name is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
            }
            else if (productName.Length > 30)
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Product Name should not more than 30 characters!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
            }

            if (string.IsNullOrEmpty(description))
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Description is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
            }
            else if (description.Length > 30)
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Description should not more than 30 characters!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
            }

            if (int.TryParse(quantityTextBox.Text, out quantity))
            {
                if (quantity == 0)
                {
                    string script = "Swal.fire({ title: 'Invalid', text: 'Quantity cannot be zero! It must at least 10', icon: 'error' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    e.Cancel = true;
                }
                else if (quantity < 0)
                {
                    string script = "Swal.fire({ title: 'Invalid', text: 'Quantity cannot be negative!', icon: 'error' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    e.Cancel = true;
                }
                else if (quantity < 10)
                {
                    string script = "Swal.fire({ title: 'Invalid', text: 'Quantity must at least 10!', icon: 'error' });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    e.Cancel = true;
                }
            }

            if (ddlCategory.SelectedValue == "" || ddlCategory.SelectedValue == null)
            {
                string script = "Swal.fire({ title: 'Invalid', text: 'Category is required!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
            }

            FileUpload fileUpload1 = (FileUpload)DetailsView1.FindControl("FileUpload1");
            FileUpload fileUpload2 = (FileUpload)DetailsView1.FindControl("FileUpload2");
            FileUpload fileUpload3 = (FileUpload)DetailsView1.FindControl("FileUpload3");

            if (fileUpload1.HasFile && !IsImageValid(fileUpload1))
            {
                // Show an error message for invalid file format
                string script = "Swal.fire({ title: 'Invalid', text: 'Please upload a JPG or PNG image for File 1!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
                return;
            }
            if (fileUpload2.HasFile && !IsImageValid(fileUpload2))
            {
                // Show an error message for invalid file format
                string script = "Swal.fire({ title: 'Invalid', text: 'Please upload a JPG or PNG image for File 2!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
                return;
            }
            if (fileUpload3.HasFile && !IsImageValid(fileUpload3))
            {
                // Show an error message for invalid file format
                string script = "Swal.fire({ title: 'Invalid', text: 'Please upload a JPG or PNG image for File 3!', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                e.Cancel = true;
                return;
            }

            e.NewValues["product_category"] = ddlCategory.SelectedValue;
            e.NewValues["product_type"] = ddlProductType.SelectedValue;

            for (int i = 1; i <= 3; i++)
            {
                FileUpload fileUpload = DetailsView1.FindControl("FileUpload" + i) as FileUpload;
                if (fileUpload.HasFile)
                {
                    string fileName = fileUpload.FileName;
                    string newFileName = Server.MapPath("~/assets/images/products/") + fileName;
                    fileUpload.PostedFile.SaveAs(newFileName);

                    e.NewValues["product_image_" + i] = fileName;
                }
                else
                {
                    HiddenField hfProductImage1 = (HiddenField)DetailsView1.FindControl("hfProductImage1");
                    HiddenField hfProductImage2 = (HiddenField)DetailsView1.FindControl("hfProductImage2");
                    HiddenField hfProductImage3 = (HiddenField)DetailsView1.FindControl("hfProductImage3");

                    string originalImage1 = hfProductImage1.Value;
                    string originalImage2 = hfProductImage2.Value;
                    string originalImage3 = hfProductImage3.Value;

                    if (string.IsNullOrEmpty(originalImage1))
                    {
                        e.NewValues["product_image_1"] = originalImage1;
                    }

                    if (string.IsNullOrEmpty(originalImage2))
                    {
                        e.NewValues["product_image_2"] = originalImage2;
                    }

                    if (string.IsNullOrEmpty(originalImage3))
                    {
                        e.NewValues["product_image_3"] = originalImage3;
                    }
                }
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

        private string GetImageFileNameFromUrl(string imageUrl)
        {
            // Extract the file name from the image URL
            Uri uri = new Uri(imageUrl);
            string fileName = Path.GetFileName(uri.LocalPath);

            return fileName;
        }

        protected void cvCategory_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DropDownList ddlCategory = (DropDownList)DetailsView1.FindControl("ddlCategory");
            args.IsValid = !string.IsNullOrEmpty(ddlCategory.SelectedValue);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCategory = (DropDownList)sender;
            DetailsViewRow row = FindParentDetailsViewRow(ddlCategory);

            if (row != null)
            {
                DropDownList ddlProductType = (DropDownList)row.FindControl("ddlProductType");

                // Rest of the code to populate product types based on selected category
                // ...
                // Clear previous items
                ddlProductType.Items.Clear();

                // Get the selected category
                string selectedCategory = ddlCategory.SelectedValue;

                // Populate the product types based on the selected category
                if (selectedCategory == "Data Storage")
                {
                    ddlProductType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    ddlProductType.Items.Add(new ListItem("External Hard Drives"));
                    ddlProductType.Items.Add(new ListItem("External SSD"));
                    ddlProductType.Items.Add(new ListItem("Flash Drives"));
                    ddlProductType.Items.Add(new ListItem("Internal Hard Drives"));
                    // Add more items for Data Storage category if needed
                }
                else if (selectedCategory == "Monitors")
                {
                    ddlProductType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    ddlProductType.Items.Add(new ListItem("Curved Monitors"));
                    ddlProductType.Items.Add(new ListItem("Flat Monitors"));
                    ddlProductType.Items.Add(new ListItem("Gaming Monitors"));
                }
                else if (selectedCategory == "Computer Accessories")
                {
                    ddlProductType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    ddlProductType.Items.Add(new ListItem("Cooling Pads & Stands"));
                    ddlProductType.Items.Add(new ListItem("Keyboard & Mouse"));
                    ddlProductType.Items.Add(new ListItem("Projector"));
                }
                else if (selectedCategory == "Network Components & IP Cameras")
                {
                    ddlProductType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    ddlProductType.Items.Add(new ListItem("Network Adapters"));
                    ddlProductType.Items.Add(new ListItem("Range Extender / Powerline"));
                    ddlProductType.Items.Add(new ListItem("Smart Home & IP Cameras"));
                }
                else if (selectedCategory == "Printer & Ink")
                {
                    ddlProductType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    ddlProductType.Items.Add(new ListItem("Ink Catridges & Toners"));
                    ddlProductType.Items.Add(new ListItem("Printers & Scanners"));
                }
                else if (selectedCategory == "Mobile & Tablets")
                {
                    ddlProductType.Items.Add(new ListItem("Please Select a Product Type :-"));
                    ddlProductType.Items.Add(new ListItem("Mobile Accessories"));
                    ddlProductType.Items.Add(new ListItem("Mobile Phones"));
                    ddlProductType.Items.Add(new ListItem("Tablets"));
                }
            }
        }
        private DetailsViewRow FindParentDetailsViewRow(Control control)
        {
            Control parent = control.Parent;

            if (parent == null)
                return null;

            if (parent is DetailsViewRow detailsViewRow)
                return detailsViewRow;

            return FindParentDetailsViewRow(parent);
        }

    }
}