﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubForumPage.aspx.cs" Inherits="WebApplication.SubForumPage" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


		.rounded-corners{
	 
		-webkit-border-radius: 10px; 
		-moz-border-radius: 10px;
		}
		.auto-style3 {
            width: 263px;
            height: 113px;
        }
		.auto-style4 {
            font-size: medium;
        }
        .rounded-corners-light{
	 
		-webkit-border-radius: 5px; 
		-moz-border-radius: 5px;
		}
 
.ModalPopupBG
{
    background-color: #666699;
    filter: alpha(opacity=50);
    opacity: 0.7;
}

.HellowWorldPopup
{
    min-width:200px;
    min-height:150px;
    background:white;
}


        </style>
</head>
<body style="background-color: #C0C0C0">
    <form id="form1" runat="server">

<asp:panel id="Panel4" style="display: none" runat="server">
	<div class="HellowWorldPopup">
                <div class="PopupHeader" id="PopupHeader">Header</div>
                <div class="PopupBody">
                    <p>This is a simple modal dialog</p>
                </div>
                <div class="Controls">
                    <input id="btnOkay" type="button" value="Done" />
                    <input id="btnCancel" type="button" value="Cancel" />
		</div>
        </div>
</asp:panel>
    <div>
        <asp:Panel ID="Panel1" runat="server" BackColor="Black" BorderColor="Black" BorderWidth="4px" CssClass="rounded-corners" Height="81px" Width ="1104px">
            <img alt="" class="auto-style3" src="images/logo.png" />
            <asp:Panel ID="Panel3" runat="server" Height="420px" style="margin-left: 806px; margin-top: -22px; color: #FFFFFF;" Width="300px" BackColor="Black" CssClass="rounded-corners">
                 <br />&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelLogin" runat="server" style="font-size: medium" Text="logged in as "></asp:Label>
                 <br /><asp:Button ID="BtnLogout" runat="server" style="margin-left: 7px; margin-top: 14px; background-color: #000000; text-decoration: underline; color: #0099FF; font-size: medium;" Text="logout" CssClass="rounded-corners-light" Width="56px" Height="28px" ForeColor="White" BorderColor="Black" BackColor="Black" BorderStyle="Solid" BorderWidth="0px" OnClick="BtnLogout_Click"  />
                <br /> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="LabelUserName" runat="server" style="font-size: medium" Text="user name:"></asp:Label>
                <br /><asp:TextBox ID="TextBox1" runat="server" CssClass="rounded-corners-light" style="margin-left: 55px; margin-top: 2px"></asp:TextBox>
                <br /><br /> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="LabelPassword" runat="server" style="font-size: medium" Text="password:"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="rounded-corners-light" style="margin-left: 55px; margin-top: 2px" TextMode="Password"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" style="margin-left: 57px; margin-top: 14px; background-color: #CCCCCC;" Text="login" CssClass="rounded-corners-light" Width="56px" Height="28px" OnClick="Button1_Click"/>
            </asp:Panel>
            &nbsp;</asp:Panel>
    
    </div>
        <asp:Panel ID="Panel2" runat="server" BackColor="Black" CssClass="rounded-corners" Height="420px" style="margin-right: 4px; margin-top: 10px; color: #FFFFFF;" Width="800px" >
            <br />
            <strong><span class="auto-style4">&nbsp;&nbsp; </span>
            <asp:Label ID="Label1" runat="server" style="font-size: large" Text="Label"></asp:Label>
            </strong><br />
            <strong><span class="auto-style4">&nbsp;&nbsp; </span>
            <asp:Button ID="BtnAdd" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" CssClass="auto-style4" ForeColor="White" Height="28px" style="margin-left: 0px; margin-top: 0px; background-color: #000000; text-decoration: underline; color: #0099FF; font-size: medium;" Text="add thread" Width="92px" OnClick="BtnAdd_Click"/>
            </strong><br />
            <asp:ListBox ID="ListBox1" runat="server" Height="304px" style="background-color: #99CCFF; margin-left: 11px; margin-top: 2px;" Width="580px" ForeColor="Black" ></asp:ListBox>
            <asp:Button ID="Button2" runat="server" style="margin-left: 41px; background-color: #CCCCCC;" Text="Ok" CssClass="rounded-corners-light" Width="56px" Height="28px" OnClick="Button2_Click"/>
            <br />
            <asp:Button ID="BtnBack" runat="server" BackColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px" CssClass="auto-style4" ForeColor="White" Height="28px" style="margin-left: 7px; margin-top: 0px; background-color: #000000; text-decoration: underline; color: #0099FF; font-size: medium;" Text="back" Width="56px" OnClick="BtnBack_Click" />
        </asp:Panel>
    </form>
</body>
</html>
