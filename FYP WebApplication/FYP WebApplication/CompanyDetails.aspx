<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="CompanyDetails.aspx.cs" Inherits="FYP_WebApplication.CompanyDetails" %>

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
            <p>User Details</p>

            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="addBtn"  OnClick="btnBack_Click"/>
           
              <div class="profileContainer">
                <div class="leftContainer">
                    <div class="profileImg" style="align-self:center;">
                        <asp:Image ID="imgProfile" runat="server" Height="200" Width="200" CssClass="imgProfile" />
                    </div>
                    <div class="leftCont">
                         <span>Company ID: </span>
                         <asp:Label ID="lblComID" runat="server" ></asp:Label>
                    </div>
                     <div class="leftCont">
                         <span>Company Name: </span>
                         <asp:Label ID="lblComName" runat="server" ></asp:Label>
                    </div>
                     <div class="leftCont">
                         <span>Registration Number: </span>
                         <asp:Label ID="lblRegNum" runat="server" ></asp:Label>
                    </div>
                    
              
                </div>
                <div class="rightContainer">
                    <div class="row">
                      <div>
                            <span>Address:</span>
                            <asp:Label ID="lblAddress" runat="server" Text="Name: "></asp:Label>
                        </div>
                        <div>
                            <span>Contact Number:</span>
                            <asp:Label ID="lblContactNum" runat="server" Text="Label"></asp:Label>
                          
                        </div>
                        <div>
                            <span>Admin:</span>
                            <asp:Label ID="lblAdmin" runat="server" Text="Label"></asp:Label>
                          
                        </div> 
                        <div style="justify-content:flex-end;">
                            <asp:Button ID="btnEdit" Width="150px" runat="server" Text="Edit" CssClass="fas fa-user-edit addBtn" OnClick="btnEdit_Click"/>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>