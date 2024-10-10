<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstTimeLogin.aspx.cs" Inherits="FYP_WebApplication.FirstTimeLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        body {
            background-color: #9f9da7;
            font-size: 1.6rem;
            font-family: "Open Sans", sans-serif;
            color: #2b3e51;
        }

        h2 {
            font-weight: 300;
            text-align: center;
        }

        p {
            position: relative;
        }

        a,
        a:link,
        a:visited,
        a:active {
            color: #3ca9e2;
            -webkit-transition: all 0.2s ease;
            transition: all 0.2s ease;
        }

            a:focus, a:hover,
            a:link:focus,
            a:link:hover,
            a:visited:focus,
            a:visited:hover,
            a:active:focus,
            a:active:hover {
                color: #329dd5;
                -webkit-transition: all 0.2s ease;
                transition: all 0.2s ease;
            }

        #login-form-wrap {
            background-color: #fff;
            width: 35%;
            margin: 30px auto;
            text-align: center;
            padding: 20px 0 0 0;
            border-radius: 4px;
            box-shadow: 0px 30px 50px 0px rgba(0, 0, 0, 0.2);
        }

        #login-form {
            padding: 0 60px;
        }

        input {
            display: block;
            box-sizing: border-box;
            width: 100%;
            outline: none;
            height: 60px;
            line-height: 60px;
            border-radius: 4px;
        }

            input[type="text"],
            input[type="email"] {
                width: 100%;
                padding: 0 0 0 10px;
                margin: 0;
                color: #8a8b8e;
                border: 1px solid #c2c0ca;
                font-style: normal;
                font-size: 16px;
                -webkit-appearance: none;
                -moz-appearance: none;
                appearance: none;
                position: relative;
                display: inline-block;
                background: none;
            }

                input[type="text"]:focus,
                input[type="email"]:focus {
                    border-color: #3ca9e2;
                }

                    input[type="text"]:focus:invalid,
                    input[type="email"]:focus:invalid {
                        color: #cc1e2b;
                        border-color: #cc1e2b;
                    }

                input[type="text"]:valid ~ .validation,
                input[type="email"]:valid ~ .validation {
                    display: block;
                    border-color: #0C0;
                }

                    input[type="text"]:valid ~ .validation span,
                    input[type="email"]:valid ~ .validation span {
                        background: #0C0;
                        position: absolute;
                        border-radius: 6px;
                    }

                        input[type="text"]:valid ~ .validation span:first-child,
                        input[type="email"]:valid ~ .validation span:first-child {
                            top: 30px;
                            left: 14px;
                            width: 20px;
                            height: 3px;
                            -webkit-transform: rotate(-45deg);
                            transform: rotate(-45deg);
                        }

                        input[type="text"]:valid ~ .validation span:last-child,
                        input[type="email"]:valid ~ .validation span:last-child {
                            top: 35px;
                            left: 8px;
                            width: 11px;
                            height: 3px;
                            -webkit-transform: rotate(45deg);
                            transform: rotate(45deg);
                        }

        .validation {
            display: none;
            position: absolute;
            content: " ";
            height: 60px;
            width: 30px;
            right: 15px;
            top: 0px;
        }

        input[type="submit"] {
            border: none;
            display: block;
            background-color: #3ca9e2;
            color: #fff;
            font-weight: bold;
            text-transform: uppercase;
            cursor: pointer;
            -webkit-transition: all 0.2s ease;
            transition: all 0.2s ease;
            font-size: 18px;
            position: relative;
            display: inline-block;
            cursor: pointer;
            text-align: center;
        }

            input[type="submit"]:hover {
                background-color: #329dd5;
                -webkit-transition: all 0.2s ease;
                transition: all 0.2s ease;
            }

        #create-account-wrap {
            background-color: #eeedf1;
            color: #8a8b8e;
            font-size: 14px;
            width: 100%;
            padding: 10px 0;
            border-radius: 0 0 4px 4px;
        }

        .textbox {
            margin-left: 25%
        }

        .row > div, #btnSubmit {
            margin: 20px 0px;
        }

        .pw {
            padding: 0 0 0 10px;
            margin: 0;
            color: #8a8b8e;
            border: 1px solid #c2c0ca;
            font-style: normal;
            font-size: 16px;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            position: relative;
            display: inline-block;
            background: none;
        }

        #updatePanel {
            margin: 0px;
        }
        .lbl{
            font-size: 18px;
        }
    </style>
    <title>HTML5 Login Form with Validation Example</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css" />

</head>


