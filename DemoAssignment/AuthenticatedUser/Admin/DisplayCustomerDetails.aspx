<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayCustomerDetails.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayCustomerDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>WillNet - eCommerce Website</title>
    <link rel="shortcut icon" href="../../assets/images/will_logo.jpg" type="image" />
    <link rel="stylesheet" href="../../assets/css/Staff.css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons+Sharp" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/admin-style.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css" />
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
                <a href="AdminDashboard.aspx" >
                    <span class="material-icons-sharp">grid_view</span>
                    <h3>Dashboard</h3>
                </a>
                <a href="DisplayCustomer.aspx" class="active">
                    <span class="material-icons-sharp">person_outline</span>
                    <h3>Customers</h3>
                </a>
               <a href="DisplayOrder.aspx"> <%--DisplayOrder--%>
                    <span class="material-icons-sharp">receipt_long</span>
                    <h3>Orders</h3>
                </a>
                <a href="DisplayProduct.aspx">
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
                <h1 id="staff" class="menu-title">>>> Customer Details</h1>               
                <a href="DisplayCustomer.aspx" class="button3">Back to customer list</a>
                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="StaffDetailView" HeaderStyle-BackColor="#96C2DB"  
                    AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource2" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <CommandRowStyle BackColor="white" Font-Bold="True" />
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="white" ForeColor="black" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" />
                    <FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True" />


                    <Fields>
                        <asp:BoundField DataField="id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField"  HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                        
                        <%--<asp:TemplateField HeaderText="Profile" HeaderStyle-CssClass="detailHeaderField" >
                            <ItemTemplate>
                                <asp:Image ID="ProfileImage" ItemStyle-CssClass="ImageItemField" runat="server" ImageUrl='<%# GetProfileImageUrl(Eval("profile")) %>' Width="150px" Height="150px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="name" ItemStyle-CssClass="detailItemField"  ReadOnly="True" HeaderStyle-CssClass="detailHeaderField" HeaderText="Customer Name" SortExpression="name" />
                         <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="NameHiddenField" runat="server" Value='<%# Bind("name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Customer Name" HeaderStyle-CssClass="detailHeaderField" ReadOnly="True" SortExpression="name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="textField">
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("name") %>' CssClass="detailItemField"></asp:TextBox>
                                <asp:Label ID="lblNameError" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
                                    </div>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:BoundField DataField="email" ItemStyle-CssClass="detailItemField"  ReadOnly="True" HeaderStyle-CssClass="detailHeaderField"  HeaderText="Email" SortExpression="email" />
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="EmailHiddenField" runat="server" Value='<%# Bind("email") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%> 
                        <%--<asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="detailHeaderField" SortExpression="email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="textField">
                                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>' CssClass="detailItemField"></asp:TextBox>
                                <asp:Label ID="lblEmailError" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
                                    </div>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:BoundField DataField="dob" ItemStyle-CssClass="detailItemField" ReadOnly="True" HeaderStyle-CssClass="detailHeaderField" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date of Birth" SortExpression="dob" />
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="DOBHiddenField" runat="server" Value='<%# Bind("dob") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> --%>
                        <asp:BoundField DataField="address" ItemStyle-CssClass="detailItemField" ReadOnly="True" HeaderStyle-CssClass="detailHeaderField" HeaderText="Address" SortExpression="address" />           
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="AddressHiddenField" runat="server" Value='<%# Bind("address") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> --%>
                        <%--<asp:TemplateField HeaderText="Address" HeaderStyle-CssClass="detailHeaderField" SortExpression="address">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="textField">
                                <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("address") %>' CssClass="detailItemField"></asp:TextBox>
                                <asp:Label ID="lblAddressError" runat="server" CssClass="errorLabel" Visible="False"></asp:Label>
                                    </div>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:BoundField DataField="status" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Status" SortExpression="status" />--%>
                        <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" >
                            <ItemTemplate>
                                <asp:Label ID="StatusLabel" runat="server" Text='<%# GetStatusText(Eval("status")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:RadioButtonList ID="statusRadioButtonList" runat="server" CssClass="radio-button-list" SelectedValue='<%# Bind("status") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </EditItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/editing.png" runat="server" CssClass="iconClass" CommandName="Edit" ToolTip="Edit" />
                                <%--<asp:ImageButton ImageUrl="~/assets/icons/delete.png" runat="server" CssClass="iconClass" CommandName="Delete" ToolTip="Delete" />--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/save.png" runat="server" CssClass="iconClass" CommandName="Update" ValidationGroup="UpdateValidationGroup" ToolTip="Update" />
                                <asp:ImageButton ImageUrl="~/assets/icons//cancel.png" runat="server" CssClass="iconClass" CommandName="Cancel" ToolTip="Cancel" />
                                <asp:ValidationSummary runat="server" ValidationGroup="UpdateValidationGroup" CssClass="validationSummaryClass" />
                            </EditItemTemplate>

                        </asp:TemplateField>
                    </Fields>

                </asp:DetailsView>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [User] WHERE ([id] = @id) ORDER BY [id], [name], [email]"
                    DeleteCommand="DELETE FROM [User] WHERE [id] = @id"
                    InsertCommand="INSERT INTO [User] ([name], [email], [address], [user_type], [dob], [status], VALUES (@name, @email, @address, @user_type, @dob, @status)"
                    UpdateCommand="UPDATE [User] SET  [status] = @status WHERE [id] = @id">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="1" Name="id" QueryStringField="id" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="email" Type="String" />
<%--                        <asp:Parameter Name="phone_num" Type="String" />--%>
                        <asp:Parameter Name="address" Type="String" />
                        <asp:Parameter Name="name" Type="String" />
                        <asp:Parameter DbType="Date" Name="dob" />
                        <asp:Parameter Name="status" Type="Int32" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="id" Type="Int32" />
                    </DeleteParameters>
                   

                </asp:SqlDataSource>
            </div>
            <div class="right">
                <div class="top">
                    <button id="menu-btn">
                        <span class="material-icons-sharp">menu</span>
                    </button>

                  
                </div>
            </div>
        </div>

    </form>
</body>

</html>