<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.AdminDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>WillNet - eCommerce Website</title>
    <link rel="shortcut icon" href="../../assets/images/will_logo.jpg" type="image">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons+Sharp" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/admin-style.css" />

</head>

<body>
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
                <a href="AdminDashboard.aspx" class="active">
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
        <main>
            <h1 class="info-text" style="margin-top:0px;">Dashboard</h1>

           
                <form runat="server" id="form1">

                    <div class="insights">
                
                        <div class="sales">
                            <span class="material-icons-sharp">analytics</span>
                            <div class="middle">
                                <div class="left">
                                    <h3>Total Sales</h3>
                                    <h1>RM<asp:Literal ID="lblTotalSalesWithin24" runat="server"></asp:Literal></h1>
                                </div>
                                <div class="progress">
                                    <svg runat="server" id="salesSVG">
                                        <circle id="salesCircle" cx="38" cy="38" r="36"></circle>
                                    </svg>
                                    <div class="number">
                                        <p><asp:Literal ID="lblPercentage" runat="server"></asp:Literal>%</p>
                                    </div>
                                </div>
                            </div>
                            <small class="text-muted">Last 24 Hours</small>
                        </div>

                        <div class="expenses">
                            <span class="material-icons-sharp">bar_chart</span>
                            <div class="middle">
                                <div class="left">
                                    <h3>Total Expenses</h3>
                                    <h1>RM<asp:Literal ID="lblExpense" runat="server"></asp:Literal></h1>
                                </div>
                                <div class="progress">
                                    <svg id="svgExpense" runat="server">
                                        <circle cx="38" cy="38" r="36" id="circleExpenses" runat="server"></circle>
                                    </svg>
                                    <div class="number">
                                        <p><asp:Literal ID="lblExpensePercent" runat="server"></asp:Literal>%</p>
                                    </div>
                                </div>
                            </div>
                            <small class="text-muted">Last 24 Hours</small>
                        </div>
                        <div class="income">
                            <span class="material-icons-sharp">stacked_line_chart</span>
                            <div class="middle">
                                <div class="left">
                                    <h3>Total Income</h3>
                                    <h1>RM <asp:Literal ID="lblIncome" runat="server"></asp:Literal></h1>
                                </div>
                                <div class="progress">
                                    <svg>
                                        <circle cx="38" cy="38" r="36"></circle>
                                    </svg>
                                    <div class="number">
                                        <p><asp:Literal ID="lblIncomePercent" runat="server"></asp:Literal>%</p>
                                    </div>
                                </div>
                            </div>
                            <small class="text-muted">Last 24 Hours</small>
                        </div>
               
                    </div>
                </form>

            <%--Recent order--%>
            <div class="recent-orders">
                <h2>Recent Orders</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Order ID</th>
                            <th>Order Status</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                       <asp:Repeater ID="OrderRepeater" runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("product_name") %></td>
                                <td><%# Eval("order_id") %></td>
                                <td><%# GetStatusText(Eval("status")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                </table>
                <a href="DisplayOrder.aspx">Show All</a>

            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT P.product_name, O.id AS order_id, O.status
                               FROM [Order] AS O
                               INNER JOIN Order_Item AS OI ON O.id = OI.order_id
                               INNER JOIN Product_Variation AS PV ON OI.variation_id = PV.id
                               INNER JOIN Product AS P ON PV.product_id = P.id">
            </asp:SqlDataSource>
        </main>
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
   
    <script src="../../assets/js/admin.js"></script>

</body>
</html>