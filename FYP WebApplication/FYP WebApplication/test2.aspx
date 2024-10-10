<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test2.aspx.cs" Inherits="FYP_WebApplication.test2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://cdn.rawgit.com/davidshimjs/qrcodejs/gh-pages/qrcode.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">


        <div>
               <div style="margin-left:28%;" id="qrcode"></div>
        </div>

        <asp:Button OnClick="Unnamed_Click"   runat="server"/>
          <script>
                   // Generate and display the QR code

            var userGoogleAuthKey = "<%=Request.QueryString["id"] %>";
            var appName = "YourDesiredAppName"; // Replace with your desired app name
            var qr = new QRCode(document.getElementById("qrcode"), {
                text: "otpauth://totp/" + appName + ":" + userGoogleAuthKey + "?secret=" + userGoogleAuthKey,
                width: 300,
                height: 300
            });
          </script>
    </form>
</body>
</html>
