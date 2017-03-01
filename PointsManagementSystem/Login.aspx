<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
            <tr>
                <td><asp:Literal runat="server" Text="<%$ Resources:username%>" /></td>
                <td><asp:TextBox ID="txtUserName" runat="server" Width="98px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Literal runat="server" Text="<%$ Resources:password%>" /></td>
                <td><asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="98px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Literal runat="server" Text="<%$ Resources:language%>" /></td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Text="English" Value="en-US"></asp:ListItem>
                        <asp:ListItem Text="简体中文" Value="zh-CN"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="<%$ Resources:login%>" OnClick="btnLogin_Click" ForeColor="DodgerBlue" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
