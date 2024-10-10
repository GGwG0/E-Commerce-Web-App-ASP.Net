<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="SignBoardResolution.aspx.cs" Inherits="FYP_WebApplication.SignBoardResolution" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <style>
        .page-content {
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

        .detail-inner-cont, .detail-inner-cont > div {
            display: flex;
            flex-direction: column;
        }

        .detail-cont-title {
            padding-bottom: 10px;
            border-bottom: 1px solid #eaeaea;
        }

            .detail-cont-title > div {
                display: flex;
                justify-content: space-between;
                color: #7d848a;
            }
    </style>
    <link rel="stylesheet" type="text/css" href="assets/css/createTemplate.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/admin_panel.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://acrobatservices.adobe.com/view-sdk/viewer.js"></script>
</asp:Content>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-1">
        <p runat="server" id="title">Signing Board Resolution</p>

        <div style="display: flex; margin-top: 25px; height: 500px">
            <div style="width: 70%;">
                <iframe id="iframePdfViewer" style="width: 100%; height: 100%;" runat="server"></iframe>
            </div>

            <div class="detail-cont">
                <div class="detail-inner-cont">
                    <p style="font-size: 18px; margin-bottom: 30px;">Board Resolution Details</p>
                    <div style="" class="detail-cont-title">
                        <div>
                            <asp:Label runat="server" Text="Request ID:" CssClass="gray-title" />
                            <asp:Label runat="server" ID="requestID"  />
                        </div>
                        <div>
                            <asp:Label runat="server" Text="Created Date :" CssClass="gray-title"  />
                            <asp:Label runat="server" ID="createdDate" />
                        </div>

                        <div>
                            <asp:Label runat="server" Text="Title :" CssClass="gray-title" />
                            <asp:Label runat="server" ID="titleLabel"  />
                        </div>
                        <div>
                            <asp:Label runat="server" Text="Description :" CssClass="gray-title" />
                            <asp:Label runat="server" ID="descLabel" />
                        </div>
                        <div>
                            <asp:Label runat="server" Text="Status :" CssClass="gray-title" />
                            <asp:Label runat="server" ID="statusLabel"  />
                        </div>
                    </div>
                    <div style="margin-top: 20px;">
                        <div runat="server" id="containerOTP">
                            <asp:Label runat="server" Text="Google OTP :" CssClass="gray-title" />
                            <asp:TextBox ID="otpTxt" runat="server" CssClass="TextBox"></asp:TextBox>

                        </div>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidatorOTP" runat="server"
                                ControlToValidate="otpTxt"
                                ValidationExpression="^\d{6}$"
                                ErrorMessage="Please enter a valid 6-digit OTP."
                                Display="Dynamic"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredField1" runat="server" ControlToValidate="otpTxt" Display="Dynamic" ErrorMessage="OTP is required" ForeColor="Red" CssClass="validation"></asp:RequiredFieldValidator>

                    </div>
                </div>

                <div style="justify-content: space-between; display: flex;">

                    <asp:Button ID="btnApprove" runat="server" CssClass="btn" Text="Approve" OnClick="btnSign_Click" />
                    <asp:Button ID="btnEdit" runat="server" CssClass="btn" Text="Edit" OnClick="btnEdit_Click" />

                    <asp:Button ID="btnPublish" OnClick="verifybtn_Click" OnClientClick="return confirm('Are you sure you want to verify this resolution ?')" runat="server" CssClass="btn" Text="Publish"  CausesValidation="false" />
                </div>

            </div>

        </div>

    </div>
</asp:Content>