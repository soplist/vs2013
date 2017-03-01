<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixedPointsSetting.aspx.cs" Inherits="FixedPointsSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DataList ID="fixedPoints" runat="server"  Font-Size="9pt"  OnEditCommand="fixedPoints_EditCommand" OnUpdateCommand="fixedPoints_UpdateCommand" OnCancelCommand="fixedPoints_CancelCommand">
        <ItemTemplate>
            <table cellpadding="0" cellspacing="0" style="width: 950px">
                <tr>
                    <td style="width: 50px"><asp:Label ID="Label10" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"real_name") %>'></asp:Label></td>
                    <td><asp:Label ID="labID" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"education") %>'></asp:Label></td>
                    <td><asp:Label ID="Label1" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"specialty") %>'></asp:Label></td>
                    <td><asp:Label ID="Label2" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"office") %>'></asp:Label></td>
                    <td><asp:Label ID="Label3" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"level") %>'></asp:Label></td>  
                    <td><asp:Label ID="Label4" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"certificate") %>'></asp:Label></td>  
                    <td>
                    <asp:Button ID="btnEdit" runat="server" CommandName="edit" Text="edit" />
                    <asp:Button ID="btnAdd" runat="server" Text="add" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"no") %>' OnCommand="btnAdd_Click" />
                    </td>
                </tr> 
            </table>
        </ItemTemplate>
        <HeaderTemplate>
            <table>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:name%>" /></td>
                    <td><asp:Literal runat="server" Text="<%$ Resources:education%>" /></td>
                    <td><asp:Literal runat="server" Text="<%$ Resources:specialty%>" /></td>
                    <td><asp:Literal runat="server" Text="<%$ Resources:office%>" /></td>
                    <td><asp:Literal runat="server" Text="<%$ Resources:level%>" /></td>
                    <td><asp:Literal runat="server" Text="<%$ Resources:certificate%>" /></td>
                </tr>
            </table>
        </HeaderTemplate>
        <HeaderStyle BackColor="#0973DC" Font-Bold="True" ForeColor="White" />
        <EditItemTemplate>
            <table>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:name%>" /></td>
                    <td>
                        <asp:Label ID="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"real_name") %>'></asp:Label></td>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:education%>" /></td>
                    <td>
                        <asp:TextBox ID="update_txtEducation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"education") %>' Width="98px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="update_txtEducation" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:specialty%>" /></td>
                    <td>
                        <asp:TextBox ID="update_txtSpecialty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"specialty") %>' Width="98px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="update_txtSpecialty" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:office%>" /></td>
                    <td>
                        <asp:TextBox ID="update_txtOffice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"office") %>' Width="98px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="update_txtOffice" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:level%>" /></td>
                    <td>
                        <asp:TextBox ID="update_txtLevel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"level") %>' Width="98px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="update_txtLevel" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Literal runat="server" Text="<%$ Resources:certificate%>" /></td>
                    <td>
                        <asp:TextBox ID="update_txtCertificate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"certificate") %>' Width="98px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="update_txtCertificate" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="update" CommandArgument = '<%#DataBinder.Eval(Container.DataItem,"no")%>' Text="update" />
                        <asp:Button ID="btnCancel" runat="server" CommandName="cancel" Text="cancel" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
    </asp:DataList>
    </div>
    </form>
</body>
</html>
