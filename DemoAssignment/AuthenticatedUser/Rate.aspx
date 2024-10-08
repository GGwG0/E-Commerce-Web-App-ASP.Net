<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rate.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Rate" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://kit.fontawesome.com/a03ff1d483.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../assets/css/rate.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Button that opens the modal -->
        <!-- The modal dialog box -->
        <div id="myModal" runat="server" class="modal">
            <div class="modal-content">

                <div class="details">
                    <asp:Image ID="ProductImage" runat="server" ImageUrl="~/assets/images/electronics-banner-1.jpg" Width="200" Height="100" />
                    <div class="text-word" style="padding-left: 20PX; display: flex; flex-direction: column">
                        <div><span runat="server" id="header" style="font-size: 40px;"><b>HEADSET</b></span></div>
                        <div> <p runat="server" id="variation">variation: BLACK</p>
                        </div>
                    </div>
                </div>
                <div>
                    <p runat="server">
                        Product Quality &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton CssClass="demopls" ID="stars1" runat="server" Text="" OnClick="ChangeStar">
                    <i id="star_1" runat="server" class="fa-sharp fa-solid fa-star"></i>
                </asp:LinkButton>

                        <asp:LinkButton CssClass="demopls" ID="stars2" runat="server" Text="" OnClick="ChangeStar">
                            <i id="star_2" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>

                        <asp:LinkButton CssClass="demopls" ID="stars3" runat="server" Text="" OnClick="ChangeStar">
                            <i id="star_3" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>


                        <asp:LinkButton CssClass="demopls" ID="stars4" runat="server" Text="" OnClick="ChangeStar">
                            <i id="star_4" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>


                        <asp:LinkButton CssClass="demopls" ID="stars5" runat="server" Text="" OnClick="ChangeStar">
                            <i id="star_5" runat="server" class="fa-sharp fa-solid fa-star"></i>
                        </asp:LinkButton>
                    </p>
                </div>
                <div>
                    <asp:TextBox CssClass="textbox" TextMode="MultiLine" Style="resize: none;" Height="90px" Width="100%" runat="server" ID="comment" Placeholder="place your expression here"></asp:TextBox>
                </div>

                <br />

                <div style="display: flex; flex-direction: row; justify-content: space-between;">

                    <div>
                        <asp:Button runat="server" CssClass="rate-btn" OnClick="cancel_Click" Text="Cancel" /></div>
                    <div style="float: right;">
                        <asp:Button CssClass="rate-btn" OnClick="rate_Click" runat="server" Text="Rate" /></div>
                </div>
            </div>


        </div>
        <!-- The modal background overlay -->
        <div id="modalOverlay" runat="server" class="modal-overlay"></div>
        <asp:HiddenField runat="server" ID="numberrating" Value="1" />
    </form>
</body>
</html>