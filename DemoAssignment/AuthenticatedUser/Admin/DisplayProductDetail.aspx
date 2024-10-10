<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayProductDetail.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayProductDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>WillNet - eCommerce Website</title>
    <link rel="shortcut icon" href="../../assets/images/will_logo.jpg" type="image" />
    <link rel="stylesheet" href="../../assets/css/Staff.css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons+Sharp" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/admin-style.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.19/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.19/dist/sweetalert2.min.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <aside>

            <div class="top">
               
                    <a href="../../Index.aspx" class="logo">
                    <img src="../../assets/images/will_logo.jpg" />
                    <h2>Will<span>Nett</span></h2>
                    </a>
                   
               
                <div class="close" id="close-btn">
                    <span class="material-icons-sharp">close</span>
                </div>
            </div>



            <div class="sidebar">
                <a href="AdminDashboard.aspx">
                    <span class="material-icons-sharp">grid_view</span>
                    <h3>Dashboard</h3>
                </a>
                <a href="DisplayCustomer.aspx">
                    <span class="material-icons-sharp">person_outline</span>
                    <h3>Customers</h3>
                </a>
               <a href="DisplayOrder.aspx"> <%--DisplayOrder--%>
                    <span class="material-icons-sharp">receipt_long</span>
                    <h3>Orders</h3>
                </a>
                <a href="DisplayProduct.aspx" class="active">
                    <span class="material-icons-sharp">inventory</span>
                    <h3>Products</h3>
                </a>
                 <a href="DisplayRating.aspx">
                    <span class="material-icons-sharp">star_outline</span>
                    <h3>Ratings</h3>
                </a>
                <a href="DisplayReport.aspx"><%--DisplayReport.aspx--%>
                    <span class="material-icons-sharp">report_gmailerrorred</span>
                    <h3>Reports</h3>
                </a>
                <a href="AddProduct.aspx">
                    <span class="material-icons-sharp">add</span>
                    <h3>Add Product</h3>
                </a>
                <a href="../../Index.aspx">
                    <span class="material-icons-sharp">logout</span>
                    <h3>Logout</h3>
                </a>
            </div>
        </aside>

            <div class="staffMain">
                 <h1 id="staff" class="menu-title">>>> Product Details</h1> 
                <%--<h1 id="staff"><asp:Button ID="backButton" runat="server" Text="Back" OnClick="BackButton_Click" /></h1> --%>
                <a href="DisplayProduct.aspx" onclick="Backton_Click" class="button3">Back to product list</a>
                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="StaffDetailView" OnItemUpdating="DetailsView1_ItemUpdating" HeaderStyle-BackColor="#96C2DB" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <CommandRowStyle BackColor="white" Font-Bold="True" />
                    <%--<AlternatingRowStyle BackColor="White"  />
               <%-- <RowStyle BackColor="#EFF3FB" />
<%--            <EditRowStyle BackColor="#"  ForeColor="White"/>--%>
                    <%--<FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True" />--%>

                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="white" ForeColor="black" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" />
                    <FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True" />
                    <Fields>
                        <asp:BoundField DataField="id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="product_name" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product Name" SortExpression="Product Name" />
                        <%--<asp:BoundField DataField="product_type" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product Type" SortExpression="product_type" />--%>
                        
                        <asp:TemplateField HeaderText="Product Category" SortExpression="Category" HeaderStyle-CssClass="detailHeaderField">
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" ItemStyle-CssClass="detailItemField" runat="server" Text='<%# Eval("product_category") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ddlDetailItemField" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="Please Select a Category :-"></asp:ListItem>
                                    <asp:ListItem Value="Data Storage" Text="Data Storage"></asp:ListItem>
                                    <asp:ListItem Value="Monitors" Text="Monitors"></asp:ListItem>
                                    <asp:ListItem Value="Computer Accessories" Text="Computer Accessories"></asp:ListItem>
                                    <asp:ListItem Value="Network Components & IP Cameras" Text="Network Components & IP Cameras"></asp:ListItem>
                                    <asp:ListItem Value="Printer & Ink" Text="Printer & Ink"></asp:ListItem>
                                    <asp:ListItem Value="Mobile & Tablets" Text="Mobile & Tablets"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="cvCategory" runat="server" ControlToValidate="ddlCategory"
                                ErrorMessage="Please select a category" ValidationGroup="SaveValidation"
                                OnServerValidate="cvCategory_ServerValidate"></asp:CustomValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Product Type" SortExpression="product_type" HeaderStyle-CssClass="detailHeaderField">
                            <ItemTemplate>
                                <asp:Label ID="lblProductType" CssClass="detailItemField" runat="server" Text='<%# Eval("product_type") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlProductType" runat="server" CssClass="ddlDetailItemField">
                                    <asp:ListItem Value="" Text="Please Select a Type :-"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="description" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Description" SortExpression="description" />
