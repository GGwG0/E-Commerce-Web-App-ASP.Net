<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DemoAssignment.Register" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://kit.fontawesome.com/64d58efce2.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="assets/css/register-login.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="forms-container">
                <div class="signin-signup">
                    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" CssClass="form sign-up-form" ContinueDestinationPageUrl="~/Login.aspx" CreateUserButtonType="Button" OnCreatedUser="CreateUserWizard1_NextButtonClick" CausesValidation="true" >

                          <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" ForeColor="#284E98" />
                        <WizardSteps>
                            <asp:CreateUserWizardStep runat="server" >
                              
                                <ContentTemplate>
                                    <h2 class="title">Sign up</h2>
                                    <div class="input-field">
                                        <i class="fas fa-user"></i>
                                        <asp:TextBox ID="UserName" runat="server" placeholder="Login Name"></asp:TextBox>
                                     </div>

                                  <%--  <validation >--%>
             <asp:RequiredFieldValidator ID="userNameRequired" runat="server" ControlToValidate="UserName" ForeColor="Red" ErrorMessage="User Name is required." ToolTip="UserName is required." ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RequiredFieldValidator>

             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UserName" ForeColor="Red" ErrorMessage="Incorrect Format" ValidationGroup="CreateUserWizard1" Display="Dynamic" ValidationExpression="^[a-zA-Z\d_@!*&>.,\$]+$" CssClass="validation-error"></asp:RegularExpressionValidator>

                                     <div class="input-field">
                                        <i class="fas fa-user"></i>
                                        <asp:TextBox ID="Name" runat="server" placeholder="Full Name"></asp:TextBox><br />  </div>

                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="nameRequired" runat="server" ControlToValidate="Name" ForeColor="Red"
                                            ErrorMessage="Name is required." ToolTip="Name is required." ValidationGroup="CreateUserWizard1" Display="Dynamic" CssClass="validation-error"></asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Name" ValidationGroup="CreateUserWizard1" Display="Dynamic" ForeColor="Red" ErrorMessage="Only 5 to 50 alphabhets allowed"  ValidationExpression="^[A-Za-z\s]{1,50}$"  ></asp:RegularExpressionValidator>
                                    
                                    <!-- Email -->
                                    <div class="input-field">
                                        <i class="fas fa-envelope"></i>
                                        <asp:TextBox ID="Email" runat="server" placeholder="Email Address"></asp:TextBox><br />
                                    </div>
                                       <%--  <validation >--%>
                                     <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ForeColor="Red"
                                            ErrorMessage="Email is required." ToolTip="Email is required." ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RequiredFieldValidator>
                                 
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Email" ErrorMessage="Insert Email in a correct format" ValidationGroup="CreateUserWizard1" Display="Dynamic" ForeColor="Red" ValidationExpression="^[A-Za-z]+[A-Za-z0-9._-]*@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"></asp:RegularExpressionValidator>
                            
                                    <!-- Address -->
                                    <div class="textarea input-field">
                                        <i class="fas fa-map-marker-alt"></i>
                                        <asp:TextBox ID="Address" TextMode="MultiLine" CssClass="textarea" Columns="20" Rows="5" runat="server" placeholder="Home Address"></asp:TextBox>
                                    </div>
                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Address" ErrorMessage="Address is required"  ValidationGroup="CreateUserWizard1" Display="Dynamic" ForeColor="Red" ></asp:RequiredFieldValidator>

                                    <!-- Mobile -->
                                    <div class="input-field">
                                        <i class="fas fa-mobile-alt"></i>
                                        <asp:TextBox ID="MobileNumber" runat="server" placeholder="Mobile Number"></asp:TextBox>
                                        
                                    </div>
                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="MobileNumber" ForeColor="Red"
                                            ErrorMessage="Mobile Number is required" ToolTip="Mobile Number is required." ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"  ControlToValidate="MobileNumber" ForeColor="Red"  ValidationGroup="CreateUserWizard1" Display="Dynamic" ErrorMessage="Incorrect Mobile Number Format (01X-XXXXXXX)
                                            " ValidationExpression="^[0-9]{3}-[0-9]{7,8}$" ></asp:RegularExpressionValidator>

                                    <!-- DOB -->
                                    <div class="input-field">
                                        <i class="fas fa-calendar"></i>
                                        <asp:TextBox ID="DOB" runat="server" type="date" placeholder="Date of Birth"></asp:TextBox>
                                    </div>
                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DOB" ForeColor="Red"  ValidationGroup="CreateUserWizard1" Display="Dynamic" ErrorMessage="Date of Birth is Required"></asp:RequiredFieldValidator>
                               
                                    <!-- Password -->
                                    <div class="input-field">
                                        <i class="fas fa-lock"></i>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox><br />
                                        
                                    </div>
                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ForeColor="Red"
                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="Password" ForeColor="Red" ErrorMessage="Password must be minimum 8 characters, at least 1 special characters and at least 1 capital letter." ToolTip="Invalid Password" ValidationExpression="^(?=.*[\W_])(?=.*[A-Z])[a-zA-Z\d\W_]{8,}$" ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RegularExpressionValidator>
                                    <!-- Confirm Password -->
                                    <div class="input-field">
                                        <i class="fas fa-lock"></i>
                                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm password"></asp:TextBox>
                                       
                                    </div>
                                       <%--  <validation >--%>
                                     <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ForeColor="Red"
                                            ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                            ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RequiredFieldValidator>

                                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ForeColor="Red"
                                            ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                            ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                        <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>

                                    <%--question--%>
                                    <div class="input-field">
                                      <i class=" fas fa-solid fa-question"></i>
                                        <asp:TextBox ID="Question" runat="server" placeholder="Question"></asp:TextBox>
                                    </div>

                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ForeColor="Red"
                                            ErrorMessage="Security question is required." ToolTip="Security question is required."
                                            ValidationGroup="CreateUserWizard1" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"  
                                     ValidationGroup="CreateUserWizard1" Display="Dynamic" ControlToValidate="Question" ForeColor="Red" ErrorMessage="Only Alphabhets Allowed" ValidationExpression="^[A-Za-z\s]*$"></asp:RegularExpressionValidator>

                                    <div class="input-field">

                                
                                      <i class="fas fa-regular fa-comment-dots"></i>
                                        <asp:TextBox ID="Answer" runat="server" placeholder="Answer"></asp:TextBox> 
                                        
                                    </div>
                                       <%--  <validation >--%>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ForeColor="Red"
                                            ErrorMessage="Security answer is required." ToolTip="Security question is required."
                                            ValidationGroup="CreateUserWizard1" ClientIDMode="Inherit" Display="Dynamic" ></asp:RequiredFieldValidator>
                                  
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"   ValidationGroup="CreateUserWizard1" Display="Dynamic" ControlToValidate="Answer" ForeColor="Red" ErrorMessage="Only Alphabhets Allowed" ValidationExpression="^[a-zA-Z0-9]*$"></asp:RegularExpressionValidator>
                                    <div>
                                        <%--<input type="submit" name="CreateUserWizard1$_CustomNav0$StepNextButtonButton" value="Create User" onclick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;CreateUserWizard1$__CustomNav0$StepNextButtonButton&quot;, &quot;&quot;, true, &quot;CreateUserWizard1&quot;, &quot;&quot;, false, false))" id="CreateUserWizard1__CustomNav0_StepNextButtonButton">
                                       --%><%-- <asp:Button ID="CreateUserWizard1$__CustomNav0$StepNextButtonButton" runat="server" Text="CreateUSer" CommandName="Create User"  />--%>
                                    </div>
                                    <div class="links">
                                        <asp:HyperLink ID="HyperLink2" Style="margin-left: 40px;" runat="server" CssClass="hyperlink" NavigateUrl="~/Index.aspx">Back To Home</asp:HyperLink>
                                    </div>
                                    
                              <%--      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CreateUserWizard1" ForeColor="red" />--%>

                                </ContentTemplate>

                            </asp:CreateUserWizardStep>


                            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                                <ContentTemplate>

                                    <div class="title">
                                        Complete
                                    </div>
                                    <p class="banner-subtitle">Your account has been successfully created.</p>
                                    <asp:Button ID="ContinueButton" runat="server"
                                        CausesValidation="False" CommandName="Continue"
                                        CssClass="btn solid" Text="Continue" ValidationGroup="CreateUserWizard1" OnClick="ContinueButton_Click" />
                                </ContentTemplate>
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                       
                        <CreateUserButtonStyle CssClass="btn solid" />
                    </asp:CreateUserWizard>

                </div>
            </div>

            <div class="another-panels-container">
                <div class="panel right-panel">
                    <div class="content2">
                        <h3>already one of us?</h3>
                        <p>
                            Click the button below to sign in now!!
                        </p>
                        <asp:Button ID="Button2" runat="server" CssClass="btn transparent" Text="LOGIN" OnClick="Button2_Click" />
                    </div>
                    <img src="./assets/images/SignUp.png" class="image" alt="" />
                </div>
            </div>
        </div>

    </form>
</body>
</html>