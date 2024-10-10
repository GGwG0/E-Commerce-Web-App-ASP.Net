<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="FYP_WebApplication.CompanyList" %>

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
            <p>Manage Company</p>

                           <asp:Button ID="btnAddUser" runat="server" Text="Add Company" CssClass="addBtn"  PostBackUrl="~/CreateCompany.aspx"/>

            <div class="upper-cont">
           
                <div>
                    <%--<i class="fa fa-filter" aria-hidden="true" style="padding: 5px 0px; font-size:15px;"></i>
                    <span>Filter: </span>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlist" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                        <asp:ListItem Text="Waiting" Value="Waiting"></asp:ListItem>
                    </asp:DropDownList>--%>
                </div>
                <div>
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Write something" class="search-table"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="button" />
                </div>
            </div>
            <div>
                <asp:GridView ID="GridView1" CssClass="gridview" runat="server" AllowPaging="True" AllowSorting="True"  AutoGenerateColumns="False" DataKeyNames="companyID"  CellPadding="4" ForeColor="Black" BackColor="#CCCCCC" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting" CellSpacing="2">
                    <Columns>
                        <asp:BoundField DataField="companyID" HeaderText="Company ID" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" InsertVisible="False" ReadOnly="True" SortExpression="companyID">
                            <HeaderStyle CssClass="gridHeaderField" />
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Company Logo" >
                            <ItemTemplate>
                                <asp:Image ID="imgProfile" CssClass="grid" runat="server" Width="30" Height="30" ImageUrl='<%# Eval("Base64ProfilePicture") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="comName" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Company Name" SortExpression="comName">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="comRegNum" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Registration No" SortExpression="comRegNum">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="address" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Address" SortExpression="address">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                        </asp:BoundField>
                        <asp:BoundField DataField="contactNum" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Contact No" SortExpression="contactNum">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField colorField" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="cosecId" ItemStyle-CssClass="gridItemField" HeaderStyle-CssClass="gridHeaderField" HeaderText="Cosec ID" SortExpression="cosecId">
                            <HeaderStyle CssClass="gridHeaderField"></HeaderStyle>
                            <ItemStyle CssClass="gridItemField" />
                         </asp:BoundField>     
                                            
                        <asp:TemplateField HeaderText="Action" >
                            <ItemTemplate>                   
                                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/assets/image/edit.png" Width="15px" CommandName="ViewItem" CommandArgument='<%# Eval("companyID") %>' />                             
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/assets/image/delete.png" Width="15px" OnClientClick="return confirm('Are you sure you want to delete this company?')" CommandName="DeleteItem" CommandArgument='<%# $"{Eval("companyID")}, {Eval("cosecId")}"  %>' />

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