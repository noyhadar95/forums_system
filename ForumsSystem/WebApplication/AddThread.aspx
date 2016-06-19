<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddThread.aspx.cs" Inherits="WebApplication.AddThread" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


.HellowWorldPopup
{
    min-width:200px;
    min-height:150px;
    background:white;
}


        .rounded-corners{
	 
		-webkit-border-radius: 10px; 
		-moz-border-radius: 10px;
		}
		.auto-style3 {
            width: 263px;
            height: 113px;
        }
		.rounded-corners-light{
	 
		-webkit-border-radius: 5px; 
		-moz-border-radius: 5px;
		}
 
		.auto-style4 {
            font-size: medium;
        }
        </style>
</head>
<body style="background-color: #C0C0C0">
    <form id="form2" runat="server">

    <div>
        <asp:Panel ID="Panel1" runat="server" BackColor="Black" BorderColor="Black" BorderWidth="4px" CssClass="rounded-corners" Height="81px" Width ="1104px">
            <img alt="" class="auto-style3" src="images/logo.png" />
            &nbsp;</asp:Panel>
    
    </div>
        <asp:Panel ID="Panel2" runat="server" BackColor="Black" CssClass="rounded-corners" Height="420px" style="margin-right: 4px; margin-top: 10px; color: #FFFFFF;" Width="1112px" >
            <br />
            <strong><span class="auto-style4">&nbsp;&nbsp; </span>
            <asp:Label ID="Label1" runat="server" style="font-size: large" Text="Label"></asp:Label>
            </strong><br />
            <strong><span class="auto-style4">&nbsp;&nbsp; </span>
            </strong>
            <br /> &nbsp;&nbsp;
            <asp:Label ID="LabelTitle" runat="server" style="font-size: large" Text="Title:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" style="margin-top: 9px; margin-left: 13px;" Height="22px" Width="418px"></asp:TextBox>
            <br />
            <br/> &nbsp;&nbsp;
            <asp:Label ID="LabelContent" runat="server" style="font-size: large" Text="Content:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" Height="195px" style="margin-left: 14px; margin-top: 12px;" Width="417px"></asp:TextBox>
            <br />
            <asp:Button ID="BtnBack" runat="server"  style="margin-left: 16px; margin-top: 20px;" CssClass="rounded-corners-light" Height="28px" Width="55px" Text="cancel" OnClick="BtnBack_Click"/>
            
            <asp:Button ID="BtnAdd" runat="server" style="margin-left: 20px" Text="save" CssClass="rounded-corners-light" Height="28px" Width="55px" OnClick="BtnAdd_Click"/>
            
        </asp:Panel>
    </form>
</body>
</html>
