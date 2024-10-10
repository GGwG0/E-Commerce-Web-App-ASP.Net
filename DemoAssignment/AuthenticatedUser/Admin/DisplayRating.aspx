<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayRating.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayRating" %>

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


    <%--<link rel="stylesheet" href="assets/css/Staff.css">--%>
</head>

<body>
    <form id="form2" runat="server">
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
                <a href="DisplayCustomer.aspx">
                    <span class="material-icons-sharp">person_outline</span>
                    <h3>Customers</h3>
                </a>
               <a href="DisplayOrder.aspx" > <%--DisplayOrder--%>
                    <span class="material-icons-sharp">receipt_long</span>
                    <h3>Orders</h3>
                </a>
                <a href="DisplayProduct.aspx" >
                    <span class="material-icons-sharp">inventory</span>
                    <h3>Products</h3>
                </a>
                 <a href="DisplayRating.aspx"  class="active">
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
                <h1 id="staff" class="menu-title">>>> Maintain Ratings</h1>
                <%--<a href="AddStaff.aspx" class="addRoleBtn">Add </a>--%>

                <asp:GridView ID="GridView1" CssClass="StaffGridView" runat="server" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="id"
                    DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="NO." ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:BoundField DataField="id" HeaderText="Rating ID" InsertVisible="False" ReadOnly="True"  SortExpression="id" />
                        <asp:BoundField DataField="user_id" HeaderText="User ID" InsertVisible="False" ReadOnly="True" SortExpression="user_id" />
                        <asp:BoundField DataField="name" HeaderText="Username" SortExpression="name" />
                        <%--<asp:BoundField DataField="rating_num" HeaderText="Rating Num" SortExpression="rating_num" />--%>
                        <asp:TemplateField HeaderText="Rating Num" ItemStyle-CssClass="gridItemField" SortExpression="rating_num" >
                            <ItemTemplate>
                                <asp:Literal ID="litStars" runat="server" Text='<ion-icon name="star"></ion-icon><ion-icon name="star"></ion-icon>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="comment" HeaderText="Comment" SortExpression="comment" />
                        <asp:BoundField DataField="adminReply" HeaderText="Admin Reply" SortExpression="adminReply" />
                        <asp:BoundField DataField="product_name" HeaderText="Product Name" SortExpression="product_name" />
                        

                         <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="selectBtn">
                            <ControlStyle CssClass="selectBtn"></ControlStyle>
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="gridItemField">
                            <ItemTemplate>
                              <%--  <asp:ImageButton ImageUrl="~/assets/icons/rate-icon.png" runat="server" OnClick="replyComment_Click" ToolTip="Reply" Width="20px" Height="20px" />--%>
                                <asp:ImageButton ImageUrl="~/assets/icons/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gridItemField"></ItemStyle>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/DisplayCustomerDetailss.aspx?id={0}" HeaderText="View More " Visible="False" />
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
                DeleteCommand="DELETE FROM [Product_Rating] WHERE [id] = @id"
                SelectCommand="SELECT R.id, R.user_id, U.name, R.rating_num , R.comment, ISNULL(R.adminReply, '') AS adminReply, R.created_at, P.product_name, R.variation_id, V.variation_name 
                                FROM Product_Rating R
                                LEFT JOIN Product_Variation V ON R.variation_id = V.id
                                LEFT JOIN Product P ON V.product_id = P.id
                                LEFT JOIN [User] U ON R.user_id = U.id;"
                UpdateCommand="UPDATE [Product_Rating] SET [name] = @name, [email] = @email WHERE [id] = @id">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="int32" />
                </DeleteParameters>


                <%--<InsertParameters>
                    <asp:Parameter Name="name" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="name" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="id" Type="Int32" />
                </UpdateParameters>--%>
            </asp:SqlDataSource>




            <div class="right">
                <div class="top">
                    <button id="menu-btn">
                        <span class="material-icons-sharp">menu</span>
                    </button>
                    <div class="theme-toggler">
                        <span class="material-icons-sharp active">light_mode</span>
                        <span class="material-icons-sharp">dark_mode</span>
                    </div>
                  
                </div>

            </div>
        </div>
    </form>

        <!--- ionicon link  -->
    <script type="module" src="https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.js"></script>
</body>
</html>