<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditRequestType.aspx.cs" Inherits="FYP_WebApplication.EditRequestType" %>


<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="assets/css/createTemplate.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/admin_panel.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

  
    <style>
        .page-content{
            background-color: white;
        }
        .content-1 p {
            font-size: 25px;
            padding-bottom: 10px;
            border-bottom: 1px solid #eaeaea;
        }

        .TextBox {
            padding-left: 20px;
            border: 1px solid #e7e5e5;
            padding: 5px;
            border-radius: 5px;
            margin: 10px 0px 20px;
            
        }

        .btn {
            background-color: #3d3636;
            color: white;
            padding: 10px;
            border-radius: 10px;
            width: 110px;
            margin: 20px 0px;
            border: 1px solid #e0e0e0;
        }

            .btn:hover, .upper-cont .button:hover {
                background-color: #e7e5e5;
                color: black;
                cursor: pointer;
            }
        
        .gray-title {
            color: #af9e9e;
            margin-bottom: 10px;
        }

        .detail-cont {
            width: 40%;
            padding: 0px 30px;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .detail-inner-cont,.detail-inner-cont > div {
            display: flex;
            flex-direction: column;
        }
       .detail-cont-title{
           padding-bottom: 10px; 
           border-bottom: 1px solid #eaeaea;
       }
              .detail-cont-title > div{
                  display:flex;
                  justify-content:space-between;
                  color: #7d848a;
              }
    </style>


</asp:Content>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-1">
                    <p>Edit Request Type Details</p>
                    <div style="display: flex; margin-top: 25px;">
                        <div style="width: 60%;">
                              
                            <div class="detail-inner-cont">
                                <p style="font-size: 18px; margin-bottom: 30px;">Type Details</p>
                                <div style="" class="detail-cont-title">
                                    <div>
                                         <asp:Label runat="server" Text="Format :" CssClass="gray-title" />
                                         <asp:Label runat="server" Text="Request Type"  />
                                    </div>
                                    <div>
                                        <asp:Label runat="server" Text="Created Date :" CssClass="gray-title" />
                                        <asp:Label runat="server" Text="" ID="createdDate"/>
                                    </div>
                                    
                                </div>
                                <div style="margin-top: 20px;">
                                    <asp:Label runat="server" Text="Type Name :" />
                                    <asp:TextBox ID="Name" runat="server" CssClass="TextBox" placeholder="Name"></asp:TextBox>

                                    <asp:Label runat="server" Text="Description :" />
                                    <asp:TextBox ID="Description" runat="server" CssClass="TextBox" placeholder="Name"></asp:TextBox>

                                </div>


                            </div>

                            <div style="justify-content: space-between; display: flex;">
                                <asp:Button ID="addbtn" runat="server" CssClass="btn" Text="Edit"  OnClick="addbtn_Click" />
                                <asp:Button ID="cancelbtn" runat="server" CssClass="btn" OnClick="cancelbtn_Click" Text="Back" />
                            </div>

                        </div>

                        <div class="detail-cont">
                        </div>
                    </div>
                </div>
      

</asp:Content>

   






                





