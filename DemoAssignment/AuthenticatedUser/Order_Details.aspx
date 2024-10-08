<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Order_Details.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Order_Details" %>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../../assets/css/Order_detail.css" />
        <script src="https://kit.fontawesome.com/a03ff1d483.js" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <form style="width: 100%" runat="server">
        <br />
        <br />
        <div class="main-container1">
            <div class="header-container">
                <div>
                    <asp:Button CssClass="button-back" Text="< BACK" OnClick="Back_Click" runat="server" />
                </div>
                <div>
                    ORDER ID : <span runat="server" id="OrderID"></span>| STATUS  <span runat="server" id="Status"></span>
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
                        <i style="color: #2dc258;" class="fa-solid fa-cash-register"></i>

                    </div>
                    <div>
                        <i style="color: #2dc258;" class="fa-regular fa-pipe">------</i>
                    </div>
                    <div>
                        <i style="color: #2dc258;" class="fa-solid fa-box-archive"></i>

                    </div>
                    <div>
                        <i runat="server" id="pipeDelivery" style="color: #6e6e73;" class="fa-regular fa-pipe">------</i>
                    </div>
                    <div>
                        <i runat="server" id="DeliveryIcon" style="color: #6e6e73;" class="fa-solid fa-truck-fast"></i>

                    </div>
                    <div>
                        <i runat="server" id="pipeCompleted" style="color: #6e6e73;" class="fa-regular fa-pipe">--------</i>
                    </div>
                    <div>

                        <i runat="server" id="CompletedIcon" style="color: #6e6e73;" class="fa-solid fa-check"></i>

                    </div>
                </div>




                <div style="height: 35px;" class="showcase-status">

                    <div>
                        <p>Check Out</p>
                    </div>
                    <div>
                        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    </p>

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
            <div>
                <div class="mail-line"></div>
            </div>
            <br />
            <div class="mailing-detail">
                <div class="header-mail">
                    <div>
                        <h2>Delivery Address</h2>
                    </div>
                    <div>
                        <p style="padding-top: 10px;">Standard Delivery</p>
                    </div>
                </div>
            </div>

            <div style="margin-left: 10%; width: 80%" class="address-detail">
                <div runat="server" id="address" style="width: 30%">
                    <br />
                    <span runat="server" id="contactName"></span>
                    <br />
                    <span runat="server" id="contactNo"></span>
                    <br />
                    <br />

                    <span runat="server" id="addresscontent"></span>
                </div>
                <hr style="width: 1%; color: rgba(0,0,0,.26);" />
                <div style="width: 69%">
                    <br />
                    <asp:Label Text="Estimated Delivery Date : " runat="server" /> <asp:Label Text="18/05/2023" ID="estimatedLabel" runat="server" /><br /><br />
                    <asp:Label Text="Actual Delivery Date : " runat="server" /><asp:Label Text="21/05/2023" ID="actualLabel" runat="server" />
                </div>
            </div>
            <br />
            <div>
                <div class="mail-line"></div>
            </div>
            <br />
            <br />
            <div class="product-details">
                <div class="product-details-header">
                    <div style="font-size: 15px; padding-top: 15px;">Payment ID: <span runat="server" id="paymentIDs"></span></div>
                    <div style="font-size: 15px; padding-top: 15px;">Order Date: <span runat="server" id="orderdate"></span></div>
                </div>

                <hr style="margin-top: 7px;" />

                <div class="product-content">
                    <div style="width: 20%;">
                        <asp:Image ID="ProductImage" Width="100%" runat="server" ImageUrl="~/assets/images/electronics-banner-1.jpg" />
                    </div>
                    <div style="width: 70%; padding-left: 10px;">
                        <h3 runat="server" id="productname"></h3>
                        <span runat="server" id="variationName"></span>
                        <br />
                        x<span runat="server" id="quantity"></span>
                    </div>
                    <div style="width: 10%; display: flex; justify-content: center;">
                        <br />
                        <span class="class-show" runat="server" id="unitprice">RM 60</span></div>
                </div>
                <hr style="margin-top: 7px;" />
            </div>
            <div style="width: 80%; margin-left: 10%">
                <span runat="server" id="totalPrice" style="float: right;"></span>
            </div>
            <br /><br />
           
            <div  style="width: 80%; margin-left: 10%" runat="server" id="ratingContainer">
                <h3>RATING ID <asp:Label runat="server" ID="ratingIDs" >1</asp:Label></h3>
                 <div>
                    <p runat="server" style="display:flex;">
                        Product Quality: &nbsp;&nbsp;
                  <asp:LinkButton CssClass="demopls" ID="stars1" runat="server" Text="" >
                    <i id="star_1" runat="server" class="fa-sharp fa-solid fa-star"></i>
                   </asp:LinkButton>

                        <asp:LinkButton CssClass="demopls" ID="stars2" runat="server" Text="" >
                            <i id="star_2" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>

                        <asp:LinkButton CssClass="demopls" ID="stars3" runat="server" Text="">
                            <i id="star_3" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>


                        <asp:LinkButton CssClass="demopls" ID="stars4" runat="server" Text="" >
                            <i id="star_4" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>


                        <asp:LinkButton CssClass="demopls" ID="stars5" runat="server" Text="" >
                            <i id="star_5" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>
                    </p>
                </div>
                <div>
                    <asp:Label runat="server" ID="customerLabel" Text="Your Comment :"></asp:Label> <asp:Label runat="server" ID="CustomerComment">Good</asp:Label><br />
                    <asp:Label runat="server" ID="adminlabel" Text="Admin Comment:"></asp:Label> <asp:Label runat="server" ID="AdminComment">Thank You</asp:Label><br />

                </div>
            </div>
        </div>
    </form>

</asp:Content>