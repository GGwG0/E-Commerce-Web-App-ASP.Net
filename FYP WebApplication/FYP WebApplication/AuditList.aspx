<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AuditList.aspx.cs" Inherits="FYP_WebApplication.WebForm1" %>

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
            <p style="margin-bottom: 30px;">Audit List</p>
            <div class="upper-cont">
                <div>
                    
                </div>
                <div>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Write something" class="search-table"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="button" />
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" CssClass="gridview" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"  AutoGenerateColumns="False" DataKeyNames="audit_ID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" CellSpacing="2" OnSorting="GridView1_Sorting">
                    <Columns>
                        <asp:BoundField DataField="audit_ID" HeaderText="Audit ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="audit_ID">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="req_ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Request ID" SortExpression="req_ID">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Action" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Action" SortExpression="Action">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="date" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField" DataFormatString="{0:dd MMM yyyy HH:mm:ss}" HeaderText="Created Date" SortExpression="date">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" Width="300" />
                        </asp:BoundField>
                        <asp:BoundField DataField="username" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Username" SortExpression="username">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="comName" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Company Name" SortExpression="comName">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>


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