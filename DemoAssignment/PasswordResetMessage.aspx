<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordResetMessage.aspx.cs" Inherits="DemoAssignment.PasswordResetMessage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>Dear user,</p>
    <p>We have received a request to reset your password.</p>
    <p>Please click on the following link to answer the security questions and reset your password:</p>
    <p><a href="<%= PasswordRecovery1.QuestionTemplateUrl %>">Reset Password</a></p>
    <p>If you did not request a password reset, please ignore this email.</p>
    <p>Thank you,</p>
    <p>Your Website Team</p>
    </form>
</body>
</html>
