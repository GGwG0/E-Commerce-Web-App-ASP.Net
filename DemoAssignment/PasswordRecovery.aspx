<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="DemoAssignment.PasswordRecovery" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script
        src="https://kit.fontawesome.com/64d58efce2.js"
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./assets/css/register-login.css" />
    <%--<link rel="stylesheet" href="./assets/css/user-style.css" />--%>

    <title></title>
    <style>
        .ques {
            display: inline;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="forms-container">
                <div class="forgotpwd">
                    <asp:ChangePassword ID="ChangePassword1" runat="server" OnChangedPassword="ChangePassword1_ChangedPassword" DisplayUserName="true">
                        <ChangePasswordTemplate>
                            <h2 class="title">Change Password</h2>

                            <div class="input-field">
                                <i class="fas fa-user"></i>
                                <asp:TextBox ID="Username" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                      
                            </div>
                            <div class="input-field">
                                <i class="fas fa-lock"></i>
                                <asp:TextBox ID="CurrentPassword" runat="server" CssClass="input" TextMode="Password" placeholder="Current Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server"
                                    ControlToValidate="CurrentPassword" ErrorMessage="Current Password is required."
                                    ToolTip="Current Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="input-field">
                                <i class="fas fa-lock"></i>
                                <asp:TextBox ID="NewPassword" runat="server" CssClass="input" TextMode="Password" placeholder="New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server"
                                    ControlToValidate="NewPassword" ErrorMessage="New Password is required."
                                    ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                  <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" font-size="Medium" ForeColor="Red" Display="Dynamic" Font-Bold="true" ControlToValidate="NewPassword" ErrorMessage="Password must be minimum 8 characters, at least 1 special characters and at least 1 capital letter." ValidationExpression="^(?=.*[\W_])(?=.*[A-Z])[a-zA-Z\d\W_]{8,}$" ></asp:RegularExpressionValidator>--%>
                            </div>
                            <div class="input-field">
                                <i class="fas fa-lock"></i>
                                <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="input" TextMode="Password" placeholder="Confirm New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server"
                                    ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is required."
                                    ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is not same as New Password" ValidationGroup="ChangePassword1"></asp:CompareValidator>--%>
                            </div>
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>

                            <div class="links">
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hyperlink" NavigateUrl="~/ForgotPassword.aspx">Forgot Password</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hyperlink" NavigateUrl="~/Index.aspx">Back To Home</asp:HyperLink>
                            </div>
                             <asp:Button ID="ChangePasswordButton" CssClass="btn solid" runat="server" CommandName="ChangePassword" Text="Change Password" ValidationGroup="ChangePassword1" />
                            <asp:Button ID="CancelButton" runat="server"  CssClass="btn solid" CausesValidation="False" Text="Cancel" CommandName="Cancel" OnClick="CancelButton_Click" />
                        </ChangePasswordTemplate>
                        <SuccessTemplate>
                              <div class="title">
                                       Change Password Complete

                                    </div>
                                    <p class="banner-subtitle">Your password has been changed!</p>
                                    <asp:Button ID="ContinueButton" runat="server"
                                        CausesValidation="False" CommandName="Continue"
                                        CssClass="btn solid" Text="Continue" ValidationGroup="ChangePassword1" OnClick="ContinueButton_Click" />
                        </SuccessTemplate>
                    </asp:ChangePassword>

                </div>
            </div>
        </div>
        <div class="panels-container">
            <div class="panel left-panel" style="">
                <img src="./assets/images/forgot.png" class="image" alt="" style="width: 700px; height: 500px;" />
            </div>

        </div>
    </form>
</body>
</html>
