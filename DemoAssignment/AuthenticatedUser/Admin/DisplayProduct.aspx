<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayProduct.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayProduct" %>

<html>
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
                <a href="AdminDashboard.aspx">
                    <span class="material-icons-sharp">grid_view</span>
                    <h3>Dashboard</h3>
                </a>
                <a href="DisplayCustomer.aspx" >
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

            <%--END OF SIDE BAR--%>
            <div class="staffMain">
                <h1 id="staff" class="info-text">>>> Maintain Product</h1>
                <a href="AddProduct.aspx" class="addRoleBtn">Add Product</a>
                <asp:GridView ID="GridView1" CssClass="StaffGridView" runat="server" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id"
                    DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" Style="height: 30%;">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="NO." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="id" HeaderText="Product ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="id">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="product_name" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Product Name" SortExpression="product_name">
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="product_type" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Product Type" SortExpression="product_type">
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="product_category" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Product Category" SortExpression="product_category">
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField> 
                         <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="selectBtn">
                            <ControlStyle CssClass="selectBtn"></ControlStyle>
                        </asp:CommandField>
                        <%--<asp:TemplateField HeaderText="Action" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField">
                            <ItemTemplate>
                                <%--<asp:ImageButton ImageUrl="~/assets/icons/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />--%>
                            <%--</ItemTemplate>
                            <ItemStyle CssClass="gridItemField"></ItemStyle>
                        </asp:TemplateField>--%>
                        <asp:HyperLinkField  DataNavigateUrlFields="id"  DataNavigateUrlFormatString="~/DisplayProductDetail.aspx?id={0}" HeaderText="View More" Visible="False" />
                    </Columns>
                    <EditRowStyle BackColor="#96C2DB" ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle Height="40" BackColor="#96C2DB" Font-Bold="False" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" CssClass="gridRowStyle" />
                    <SelectedRowStyle BackColor="white" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                DeleteCommand="DELETE FROM [Product] WHERE [id] = @id"
                InsertCommand="INSERT INTO [Product] ([product_type], [product_name]) VALUES (@product_type, @product_name)"
                SelectCommand="SELECT * FROM [Product]"
                UpdateCommand="UPDATE [Product] SET [product_type] = @product_type, [product_name] = @product_name WHERE [id] = @id">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="product_type" Type="String" />
                    <asp:Parameter Name="product_name" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="product_type" Type="String" />
                    <asp:Parameter Name="product_name" Type="String" />
                    <asp:Parameter Name="id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            </div>
            <div class="right">
                <div class="top">
                    <button id="menu-btn">
                        <span class="material-icons-sharp">menu</span>
                    </button>
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
</html>