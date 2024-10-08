<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PurchaseHistory.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.PurchaseHistory" %>

<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../assets/css/PurchaseHistory.css" />
    <script src="https://kit.fontawesome.com/a03ff1d483.js" crossorigin="anonymous"></script>
    <style>
        a{
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form runat="server">
        <div class="cart-purchase">
            <div style="display: flex; justify-content: center">
                <h1>Purchase History</h1>
            </div>
            <br />
            <div class="pruchase-history-main-container">
              <div class="Selection-container">
                  <asp:HyperLink runat="server"  CssClass="selection-item" ID="selectionitem"  NavigateUrl="~/AuthenticatedUser/PurchaseHistory.aspx?status=All" >ALL</asp:HyperLink>
                   <asp:HyperLink runat="server" CssClass="selection-item" ID="selectionitem2"  NavigateUrl="~/AuthenticatedUser/PurchaseHistory.aspx?status=to-package" >To Package</asp:HyperLink>
                   <asp:HyperLink runat="server" CssClass="selection-item" ID="selectionitem3"  NavigateUrl="~/AuthenticatedUser/PurchaseHistory.aspx?status=to-ship" >To Ship</asp:HyperLink>
                   <asp:HyperLink runat="server" CssClass="selection-item" ID="selectionitem4"  NavigateUrl="~/AuthenticatedUser/PurchaseHistory.aspx?status=to-completed" >Completed</asp:HyperLink>
</div>
                <br />


                <div>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table class="table-display" style="width: 100%;">
                                <tr>
                                    <th>Order ID</th>
                                    <th>Product</th>
                                    <th></th>
                                    <th>Variation</th>
                                    <th>Unit Price</th>
                                    <th>Quantity</th>
                                    <th>Total Price</th>
                                    <th>Order Status</th>
                                    <th>Action</th>
                                    <th></th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>






                                <td><%# Eval("orderID") %></td>
                                <td>
                                    <a href='Order_Details.aspx?orderItemID=<%# Eval("CartItemId") %>'>
                                        <asp:Image ID="ProductImage" runat="server" ToolTip="Click Me to View Order Details" Width="200" Height="150" ImageUrl='<%# "~/assets/images/products/" + Eval("ImageUrl").ToString() %>' AlternateText='<%# Eval("ProductName") %>' />
                                    </a>

                                </td>
                                <td><a style="text-decoration: none;" href='Order_Details.aspx?orderItemID=<%# Eval("CartItemId") %>'><%# Eval("ProductName") %></a></td>
                                <td><a style="text-decoration: none;" href='Order_Details.aspx?orderItemID=<%# Eval("CartItemId") %>'><%# Eval("variation_name") %></a></td>
                                <td>RM <%# Eval("UnitPrice") %></td>
                                <td><%# Eval("Quantity") %></td>
                                <td>RM <%# Eval("Price") %></td>
                                <td runat="server" id="status"></td>
                                <td><asp:Button runat="server" ID="viewDetails"  CssClass="rate_button" PostBackUrl='<%# "Order_Details.aspx?orderItemID=" + Eval("CartItemId") %>' Text='Details' /></td>

                                <td>
                                    <asp:HiddenField runat="server" id="RatingID" Value='<%# Eval("RatingID") %>'/>
                                    <asp:HiddenField runat="server" id="Statusvalue" Value='<%# Eval("Status") %>'/>

                                    <asp:Button ID="myBtn" runat="server" CssClass="rate_button" Text="RATE" OnClick="myBtn_Click" CommandArgument='<%# Eval("CartItemId") + "," + Eval("ProductName") + "," + Eval("variation_name") + "," + Eval("ImageUrl") + "," + Eval("VariationID") %>' /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>


                </div>

            </div>
        </div>

    </form>
</asp:Content>