<%--                        <asp:BoundField DataField="stock_status" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Stock Quantity" SortExpression="stock_status" />--%>
                        
                        <asp:TemplateField HeaderText="Stock Quantity" SortExpression="stock_status" HeaderStyle-CssClass="detailHeaderField">
                            <ItemTemplate>
                                <asp:Label ID="lblStockQuantity" CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" ItemStyle-CssClass="detailItemField" runat="server" Text='<%# Eval("stock_status") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="quantity3" Text='<%# Bind("stock_status") %>' CssClass="quantityTxtBx" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Product Images" CssClass="detailHeaderField" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="product-images">
                                    <asp:Image ID="Image1" runat="server" alt="" ItemStyle-CssClass="detailItemField" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_1").ToString() %>' CssClass="product-img default" />
                                    <asp:Image ID="Image2" runat="server" alt="" ItemStyle-CssClass="detailItemField" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_2").ToString() %>' CssClass="product-img default" />
                                    <asp:Image ID="Image3" runat="server" alt="" ItemStyle-CssClass="detailItemField" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_3").ToString() %>' CssClass="product-img default" />
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="product-images">
                                    <asp:Image ID="Image1" runat="server" alt="" ItemStyle-CssClass="detailItemField" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_1").ToString() %>' CssClass="product-img default" />
                                    <asp:Image ID="Image2" runat="server" alt="" ItemStyle-CssClass="detailItemField" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_2").ToString() %>' CssClass="product-img default" />
                                    <asp:Image ID="Image3" runat="server" alt="" ItemStyle-CssClass="detailItemField" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_3").ToString() %>' CssClass="product-img default" />
                                    <div class="file-upload-container">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                   <asp:FileUpload ID="FileUpload2" runat="server" Css-class="file-upload" />
                                    <asp:FileUpload ID="FileUpload3" runat="server" CssClass="file-upload"/>
                                        </div>
                                   <asp:HiddenField ID="hfProductImage1" runat="server" Value='<%# Bind("product_image_1") %>' />
                                    <asp:HiddenField ID="hfProductImage2" runat="server" Value='<%# Bind("product_image_2") %>' />
                                    <asp:HiddenField ID="hfProductImage3" runat="server" Value='<%# Bind("product_image_3") %>' />
                                </div>
                                <%--<asp:LinkButton ID="UpdateImageButton" runat="server" Text="Update" CommandName="Update" />--%>
                            </EditItemTemplate>
                        </asp:TemplateField>
<%--                        <asp:BoundField DataField="status" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText=" Status" SortExpression="status" />--%>
                        <asp:TemplateField HeaderText="Product Status" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField">
                            <ItemTemplate>
                                <asp:Label ID="StatusLabel" runat="server" Text='<%# GetStatusText(Eval("status")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:RadioButtonList ID="statusRadioButtonList" runat="server" CssClass="radio-button-list" SelectedValue='<%# Bind("status") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Listed" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Listed" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="product_image_1"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product Image 1" SortExpression="product_image_1" />
                <asp:BoundField DataField="product_image_2"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product Image 2" SortExpression="product_image_2" />
                <asp:BoundField DataField="product_image_3" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product Image 3" SortExpression="product_image_3" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/editing.png" runat="server" CssClass="iconClass" CommandName="Edit" ToolTip="Edit" />
                               <%-- <asp:ImageButton ImageUrl="~/assets/icons/delete.png" runat="server" CssClass="iconClass" CommandName="Delete" ToolTip="Delete" />--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/assets/icons/save.png" CssClass="iconClass"
                                CommandName="Update" ToolTip="Update" ValidationGroup="SaveValidation" />
                                <asp:ImageButton ImageUrl="~/assets/icons//cancel.png" runat="server" CssClass="iconClass" CommandName="Cancel" ToolTip="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Fields>

                </asp:DetailsView>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [Product] WHERE ([id] = @id) ORDER BY [id], [product_name], [product_type]"
                    DeleteCommand="DELETE FROM [Product] WHERE [id] = @id"
                    InsertCommand="INSERT INTO [Product] ([product_type], [product_name], [product_category], [description], [stock_status], 
            [product_image_1]), [product_image_2]), [product_image_3]) VALUES (@product_type, @product_name, @product_category, @description, @stock_status, @product_image_1, @product_image_2, @product_image_3)"
                    UpdateCommand="UPDATE [Product] SET [product_type] = @product_type, [product_name] = @product_name, [product_category] = @product_category, 
            [description] = @description, [stock_status] = @stock_status, [status] = @status, [product_image_1] = @product_image_1, 
            [product_image_2] = @product_image_2, [product_image_3] = @product_image_3 WHERE [id] = @id">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="1" Name="id" QueryStringField="id" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="product_type" Type="String" />
                        <asp:Parameter Name="product_name" Type="String" />
                        <asp:Parameter Name="product_category" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="stock_status" Type="String" />
                        <asp:Parameter Name="status" Type="Int32" />
                        <asp:Parameter Name="product_image_1" Type="String" />
                        <asp:Parameter Name="product_image_2" Type="String" />
                        <asp:Parameter Name="product_image_3" Type="String" />
                    </UpdateParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="id" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="product_type" Type="String" />
                        <asp:Parameter Name="product_name" Type="String" />
                        <asp:Parameter Name="product_category" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="stock_status" Type="String" />
                  
                        <asp:Parameter Name="product_image_1" Type="String" />
                        <asp:Parameter Name="product_image_2" Type="String" />
                        <asp:Parameter Name="product_image_3" Type="String" />
                    </InsertParameters>

                </asp:SqlDataSource>
            </div>
            <div class="right">
                <div class="top">
                    <button id="menu-btn">
                        <span class="material-icons-sharp">menu</span>
                    </button>
                    <div class="theme-toggler">
                        <span class="material-icons-sharp active">light_mode</span>
                        <span class="material-icons-sharp">dark_mode</span>
                    </div>
                    <div class="profile">
                        <div class="info">
                            <p>Hey, <b>Daniel</b></p>
                            <small class="text-muted">Admin</small>
                        </div>
                        <div class="profile-photo">
                            <a href="#">
                                <img src="../../assets/images/profile/profile-1.jpg" /></a>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
<script>
    const backButton = document.getElementById("back-button");
    backButton.addEventListener("click", function () {
        window.location.href = "DisplayProduct.aspx";
    });
</script>
</html>