<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FYP_WebApplication.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Creating A Board Resolution</div>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </p>
        <br />
        Resolution No:<br />
        Purpose of Board:
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="Appointment of any position">Appointment of any position</asp:ListItem>
            <asp:ListItem>Bank account opening</asp:ListItem>
            <asp:ListItem>Sale of company properties</asp:ListItem>
            <asp:ListItem>Issue related share</asp:ListItem>
            <asp:ListItem>Approval of mergers and acquisitions</asp:ListItem>
            <asp:ListItem>Borrowing of loans</asp:ListItem>
            <asp:ListItem>Dividend Declaration</asp:ListItem>
            <asp:ListItem>Mortgaging company properties</asp:ListItem>
        </asp:DropDownList>
        <p>
            Company Name :
        </p>
        <p>
            Title:</p>
        <p>
            Date:</p>
        <p>
            Time:</p>
        <p>
            Address:</p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