<body>
    <form id="form1" runat="server">
        <div id="login-form-wrap">
            <div class="profileContainer">
                <div class="leftContainer">
                    <div class="user-detail" style="width: 100%; margin-top: 20px;">
                        <div style="padding: 10px; display: flex; flex-direction: column;">
                            <asp:Label runat="server" ID="lblUsername" CssClass="lbl" Text="Username :" />
                        </div>
                    </div>
                </div>
                <div class="rightContainer">
                    <p style="font-size:18px; font-weight:600;">
                        First Time Login
                    <asp:Label ID="Label1" runat="server" CssClass="fa fa-info-circle" ToolTip="Please change your password and upload your signature."></asp:Label>
                    </p>
                    <div class="row">

                        <div>
                            <span style="font-size: 18px;">New Password :</span>
                            <div class="field">
                                <asp:TextBox Width="50%" ID="txtNewPassword" CssClass="pw" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNewPassword"
                                    Display="Dynamic" ErrorMessage="New Password is required" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div>
                            <span style="font-size: 18px;">Confirm Password :</span>
                            <div class="field">
                                <asp:TextBox Width="50%" ID="txtConfirmPassword" CssClass="pw" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtConfirmPassword"
                                    Display="Dynamic" ErrorMessage="Confirm Password is required" ForeColor="Red"></asp:RequiredFieldValidator>

                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" Display="Dynamic"
                                    ErrorMessage="The passwords do not match" ForeColor="Red"></asp:CompareValidator>

                            </div>
                        </div>

                        <div id="signature" runat="server">
                            <span style="font-size: 18px;">Insert Signature:
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            </span>
                            <div>
                                <canvas id="signatureCanvas" class="pw" width="200" height="100" runat="server"></canvas>
                            </div>
                        </div>

                        <asp:HiddenField ID="hdnSignatureData" runat="server" />
                        <asp:Button Width="50%" ID="btnClear" runat="server" Text="Clear Signature" OnClientClick="clearSignature(); return false;" CssClass="btnLogin_Click" />


                        <asp:UpdatePanel ID="updatePanel" runat="server">
                            <ContentTemplate>

                                <asp:Button Width="50%" ID="btnSubmit" runat="server" Text="Submit" OnClientClick="saveSignature(); return false;" OnClick="btnSubmit_Click" CssClass="btnLogin_Click" CausesValidation="true" />

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>

<script>
    // Get the canvas and 2D context
    var canvas = document.getElementById("signatureCanvas");
    var context = canvas.getContext("2d");

    // Customize the appearance of the signature
    context.lineWidth = 1;
    context.strokeStyle = "black";

    // Variable to track whether the user is currently drawing
    var isDrawing = false;

    // Event listeners to handle mouse/touch interactions
    canvas.addEventListener("mousedown", startDrawing);
    canvas.addEventListener("mousemove", draw);
    canvas.addEventListener("mouseup", stopDrawing);
    canvas.addEventListener("touchstart", startDrawing);
    canvas.addEventListener("touchmove", draw);
    canvas.addEventListener("touchend", stopDrawing);

    // Function to start drawing
    function startDrawing(e) {
        isDrawing = true;
        var coords = getCoordinates(e);
        context.beginPath();
        context.moveTo(coords.x, coords.y);
    }

    // Function to draw when the user moves the pointer
    function draw(e) {
        if (isDrawing) {
            var coords = getCoordinates(e);
            context.lineTo(coords.x, coords.y);
            context.stroke();
        }
    }

    // Function to stop drawing
    function stopDrawing() {
        isDrawing = false;
    }
    // Function to get the coordinates based on event type (mouse or touch)
    function getCoordinates(e) {
        var x, y;
        if (e.touches && e.touches.length === 1) {
            var touch = e.touches[0];
            var rect = canvas.getBoundingClientRect(); // Get canvas position relative to the viewport
            x = touch.clientX - rect.left;
            y = touch.clientY - rect.top;
        } else {
            var rect = canvas.getBoundingClientRect(); // Get canvas position relative to the viewport
            x = e.clientX - rect.left;
            y = e.clientY - rect.top;
        }
        return { x: x, y: y };
    }

    // Function to clear the signature canvas
    function clearSignature() {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }

    // Function to save the signature data (optional)
    function saveSignature() {
        var signatureDataURL = canvas.toDataURL("image/png"); // Get the signature as a base64-encoded image data URL
        var signatureInput = document.getElementById('<%= hdnSignatureData.ClientID %>');
            signatureInput.value = signatureDataURL; // Assign the signature data URL to a hidden input for form submission (if needed)

            __doPostBack('<%= btnSubmit.UniqueID %>', '');
    }
</script>
<script src="https://cdn.jsdelivr.net/npm/signature_pad@2.5.3/dist/signature_pad.js"></script>
</html>

