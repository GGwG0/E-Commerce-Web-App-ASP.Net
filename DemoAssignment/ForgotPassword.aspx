<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DemoAssignment.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script
        src="https://kit.fontawesome.com/64d58efce2.js"
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="./assets/css/register-login.css" />
    <%--<link rel="stylesheet" href="./assets/css/user-style.css" />--%>

    <title></title>
    <style>
        .ques{
            display:inline;
        }
    </style>
</head>
 
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="forms-container">
                <div class="forgotpwd">


                    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" MailDefinition-From="noreply@example.com" MailDefinition-Subject="Password Reset" OnSendingMail="PasswordRecovery1_SendingMail" >
                        <QuestionTemplate>
                            <div class="form forgot-form">
                                <h2 class="title">Identity Confirmation</h2>
                                <p>Answer the following question to receive your password.</p>
                                <div class="input-field ques">
                                    User Name: 
                                    <asp:Literal ID="Username" runat="server"></asp:Literal>
                                </div>
                                <div class="input-field ques">
                                    Question: 
                                   <asp:Literal ID="Question" runat="server"></asp:Literal>

                                </div>
                                <div class="input-field ques">
                                   <%-- <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Answer:</asp:Label></td>--%>
                                    <asp:TextBox ID="Answer" runat="server" Placeholder="Answer"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                        ErrorMessage="Answer is required." ToolTip="Answer is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>

                                </div>
                               
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                   <asp:Button ID="Button1" CssClass="btn solid" runat="server" Text="Back To Home" PostBackUrl="~/Login.aspx" />
                                
                                    <asp:Button ID="SubmitButton" CssClass="btn solid" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
                                
                               
                            </div>
                        </QuestionTemplate>
                        <UserNameTemplate>
                            <div class="form forgot-form">
                                <h2 class="title">Forgot Password</h2>
                                <p>You will receive a email verification for verification purposes</p>
                                <div class="input-field">
                                    <i class="fas fa-user"></i>
                                    <asp:TextBox ID="UserName" runat="server" placeholder="Login Name"></asp:TextBox><br />
                                    <div id="checkuseravilable" runat="server" visible="false"></div>
                                    <asp:Literal ID="status" runat="server"></asp:Literal>

                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ForeColor="Red"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                                </div>
                           
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                <div class="links">
                                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hyperlink" NavigateUrl="~/index.aspx">Back To Home</asp:HyperLink>
                                </div>
                                <asp:Button ID="SubmitButton" CssClass="btn solid" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
                            </div>


                        </UserNameTemplate>
                       
                        <SuccessTemplate>
                            <div class="form forgot-form">
                                <p>Your password has been sent to you.</p>
                                <%--<asp:Button ID="back" CssClass="btn solid" runat="server" Text="Back" PostBackUrl="~/ForgotPassword.aspx" />--%>
                                <asp:Button ID="login" CssClass="btn solid" runat="server" Text="Login" PostBackUrl="~/Login.aspx" />

                            </div>
                        </SuccessTemplate>

                    </asp:PasswordRecovery>

                    <%--<script type="text/javascript">
                            function DisplayAlert() {
                                alert("Email has been sent to your registered account");
                                return true;
                            }
                        </script>--%>
                </div>
            </div>


        </div>
        <div class="panels-container">
            <div class="panel left-panel" style="">
                <img src="./assets/images/forgot.png" class="image" alt="" style="width: 700px; height: 500px;" />
            </div>

        </div>
    </form>
</body>
</html>