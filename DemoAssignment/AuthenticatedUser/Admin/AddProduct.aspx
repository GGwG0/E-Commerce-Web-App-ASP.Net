<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WillNet - eCommerce Website</title>
    <link rel="shortcut icon" href="../../assets/images/will_logo.jpg" type="image">

    <link href="https://fonts.googleapis.com/icon?family=Material+Icons+Sharp" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/admin-style.css" />
    <link rel="stylesheet" href="../../assets/css/add_staff_product.css" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.19/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.19/dist/sweetalert2.min.js"></script>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 247px;
        }

        .auto-style3 {
            width: 247px;
            height: 18px;
        }

        .auto-style4 {
            height: 18px;
        }
    </style>

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
                <a href="AddProduct.aspx"  class="active">
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

        <div>
            <h1 class="info-text">>>> Add Product</h1>

            <form id="form1" runat="server">
                <div class="wrap">
                    <div class="staff-box" >

                        <div class="staff-container">

                            <table class="auto-style1">
                                <tr>
                                    <td class="auto-style3">Product Name :</td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtProductName" placeholder="Product Name" CssClass="form-element input-field" runat="server"></asp:TextBox>
                                    </td>
                                </tr>           
                                <tr>
                                    <td class="auto-style2">Product Category:</td>
                                    <td>

                                        <asp:DropDownList ID="ddlProdCat" runat="server" CssClass="ddlDetailItemField" OnSelectedIndexChanged="ddlProdCat_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="" Text="Please Select a Category :-"></asp:ListItem>
                                            <asp:ListItem Value="cat1">Data Storage</asp:ListItem>
                                            <asp:ListItem Value="cat2">Monitors</asp:ListItem>
                                            <asp:ListItem Value="cat3">Computer Accessories</asp:ListItem>
                                            <asp:ListItem Value="cat4">Network Components & IP Cameras</asp:ListItem>
                                            <asp:ListItem Value="cat5">Printers & Ink</asp:ListItem>
                                            <asp:ListItem Value="cat6">Mobile & Tablets</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Product Type:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProdType" placeholder="Product Type" CssClass="ddlDetailItemField" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
               
                                <tr>
                                    <td class="auto-style2">Description:</td>
                                    <td>
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-element input-field textarea" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Stock Status:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlStockStatus" CssClass="input-field ddlList" runat="server">
                                            <asp:ListItem Value="0">Not available yet</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Available</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr
                                 <tr>
                                    <td class="auto-style2">Status:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" CssClass="input-field ddlList" runat="server">
                                            <asp:ListItem Value="0">Not Listed</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Listed</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Product Image 1:</td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload1" runat="server" Css-class="file-upload" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Product Image 2:</td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload2" runat="server" Css-class="file-upload" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Product Image 3:</td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload3" runat="server" Css-class="file-upload"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Variation 1:</td>
                                    <td>
                                          <asp:TextBox ID="variation1" CssClass="form-element variation_input_field"  runat="server"></asp:TextBox>
                                          Price: <asp:TextBox ID="price1" Text="0.00" CssClass="form-element input-field" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                                        
                                          Quantity:<asp:TextBox ID="quantity1" Text="0" CssClass="form-element input-field" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Variation 2:</td>
                                    <td>
                                        <asp:TextBox ID="variation2" CssClass="form-element variation_input_field" runat="server"></asp:TextBox>
                                         Price: <asp:TextBox ID="price2" Text="0.00" CssClass="form-element input-field" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                                        
                                        Quantity: <asp:TextBox ID="quantity2"   Text="0" CssClass="form-element input-field" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2">Variation 3:</td>
                                    <td>
                                        <asp:TextBox ID="variation3"  CssClass="form-element variation_input_field"  runat="server"></asp:TextBox>
                                        Price: <asp:TextBox ID="price3" Text="0.00" CssClass="form-element input-field" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                                        Quantity: <asp:TextBox ID="quantity3"  Text="0" CssClass="form-element input-field" Width="100px" TextMode="Number" MaxLength="300" runat="server"></asp:TextBox>
                                       
                                    </td>
                                </tr>
                            </table>
                                           <br />
                            <div class="form-btn">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <asp:Button ID="btnClear" CssClass="addProductBtnLeft " runat="server" Text="Clear" OnClick="btnClear_Click" />
                                <asp:Button ID="btnSubmit" CssClass="addProductBtnRight" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                </div>
                            <br />
                            <asp:Label ID="output" runat="server"></asp:Label>

                        </div>
                    </div>
                </div>
            </form>
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
</body>
</html>