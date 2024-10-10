<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="CreateUser.aspx.cs" Inherits="FYP_WebApplication.CreateUser" %>

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
       
    </style>
    
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="content">
        <div class="content-1">
            <div class="sitemap">
                <a href="ManageUser.aspx"><strong>Manage User</strong></a>
                <i class="fa fa-angle-right gray-title" aria-hidden="true"></i>
                <span class="gray-title">Add User</span>
            </div>
            
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="addBtn" />

            <div class="profileContainer">
                   <div class="rightContainer">
                    <p>User Details  <asp:Label ID="Label1" runat="server" CssClass="fa fa-info-circle" ToolTip="Username will be automatically generate." ></asp:Label></p><div class="row">
                        <div>
                            <span>Name: </span>
                            <div class="field">
                                <asp:TextBox ID="txtName" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <span>Phone number :</span>
                            <div class="field">
                                <asp:TextBox ID="txtPhoneNum" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <span>Email Address :</span>
                            <div class="field">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </div>
                         <div>
                            <span>Roles :</span>
                            <div class="field">

                                <asp:RadioButtonList ID="rblRole2" OnSelectedIndexChanged="rblRole_SelectedIndexChanged" runat="server" AutoPostBack="true"></asp:RadioButtonList>           
                               
                            </div>
                        </div>
                        <div id="companyCtn" runat="server">
                            <span>Company Name :</span>
                            <div class="field">
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="TextBox"></asp:DropDownList>
                            </div>
                        </div>
                        <div>
                            <span>Position :</span>
                            <div class="field">
                                <asp:TextBox ID="txtPosition" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </div>
                       
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnCreate_Click" CssClass="addBtn" />
                  </div>
                </div>
            </div>
        </div>
    </div>
            
</asp:Content>