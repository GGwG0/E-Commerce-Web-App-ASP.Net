<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewProfile.aspx.cs" Inherits="DemoAssignment.ViewProfile" %>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../assets/css/Profile.css">
    <link rel="shortcut icon" href="../assets/images/logo/favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css">
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="product-box">
        <form runat="server">
              <div class="profileContainer">
                <div class="leftContainer">
                    <div class="profileImg">
                        <asp:Image ID="imgProfile" runat="server" Height="200" Width="200" CssClass="imgProfile" />
                        <asp:Label runat="server" ID="lblName" CssClass="lblName" />
                          
                       <%-- <asp:HyperLink ID="hpyResetpassword" runat="server" NavigateUrl="~/AuthenticatedUser/ResetPassword.aspx">Reset Password</asp:HyperLink>
                       --%>
                        <%-- <asp:FileUpload ID="profileImg-Ctrl" runat="server" />--%>
                    </div>
                </div>
                <div class="rightContainer">

                    <h1>My Profile </h1>
                    <div class="row">
                        <h3><span>ID : </span>
                            <asp:Label ID="id" runat="server"></asp:Label></h3>
                        <h3><span>Login Name : </span>
                            <asp:Label ID="login_name" runat="server"></asp:Label></h3>
                        <h3><span>Name : </span>
                            <asp:Label ID="name" runat="server"></asp:Label></h3>
                        <h3><span>Phone number : </span>
                            <asp:Label ID="phone_num" runat="server"></asp:Label></h3>
                        <h3><span>Email Address : </span>
                            <asp:Label ID="email" runat="server"></asp:Label></h3>
                        <h3><span>Address : </span>
                            <asp:Label ID="address" runat="server"></asp:Label></h3>
                        <h3><span>Date of Birth  : </span>
                            <asp:Label ID="dob" runat="server"></asp:Label></h3>
                        <asp:Button ID="edit_btn" Width="150px" runat="server" Text="Edit" PostBackUrl="~/AuthenticatedUser/EditProfile.aspx" CssClass="fas fa-user-edit add-cart-btn"/>
                   
                    </div>
                </div>
            </div>
        </form>
          
       
    </div>

</asp:Content>