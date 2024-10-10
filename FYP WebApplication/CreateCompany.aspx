<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="CreateCompany.aspx.cs" Inherits="FYP_WebApplication.CreateCompany" %>

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
                <a href="CompanyList.aspx"><strong>Manage Company</strong></a>
                <i class="fa fa-angle-right gray-title" aria-hidden="true"></i>
                <span class="gray-title">Add Company</span>
            </div>

            <asp:Button ID="btnBack" runat="server" Text="Back"  OnClick="btnBack_Click" CssClass="addBtn" CausesValidation="false" />

            <div class="profileContainer">
                <div class="rightContainer">
                    <p>Company Details</p>
                    <div class="row">
                        <div>
                            <span>Company Name: </span>
                            <div class="field">
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" ErrorMessage="Company Name is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                         
                            </div>
                        </div>
                        <div>
                            <span>Registration Number :</span>
                            <div class="field">
                                <asp:TextBox ID="txtRegNum" runat="server" CssClass="TextBox"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegNum" Display="Dynamic" ErrorMessage="Registration Number is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRegNum"
            Display="Dynamic" ErrorMessage="Invalid Registration Number. Please enter a valid registration number."
            ValidationExpression="^[A-Za-z0-9]{1,15}$" ForeColor="Red">
        </asp:RegularExpressionValidator>
                            </div>
                        </div>
                          <div>
                            <span>Address :</span>
                            <div class="field">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Address is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                         
                            </div>
                        </div>
                         <div>
                            <span>Contact Number :</span>
                            <div class="field">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="TextBox" placeholder="01X-XXXXXXX"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ErrorMessage="Contact Number is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhone"
            Display="Dynamic" ErrorMessage="Invalid Contact Number. Please enter a valid phone number."
            ValidationExpression="^\d{3}-\d{8}$|^\d{3}-\d{7}$" ForeColor="Red">
        </asp:RegularExpressionValidator>
                            </div>
                        </div>
                         <div>
                             <span>Upload Company Logo</span>
                              <div class="field">
                            <asp:FileUpload ID="FileUpload1" runat="server" /></div>
                        </div>
                        <br />
                          <div runat="server" id="cosecAdminContainer">
                             <span>Admin</span> <div class="field">
                              <asp:DropDownList ID="ddlCosecAdmin" runat="server" CssClass="TextBox"></asp:DropDownList>
                             </div>
                        </div>
                        <div style="justify-content: flex-end">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="addBtn" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>