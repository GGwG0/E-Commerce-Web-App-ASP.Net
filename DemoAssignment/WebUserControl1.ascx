<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="DemoAssignment.WebUserControl1" %>
<asp:HyperLink ID="hplSignUp" runat="server" ToolTip="Sign Up" NavigateUrl="~/Register.aspx" CssClass="">               
    <span class="material-icons-sharp icon">person_add_alt_1</span><span>Sign Up</span>
</asp:HyperLink>
<asp:HyperLink ID="hplLogin" runat="server" ToolTip="Login" NavigateUrl="~/Login.aspx" CssClass="">
    <span class="material-icons-sharp icon">login</span><span>Login</span>
</asp:HyperLink>


<asp:HyperLink ID="hplCart" runat="server" ToolTip="Cart" NavigateUrl="~/AuthenticatedUser/Cart.aspx" CssClass="action-btn">
    <span class="material-icons-sharp icon" style="margin-top: 22px;">shopping_cart</span>
</asp:HyperLink>

<div class="dropdown" id="account" runat="server">
    <span class="material-icons-sharp icon">account_circle</span>
    <div class="dropdown-content">
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/AuthenticatedUser/ViewProfile.aspx" CssClass="">Profile</asp:HyperLink>
        <asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/AuthenticatedUser/ResetPassword.aspx" CssClass="">Change Password</asp:HyperLink>
        <asp:HyperLink ID="HyperLink28" runat="server" NavigateUrl="~/AuthenticatedUser/PurchaseHistory.aspx" CssClass="action-btn">Purchase History</asp:HyperLink>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Index.aspx?command=logout" CssClass="action-btn">Log Out</asp:HyperLink>
    </div>
</div>