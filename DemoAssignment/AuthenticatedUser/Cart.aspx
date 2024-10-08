<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="DemoAssignment.AuthenticatedUser.Cart" %>

<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="../assets/css/cart.css">
    <style>
        a{
            color:black;
        }
    </style>
</asp:Content>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form runat="server">
        <div class="Cart-Container">
         
            <h1 class="titleCart">Shopping Cart</h1>
            <br />
            <br />
            <asp:GridView ID="CartGridView" DataKeyNames="CartItemId" runat="server" AutoGenerateColumns="False" CssClass="auto-style1" OnRowEditing="CheckBox_CheckedChanged" OnSelectedIndexChanged="CheckBox_CheckedChanged"
                OnRowDeleting="CartGridView_RowDeleting" OnRowDataBound="CartGridView_RowDataBound">
                <Columns>

                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="ProductIDHiddenField" runat="server" Value='<%# Eval("ProductID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Select All">
                        <HeaderTemplate>
                            <asp:CheckBox ID="HeaderCheckBox" runat="server" OnCheckedChanged="HeaderCheckBox_CheckedChanged" AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox" runat="server" OnCheckedChanged="CheckBox_CheckedChanged" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product" ItemStyle-Width="220px">
                        <ItemTemplate>
                            <asp:Image ID="ProductImage" runat="server" Width="200" Height="150" ImageUrl='<%# "~/assets/images/products/" + Eval("ImageUrl").ToString() %>' />
                        </ItemTemplate>
                        <ItemStyle Width="220px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Variation">
                        <ItemTemplate>
                            <asp:DropDownList ID="VariationDropDown" runat="server" CssClass="cart-container" OnSelectedIndexChanged="VariationDropDown_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemTemplate>
                            <asp:Label ID="UnitPriceLabel" runat="server" Text='<%# Eval("UnitPrice") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <div>
                                <asp:LinkButton ID="DecreaseButton" runat="server" Text="-" CssClass="quantity-button" CommandName="Decrease" CommandArgument='<%# Eval("CartItemId") %>' OnClick="Quantity_Click" />
                                <asp:Label ID="QuantityLabel" runat="server" Text='<%# Eval("Quantity") %>' />
                                <asp:LinkButton ID="IncreaseButton" runat="server" Text="+" CssClass="quantity-button" CommandName="Increase" CommandArgument='<%# Eval("CartItemId") %>' OnClick="Quantity_Click" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' CssClass="TotalpaymentColumn" Style="font-weight: 600; color: black;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="results" CssClass="PayemntAmount" runat="server">0.00</asp:Label>
            <asp:Label ID="Label1" CssClass="PayemntAmount" runat="server">Total: RM </asp:Label>
            <br />
            <asp:Button runat="server" ID="checkOutBtn" CssClass="checkOutBtn" Text="Check Out" OnClick="checkOutBtn_Click" />

        </div>
    </form>


</asp:Content>