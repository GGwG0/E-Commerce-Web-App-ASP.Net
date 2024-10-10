<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="EditCompanyDetails.aspx.cs" Inherits="FYP_WebApplication.EditCompanyDetails" %>


<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
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
            width:75%;
            justify-content:space-between;
        }

        .leftContainer, .rightContainer {
            padding: 10px;
        }

        .leftContainer {
            width: 30%;
            display: flex;
            flex-direction: column;
      
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
                margin-bottom: 25px;
            }

        .leftContainer > div {
            margin-bottom: 10px;
            display: flex;
             justify-content:space-around;
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
        .leftCont span{
            width: 35%;
           
        }
    </style>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="content-1">
            <p>Company Details</p>

            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="addBtn"  OnClick="btnBack_Click" CausesValidation="false"/>
           
              <div class="profileContainer">
                <div class="leftContainer">
                    <div class="profileImg" style="align-self:center;">
                        <asp:Image ID="imgProfile" runat="server" Height="200" Width="200" CssClass="imgProfile" />
                    </div>
                    <div class="leftCont">
                         <span>Company ID: </span>
                         <asp:Label ID="lblComID" runat="server" ></asp:Label>
                    </div>
                    
                    
              
                </div>
                <div class="rightContainer">
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
                        <div style="justify-content:flex-end;">
                            <asp:Button ID="btnSave" Width="150px" runat="server" Text="Save" CssClass="fas fa-user-edit addBtn" OnClick="btnSave_Click"/>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>