<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="DemoAssignment.ProductList" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <%--sidebar--%>
    <div class="sidebar  has-scrollbar" data-mobile-menu>

        <div class="sidebar-category">

            <div class="sidebar-top">
                <h2 class="sidebar-title">Category</h2>

                <button class="sidebar-close-btn" data-mobile-menu-close-btn>
                    <ion-icon name="close-outline"></ion-icon>
                </button>
            </div>

            <ul class="sidebar-menu-category-list">

                <li class="sidebar-menu-category">

                    <button class="sidebar-accordion-menu" data-accordion-btn>

                        <div class="menu-title-flex">
                            <span class="material-icons-sharp">storage</span>
                            <p class="menu-title">Data Storage</p>
                        </div>

                        <div>
                            <ion-icon name="add-outline" class="add-icon"></ion-icon>
                            <ion-icon name="remove-outline" class="remove-icon"></ion-icon>
                        </div>

                    </button>

                    <ul class="sidebar-submenu-category-list" data-accordion>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=1&type=1" class="sidebar-submenu-title">
                                <p class="product-name">External Hard Drives</p>
                                <data value="300" class="stock" title="Available Stock">300</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=1&type=2" class="sidebar-submenu-title">
                                <p class="product-name">External SSD</p>
                                <data value="60" class="stock" title="Available Stock">60</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=1&type=3" class="sidebar-submenu-title">
                                <p class="product-name">Flash Drives</p>
                                <data value="50" class="stock" title="Available Stock">50</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=1&type=4" class="sidebar-submenu-title">
                                <p class="product-name">Internal Hard Drives</p>
                                <data value="87" class="stock" title="Available Stock">87</data>
                            </a>
                        </li>
                    </ul>

                </li>

                <li class="sidebar-menu-category">

                    <button class="sidebar-accordion-menu" data-accordion-btn>

                        <div class="menu-title-flex">
                            <span class="material-icons-sharp">computer</span>

                            <p class="menu-title">Monitors</p>
                        </div>

                        <div>
                            <ion-icon name="add-outline" class="add-icon"></ion-icon>
                            <ion-icon name="remove-outline" class="remove-icon"></ion-icon>
                        </div>

                    </button>

                    <ul class="sidebar-submenu-category-list" data-accordion>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=2&type=1" class="sidebar-submenu-title">
                                <p class="product-name">Curved Monitors</p>
                                <data value="45" class="stock" title="Available Stock">45</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=2&type=2" class="sidebar-submenu-title">
                                <p class="product-name">Flat Monitors</p>
                                <data value="75" class="stock" title="Available Stock">75</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=2&type=3" class="sidebar-submenu-title">
                                <p class="product-name">Gaming Monitors</p>
                                <data value="35" class="stock" title="Available Stock">35</data>
                            </a>
                        </li>
                    </ul>

                </li>

                <li class="sidebar-menu-category">
                    <button class="sidebar-accordion-menu" data-accordion-btn>

                        <div class="menu-title-flex">
                            <span class="material-icons-sharp">mouse</span>

                            <p class="menu-title">Computer Accessories</p>
                        </div>

                        <div>
                            <ion-icon name="add-outline" class="add-icon"></ion-icon>
                            <ion-icon name="remove-outline" class="remove-icon"></ion-icon>
                        </div>

                    </button>

                    <ul class="sidebar-submenu-category-list" data-accordion>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=3&type=1" class="sidebar-submenu-title">
                                <p class="product-name">Cooling Pads & Stands</p>
                                <data value="46" class="stock" title="Available Stock">46</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=3&type=2" class="sidebar-submenu-title">
                                <p class="product-name">Keyboard & Mouse</p>
                                <data value="73" class="stock" title="Available Stock">73</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=3&type=3" class="sidebar-submenu-title">
                                <p class="product-name">Projector</p>
                                <data value="61" class="stock" title="Available Stock">61</data>
                            </a>
                        </li>
                    </ul>

                </li>

                <li class="sidebar-menu-category">

                    <button class="sidebar-accordion-menu" data-accordion-btn>

                        <div class="menu-title-flex">

                            <span class="material-icons-sharp">router</span>
                            <p class="menu-title" style="text-align: left;">Network Components & IP Cameras</p>
                        </div>

                        <div>
                            <ion-icon name="add-outline" class="add-icon"></ion-icon>
                            <ion-icon name="remove-outline" class="remove-icon"></ion-icon>
                        </div>

                    </button>

                    <ul class="sidebar-submenu-category-list" data-accordion>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=4&type=1" class="sidebar-submenu-title">
                                <p class="product-name">Network Adapters</p>
                                <data value="12" class="stock" title="Available Stock">12 pcs</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=4&type=2" class="sidebar-submenu-title">
                                <p class="product-name">Range Extender / Powerline</p>
                                <data value="60" class="stock" title="Available Stock">60 pcs</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=4&type=3" class="sidebar-submenu-title">
                                <p class="product-name">Smart Home & IP Cameras</p>
                                <data value="50" class="stock" title="Available Stock">50 pcs</data>
                            </a>
                        </li>
                    </ul>

                </li>

                <li class="sidebar-menu-category">

                    <button class="sidebar-accordion-menu" data-accordion-btn>

                        <div class="menu-title-flex">

                            <span class="material-icons-sharp">print</span>
                            <p class="menu-title">Printer & Ink</p>
                        </div>

                        <div>
                            <ion-icon name="add-outline" class="add-icon"></ion-icon>
                            <ion-icon name="remove-outline" class="remove-icon"></ion-icon>
                        </div>

                    </button>

                    <ul class="sidebar-submenu-category-list" data-accordion>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=5&type=1" class="sidebar-submenu-title">
                                <p class="product-name">Ink Cartridges & Toners</p>
                                <data value="68" class="stock" title="Available Stock">68</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=5&type=2" class="sidebar-submenu-title">
                                <p class="product-name">Printers & Scanners</p>
                                <data value="46" class="stock" title="Available Stock">46</data>
                            </a>
                        </li>


                    </ul>

                </li>

                <li class="sidebar-menu-category">

                    <button class="sidebar-accordion-menu" data-accordion-btn>

                        <div class="menu-title-flex">
                            <span class="material-icons-sharp">devices</span>

                            <p class="menu-title">Mobile & Tablets</p>
                        </div>

                        <div>
                            <ion-icon name="add-outline" class="add-icon"></ion-icon>
                            <ion-icon name="remove-outline" class="remove-icon"></ion-icon>
                        </div>

                    </button>

                    <ul class="sidebar-submenu-category-list" data-accordion>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=6&type=1" class="sidebar-submenu-title">
                                <p class="product-name">Mobile Accessories</p>
                                <data value="50" class="stock" title="Available Stock">50</data>
                            </a>
                        </li>

                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=6&type=2" class="sidebar-submenu-title">
                                <p class="product-name">Mobile Phones</p>
                                <data value="48" class="stock" title="Available Stock">48</data>
                            </a>
                        </li>
                        <li class="sidebar-submenu-category">
                            <a href="ProductList.aspx?cat=6&type=3" class="sidebar-submenu-title">
                                <p class="product-name">Tablets</p>
                                <data value="48" class="stock" title="Available Stock">48</data>
                            </a>
                        </li>
                    </ul>
                </li>

            </ul>

        </div>
    </div>
    <%--product--%>
    <form runat="server">
        <div class="product-box">
            <div class="product-main">
                <%--<div style="display:flex; margin-bottom:15px;">
                    <asp:HyperLink ID="Home" NavigateUrl="~/Index.aspx" runat="server">Home </asp:HyperLink>
                    <img src="assets/images/arrow.png" alt="arrow" style="width:30px; height:25px;" />
                    <asp:HyperLink ID="Product" NavigateUrl="~/ProductList.aspx" runat="server">All Products </asp:HyperLink>
                </div>--%>
               <asp:SiteMapPath ID="SiteMapPath1" runat="server"  >

                    <CurrentNodeStyle CssClass="Some class" />
                    <PathSeparatorTemplate>

                        <img runat="server" alt="arrow" src="~/assets/images/arrow.png" height="15" width="25" />

                    </PathSeparatorTemplate>

                </asp:SiteMapPath>
                <h2 class="title">Products</h2>

                <div class="product-grid">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>


                            <div class="showcase">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/ProductDetail.aspx?id=" + WebApplication2.WebCustomControl1.EncryptQueryString(Eval("product_id").ToString()) %>'>

                                    <div class="showcase-banner">
                                        <asp:Image ID="Image1" runat="server" alt="" ImageUrl='<%# "./assets/images/products/" + Eval("product_image_1").ToString() %>' CssClass="product-img default" />
                                        <asp:Image ID="Image2" runat="server" alt="" ImageUrl='<%# "./assets/images/products/" + Eval("product_image_2").ToString() %>' CssClass="product-img hover" />

                                        <p class="showcase-badge">15%</p>

                                        <div class="showcase-actions">

                                            <asp:HyperLink ID='hypProdDetail' CssClass="btn-action" runat="server" NavigateUrl='<%# "~/ProductDetail.aspx?id=" + WebApplication2.WebCustomControl1.EncryptQueryString(Eval("product_id").ToString()) %>'>
                                            
                                            </asp:HyperLink>
                                            
                                        </div>

                                    </div>

                                    <div class="showcase-content">

                                        <a href="#" class="showcase-category"><%# Eval("product_category") %></a>

                                        <a href="#">

                                            <h3 class="showcase-title"><%# Eval("product_name") %></h3>
                                        </a>

                                        <div class="showcase-rating">
                                            <%--<asp:Label ID="lblAvgRating" runat="server" Text='<%# Eval("avg_rating") %>'></asp:Label>--%>

                                            <asp:Literal ID="litStars" Text="" runat="server"></asp:Literal>

                                        </div>

                                        <div class="price-box">
                                            <p class="price">RM<%# Eval("min_price") %> to RM <%# Eval("max_price") %></p>
                                            <p style="color: hsl(0, 0%, 47%); font-size: 12px;"><%# Eval("total_stock_quantity") %> Left</p>
                                            <%--<del>$75.00</del>--%>
                                        </div>

                                    </div>
                                </asp:HyperLink>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>


    </form>



</asp:Content>