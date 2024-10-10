<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="DemoAssignment.ResetPassword" %>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../assets/css/Profile.css">
    <link rel="shortcut icon" href="../assets/images/logo/favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css">
    <style>
        .input-field{
            display:flex;
            margin-top:20px;
        }
    </style>
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="product-box">
        <form runat="server">
              <div class="profileContainer">
                <div class="leftContainer">
                    <div class="profileImg">
                        <asp:Image ID="imgProfile" ImageUrl="../assets/images/profile/profile-2.jpg" runat="server" Height="200" Width="200" CssClass="imgProfile" />
                        <asp:Label runat="server" CssClass="lblName" ID="lblName" />
                        
                        </div>
                </div>
                 <div class="rightContainer">
                    <h1>Change Password</h1>

                    <div class="row">
                   <asp:ChangePassword ID="ChangePassword1" runat="server" OnChangedPassword="ChangePassword1_ChangedPassword" DisplayUserName="true">
                        <ChangePasswordTemplate>
                           
                            <h3><span>Username : </span>
                            <asp:TextBox ID="Username" runat="server" CssClass="txtBox" Enabled="false"></asp:TextBox>
                            </h3>
                            <h3><span>Current Password:</span><asp:TextBox ID="CurrentPassword" runat="server" CssClass="txtBox" TextMode="Password" placeholder="Current Password"></asp:TextBox>    
                            </h3>

                              <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server"
                                    ControlToValidate="CurrentPassword"  ForeColor="Red" Display="Dynamic" ErrorMessage="Current Password is required."
                                    ToolTip="Current Password is required." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>

                           <h3><span>New Password:</span>
                               <asp:TextBox ID="NewPassword" runat="server" CssClass="txtBox" TextMode="Password" placeholder="New Password"></asp:TextBox>
           
                          </h3>

                              <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server"
                                    ControlToValidate="NewPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="New Password is required."
                                    ToolTip="New Password is required." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>

                          
                          <h3>
                              <span>Confirm New Password:</span>
                              <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="txtBox" TextMode="Password" placeholder="Confirm New Password"></asp:TextBox>
                              
                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is not same as New Password" ValidationGroup="ChangePassword1"></asp:CompareValidator>--%>
                            
                          </h3>

                              <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server"
                                    ControlToValidate="ConfirmNewPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="Confirm New Password is required."
                                    ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>
                            
                               
                               <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" font-size="Medium" ForeColor="Red" Display="Dynamic" Font-Bold="true" ControlToValidate="NewPassword" ErrorMessage="Password must be minimum 8 characters, at least 1 special characters and at least 1 capital letter." ValidationExpression="^(?=.[\W_])(?=.[A-Z])[a-zA-Z\d\W_]{8,}$" ></asp:RegularExpressionValidator>--%>
                          
                             <h3><asp:Literal ID="FailureText" runat="server" EnableViewState="False"  ></asp:Literal></h3>
                            
                      
                       <div class="btn">
                            
                             <asp:Button ID="ChangePasswordButton" CssClass="add-cart-btn" runat="server" CommandName="ChangePassword" Text="Change Password" ValidationGroup="ChangePassword1" />
                            <asp:Button ID="CancelButton" runat="server" CausesValidation="False" CssClass="add-cart-btn" CommandName="Cancel" Text="Cancel" OnClick="CancelButton_Click" />
                           </div>
                        </ChangePasswordTemplate>
                        <SuccessTemplate>
                              <div class="title">
                                       Change Password Complete
                                    </div>
                                    <p class="banner-subtitle">Your password has been changed!</p>
                                    <asp:Button ID="ContinueButton" runat="server"
                                        CausesValidation="False" CommandName="Continue"
                                        CssClass="add-to-cart" Text="Continue" ValidationGroup="ChangePassword1" OnClick="ContinueButton_Click" />
                        </SuccessTemplate>
                       <FailureTextStyle  ForeColor="Red"/>
                    </asp:ChangePassword>
                        </div>
                     

                </div>
            </div>
        </form>
          
       
    </div>

</asp:Content>