<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayCustomer.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayCustomer" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

            <%--END OF SIDE BAR--%>
            <div class="staffMain">
                <h1 id="staff" class="menu-title">>>> Maintain Customer</h1>
                <asp:GridView ID="GridView1" CssClass="StaffGridView" runat="server" AllowPaging="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id"
                    DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="NO."  ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="id" HeaderText="Customer ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="id">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="name" ItemStyle-CssClass="gridItemField"  HeaderStyle-CssClass="gridHeaderField" HeaderText="Customer Name" SortExpression="name">
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" ItemStyle-CssClass="gridItemField"  HeaderStyle-CssClass="gridHeaderField" HeaderText="Email" SortExpression="email">
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                         
                        <asp:CommandField ShowSelectButton="True" SelectText="View More" ControlStyle-CssClass="selectBtn">
                            <ControlStyle CssClass="selectBtn"></ControlStyle>
                        </asp:CommandField>


                       <%-- <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="gridHeaderField" ItemStyle-CssClass="gridItemField">
                            <ItemTemplate>--%>
                                <%--asp:ImageButton ImageUrl="~/assets/icons/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />--%>
                            <%--</ItemTemplate>
                            <ItemStyle CssClass="gridItemField"></ItemStyle><a href="DisplayOrderDetails.aspx.designer.cs">DisplayOrderDetails.aspx.designer.cs</a>
                        </asp:TemplateField>--%>
                        <asp:HyperLinkField DataNavigateUrlFields="id"  DataNavigateUrlFormatString="~/DisplayCustomerDetails.aspx?id={0}" HeaderText="View More " Visible="False" />


                    </Columns>
                    <EditRowStyle BackColor="#96C2DB" ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#96C2DB" Font-Bold="False" ForeColor="White" Height="40" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" CssClass="gridRowStyle" />
                    <SelectedRowStyle BackColor="white" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>

            </div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                DeleteCommand="DELETE FROM [User] WHERE [id] = @id"
                SelectCommand="SELECT [id], [name], [email] FROM [User] where user_type ='customer' "
                InsertCommand="INSERT INTO [User] ([name], [email]) VALUES (@name, @email)"
                UpdateCommand="UPDATE [User] SET [name] = @name, [email] = @email WHERE [id] = @id">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
<%--                    <asp:Parameter Name="id" Type="String" />--%>
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="name" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="name" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
<%--                    <asp:Parameter Name="id" Type="String" />--%>
                    <asp:Parameter Name="id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

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