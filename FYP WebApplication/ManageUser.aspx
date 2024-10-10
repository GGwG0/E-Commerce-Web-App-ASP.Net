<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="FYP_WebApplication.ManageUser" %>

<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/gridview.css" />
    <style>
        .content-1{
            min-width: 90%;
        }
        .grid{
            border-radius: 50%;
        }

        td{
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="content-1">
            <p>Manage User</p>

            <div>
            <asp:Button ID="btnAddUser" runat="server" Text="Add User" CssClass="addBtn"  PostBackUrl="~/CreateUser.aspx"/>
           </div>
            <div class="upper-cont">
                <div>
                    <i class="fa fa-filter" aria-hidden="true" style="padding: 5px 0px; font-size:15px;"></i>
                    <span>Filter: </span>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                        <asp:ListItem Text="Waiting" Value="Waiting"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Write something" class="search-table"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="button" />
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" CssClass="gridview" runat="server" AllowPaging="True" AllowSorting="True"  AutoGenerateColumns="False" DataKeyNames="userID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting" CellSpacing="2">
                    <Columns>
                        <asp:BoundField DataField="userID" HeaderText="User ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="userID">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Profile" >
                            <ItemTemplate>
                                <asp:Image ID="imgProfile" CssClass="grid" runat="server" Width="30" Height="30" ImageUrl='<%# Eval("Base64ProfilePicture") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="status" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Status" SortExpression="status">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="username" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Username" SortExpression="username">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="name" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Name" SortExpression="name">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="position" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Position" SortExpression="position">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField colorField" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="email" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Email" SortExpression="email">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                         </asp:BoundField>     
                        <asp:BoundField DataField="comName" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Company Name" SortExpression="comName">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                         </asp:BoundField>  
                        <asp:BoundField DataField="RoleNames" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Role Name" SortExpression="RoleNames">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                         </asp:BoundField>                       
                        <asp:TemplateField HeaderText="Action" >
                            <ItemTemplate>                   
                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/assets/image/edit.png" Width="15px" CommandName="ViewItem" CommandArgument='<%# Eval("userID") %>' />                             
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
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
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