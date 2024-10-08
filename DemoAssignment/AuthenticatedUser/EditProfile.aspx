<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.EditProfile" %>

<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../assets/css/Profile.css">
    <link rel="shortcut icon" href="../assets/images/logo/favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" integrity="sha512-McFjf+kMyIgEoW8LJiyLnTHne7ufz9S66uul5+5JtEX/FV7Lw4JQ4I7M4rqf8uJ7Cz9XzgR7GKZ+wQthxl7dJg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
        <div class="product-box">
<form runat="server">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Delivery]"></asp:SqlDataSource>
            <%-- <div class="wrapper">--%>
            <div class="profileContainer">
                <div class="leftContainer">
                    <div class="profileImg">
                        <asp:Image ID="imgProfile"  runat="server" Height="200" Width="200" CssClass="imgProfile" />
                        <asp:Label runat="server" ID="lblName" CssClass="lblName" />
                            <asp:HyperLink ID="hplViewProfile" runat="server" NavigateUrl="~/AuthenticatedUser/ViewProfile.aspx">Profile</asp:HyperLink>
                        <%--<asp:HyperLink ID="hpyResetpassword" runat="server" NavigateUrl="~/AuthenticatedUser/ResetPassword.aspx">Reset Password</asp:HyperLink>--%>
                       
                        <%-- <asp:FileUpload ID="profileImg-Ctrl" runat="server" />--%>
                    </div>
                </div>
                <div class="rightContainer">
                    <h1>Edit Profile</h1>

                    <div class="row">
                        <h3><span>ID : </span>
                            <asp:TextBox ID="txtID" runat="server" Type="text" CssClass="txtBox" ReadOnly="true" Enabled="false"></asp:TextBox></h3>
                         <h3><span>Login Name  : </span>
                            <asp:TextBox ID="txtLoginName" runat="server" CssClass="txtBox" Type="text" ReadOnly="true" Enabled="false"></asp:TextBox></h3>
                        <h3><span>Name  : </span>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txtBox" Type="text"></asp:TextBox></h3>
                         <asp:RegularExpressionValidator ID="regName" runat="server" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="Invalid characters in name" ValidationExpression="[a-zA-Z ]+"></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator ID="nameRequired" runat="server" ControlToValidate="txtName" ForeColor="Red"
                                            ErrorMessage="Name is required." ToolTip="Name is required." Display="Dynamic" CssClass="validation-error"></asp:RequiredFieldValidator>
                                    
                        <h3><span>Phone Number : </span>
                            <asp:TextBox ID="txtPhoneNum" Type="text" CssClass="txtBox" runat="server"></asp:TextBox></h3>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhoneNum" ForeColor="Red"
                                            ErrorMessage="Mobile Number is required" ToolTip="Mobile Number is required." Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"  ControlToValidate="txtPhoneNum" ForeColor="Red" Display="Dynamic" ErrorMessage="Incorrect Mobile Number Format (01X-XXXXXXX)
                                            " ValidationExpression="^[0-9]{3}-[0-9]{7,8}$" ></asp:RegularExpressionValidator>

                        <h3><span>Email Address  : </span>
                            <asp:TextBox ID="txtEmail" Type="text" CssClass="txtBox" runat="server"></asp:TextBox></h3>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail" ForeColor="Red"
                                            ErrorMessage="Email is required." ToolTip="Email is required."  Display="Dynamic"></asp:RequiredFieldValidator>
                                 
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail" ErrorMessage="Insert Email in a correct format"  Display="Dynamic" ForeColor="Red" ValidationExpression="^[A-Za-z]+[A-Za-z0-9._-]*@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"></asp:RegularExpressionValidator>
                            

                        <h3><span>Address  : </span>
                            <asp:TextBox ID="txtAddress" Type="text" CssClass="txtBox" runat="server"></asp:TextBox></h3>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required" Display="Dynamic" ForeColor="Red" ></asp:RequiredFieldValidator>

                        <div class="imageBtn">
                            <h3><span>Date of Birth :</span></h3>
                            <div class="DOBicon">
                                <asp:TextBox ID="dob" Type="text" CssClass="txtBox" TextMode="Date" runat="server"></asp:TextBox>
                                <%--<asp:ImageButton ID="ImageButton" runat="server" CssClass="ImageButton" ImageUrl="~/assets/icons/calendar.png" ImageAlign="AbsBottom" Width="20px" Height="20px" />--%>

                            </div>
                           
                            <%-- <h3><span>Date of Birth  : </span><asp:TextBox id="dob" Type="text" CssClass="txtBox" runat="server"> </asp:TextBox></h3>--%>
                        </div>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dob" ForeColor="Red" Display="Dynamic" ErrorMessage="Date of Birth is Required"></asp:RequiredFieldValidator>
                           
                        <h3><span>Upload Image  :</span><asp:FileUpload ID="FileUpload1" runat="server"  /></h3>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1" ForeColor="Red" Display="Dynamic" ErrorMessage="Profile Image is Required"></asp:RequiredFieldValidator>--%>
                           
                        <div class="btn">
                            <asp:Button ID="updateBtn" CssClass="add-cart-btn" Width="150px" runat="server" Text="Update" OnClick="updateBtn_Click" />
                            <asp:Button ID="cancelBtn" CssClass="add-cart-btn" Width="150px" runat="server" Text="Cancel" PostBackUrl="ViewProfile.aspx" />
                        </div>

                    </div>
                </div>
            </div>
            <%--</div>--%>  </form>
        </div>
  
</asp:Content>
