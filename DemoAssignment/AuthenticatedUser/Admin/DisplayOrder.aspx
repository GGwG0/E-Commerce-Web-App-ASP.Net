<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayOrder.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayOrder" %>

<html>
     
<head>
    <style>
        td{
            text-align:center;
        }
    </style>
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
               <a href="DisplayOrder.aspx" class="active"> <%--DisplayOrder--%>
                    <span class="material-icons-sharp">receipt_long</span>
                    <h3>Orders</h3>
                </a>
                <a href="DisplayProduct.aspx">
                    <span class="material-icons-sharp">inventory</span>
                    <h3>Products</h3>
                </a>
                 <a href="DisplayRating.aspx" >
                    <span class="material-icons-sharp">star_outline</span>
                    <h3>Ratings</h3>
                </a>
                <a href="DisplayReport.aspx"><%----%>
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
                <h1 id="staff">>>> Maintain Orders</h1>

      
                    <asp:GridView ID="GridView1" CssClass="StaffGridView" runat="server" AllowPaging="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id"
                    DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="White" />

                    <Columns>
                        <asp:TemplateField HeaderText="NO." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="id" HeaderText="Order ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="id"> </asp:BoundField>
                        <asp:BoundField DataField="orderDatetime" ItemStyle-CssClass="gridItemField" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-CssClass="gridHeaderField" HeaderText="Order Date" SortExpression="Order Date"></asp:BoundField>
                       <%-- <asp:BoundField DataField="Status" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Order Status" SortExpression="Order Status"> </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="detailHeaderField">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatusText(Eval("status")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                         <asp:CommandField ShowSelectButton="True" SelectText="View More" ControlStyle-CssClass="selectBtn">
                            <ControlStyle CssClass="selectBtn"></ControlStyle>
                        </asp:CommandField>
                        

                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/DisplayOrderDetails.aspx?id={0}" HeaderText="View More " Visible="False" />
                    </Columns>
                    <EditRowStyle BackColor="#96C2DB" ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#96C2DB" Font-Bold="False" ForeColor="White" />
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
                DeleteCommand="DELETE FROM [Order] WHERE [id] = @id"
                InsertCommand="INSERT INTO [Order] ([orderDatetime], [status]) VALUES (@orderDatetime, @status)"
                SelectCommand="SELECT [id], [orderDatetime], [status] FROM [Order]"
                UpdateCommand="UPDATE [Order] SET [orderDatetime] = @orderDatetime, [status] = @status WHERE [id] = @id">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="orderDatetime" Type="String" />
                    <asp:Parameter Name="status" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="orderDatetime" Type="String" />
                    <asp:Parameter Name="status" Type="String" />
                    <asp:Parameter Name="id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

            


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