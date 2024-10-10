<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="BoardResolutionDashboard.aspx.cs" Inherits="FYP_WebApplication.BoardResolutionDashboard" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <style>
        .content-1 {
            min-width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="content-1">
            <p style="margin-bottom: 30px;">Board Resolution List</p>
            <div class="upper-cont">
                <div>
                    <i class="fa fa-filter" aria-hidden="true" style="padding: 5px 0px; font-size: 15px;"></i>
                    <span>Request Progress: </span>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Deleted" Value="Deleted"></asp:ListItem>
                        <asp:ListItem Text="Verification" Value="Verification"></asp:ListItem>
                        <asp:ListItem Text="Published" Value="Published"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                        <asp:ListItem Text="Incomplete" Value="Incomplete"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Write something" class="search-table"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="button" />
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" CssClass="gridview" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"  AutoGenerateColumns="False" DataKeyNames="requestID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" CellSpacing="2" OnSorting="GridView1_Sorting">
                    <Columns>
                        <asp:BoundField DataField="boardReID" HeaderText="Board ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="boardReID">
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
                        <asp:BoundField DataField="description" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Description" SortExpression="description">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="created_date" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField" DataFormatString="{0:dd MMM yyyy}" HeaderText="Created Date" SortExpression="created_date">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="createdBy" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Created By" SortExpression="createdBy">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="requestID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Request ID" SortExpression="requestID">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/assets/image/edit.png" Width="15px" CommandName="ViewItem"  CommandArgument='<%# $"{Eval("boardReID")}, {Eval("requestID")}"  %>' />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/image/delete.png" Width="15px" OnClientClick="return confirm('Are you sure you want to delete this request?')" CommandName="DeleteItem" CommandArgument='<%# $"{Eval("boardReID")}, {Eval("requestID")}"  %>' />
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
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle CssClass="gridRowStyle" BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" ForeColor="white" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" ForeColor="white" />
                </asp:GridView>

            </div>
        </div>
    </div>

</asp:Content>