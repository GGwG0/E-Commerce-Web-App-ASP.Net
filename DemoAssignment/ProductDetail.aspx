<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="DemoAssignment.ProductDetail" %>

<asp:Content ID="content2" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="./assets/css/product_detail.css">
    <link rel="stylesheet" href="./assets/css/prod_detail_rating.css">
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

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

        <%--        <asp:Label ID="lblProdCat" runat="server" Text=""></asp:Label>&nbsp >> &nbsp
                <asp:Label ID="lblProdType" runat="server" Text=""></asp:Label>--%>
             

                <div class="product-grid">
                    <div class="product_detail_cont product-flex">
                        <div class="left-product">
                            <div class="main_image">
                               
                                <asp:Image ID="mainImage" runat="server" onclick="img(this)" CssClass="slide img" />
                            </div>
                            <div class="option-prod product-flex">
                                <asp:Image ID="optionImage1" runat="server" onclick="img(this)" class="option-image" />
                                <asp:Image ID="optionImage2" runat="server" onclick="img(this)" class="option-image" />
                                <asp:Image ID="optionImage3" runat="server" onclick="img(this)" class="option-image" />
                            </div>
                        </div>
                        <div class="right-product">
                            <asp:Label ID="lblProdName" CssClass="title" runat="server" Text=""></asp:Label>

                            <p>
                                <asp:Label ID="lblProdDescription" runat="server" Text=""></asp:Label>
                            </p>


                            <h5>Variation</h5>
                            <div>
                                <asp:RadioButtonList ID="rblVariations" runat="server" OnSelectedIndexChanged="rblVariations_SelectedIndexChanged" CssClass="rdb" AutoPostBack="True"></asp:RadioButtonList>
                            </div>
                            <br />
                            <h5>Quantity</h5>
                            <div class="add flex1">
                                <asp:TextBox ID="txtQuantity" TextMode="Number" CssClass="qty" runat="server" Text="1" OnTextChanged="txtQuantity_TextChanged"  AutoPostBack="True"></asp:TextBox>
                            </div>

                            <h5>Total: </h5>

                            <h4><small>
                                <asp:Label ID="txtTotalPrice" runat="server" Text="RM 0.00"></asp:Label></small></h4>

                            <asp:Button ID="btnAddToBag" runat="server" CssClass="button" Text="Add To Bag" OnClick="btnAddToBag_Click" />

                        </div>
                    </div>


                </div>

            </div>
            <div class="product-rating-detail" style="display: flex">
                <div class="prod-details-rating-wrapper">
                    <div class="rating-box">
                        <div class="rating-header">
                            <div class="title">Reviews</div>
                            <div class="info-review">
                                <div class="left">
                                    <%--<img src="image/p1.jpg" alt="" />--%>
                                    <div class="text-containter">
                                        <span class="txt"><asp:Label ID="lblProductName" runat="server" ></asp:Label></span>
                                        <span class="author">
                                <asp:Label ID="lblProdDes" runat="server" Text=""></asp:Label></span>

                                    </div>
                                </div>
                                <!--End of left-->
                                <div class="right"><asp:Label ID="lblProdCatRate" runat="server" Text=""></asp:Label></div>
                                <!-- end of right -->
                            </div>
                            <!-- end of info-review -->
                            <div class="info-rating">
                                <div class="star-count">
                                    <span class="avg"><asp:Label ID="lblAvgRating" runat="server"></asp:Label></span> OUT OF 5
                                </div>
                                <div class="stars">
                                    <div class="avg-stars" style="display:flex;">
                                          <asp:Literal ID="litStarsAvg" Text="" runat="server"></asp:Literal>
                                    </div>
                                    <div class="rating-count">
                                        <div class="count" style="display:inline;"><asp:Label ID="lblRateCount" runat="server"></asp:Label> </div>
                                        ratings
                                    </div>
                                </div>
                                <!-- end of stars -->
                            </div>
                            <!-- end of info rating -->
                            <div class="rating-bar">
                                <div class="bar-item">
                                    <div class="star">
                                        5 <i class="fas fa-star"></i>
                                    </div>
                                    <div class="progress">
                                        <div class="progress-line" style="" runat="server" id="progress_line_5"></div>
                                    </div>
                                    <div class="percent"> <asp:Label ID="lblPercent5" runat="server"  Text="0" ></asp:Label>%</div>
                                </div>

                                <div class="bar-item">
                                    <div class="star">
                                        4 <i class="fas fa-star"></i>
                                    </div>
                                    <div class="progress">
                                        <div class="progress-line" style=""  runat="server" id="progress_line_4"></div>
                                    </div>
                                    <div class="percent"><asp:Label ID="lblPercent4" runat="server"  Text="0" ></asp:Label>%</div>
                                </div>

                                <div class="bar-item">
                                    <div class="star">
                                        3 <i class="fas fa-star"></i>
                                    </div>
                                    <div class="progress">
                                        <div class="progress-line" style=""  runat="server" id="progress_line_3"></div>
                                    </div>
                                    <div class="percent"><asp:Label ID="lblPercent3" runat="server"  Text="0" ></asp:Label>%</div>
                                </div>

                                <div class="bar-item">
                                    <div class="star">
                                        2 <i class="fas fa-star"></i>
                                    </div>
                                    <div class="progress">
                                        <div class="progress-line" style="" runat="server" id="progress_line_2"></div>
                                    </div>
                                    <div class="percent"><asp:Label ID="lblPercent2" runat="server" Text="0" ></asp:Label>%</div>
                                </div>
                                <div class="bar-item">
                                    <div class="star">
                                        1 <i class="fas fa-star"></i>
                                    </div>
                                    <div class="progress">
                                        <div class="progress-line" style="" runat="server" id="progress_line_1"></div>
                                    </div>
                                    <div class="percent"><asp:Label ID="lblPercent1" runat="server" Text="0" ></asp:Label> %</div>
                                </div>
                            </div>
                            <!-- end of rating-bar -->
                        </div>
                        <!-- End of rating header -->
                        <div class="review-box">
                            <div class="review-header">
                                <div class="count-review"><span></span>Reviews</div>
                                <div class="txt btn-write">
                                    Write a review
                                </div>
                            </div>
                            <!-- end of review header -->

                                <div class="review-content">

                                    <asp:Label ID="lblText" CssClass="lbl" runat="server" Text="" ></asp:Label>
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                        <ItemTemplate>
                                            
                                            <div class="review-per-person" >
                                                
                                                <div class="user-review">
                                                   
                                                    <div class="user-rating"  >
                                                        <div class="username">
                                                            <%# Eval("name") %>
                                                        </div>
                                                        <div class="showcase-rating">
                                                            
                                                            <asp:Literal ID="litStars" Text="" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <!-- end of user rating -->

                                                    <div class="comment-content">
                                                        <span class="variation-comment">Variation: &nbsp; <%# Eval("variation_name") %><br /></span>
                                                        <%# Eval("comment") %>
                                                    </div>
                                                    <time><%# Eval("created_at") %>May 14, 2023</time>
                                                </div>
                                                <div class="admin-review" id="divAdminReview" runat="server">
                                                    <div class="user-rating">
                                                        <div class="username">Admin</div>
                                                    </div>
                                                    <!-- end of user rating -->

                                                    <div class="comment-content">
                                                        <%# Eval("adminReply") %>
                                                    </div>
                                          
                                                </div>
                                            </div>

                                            
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                           
                            <!-- end of review content -->
                        </div>
                        <!-- end of review box -->

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        function img(img) {
            // Remove border from all option images
            const optionImages = document.querySelectorAll('.option-image');
            optionImages.forEach(function (optionImage) {
                optionImage.style.border = 'none';
                optionImage.style.transform = 'scale(1)';
                optionImage.style.zIndex = '0';
            });

            // Add border to the clicked option image
            img.style.border = '0.5px solid #D0D0D0';
            img.style.width = '80px';
            img.style.height = '80px';
            img.style.transform = "scale(1.5)";
            img.style.zIndex = '1';

            // Change main image to the clicked option image
            const mainImage = document.querySelector('.slide');
            mainImage.src = img.src;
        }

    </script>
</asp:Content>