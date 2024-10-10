<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SelectTemplate2.aspx.cs" Inherits="FYP_WebApplication.SelectTemplate2" %>


<asp:Content ID="content2" ContentPlaceHolderID="head" runat="server">
    


    <style>
        .main-container{
            display:flex;
            flex-direction:row;
            justify-content: space-around;
        }


        .pdf-container{
   
        }
        .sub-container{
    background-color: #fff;
    box-shadow: 0 1px 0 rgba(0,0,0,.07);
    color: #444;
    display: inline-block;
    margin-bottom: 20px;
    margin-right: 20px;
    width: 208px;
    border: 1px solid #dfe1e5;
    border-radius: 3px;
    box-shadow: none;
    cursor: pointer;
    opacity: 1;
        }

        
        .sub-container:hover{
            transition: ease-in-out;
            border:1px solid black;
        }

        .font{
    border-top: 1px solid #e2e2e2;
    padding: 16px 8px 14px 16px;
    position: relative;
        }

    </style>
    <title>Board Resolution Viewer</title>
</asp:Content>
    



    


<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="text-align:center">Select Board Resolution Template</h2>
        <div class="main-container">
            <asp:Repeater ID="repeater1" runat="server">
                <ItemTemplate>
                   <a runat="server" href='<%# "EditBoardResolutionForm.aspx?id=" + Eval("boardReID") + "&reqID=" + Request.QueryString["reqID"] %>' style="text-decoration:none; color:none;">
                    <div class="sub-container">
                        
                        <div class="pdf-container">
                            <iframe src='<%# Eval("Base64Attachment") %>' width="200" height="200"  ></iframe>
                        </div>
                        <div class="font">
                            <h4><%# Eval("title") %></h4>
                        </div>
                       
                    </div>
                         </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Content>
