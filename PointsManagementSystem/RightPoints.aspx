<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RightPoints.aspx.cs" Inherits="RightPoints" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr align =left >
            <td colspan="3"  valign =top align =left >
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:start_event_time%>" /></span>
                <asp:TextBox ID="txtStartEventTime" runat="server" Width="98px"></asp:TextBox>
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:end_event_time%>" /></span>
                <asp:TextBox ID="txtEndEventTime" runat="server" Width="98px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearck" runat="server" Text="<%$ Resources:search%>" OnClick="btnSearch_Click" ForeColor="DodgerBlue" />
            </td>
        </tr>
    </table>
    </form>
    <asp:DataList ID="points" runat="server"  Font-Size="9pt" >
        <ItemTemplate>
            <table cellpadding="0" cellspacing="0" style="width: 950px">
                <tr>
                    <td style="width:25px"><asp:Label ID="Label10" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"fill_user") %>'></asp:Label></td>
                    <td style="width:45px"><asp:Label ID="labID" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"a") %>'></asp:Label></td>
                    <td style="width:45px"><asp:Label ID="Label1" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"b") %>'></asp:Label></td>
                </tr> 
            </table>
        </ItemTemplate>

        <HeaderTemplate>
            <table>
                <tr>
                    <td style="width:25px"><asp:Literal runat="server" Text="<%$ Resources:name%>" /></td>
                    <td style="width:45px"><asp:Literal runat="server" Text="<%$ Resources:reward%>" /></td>
                    <td style="width:45px"><asp:Literal runat="server" Text="<%$ Resources:punished%>" /></td>
                </tr>
            </table>
        </HeaderTemplate>
    </asp:DataList> 
</body>
</html>
