<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPointsBill.aspx.cs" Inherits="PrintPointsBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
    </div>
    </form>

    <asp:DataList ID="points" runat="server"  Font-Size="9pt" RepeatColumns="2">
        <ItemTemplate>
            <table cellpadding="0" cellspacing="0" style="width: 300px;border:1px solid #000;">
                <tr>
                    <td colspan="2" style="text-align:center;font-weight:bold"><asp:Literal runat="server" Text="<%$ Resources:title%>" /></td>
                </tr>
                <tr>
                    <td style="width:25px"><asp:Literal runat="server" Text="<%$ Resources:staff%>" /><asp:Label ID="Label10" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"real_name_1") %>'></asp:Label></td>
                    <td></td>
                </tr> 
                <tr>
                    <td colspan="2" style="width:150px"><asp:Label ID="Label2" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"event") %>'></asp:Label><asp:Literal runat="server" Text="<%$ Resources:content_message_1%>" /><asp:Label ID="Label4" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"point_value") %>'></asp:Label><asp:Literal runat="server" Text="<%$ Resources:content_message_2%>" /></td>
                </tr> 
                <tr>
                    <td style="width:45px"><asp:Literal runat="server" Text="<%$ Resources:operate_user%>" /><asp:Label ID="labID" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"real_name_2") %>'></asp:Label></td>
                    <td style="width:25px"><asp:Literal runat="server" Text="<%$ Resources:fill__bill_user%>" /><asp:Label ID="Label1" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"fill_user") %>'></asp:Label></td>
                 </tr>
                <tr>
                    <td colspan="2" style="width:25px"><asp:Label ID="Label3" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"time") %>'></asp:Label></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
</body>
</html>
