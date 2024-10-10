<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayReport.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>WillNet - eCommerce Website</title>
    <link rel="shortcut icon" href="../../assets/images/will_logo.jpg" type="image" />
    <link rel="stylesheet" href="../../assets/css/Staff.css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons+Sharp" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/admin-style.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css" />

    <style type="text/css">
        /* Styling for the tables */
        .table-display {
            border-collapse: collapse;
            width: 100%;
            margin-bottom: 20px;
        }

            /* Styling for table headers */
            .table-display th {
                background-color: #3f51b5;
                color: #fff;
                text-align: center;
                padding: 10px;
                border: 1px solid #ddd;
            }

            /* Styling for table rows */
            .table-display tr {
                text-align: center;
                border: 1px solid #ddd;
            }

                /* Styling for alternating rows */
                .table-display tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

            /* Styling for table cells */
            .table-display td {
                padding: 10px;
                border: 1px solid #ddd;
            }

        /* Styling for product images */
        .product-image {
            max-width: 200px;
            max-height: 150px;
        }

        .rdb {
            margin-top: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }

            .rdb label {
                margin-right: 10px;
            }

            .rdb input[type="radio"] {
                display: none;
            }

                .rdb input[type="radio"] + label:before {
                    content: "";
                    display: inline-block;
                    vertical-align: middle;
                    width: 20px;
                    height: 20px;
                    margin-right: 5px;
                    border: 2px solid #333;
                    border-radius: 50%;
                }

                .rdb input[type="radio"]:checked + label:before {
                    background-color: #333;
                }

            .rdb label {
                font-size: 18px;
                margin-left: 5px;
                color: #333;
                cursor: pointer;
            }
    </style>

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
                 <a href="DisplayRating.aspx"  >
                    <span class="material-icons-sharp">star_outline</span>
                    <h3>Ratings</h3>
                </a>
                <a href="DisplayReport.aspx" class="active"><%--DisplayReport.aspx--%>
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
                <h1 id="staff" class="menu-title">>>> Report</h1>
                <div class="rdb">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Top 10 Hot Sales Product" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Top Products Requiring Restock"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <div runat="server" id="div1">
                    <h2>Top 10 Hot Sales Product</h2>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table class="table-display" style="width: 100%;">
                                <tr>
                                    <th>No</th>
                                    <th>Product ID</th>
                                    <th>Product Name</th>
                                    <th>Image</th>
                                    <th>Variation </th>
                                    <th>Sold Amount</th>
                                    <th>Unit Price</th>
                                    <th>Total Price</th>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("id") %></td>
                                <td><%# Eval("ProductName") %></td>
                                <td>
                                    <asp:Image ID="ProductImage" runat="server" Width="200" Height="150" ImageUrl='<%# "~/assets/images/products/" + Eval("ImageUrl").ToString() %>' /></td>
                                <td><a style="text-decoration: none;"><%# Eval("variation_name") %></a></td>
                                <td><%# Eval("total_qty") %></td>
                                <td>RM <%# Eval("price") %></td>
                                <td>RM <%# Eval("total_price") %></td>

                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </div>
                <div runat="server" id="div2">
                    <h2>Top Products Requiring Restock</h2>
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table-display" style="width: 100%;">
                                <tr>
                                    <th>No</th>
                                    <th>Variation Name</th>
                                    <th>Variation ID</th>
                                    <th>Product Image</th>
                                    <th>Product Name</th>
                                    <th>Product ID</th>
                                    <th>Stock Quantity</th>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("variation_name") %></td>
                                <td><%# Eval("variation_id") %></td>
                                <td>
                                    <asp:Image ID="ProductImage" runat="server" Width="200" Height="150" ImageUrl='<%# "~/assets/images/products/" + Eval("product_image_1").ToString() %>' /></td>
                                <td><%# Eval("product_name") %></td>
                                <td><%# Eval("product_id") %></td>
                                <td>
                                    <asp:Label ID="lblStockQuantity" runat="server" Text='<%# Eval("stock_quantity") %>'></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>


        </div>
    </form>
</body>
</html>