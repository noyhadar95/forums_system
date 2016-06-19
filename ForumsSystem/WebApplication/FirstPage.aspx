<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="WebApplication.FirstPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 263px;
            height: 113px;
        }
		.rounded-corners{
	 
		-webkit-border-radius: 10px; 
		-moz-border-radius: 10px;
		}
		.rounded-corners-light{
	 
		-webkit-border-radius: 5px; 
		-moz-border-radius: 5px;
		}
        .auto-style4 {
            font-size: medium;
        }
        .auto-style5 {
            font-size: large;
        }
    </style>
</head>
<body style="background-color: #C0C0C0">
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" BackColor="Black" BorderColor="Black" BorderWidth="4px" CssClass="rounded-corners" Height="81px">
            <img alt="" class="auto-style3" src="images/logo.png" />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" BackColor="Black" CssClass="rounded-corners" Height="423px" style="margin-right: 4px; margin-top: 10px; color: #FFFFFF;" >
            <br />
            <strong><span class="auto-style4">&nbsp;&nbsp; </span><span class="auto-style5">Welcome to TimTimTeam Forum system! please choose a forum</span></strong><br />
            <asp:ListBox ID="ListBox1" runat="server" Height="304px" style="background-color: #99CCFF; margin-left: 11px; margin-top: 25px;" Width="580px" ForeColor="Black" ></asp:ListBox>
            <asp:Button ID="Button1" runat="server" style="margin-left: 30px; margin-top: 0px" Text="Ok" Width="56px" BackColor="#CCCCCC" CssClass="rounded-corners-light" Height="28px" OnClick="Button1_Click1" />
        </asp:Panel>
        <p>
            &nbsp;</p>

    </form>
    <p>
        &nbsp;</p>
</body>
</html>
