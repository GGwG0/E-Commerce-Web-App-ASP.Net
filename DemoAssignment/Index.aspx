<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DemoAssignment.Index" %>

<%@ Import Namespace="WebApplication2" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main>

        <div class="banner">
            <div class="container">
                <div class="slider-container has-scrollbar">

                    <div class="slider-item">

                        <img src="./assets/images/index_banner.png" alt="computer hardware" class="banner-img">

                        <div class="banner-content">

                            <a href="ProductList.aspx" class="banner-btn">Shop now</a>

                        </div>

                    </div>

                    <div class="slider-item">

                        <img src="./assets/images/banner-2.jpg" alt="modern sunglasses" class="banner-img">

                        <div class="banner-content">

                            <p class="banner-subtitle"></p>

                            <h2 class="banner-title"></h2>

                            <p class="banner-text">
                                <b></b>
                            </p>

                            <a href="ProductList.aspx" class="banner-btn">Shop now</a>

                        </div>

                    </div>


                </div>

            </div>

        </div>


        <!--
      - PRODUCT
    -->

        <div class="product-container">

            <div class="container">


                <!--
          - SIDEBAR
        -->

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
                                            <data value="300" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=1&type=2" class="sidebar-submenu-title">
                                            <p class="product-name">External SSD</p>
                                            <data value="60" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=1&type=3" class="sidebar-submenu-title">
                                            <p class="product-name">Flash Drives</p>
                                            <data value="50" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=1&type=4" class="sidebar-submenu-title">
                                            <p class="product-name">Internal Hard Drives</p>
                                            <data value="87" class="stock" title="Available Stock"></data>
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
                                            <data value="45" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=2&type=2" class="sidebar-submenu-title">
                                            <p class="product-name">Flat Monitors</p>
                                            <data value="75" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=2&type=3" class="sidebar-submenu-title">
                                            <p class="product-name">Gaming Monitors</p>
                                            <data value="35" class="stock" title="Available Stock"></data>
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
                                            <data value="46" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=3&type=2" class="sidebar-submenu-title">
                                            <p class="product-name">Keyboard & Mouse</p>
                                            <data value="73" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=3&type=3" class="sidebar-submenu-title">
                                            <p class="product-name">Projector</p>
                                            <data value="61" class="stock" title="Available Stock"></data>
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
                                            <data value="12" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=4&type=2" class="sidebar-submenu-title">
                                            <p class="product-name">Range Extender / Powerline</p>
                                            <data value="60" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=4&type=3" class="sidebar-submenu-title">
                                            <p class="product-name">Smart Home & IP Cameras</p>
                                            <data value="50" class="stock" title="Available Stock"></data>
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
                                            <data value="68" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=5&type=2" class="sidebar-submenu-title">
                                            <p class="product-name">Printers & Scanners</p>
                                            <data value="46" class="stock" title="Available Stock"></data>
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
                                            <data value="50" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>

                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=6&type=2" class="sidebar-submenu-title">
                                            <p class="product-name">Mobile Phones</p>
                                            <data value="48" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>
                                    <li class="sidebar-submenu-category">
                                        <a href="ProductList.aspx?cat=6&type=3" class="sidebar-submenu-title">
                                            <p class="product-name">Tablets</p>
                                            <data value="48" class="stock" title="Available Stock"></data>
                                        </a>
                                    </li>
                                </ul>
                            </li>

                        </ul>

                    </div>
                </div>


                <div class="product-box">
                    <!--
            - PRODUCT MINIMAL
          -->
                    <form runat="server">

                        <div class="product-minimal">

                            <!-- new arrivals-->
                            <div class="product-showcase">

                                <h2 class="title">New Arrivals</h2>

                                <div class="showcase-wrapper has-scrollbar">

                                    <div class="showcase-container">

                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <div class="showcase">

                                                    <asp:HyperLink ID="hypNewProd" NavigateUrl='<%# "~/ProductDetail.aspx?id=" + WebApplication2.WebCustomControl1.EncryptQueryString(Eval("product_id").ToString()) %>' CssClass="showcase-img-box" runat="server">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# "./assets/images/products/" + Eval("product_image_1").ToString() %>' Width="70" CssClass="showcase-img" />
                                                    </asp:HyperLink>


                                                    <div class="showcase-content">

                                                        <a href="#">
                                                            <h4 class="showcase-title"><%# Eval("product_name") %></h4>
                                                        </a>
                                                        <p class="showcase-category"><%# Eval("product_category") %></p>
                                                        <div class="price-box">
                                                            <p class="price">RM<%# String.Format("{0:0.00}", Eval("min_price")) %></p>

                                                            <%-- <del>$12.00</del>--%>
                                                        </div>

                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                            </div>

                            <!-- trending -->
                            <div class="product-showcase">

                                <h2 class="title">Trending</h2>

                                <div class="showcase-wrapper  has-scrollbar">

                                    <div class="showcase-container">

                                        <asp:Repeater ID="Repeater3" runat="server">
                                            <ItemTemplate>
                                                <div class="showcase">

                                                    <asp:HyperLink ID="hypNewProd" NavigateUrl='<%# "~/ProductDetail.aspx?id=" + WebApplication2.WebCustomControl1.EncryptQueryString(Eval("product_id").ToString()) %>' CssClass="showcase-img-box" runat="server">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# "./assets/images/products/" + Eval("product_image_1").ToString() %>' Width="70" CssClass="showcase-img" />
                                                    </asp:HyperLink>


                                                    <div class="showcase-content">

                                                        <a href="#">
                                                            <h4 class="showcase-title"><%# Eval("product_name") %></h4>
                                                        </a>
                                                        <p class="showcase-category"><%# Eval("product_category") %></p>
                                                        <div class="price-box">
                                                            <p class="price">RM<%# String.Format("{0:0.00}", Eval("min_price")) %></p>

                                                            <%-- <del>$12.00</del>--%>
                                                        </div>

                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>


                                </div>

                            </div>

                            <!-- top rated -->
                            <div class="product-showcase">

                                <h2 class="title">Top Rated</h2>

                                <div class="showcase-wrapper  has-scrollbar">


                                    <div class="showcase-container">

                                        <asp:Repeater ID="Repeater4" runat="server">
                                            <ItemTemplate>
                                                <div class="showcase">

                                                    <asp:HyperLink ID="hypNewProd" NavigateUrl='<%# "~/ProductDetail.aspx?id=" + WebApplication2.WebCustomControl1.EncryptQueryString(Eval("product_id").ToString()) %>' CssClass="showcase-img-box" runat="server">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# "./assets/images/products/" + Eval("product_image_1").ToString() %>' Width="70" CssClass="showcase-img" />
                                                    </asp:HyperLink>


                                                    <div class="showcase-content">

                                                        <a href="#">
                                                            <h4 class="showcase-title"><%# Eval("product_name") %></h4>
                                                        </a>
                                                        <p class="showcase-category"><%# Eval("product_category") %></p>

                                                        <div class="price-box">
                                                            <p class="price">RM<%# String.Format("{0:0.00}", Eval("min_price")) %></p>

                                                            <%-- <del>$12.00</del>--%>
                                                        </div>

                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>

                                </div>

                            </div>

                        </div>


                        <!--PRODUCT GRID  --->
                        <div class="product-main">

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
                                                        <p style="color: hsl(0, 0%, 47%); font-size: 5px;"><%# Eval("total_stock_quantity") %> Left</p>
                                                        <%--<del>$75.00</del>--%>
                                                    </div>

                                                </div>
                                            </asp:HyperLink>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>


                        </div>
                    </form>
                </div>
            </div>

        </div>

    </main>
</asp:Content>
