<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="EditProfile.aspx.cs" Inherits="FYP_WebApplication.EditProfile" %>

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
            width: 90%;
            justify-content: space-between;
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
            justify-content: space-around;
        }

        .TextBox {
      
            border: 1px solid #e7e5e5;
            padding: 5px;
            border-radius: 5px;
            width: 65%;
        }

        .lbl {
            color: #908b8b;
        }
          .field{
              margin: 10px 0px 20px;
        }
        .row .field {
            width: 50%;
        }

        .leftCont span {
            width: 35%;
        }
    </style>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="content-1">
            <p>Profile Details</p>

            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="addBtn" OnClick="btnBack_Click" />

            <div class="profileContainer">
                <div class="leftContainer">
                    <div class="profileImg" style="align-self: center;">
                        <asp:Image ID="imgProfile" runat="server" Height="200" Width="200" CssClass="imgProfile" />
                    </div>
                    <div class="leftCont">
                        <span>User ID: </span>
                        <asp:Label ID="lblUserID" runat="server"></asp:Label>
                    </div>
                    <div class="leftCont">
                        <span>Username: </span>
                        <asp:Label ID="lblUsername" runat="server"></asp:Label>
                    </div>
                    <div class="leftCont">
                        <span>Status: </span>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
                    <div class="leftCont">
                        <span>Roles: </span>
                        <asp:Label ID="lblRole" runat="server"></asp:Label>
                    </div>
                       <div class="leftCont">
                        <span>Managed By:</span>
                        <asp:Label ID="lblManagedBy" runat="server" Text="Label"></asp:Label>

                    </div>
                    
                    <div class="leftCont">
                        <span>Company Name:</span>
                        <asp:Label ID="lblCompanyName" runat="server" Text="Label"></asp:Label>

                    </div>

                </div>
                <div class="rightContainer">
                    <div class="row">
                        <div>
                            <span>Name:</span>
                               <div class="field">
                            <asp:TextBox ID="lblName" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lblName" Display="Dynamic" ErrorMessage="Name is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                         </div>
                        </div>
                        <div>
                            <span>Phone Number:</span>
                               <div class="field">
                            <asp:TextBox ID="lblPhoneNum" runat="server"  CssClass="TextBox" placeholder="01X-XXXXXXXX" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="lblPhoneNum" Display="Dynamic" ErrorMessage="Phone Number is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="lblPhoneNum"
            Display="Dynamic" ErrorMessage="Invalid Contact Number. Please enter a valid phone number."
            ValidationExpression="^\d{3}-\d{8}$|^\d{3}-\d{7}$" ForeColor="Red">
        </asp:RegularExpressionValidator>
                                   </div>
                            </div>

                        
                        <div>
                            <span>Position:</span>
                               <div class="field">
                            <asp:TextBox ID="lblPosition" runat="server"  CssClass="TextBox" ></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="lblPosition" Display="Dynamic" ErrorMessage="Position is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                               </div>
                        <div>
                            <span>Email Address:</span>
                               <div class="field">
                            <asp:TextBox ID="lblEmail" runat="server"  CssClass="TextBox"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lblEmail" Display="Dynamic" ErrorMessage="Email is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                      
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ControlToValidate="lblEmail" Display="Dynamic" 
            ErrorMessage="Invalid Email Address. Please enter a valid email address."
            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" 
            ForeColor="Red">
        </asp:RegularExpressionValidator>
                                   </div>
                        </div>
                        <div>
                             <span>Upload Profile Image:</span>

                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        
                        </div>
                        <div id="signatureDiv" runat="server">
                            <span>Signature:</span>
                            <asp:Image ID="imgSignature" runat="server" Height="200" Width="200" />

                        </div>
                        
                        <div style="justify-content: flex-end; margin-right: 80px;">
                            <asp:Button ID="btnEdit" Width="150px" runat="server" Text="Save" CssClass="fas fa-user-edit addBtn" OnClick="btnEdit_Click" />
                        </div>
                
                    </div>
                </div>
            </div>
        </div>
        
    </div>
</asp:Content>