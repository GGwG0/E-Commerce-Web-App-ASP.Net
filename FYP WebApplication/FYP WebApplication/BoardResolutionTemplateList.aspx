<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="BoardResolutionTemplateList.aspx.cs" Inherits="FYP_WebApplication.BoardResolutionTemplateList" %>



<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <style>
        .content-1{
            min-width: 90%;
        }
    </style>
</asp:Content>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="content">
        <div class="content-1">
            <p>Board Resolution Template List</p>
            <a href="CreateBoardResolutionTemplate.aspx" style="text-decoration:none">
            <div class="addBtn">
                    <span style="margin-left: 10px;">Add Template</span>             
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

            <div>

<asp:GridView ID="GridView1" CssClass="gridview" runat="server" OnSorting="GridView1_Sorting" AllowPaging="True" AllowSorting="True"  AutoGenerateColumns="False" DataKeyNames="boardReID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" OnRowCommand="GridView1_RowCommand" CellSpacing="2">
                    <Columns>
                                     
                        <asp:BoundField DataField="boardReID" HeaderText="BR. ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="boardReID">
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
                              <asp:BoundField DataField="created_date" ItemStyle-CssClass="gridItemField" ItemStyle-Width="100px" HeaderStyle-CssClass="gridHeaderField"  DataFormatString="{0:dd MMM yyyy}" HeaderText="Created Date" SortExpression="created_date">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
               
                        <asp:TemplateField HeaderText="Action" >
                            <ItemTemplate>                   
                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/assets/image/edit.png" Width="15px" CommandName="ViewItem" CommandArgument='<%# Eval("boardReID") %>' />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/image/delete.png" Width="15px"  OnClientClick="return confirm('Are you sure you want to delete this template?')" CommandName="DeleteItem" CommandArgument='<%# $"{Eval("boardReID")}"  %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
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
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
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
