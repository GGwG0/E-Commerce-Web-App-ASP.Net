<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayRatingDetails.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Admin.DisplayRatingDetails" %>

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

            <div class="staffMain">
                 <h1 id="staff" class="menu-title">>>> Rating Details</h1> 
                <%--<h1 id="staff"><asp:Button ID="backButton" runat="server" Text="Back" OnClick="BackButton_Click" /></h1> --%>
                <a href="DisplayRating.aspx" onclick="Backton_Click" class="button3">Back to Rating list</a>
                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="StaffDetailView" HeaderStyle-BackColor="#96C2DB"  AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource2" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <CommandRowStyle BackColor="white" Font-Bold="True" />
                    <%--<AlternatingRowStyle BackColor="White"  />
               <%-- <RowStyle BackColor="#EFF3FB" />
<%--            <EditRowStyle BackColor="#"  ForeColor="White"/>--%>
                    <%--<FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True" />--%>

                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="white" ForeColor="black" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" />
                    <FieldHeaderStyle BackColor="#96C2DB" Font-Bold="True" />

                    <Fields>
                        <asp:BoundField DataField="id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="created_at" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Created At" SortExpression="created_at" ReadOnly="True" />
                        <asp:BoundField DataField="product_name" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Product Name" SortExpression="product_name" ReadOnly="True" />
                        <asp:BoundField DataField="variation_id" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="Variation ID" SortExpression="variation_id" ReadOnly="True" />
                        <asp:BoundField DataField="adminReply" ItemStyle-CssClass="detailItemField" HeaderStyle-CssClass="detailHeaderField" HeaderText="adminReply" SortExpression="adminReply" />
                        <asp:TemplateField HeaderText="Reply"  HeaderStyle-CssClass="detailHeaderField">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/rate-icon.png" runat="server" CssClass="iconClass" CommandName="Edit" ToolTip="Edit" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/assets/icons/save.png" runat="server" CssClass="iconClass" CommandName="Update" ToolTip="Update" />
                                <asp:ImageButton ImageUrl="~/assets/icons//cancel.png" runat="server" CssClass="iconClass" CommandName="Cancel" ToolTip="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Fields>

                </asp:DetailsView>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    DeleteCommand="DELETE FROM [Product_Rating] WHERE [id] = @id"
                    SelectCommand="SELECT R.id, R.user_id, U.name, R.rating_num , R.comment, ISNULL(R.adminReply, '') AS adminReply, R.created_at, P.product_name, R.variation_id, V.variation_name 
                                FROM Product_Rating R
                                LEFT JOIN Product_Variation V ON R.variation_id = V.id
                                LEFT JOIN Product P ON V.product_id = P.id
                                LEFT JOIN [User] U ON R.user_id = U.id WHERE r.id = @id;"
                    UpdateCommand="UPDATE [Product_Rating] SET  [adminReply] = @adminReply WHERE id = @id">
                    <DeleteParameters>
                        <asp:Parameter Name="id" Type="String" />
                    </DeleteParameters>
                 <SelectParameters>
    <asp:Parameter  Name="id" Type="Int32" DefaultValue="1" />
</SelectParameters>
                       <UpdateParameters>
    <asp:Parameter  Name="id" Type="Int32" DefaultValue="1" />
</UpdateParameters>

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
            </div>
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
</body>
<script>
    const backButton = document.getElementById("back-button");
    backButton.addEventListener("click", function () {
        window.location.href = "DisplayProduct.aspx";
    });


</script>
</html>