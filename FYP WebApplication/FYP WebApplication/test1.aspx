<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="FYP_WebApplication.test1" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
   <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
</asp:Content>