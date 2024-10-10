<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DisplayRoleDetail.aspx.cs" Inherits="FYP_WebApplication.DisplayRoleDetail" %>


<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/signature_pad@2.5.3/dist/signature_pad.css" />
    <style>
        .content-1 {
            min-width: 90%;
        }

        .gray-title {
            color: #908b8b;
            font-weight: 500;
            padding-bottom: 10px;
        }

        .profileContainer {
            display: flex;
        }

        .leftContainer, .rightContainer {
            padding: 10px;
        }

        .leftContainer {
            width: 40%;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .rightContainer {
            width: 60%;
        }

        .row {
            margin-top: 20px;
            width: 100%;
        }

        .row > div {
            display: flex;
            justify-content: space-between;
        }

        .TextBox {
            padding-left: 20px;
            border: 1px solid #e7e5e5;
            padding: 5px;
            border-radius: 5px;
            margin: 10px 0px 20px;
            width: 96%;
        }

        .lbl {
            color: #908b8b;
        }

        .row .field {
            width: 50%;
        }

        .radiobtn {
            text-transform: capitalize;
        }

        #signatureCanvas {
            border: 1px solid #ccc;
        }
        .detail{
            margin-bottom: 20px;
        }
    </style>

</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="content-1">
            <div class="sitemap">
                <a href="ManageRole.aspx"><strong>Manage Role</strong></a>
                <i class="fa fa-angle-right gray-title" aria-hidden="true"></i>
                <span class="gray-title">Role Details</span>
            </div>
            <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ManageRole.aspx" CssClass="addBtn" />

            <div class="profileContainer">
                <div class="rightContainer">
                    <p>Role Details</p>
                    <div class="row">
                        <div class="detail">
                            <span>Name: </span>
                            <div class="field">
                                <asp:Label ID="lblRoleName" runat="server" CssClass="capitalize"></asp:Label>
                            </div>
                        </div>
                        <div class="detail">
                            <span>Description :</span>
                            <div class="field">
                                 <asp:Label ID="lblDescription" runat="server" CssClass="capitalize"></asp:Label>
                            </div>
                        </div>
                        <div style="justify-content: flex-end">
                            <asp:Button ID="btnSubmit" runat="server" Text="Edit" OnClick="btnSubmit_Click" CssClass="addBtn" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
