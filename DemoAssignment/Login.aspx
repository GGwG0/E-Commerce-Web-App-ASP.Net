<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DemoAssignment.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script
        src="https://kit.fontawesome.com/64d58efce2.js"
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="assets/css/register-login.css" />
    <%--<link rel="stylesheet" href="assets/css/user-style.css" />--%>

    <title></title>
</head>
<body>

 <style>
    #lblMessage {
        text-align: center;
        color:red;
        font-size:17px;
    }
</style>

    <form id="form1" runat="server">


        <div class="container">
            <div class="forms-container">
                <div class="signin-signup">
                    <asp:Login ID="Login1" runat="server"  CssClass="form sign-in-form" UserNameLabelText="Email">
                        <LayoutTemplate>
                            <h2 class="title">Sign in</h2>
                            <div class="input-field">
                                <i class="fas fa-user"></i>
                                <asp:TextBox ID="UserName" runat="server" CssClass="input" placeholder="User Name"></asp:TextBox>
                            </div>
                            <div class="input-field">
                                <i class="fas fa-lock"></i>
                                <asp:TextBox ID="Password" runat="server" CssClass="input" TextMode="Password" placeholder="Password"></asp:TextBox>
                            </div>
                            <div class="links">
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hyperlink" NavigateUrl="~/ForgotPassword.aspx">Forgot Password</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hyperlink" NavigateUrl="~/Index.aspx">Back To Home</asp:HyperLink>
                            </div>
                            <asp:Button ID="LoginButton" runat="server" CssClass="btn solid" Text="Login" CommandName="Login"  OnClick="LoginButton_Click" />
                        </LayoutTemplate>
                    </asp:Login>
                    <asp:Label ID="lblMessage" runat="server" />


                </div>
            </div>
   

            <div class="another-panels-container">
                <div class="panel right-panel">
                    <div class="content2">
                        <h3>New here ?</h3>
                        <p>
                            Click the button to sign up here!!
                        </p>
                        <asp:Button ID="Button3" CssClass="btn transparent" runat="server" Text="SIGN UP" OnClick="Button3_Click" /><br />

                        <%--<button class="btn transparent" id="sign-up-btn">--%>
                    </div>
                    <img src="./assets/images/login.png" class="image" alt="" />
                </div>
            </div>
        </div>
    </form>

</body>
</html>