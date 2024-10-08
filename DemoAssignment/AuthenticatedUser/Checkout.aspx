<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="DemoAssignment.Checkout" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script src="https://kit.fontawesome.com/a03ff1d483.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" type="text/css" href="../assets/css/checkout.css" />
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <form style="width: 100%;" id="form1" runat="server" class="checkoutForm">

        <div class="checkout-cont">


            <div class="first_row_container" style="display: flex; justify-content: space-between;">

                <div>
                    <asp:Button ID="Button1" runat="server" Text="Back" Width="115px" OnClick="Button1_Click" CssClass="add-cart-btn" />
                </div>
                <div>
                    <h1>Check Out</h1>
                </div>
                <div>
                    <%--<asp:Button ID="Button2" runat="server" Text="Pay" Width="115px" CssClass="button_pay add-cart-btn" />--%>
                </div>
            </div>



            <div class="status_container">
                <br />
                <br />
                <div style="height: 75px;" class="showcase-status">

                    <div>
                        <i style="color: #2dc258;" class="fa-solid fa-receipt"></i>
                    </div>
                    <div>
                        <i style="color: #2dc258;" class="fa-regular fa-pipe">--------</i>

                    </div>
                    <div>
                        <i style="color: #6e6e73;" class="fa-solid fa-cash-register"></i>

                    </div>
                    <div>
                        <i style="color: #6e6e73;" class="fa-regular fa-pipe">------</i>
                    </div>
                    <div>
                        <i style="color: #6e6e73;" class="fa-solid fa-box-archive"></i>

                    </div>
                    <div>
                        <i style="color: #6e6e73;" class="fa-regular fa-pipe">------</i>
                    </div>
                    <div>
                        <i style="color: #6e6e73;" class="fa-solid fa-truck-fast"></i>

                    </div>
                    <div>
                        <i style="color: #6e6e73;" class="fa-regular fa-pipe">--------</i>
                    </div>
                    <div>

                        <i style="color: #6e6e73;" class="fa-solid fa-check"></i>

                    </div>
                </div>




                <div style="height: 35px;" class="showcase-status">

                    <div>
                        <p>Check Out</p>
                    </div>
                    <div>
                        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   </p>

                    </div>
                    <div>
                        <p>Payment</p>

                    </div>
                    <div>
                        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </p>
                    </div>
                    <div>
                        <p>Packaging</p>

                    </div>
                    <div>
                        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </p>
                    </div>
                    <div>
                        <p>Shipping</p>

                    </div>
                    <div>
                        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </p>
                    </div>
                    <div>
                        <p>Complete</p>
                    </div>
                </div>

            </div>

            <br />
            <br />




            <div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table class="table-display" style="width: 100%;">
                            <tr>
                                <th>Product</th>
                                <th></th>
                                <th>Variation</th>
                                <th>Unit Price</th>
                                <th>Quantity</th>
                                <th>Total Price</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                 <asp:Image ID="ProductImage" runat="server" Width="200" Height="150" ImageUrl='<%# "~/assets/images/products/" + Eval("ImageUrl").ToString() %>'/>
            </td>
                            <td><%# Eval("ProductName") %></td>
                            <td><%# Eval("variation_name") %></td>
                            <td>RM <%# Eval("UnitPrice") %></td>
                            <td><%# Eval("Quantity") %></td>
                            <td>RM <%# Eval("Price") %></td>
                        </tr>


                    </ItemTemplate>
                    <FooterTemplate>
                        <tfoot>
                            <tr class="tfoot-table">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>Total:</td>
                                <td><b>RM</b>
                                <asp:Label ID="totalLabel" style="font-weight:bold;" runat="server"></asp:Label></td>
                            </tr>
                        </tfoot>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>

            <div class="container-checkout">



                <div class="row">

                    <div class="col">

                        <h3 class="title">billing address</h3>

                        <div class="inputBox">
                            <span>full name :</span>
                            <asp:TextBox ID="name" runat="server" placeholder="Mr. John Deo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="name" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="name" ForeColor="Red" ErrorMessage="Only Alphabhets Allowed" ValidationExpression="^[A-Za-z ]{5,50}$"></asp:RegularExpressionValidator>
                            </div>

                        <div class="inputBox">
                            <span>Contact No :</span>
                            <asp:TextBox ID="contact_no" runat="server" placeholder="011-27680138"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="contact_no" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="contact_no" ForeColor="Red" ErrorMessage="Incorrect Mobile Number Format (01X-XXXXXXX)" ValidationExpression="^[0-9]{3}-[0-9]{7,8}$" ></asp:RegularExpressionValidator>
                        </div>

                        <div class="inputBox">
                            <span>address :</span>
                            <asp:TextBox ID="address" runat="server" placeholder="No-Street-City-Zip Code"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="address" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"  ControlToValidate="address" ForeColor="Red" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>--%>
                            </div>
                         
                        <br />
                        <div class="inputBox">
                            <span>city :</span>
                            <asp:TextBox ID="city" runat="server" placeholder="Ampang"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="city" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="city" ForeColor="Red" ErrorMessage="only alphabhets are allowed with least 3 alphabhets" ValidationExpression="^[A-Za-z ]{3,30}$" ></asp:RegularExpressionValidator>
                            </div>


                        <div class="flex">
                            <div class="inputBox">
                                <span>state :</span>

                                <asp:TextBox ID="state" runat="server" placeholder="Kuala Lumpur"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="state" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="state" ForeColor="Red" ErrorMessage="only alphabhets are allowed" ValidationExpression="^[A-Za-z ]{3,30}$" ></asp:RegularExpressionValidator>
                                </div>
                            <div class="inputBox">
                                <span>zip code :</span>
                                <asp:TextBox ID="zip_code" runat="server" placeholder="50100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="zip_code" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="zip_code" ForeColor="Red" ErrorMessage="only numeric characters are allowed" ValidationExpression="^\d{5}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                    </div>

                    <div style="margin-left: 4%;" class="col">

                        <h3 class="title">payment</h3>

                        <div style="height: 82px;" class="inputBox">
                            <span>cards accepted :</span>
                            <img src="../assets/images/card_img.jpg" alt="">
                        </div>
                        <br />
                        <br />
                        <div class="inputBox">
                            <span>name on card :</span>
                            <asp:TextBox ID="name_on_card" runat="server" placeholder="Mr. John Deo"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  ControlToValidate="name_on_card" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="name_on_card" ForeColor="Red" ErrorMessage="only alphabhets allowed with at least 5 alphabhets" ValidationExpression="^[A-Za-z ]{5,50}$"></asp:RegularExpressionValidator>  
                            </div>
                        <div class="inputBox">
                            <span>credit card number :</span>
                            <asp:TextBox ID="card_credit_number" runat="server" placeholder="1111-2222-3333-4444"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="card_credit_number" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="card_credit_number" ForeColor="Red" ErrorMessage="Only numeric characters allowed" ValidationExpression="^[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$"></asp:RegularExpressionValidator>
                            </div>
                        <div class="inputBox">
                            <span>exp month :</span>
                            <span>
                            <asp:DropDownList ID="expiry_month" runat="server" Height="50px" Width="380px" Font-Size="Medium">
                                <asp:ListItem>January</asp:ListItem>
                                <asp:ListItem>February</asp:ListItem>
                                <asp:ListItem>March</asp:ListItem>
                                <asp:ListItem>April</asp:ListItem>
                                <asp:ListItem>May</asp:ListItem>
                                <asp:ListItem>June</asp:ListItem>
                                <asp:ListItem>July</asp:ListItem>
                                <asp:ListItem>August</asp:ListItem>
                                <asp:ListItem>September</asp:ListItem>
                                <asp:ListItem>October</asp:ListItem>
                                <asp:ListItem>November</asp:ListItem>
                                <asp:ListItem>December</asp:ListItem>
                            </asp:DropDownList>
                            </span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="expiry_month" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
               
                        </div>
                        <br />
                        <div class="flex">
                            <div class="inputBox">
                                <span>exp year :</span>
                                <asp:TextBox ID="expiry_year" runat="server" placeholder="2002"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="expiry_year" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"  ControlToValidate="expiry_year" ForeColor="Red" ErrorMessage="Enter a valid year" ValidationExpression="^(19|20|30)[0-9]{2}$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="inputBox">
                                <span>CVV :</span>
                                <asp:TextBox ID="ccv" runat="server" placeholder="1234"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ccv" ForeColor="Red" ErrorMessage="This field cannot be empty"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="ccv" ForeColor="Red" ErrorMessage="Enter a valid CCV number" ValidationExpression="^[0-9]{3,4}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <asp:Button runat="server" OnClick="CheckOut_Click" Text="checkout" CssClass="submit-btn" />
        </div>

    </form>

</asp:Content>