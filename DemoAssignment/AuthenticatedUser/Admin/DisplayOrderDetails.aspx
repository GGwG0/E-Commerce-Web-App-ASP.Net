<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayOrderDetails.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayOrderDetails" %>
<!DOCTYPE html>
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
    
           <div class="staffMain" >
                <h1 id="staff" class="menu-title">>>> Order Details</h1>               
                <a href="DisplayOrder.aspx" onclick="Backton_Click" class="button3">Back to Order list</a>
            <asp:DetailsView ID="DetailsView1" runat="server"  CssClass="StaffDetailView" HeaderStyle-BackColor="#96C2DB" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <CommandRowStyle BackColor="white" Font-Bold="True" /> <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="white" ForeColor="black" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC"/>
            <FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True" />

            <Fields>
                <asp:BoundField DataField="id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField"  HeaderText="Order ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                 <asp:BoundField DataField="payment_id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField"  HeaderText="Payment ID" ReadOnly="True" SortExpression="id" />
                 <asp:BoundField DataField="delivery_id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField"  HeaderText="Delivery ID" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="user_id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField"  HeaderText="User ID" ReadOnly="True" SortExpression="id" />
                 <asp:BoundField DataField="orderDatetime" ItemStyle-CssClass="detailItemField" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="detailHeaderField"  HeaderText="Order Date" ReadOnly="True" SortExpression="id" />

             <%--   <asp:BoundField DataField="product_id"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product ID" SortExpression="Product ID" />--%>
                <%--<asp:BoundField DataField="variation_id"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Variation ID" ReadOnly="True" SortExpression="Variation ID" />
                <asp:BoundField DataField="purchase_qty"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Purchase Quantity" ReadOnly="True" SortExpression="Purchase Quantity" />--%>
              <%--  <asp:BoundField DataField="rating_id"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="rating_id" ReadOnly="True" SortExpression="rating_id" />--%>
                <%--<asp:BoundField DataField="status"  ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Status" SortExpression="Status" />--%>
                <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="detailHeaderField">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatusText(Eval("status")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="ddlDetailItemField">
                            <asp:ListItem Value="" Text="Please Select an Order Status"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Packaging"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Shipping"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Completed"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions"  HeaderStyle-CssClass="detailHeaderField">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/editing.png" runat="server" CssClass="iconClass" CommandName="Edit" ToolTip="Edit" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/save.png" runat="server" CssClass="iconClass" CommandName="Update" ToolTip="Update" />
                                <asp:ImageButton ImageUrl="~/assets/icons//cancel.png" runat="server" CssClass="iconClass" CommandName="Cancel" ToolTip="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>

            </Fields>
            
        </asp:DetailsView>
               
               <asp:DetailsView ID="DetailsView2" runat="server" DataSourceID="SqlDataSource1" AutoGenerateRows="False"
                   CssClass="StaffDetailView" HeaderStyle-BackColor="#96C2DB"  ForeColor="#333333" >
            <CommandRowStyle BackColor="white" Font-Bold="True" /> <AlternatingRowStyle BackColor="White" />
        <%--    <FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True">--%>
                    <Fields>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Repeater ID="OrderRepeater" runat="server" DataSourceID="SqlDataSource1">
                                    <HeaderTemplate>
                                        <table>
                                            <thead>
                                                <tr class="detailHeaderTableField">
                                                    <th >Product Name</th>
                                                    <th >Variation Name</th>
                                                    <th>Variation Price</th>
                                                    <th>Purchase Quantity</th>
                                                    <th>Total Price</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tableField"><%# Eval("product_name") %></td>
                                            <td class="tableField"><%# Eval("variation_name") %></td>
                                            <td class="tableField"><%# Eval("variation_price") %></td>
                                            <td class="tableField"><%# Eval("purchase_qty") %></td>
                                            <td class="tableField"><%# Eval("total") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                            </tbody>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Fields>
                </asp:DetailsView>



        <asp:SqlDataSource ID="SqlDataSource2" runat="server" OnUpdating="SqlDataSource2_Updating" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT O.*, O.[status]
                            FROM Order_Item AS OI
                            INNER JOIN [Order] AS O ON OI.order_id = O.id
                            WHERE O.id = @id
                            ORDER BY OI.id, OI.variation_id, OI.purchase_qty, OI.rating_id"
            DeleteCommand="DELETE FROM [Order_Item] WHERE [id] = @id" 
            UpdateCommand="UPDATE [Order] SET [status] = @status WHERE [id] = @id">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="id" QueryStringField="id" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="id" Type="Int32" />
                <asp:Parameter Name="product_id" Type="String" />
                <asp:Parameter Name="variation_id" Type="String" />
                <asp:Parameter Name="purchase_qty" Type="String" />
                <asp:Parameter Name="status" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="id" Type="Int32" />
                <asp:Parameter Name="product_id" Type="String" />
                <asp:Parameter Name="variation_id" Type="String" />
                <asp:Parameter Name="purchase_qty" Type="String" />
                <asp:Parameter Name="status" Type="Int32" />
            </InsertParameters>

        </asp:SqlDataSource>

               <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT P.product_name, PV.variation_name, PV.price AS variation_price, OI.purchase_qty, (PV.price * OI.purchase_qty) AS total
                                   FROM Order_Item AS OI
                                   INNER JOIN Product_Variation AS PV ON OI.variation_id = PV.id
                                   INNER JOIN Product AS P ON PV.product_id = P.id
                                   WHERE OI.order_id = @id">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="1" Name="id" QueryStringField="id" Type="Int32" />
                    </SelectParameters>
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