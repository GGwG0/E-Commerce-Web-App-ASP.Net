<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSuccessful.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.PaymentSuccessful" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script src="https://kit.fontawesome.com/a03ff1d483.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../assets/css/paymentSuccessful.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="">
            <span class="icon-sucess">
                <i class="fa-solid fa-check"></i>
            </span>
            <br />
            <h2>Payment Complete</h2>
            <h3>Thanks for shopping with us !!</h3>
     
            <div class="content">

                <div> Latest Order ID:
                    <asp:Label runat="server" ID="orderidDisplay" class="lbl"></asp:Label>
                </div>
                <div>
                    Order Status: 
                    <asp:Label runat="server" ID="OrderStatus" class="lbl"></asp:Label>
                </div>

                <div>
                    Ordered Date:
                    <asp:Label runat="server" ID="orderdate"></asp:Label>
                </div>
                <div>
                    Estimated Arrived: 
                    <asp:Label runat="server" ID="estimatedate"></asp:Label>
                </div>
                <div>
                    Payment Date: 
                    <asp:Label runat="server" ID="paymentdate"></asp:Label>
                </div>
                <div>
                    Payment amount: 
                    <asp:Label runat="server" ID="paymentamount"></asp:Label>

                </div>

                <div>
                    <asp:Button ID="Button2" PostBackUrl="~/Index.aspx" CssClass="button" runat="server" Text="Back to Main" />
                    <asp:Button ID="Button3" PostBackUrl="~/AuthenticatedUser/PurchaseHistory.aspx" CssClass="button" runat="server" Text="Track Order" />       
                </div>

                
            </div>
        </div>
    </form>
</body>
</html>