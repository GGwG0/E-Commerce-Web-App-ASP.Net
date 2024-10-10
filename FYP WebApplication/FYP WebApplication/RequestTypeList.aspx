<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestTypeList.aspx.cs" Inherits="FYP_WebApplication.RequestTypeList" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <style>
        .page-content{
            /*width:75%;*/  
        }
    </style>
</asp:Content>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <h1>Request Type List</h1>
    <a href="CreateRequestType.aspx">
            <div class="addBtn">
                    <i class="fa fa-plus" aria-hidden="true" style="padding: 0px; font-size:10px;"></i>
                    <span style="margin-left: 10px; width:fit-content;">Add Request Type</span>             
            </div>
        </a>
            <div class="upper-cont">
                <div>
                    
                </div>
                <div>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Write something" class="search-table"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="button" />

                </div>
            </div>



<asp:GridView ID="GridView1" CssClass="gridview" runat="server" OnSorting="GridView1_Sorting" AllowPaging="True" AllowSorting="True"  AutoGenerateColumns="False" DataKeyNames="requestID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" OnRowCommand="GridView1_RowCommand" CellSpacing="2">
                    <Columns>
                                     
                        <asp:BoundField DataField="requestID" HeaderText="Request ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="requestID">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="title" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Title" SortExpression="title">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        
                    
                        <asp:BoundField DataField="description" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Description" SortExpression="description">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                              <asp:BoundField DataField="createdDate" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField"  DataFormatString="{0:dd MMM yyyy}" HeaderText="Created Date" SortExpression="createdDate">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
               
                        <asp:TemplateField HeaderText="Action" >
                            <ItemTemplate>                   
                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/assets/image/edit.png" Width="15px" CommandName="ViewItem"  CommandArgument='<%# Eval("requestID") %>' />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/image/delete.png" Width="15px"  OnClientClick="return confirm('Are you sure you want to delete this request?')" CommandName="DeleteItem"  CommandArgument='<%# $"{Eval("requestID")}"  %>' />
                            </ItemTemplate>
                            <ItemStyle  CssClass="actionStyle gridItemField" />
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
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle CssClass="gridRowStyle" BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" ForeColor="white" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" ForeColor="white" />
                </asp:GridView>

</asp:Content>
