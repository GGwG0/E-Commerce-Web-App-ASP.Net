<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateBoardResolutionTemplate.aspx.cs" ValidateRequest="false" Inherits="FYP_WebApplication.CreateBoardResolutionTemplate" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/createTemplate.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/admin_panel.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <title></title>
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
           
        }

        .btn {
            background-color: #3d3636;
            color: white;
            padding: 10px;
            border-radius: 10px;
            width: 48%;
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
            width: 30%;
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
       .imgProfile{
           border-radius:50px;
       }       
         .label{
                  margin:10px 0px;
              }
    </style>

</head>
<body>



    <form runat="server">

        <header class="admin-header">

           <div class="logo">
                <img src="assets/image/companylogo.png" alt="Logo" width="100" style="margin-left: 25px;" />
            </div>

           <div class="header-right">
                <div class="search-bar">
                   </div>
            </div>

        </header>



        <div class="content">


            <!-- Admin Sidebar -->
          <div class="admin-sidebar">
               <div class="sidebar-header">  
    <a style="display: flex; align-items:center; margin-top: 10px;" href="MyProfile.aspx">
      <asp:Image ID="imgProfile" runat="server"  Height="30" Width="30" CssClass="imgProfile"  />
      <p runat="server" style="margin-left:10px;" id="names">AWIE</p>
        <!-- or use a Label control -->
        <!-- <asp:Label runat="server" Text="My Profile" /> -->
    </a>
</div>
                 <div class="sidebar-header" runat="server" id="secretaryBar">
                    <a href="#" runat="server"><i class="fa fa-address-card" aria-hidden="true" style="margin-right:10px;"></i>Company Secretary</a>
                </div>
                <ul class="sidebar-menu">
                    <li class="title">MENU</li>
                    <li runat="server" id="clientDashboard">
                        <a href="#" class="toggle-submenu"><i class="fa fa-bar-chart" aria-hidden="true"></i>Dashboard</a>
                        <ul class="submenu">
                            <li><a href="RequestDashboard.aspx">Request</a></li>
                            <li><a href="BoardResolutionDashboard.aspx">Board Resolution</a></li>
                        </ul>
                    </li>
                    <li runat="server" id="adminDashboard">
                        <a href="#" class="toggle-submenu"><i class="fa fa-user" aria-hidden="true"></i>Manage Client Admins</a>
                        <ul class="submenu">
                            <li><a href="ManageUser.aspx">Users</a></li>
                            <li><a href="ManageRole.aspx">Roles</a></li>
                        </ul>
                    </li>
                     <li runat="server" id="adminDashboard2">
                        <a href="#" class="toggle-submenu"><i class="fa fa-file" aria-hidden="true"></i>Template</a>
                        <ul class="submenu">
                            <li><a href="RequestTypeList.aspx">Request Type</a></li>
                            <li><a href="BoardResolutionTemplateList.aspx">Board Resolution Template</a></li>
                        </ul>
                    </li>

                    <li>
                        <a href="#" class=""><i class="fa fa-cog" aria-hidden="true"></i>Logs</a>
                    </li>
                    <li>
                        <a href="#" class="logout"><i class="fa fa-sign-out" aria-hidden="true"></i>Logout</a>
                    </li>
                    <!-- Add more sidebar items as needed -->
                    
                </ul>

            </div>


            <div class="page-content">
                <div class="content-1">
                    <p>Initialize Board Resolution Template</p>
                    <div style="display: flex; margin-top: 25px;">
                        <div style="width: 70%;">
                            <asp:TextBox CssClass="Editor" ID="editor" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>

                        <div class="detail-cont">
                            <div class="detail-inner-cont">
                                <p style="font-size: 18px; margin-bottom: 30px;">Board Resolution Details</p>
                                <div style="" class="detail-cont-title">
                                    <div>
                                         <asp:Label runat="server" Text="Type :" CssClass="gray-title" />
                                         <asp:Label runat="server" Text="Board Resolution Template"  />
                                    </div>
                                    <div>
                                        <asp:Label runat="server" Text="Created Date :" CssClass="gray-title" />
                                        <asp:Label runat="server" Text="" ID="createdDate"/>

                                    </div>
                                    
                                </div>
                                <div style="margin-top: 20px;">
                                    <asp:Label runat="server" Text="Template Name :" CssClass="label"/>
                                    <asp:TextBox ID="Name" runat="server" CssClass="TextBox" placeholder="Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Name" Display="Dynamic" ErrorMessage="Template Name is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                         

                                    <asp:Label runat="server" Text="Description :" CssClass="label" />
                                    <asp:TextBox ID="Description" runat="server" CssClass="TextBox" placeholder="Name"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Description" Display="Dynamic" ErrorMessage="Description is required"  ForeColor="Red"></asp:RequiredFieldValidator>
                         
                                </div>


                            </div>

                            <div style="justify-content: space-between; display: flex;">
                                <asp:Button ID="addbtn" runat="server" CssClass="btn" Text="Add Template" OnClick="addbtn_Click" />
                                <asp:Button ID="cancelbtn" runat="server" OnClick="cancelbtn_Click" CssClass="btn" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>



<script src="ckeditor/ckeditor.js"></script>
<script>
    CKEDITOR.replace('editor');
</script>
<script>
    $(document).ready(function () {
        $(".toggle-submenu").click(function (e) {
            e.preventDefault();
            $(this).next(".submenu").slideToggle();
        });
    });
</script>
