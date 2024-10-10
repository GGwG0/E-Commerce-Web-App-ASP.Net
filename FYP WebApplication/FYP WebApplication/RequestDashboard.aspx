<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestDashboard.aspx.cs" Inherits="FYP_WebApplication.RequestDashboard" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <style>
        .fa-level-up, .fa-level-down {
    padding: 5px;
}
    </style>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <%--left--%>

        <div class="content-1">
            <p>Request Dashboard</p>

            <asp:Button ID="btnAddRequest" runat="server" Text="Add Request" CssClass="addBtn" PostBackUrl="~/CreateRequest.aspx"  />
            
            <div class="upper-cont">
                <div>
                    <i class="fa fa-filter" aria-hidden="true" style="padding: 5px 0px; font-size:15px;"></i>
                    <span>Filter: </span>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="New" Value="New"></asp:ListItem>
                        <asp:ListItem Text="Verification" Value="Verification"></asp:ListItem>
                        <asp:ListItem Text="Published" Value="Published"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                        <asp:ListItem Text="Incomplete" Value="Incomplete"></asp:ListItem>
                        <asp:ListItem Text="Deleted" Value="Deleted"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Write something" class="search-table"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="button" />

                </div>
            </div>

            <div>
                <asp:GridView ID="GridView1" CssClass="gridview" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"  AutoGenerateColumns="False" DataKeyNames="requestID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" CellSpacing="2" OnSorting="GridView1_Sorting" >
                    <Columns>
                  
                      
                        <asp:BoundField DataField="requestID" HeaderText="Request ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="requestID">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="title" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Title" SortExpression="title">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="type" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Type" SortExpression="type">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Status" SortExpression="status">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField colorField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="description" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Description" SortExpression="description">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                              <asp:BoundField DataField="createdDate" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField"  DataFormatString="{0:dd MMM yyyy}" HeaderText="Created Date" SortExpression="createdDate">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                         <asp:BoundField DataField="dueDate" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField"  DataFormatString="{0:dd MMM yyyy}" HeaderText="Due Date" SortExpression="dueDate">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                          <asp:BoundField DataField="comName" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField"  HeaderText="Company Name" SortExpression="comName">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Action" >
                            <ItemTemplate>                   
                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/assets/image/edit.png" Width="15px" CommandName="ViewItem" CommandArgument='<%# Eval("requestID") %>' />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/image/delete.png" Width="15px"  OnClientClick="return confirm('Are you sure you want to delete this request?')" CommandName="DeleteItem" CommandArgument='<%# $"{Eval("requestID")},{Eval("status")}"  %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="actionStyle gridItemField" />
                        </asp:TemplateField>
                      

                    </Columns>
                    <EmptyDataTemplate>
                      <div class="dataTables_empty">
                             No records found
                      </div>
                   
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle ForeColor="Red" BackColor="White" Font-Bold="true" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="#f7f7f7" Font-Bold="True" ForeColor="#82766a" Height="40" />
                    <%--<HeaderStyle Font-Bold="False" BackColor="#808080" ForeColor="white" Height="40" />--%>
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center"  />
                    <RowStyle CssClass="gridRowStyle" BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" ForeColor="white" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" ForeColor="white" />
                </asp:GridView>

                  <!-- Paging -->
      <%--      <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                </Fields>
            </asp:DataPager>--%>
            </div>
        </div>
        <%--right--%>
        <div class="cont-right">
            <div>
         

                <table class="dash-table">

                    <tr>        
                        <td>
                            <div>
                                <p class="dash-table-title-1">Completed Request</p>
                                <span class="dash-table-title-2" runat="server" id="lblCompletedNum"></span>
                                <span class="dash-table-title-3" runat="server" id="lblCompletedPercent"></span>
                                <p class="dash-table-title-4">Requests</p>
                            </div>
                        </td>
                        <td>
                           
                            <div>
                                <p class="dash-table-title-1">Ongoing Request</p>
                                <span class="dash-table-title-2" runat="server" id="lblVerification"></span>
                                <span class="dash-table-title-3" runat="server" id="lblVerificationPercent"></span>
                                <p class="dash-table-title-4">Requests</p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <p class="dash-table-title-1">New Request</p>
                                <span class="dash-table-title-2" runat="server" id="lblNew"></span>
                                <span class="dash-table-title-3" runat="server" id="lblNewPercent"></span>
                                <p class="dash-table-title-4">Requests</p>
                            </div>
                        </td>
                        <td>
                            <div>
                                <p class="dash-table-title-1">Incomplete Request</p>
                                <span class="dash-table-title-2" runat="server" id="lblIncomplete"></span>
                                <span class="dash-table-title-3" runat="server" id="lblIncompletePercent"></span>
                                <p class="dash-table-title-4">Requests</p>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="notification-cont">
                <p class="noti-title">Notification</p>
                
                <asp:Repeater ID="Repeater1" runat="server" >
                    <ItemTemplate>
                        <div class="notification">
                            <%--<asp:Image ID="ProfileImage" runat="server" Width="30" Height="30" CssClass="profile" AlternateText="Profile Picture"></asp:Image>--%>
                           <img src='<%# Eval("Base64ProfilePicture") %>' width="30" height="30" class="profile" alt="Profile Picture" />
                            <div class="notification-content">
                                <strong class="notification-name">@<%# Eval("username") %></strong>
                               <asp:HyperLink ID="hypRequestLink" CssClass="action" Text='<%# Eval("action") %>' NavigateUrl='<%# "~/ViewRequestDetail.aspx?id=" + Eval("requestID") %>' runat="server"></asp:HyperLink>
                                 <p class="notification-date"><%# Eval("FormattedDate") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>
  


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Request]"></asp:SqlDataSource>
</asp:Content>