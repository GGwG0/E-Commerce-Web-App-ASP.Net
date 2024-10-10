<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewRequestDetail.aspx.cs" Inherits="FYP_WebApplication.ViewRequestDetail" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/noti.css" />
    <style>
        .font {
            text-transform: uppercase;
        }

        .addBtn {
            background-color: #3d3636;
            color: white;
            padding: 10px;
            border-radius: 10px;
            width: fit-content;
            margin: 20px 0px;
            border: 1px solid #e0e0e0;
        }

            .addBtn:hover, .upper-cont .button:hover {
                background-color: #e7e5e5;
                color: black;
                cursor: pointer;
            }

       .progress-container {
            overflow: hidden;
            width: 150px;
            height: 5px; 
            background-color: #F7F7F7;
                     
        }

         #progress-container .profile-bar {
           
            /*height: 5px;
            background-color: #4CAF50;         
            box-sizing:border-box;
            transition: width 0.6s ease;*/
        }

         .attachmentsLabel{
             text-decoration:none;
         }
    </style>
</asp:Content>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <%--left--%>
        <div class="content-1">
            <div class="sitemap">
                <a href="RequestDashboard.aspx"><strong>Requests</strong></a>
                <i class="fa fa-angle-right gray-title" aria-hidden="true"></i>
                <span class="gray-title">Request Details</span>
            </div>
            <asp:Button ID="btnBR" runat="server" OnClick="btnBR_Click" Text="Initialize Board Resolution" CssClass="addBtn" />
            <asp:Button ID="btnGeneral" runat="server" OnClick="btnGeneral_Click" Text="Complete Request" CssClass="addBtn" />
            <div class="big-container">

                <div class="upper-container inner-cont" style="padding: 0px 10px 20px;">
                    <div style="display: flex; justify-content: space-between; align-items: center; flex-direction: row;">
                        <h3>
                            <asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
                        <div>
                            <asp:Button ID="Editbtn" ToolTip="Edit" OnClick="Editbtn_Click" runat="server" Text="Edit" CssClass="addBtn" />

                            <asp:Button ID="ImageButton1" ToolTip="Delete" OnClick="ImageButton1_Click1" runat="server" Text="Delete" CssClass="addBtn" />
                        </div>
                    </div>
                    <table style="width: 80%; padding: 10px 0px;">
                        <tr style="color: #908b8b; font-weight: 500;">
                            <td>Assigned To</td>

                            <td>Due Date</td>
                            <td>Efforts</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAssignedTo" runat="server"></asp:Label></td>

                            <td>
                                <asp:Label ID="lblDueDate" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblTimeTaken" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <div style="display: flex; align-items: center; font-size: 14px; flex-direction: row;">
                        <p style="margin-right: 20px;">
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true" CssClass="font"></asp:Label>
                        </p>
                        <p style="margin-right: 10px;">
                            <asp:Label ID="lblProgressPercent" runat="server"></asp:Label>
                        </p>
                        <div class="progress-container">
                            <div class="progress-bar" runat="server" id="progress" ></div>
                        </div>
                    </div>
                </div>

                <div class="bottom-container">
                    <div class="left-cont">
                        <p class="gray-title">Details</p>
                        <div class="inner-cont">
                            <div>
                                <div class="left-inner">
                                    <p>Request ID</p>
                                </div>
                                <div class="right-inner">
                                    <p>
                                        <asp:Label ID="lblRequestID" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>

                            </div>
                            <div>
                                <div class="left-inner">
                                    <p>Type</p>
                                </div>
                                <div class="right-inner">
                                    <p>
                                        <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="inner-cont">
                            <p class="inner-cont-title">Additional Details</p>
                            <div>
                                <div class="left-inner">
                                    <p>Created On</p>
                                </div>
                                <div class="right-inner">
                                    <p>
                                        <asp:Label ID="lblCreatedDate" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>

                            </div>
                            <div>
                                <div class="left-inner">
                                    <p>Created By</p>
                                </div>
                                <div class="right-inner">
                                    <p>
                                        <asp:Label ID="lblCreatedBy" runat="server" CssClass="font"></asp:Label>
                                    </p>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="right-cont">
                        <div>
                            <p class="gray-title">Description</p>
                            <div class="inner-cont">
                                <div>
                                    <div class="right-inner">
                                        <p>
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <p class="gray-title">Attachments</p>
                            <div class="inner-cont">
                                <div>
                                    <div class="left-inner">
                                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                       <asp:HyperLink CssClass="attachmentsLabel" runat="server" ID="Attachment" Text=""></asp:HyperLink>
                                    </div>
                                    <div class="right-inner">
                                        <%--                                        <asp:Image ID="lblFileUpload" runat="server" />--%>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="comment-box">
                    <div class="right-cont">

                        <p class="gray-title">Comment</p>
                        <div class="content-cont">
                            <asp:Repeater ID="RepeaterMessages" runat="server" OnItemDataBound="RepeaterMessages_ItemDataBound">
                                <ItemTemplate>
                                    <div runat="server" id="messageStyle">
                                        <img src='<%# Eval("Base64ProfilePicture") %>' width="30" height="30" class="profile" alt="Profile Picture" />
                                        <div class="profile-content" runat="server" id="profileContent">
                                            <strong class="personname"><%# Eval("username") %></strong><br />
                                            <div class="message">
                                                <p><%# Eval("description") %></p>
                                                <span class="timestamp"><%# Eval("FormattedDate") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                        <div class="bottom-cont">
                            <asp:TextBox ID="txtMessage" CssClass="messageBox" Width="95%" runat="server" Placeholder="Write something ..." AutoPostBack="true"></asp:TextBox>
                            <asp:ImageButton ID="btnSend" runat="server" Width="20" Height="20" ToolTip="Send" ImageUrl="~/assets/image/send.png" OnClick="btnSend_Click"></asp:ImageButton>
                           
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>