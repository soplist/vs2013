<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    
    <div>
        <form id="form1" runat="server">
        <div style="float:left;">
        <span style="font-size: 9pt">
            <asp:Literal runat="server" Text="<%$ Resources:basic_information%>" />
            <%= Session["username"].ToString() %>
            <%= Session["role"].ToString() %>
            <%= Session["department_name"].ToString() %>
        </span>
        <a href="FixedPointsSetting.aspx" style="font-size: 9pt"><asp:Literal runat="server" Text="<%$ Resources:fixed_points_setting%>" /></a>
        <a href="RightPoints.aspx" style="font-size: 9pt"><asp:Literal runat="server" Text="<%$ Resources:right_points%>" /></a>
        <a href="Ranking.aspx" style="font-size: 9pt"><asp:Literal runat="server" Text="<%$ Resources:ranking%>" /></a>
        <a href="Ranking.aspx" style="font-size: 9pt"><asp:Literal runat="server" Text="<%$ Resources:ranking%>" /></a>
        <a href="PrintPointsBill.aspx" style="font-size: 9pt"><asp:Literal runat="server" Text="<%$ Resources:print_points_bill%>" /></a>
        <table cellpadding="0" cellspacing="0"  Width="1000px">
        <tr align =left >
            <td colspan="3"  valign =top align =left >
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:name%>" /></span>
                <asp:TextBox ID="txtName" runat="server" Width="98px"></asp:TextBox>
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:start_event_time%>" /></span>
                <asp:TextBox ID="txtStartEventTime" runat="server" Width="98px"></asp:TextBox>
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:end_event_time%>" /></span>
                <asp:TextBox ID="txtEndEventTime" runat="server" Width="98px"></asp:TextBox>
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:operate_time%>" /></span>
                <asp:TextBox ID="txtOperateTime" runat="server" Width="98px"></asp:TextBox>
                <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:department%>" /></span>
                <asp:DropDownList ID="lstDepartment" runat="server" Width="98px"></asp:DropDownList>

            </td>
            <td>
                <asp:Button ID="btnSearck" runat="server" Text="<%$ Resources:search%>" OnClick="btnSearch_Click" ForeColor="DodgerBlue" />
                <asp:Button ID="btnTurnOffSearck" runat="server" Text="<%$ Resources:search_turn_off%>" OnClick="btnTurnOffSearch_Click" ForeColor="DodgerBlue" />
                <span style="font-size: 9pt;color:blue"><asp:Literal runat="server" Text="<%$ Resources:search_condition%>" /><%= Session["search_condition"].ToString() %></span>
            </td>
        </tr>
        <tr align =left >
            <td>
                <span style="font-size: 9pt;color:green">
                <asp:Label ID="labSum" runat="server" Text=""></asp:Label>
                <asp:Label ID="labAdd" runat="server" Text=""></asp:Label>
                <asp:Label ID="labMinus" runat="server" Text=""></asp:Label>
                </span>
                <span style="font-size: 9pt;color:blue">
                <asp:Label ID="labFullYear" runat="server" Text=""></asp:Label>
                </span>
            </td>
        </tr>
        </table>

        <table cellpadding="0" cellspacing="0"  Width="471px">
            <tr align =left >
                <td colspan="3"  valign =top align =left >
                    <asp:DataList ID="points" runat="server"  Font-Size="9pt" OnDeleteCommand="points_DeleteCommand" OnEditCommand="points_EditCommand" OnUpdateCommand="points_UpdateCommand" OnCancelCommand="points_CancelCommand">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" style="width: 950px">
                                <tr>
                                    <td style="width:25px"><asp:Label ID="Label10" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"no") %>'></asp:Label></td>
                                    <td style="width:45px"><asp:Label ID="labID" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"department") %>'></asp:Label></td>
                                    <td style="width:45px"><asp:Label ID="Label1" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"username") %>'></asp:Label></td>
                                    <td style="width:70px"><asp:Label ID="Label2" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"event_time") %>'></asp:Label></td>
                                    <td style="width:20px"><asp:Label ID="Label3" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"point_value") %>'></asp:Label></td>  
                                    <td style="width:30px"><asp:Label ID="Label4" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"event_category") %>'></asp:Label></td>  
                                    <td style="width:45px"><asp:Label ID="Label5" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"fill_user") %>'></asp:Label></td>   
                                    <td style="width:45px"><asp:Label ID="Label6" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"operate_user") %>'></asp:Label></td>
                                    <td style="width:30px"><asp:Label ID="Label7" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"operate_time") %>'></asp:Label></td>  
                                    <td style="width:60px"><asp:Label ID="Label8" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"update_time") %>'></asp:Label></td>  
                                    <td><asp:Label ID="Label9" runat="server" Font-Size="9pt"  Text='<%# DataBinder.Eval(Container.DataItem,"event") %>'></asp:Label></td>  
                                    <td style="width:120px">
                                        <asp:Button ID="btnEdit" runat="server" CommandName="edit" Text="<%$ Resources:edit%>" />
                                        <asp:Button ID="btnDelete" runat="server" CommandName="delete" Text="<%$ Resources:delete%>" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"no") %>' OnLoad="btnDelete_Load" />
                                    </td>
                                </tr> 
                            </table>
                        </ItemTemplate>
                        
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td style="width:25px"><asp:Literal runat="server" Text="<%$ Resources:no%>" /></td>
                                    <td style="width:45px"><asp:Literal runat="server" Text="<%$ Resources:department%>" /></td>
                                    <td style="width:45px"><asp:Literal runat="server" Text="<%$ Resources:name%>" /></td>
                                    <td style="width:70px"><asp:Literal runat="server" Text="<%$ Resources:event_time%>" /></td>
                                    <td style="width:30px"><asp:Literal runat="server" Text="<%$ Resources:point_value%>" /></td>
                                    <td style="width:60px"><asp:Literal runat="server" Text="<%$ Resources:event_category%>" /></td>
                                    <td style="width:45px"><asp:Literal runat="server" Text="<%$ Resources:fill_user%>" /></td>
                                    <td style="width:60px"><asp:Literal runat="server" Text="<%$ Resources:operate_user%>" /></td>
                                    <td style="width:60px"><asp:Literal runat="server" Text="<%$ Resources:operate_time%>" /></td>
                                    <td style="width:60px"><asp:Literal runat="server" Text="<%$ Resources:update_time%>" /></td>
                                    <td style="width:60px"><asp:Literal runat="server" Text="<%$ Resources:event%>" /></td>
                                    <td style="width:60px"><asp:Literal runat="server" Text="<%$ Resources:operate%>" /></td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#0973DC" Font-Bold="True" ForeColor="White" />
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td><asp:Literal runat="server" Text="<%$ Resources:name%>" /></td>
                                    <td>
                                        <asp:TextBox ID="update_txtName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"username") %>' Width="98px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="update_txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Literal runat="server" Text="<%$ Resources:event_time%>" /></td>
                                    <td>
                                        <asp:TextBox ID="update_txtEventTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"event_time") %>' Width="98px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="update_txtEventTime" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Literal runat="server" Text="<%$ Resources:event_category%>" /></td>
                                    <td>
                                        <asp:TextBox ID="update_hiddenEventCategory" runat="server" Text='<%# Eval("event_category") %>' Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="update_lstEventCategory" runat="server" Width="98px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Literal runat="server" Text="<%$ Resources:point_value%>" /></td>
                                    <td>
                                        <asp:TextBox ID="update_txtPointValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"point_value") %>' Width="98px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="update_txtPointValue" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Literal runat="server" Text="<%$ Resources:fill_user%>" /></td>
                                    <td>
                                        <asp:TextBox ID="update_txtFillUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"fill_user") %>' Width="98px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="update_txtFillUser" ErrorMessage="*"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Literal runat="server" Text="<%$ Resources:event%>" /></td>
                                    <td>
                                        <asp:TextBox ID="update_txtEvent" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"event") %>' Width="98px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="update_txtEvent" ErrorMessage="*"></asp:RequiredFieldValidator>

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
                    </td>
            </tr>
            <tr>
                <td style="width: 471px; text-align: center; font-size: 9pt; height: 16px;" >
                    <asp:Label ID="labCP" runat="server" Text="<%$ Resources:current_page%>"></asp:Label><asp:Label ID="currentPage" runat="server" Text=""></asp:Label>
                    <asp:Label ID="labTP" runat="server" Text="<%$ Resources:total_page%>"></asp:Label><asp:Label ID="totalPage" runat="server"></asp:Label>
                    <asp:LinkButton ID="lnkbtnOne" runat="server" Font-Underline="False" ForeColor="Red" OnClick="lnkbtnOne_Click" CausesValidation="False"><asp:Literal runat="server" Text="<%$ Resources:frist_page%>" /></asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnUp" runat="server" Font-Underline="False" ForeColor="Red" OnClick="lnkbtnUp_Click" CausesValidation="False"><asp:Literal runat="server" Text="<%$ Resources:previous_page%>" /></asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnNext" runat="server" Font-Underline="False" ForeColor="Red" OnClick="lnkbtnNext_Click" CausesValidation="False"><asp:Literal runat="server" Text="<%$ Resources:next_page%>" /></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lnkbtnBack" runat="server" Font-Underline="False" ForeColor="Red" OnClick="lnkbtnBack_Click" CausesValidation="False"><asp:Literal runat="server" Text="<%$ Resources:last_page%>" /></asp:LinkButton>&nbsp;&nbsp;
                </td>
            </tr>
        </table> 
        </div>
        <div style="height: 600px;width: 350px;background-color: #4682B4;float:left;">
        <table cellpadding="0" cellspacing="0"  Width="600px">
            <tr>
                <td style="width: 471px; text-align: left; font-size: 9pt; height: 16px;" >
                    <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:department%>" /></span>
                    <asp:DropDownList ID="insert_lstDepartment" runat="server" Width="98px" AutoPostBack="true" OnSelectedIndexChanged="SelectedDepartmentChanged"></asp:DropDownList>
                    <br/>
                    <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:name%>" /></span>
                    <asp:CheckBoxList ID="insert_chkNames" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    </asp:CheckBoxList>
                    <br/>
                    <asp:Calendar ID="insert_calDate" runat="server"></asp:Calendar>
                    <br/>
                    <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:point_value%>" /></span>
                    <asp:TextBox ID="insert_txtValue" runat="server" Width="98px"></asp:TextBox>
                    <br/>
                    <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:event_category%>" /></span>
                    <asp:DropDownList ID="insert_lstEventCategory" runat="server" Width="98px" AutoPostBack="true"></asp:DropDownList>
                    <br/>
                    <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:fill_user%>" /></span>
                    <asp:TextBox ID="insert_txtFillUser" runat="server" Width="98px"></asp:TextBox>
                    <br/>
                    <span style="font-size: 9pt;"><asp:Literal runat="server" Text="<%$ Resources:event%>" /></span>
                    <asp:TextBox ID="insert_txtEvent" runat="server" Width="150px" TextMode="MultiLine"></asp:TextBox>
                    <asp:Button ID="btnInsert" runat="server" Text="<%$ Resources:insert%>" OnClick="btnInsert_Click" ForeColor="DodgerBlue" />
                </td>
            </tr>
        </table>
        <hr style=" height:2px;border:none;border-top:2px dotted #185598;" />
        <table cellpadding="0" cellspacing="0"  Width="600px">
            <tr>
                <td style="width: 471px; text-align: left; font-size: 9pt; height: 16px;" >
                    <a href="resource/excel_template.xls"><asp:Literal runat="server" Text="<%$ Resources:template_download%>" /></a>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload" runat="server" />
                    <asp:Button ID="Button1" runat="server" OnClick="btnUpload_Click" Text="<%$ Resources:upload%>" />
                </td>
            </tr>
        </table>
        <hr style=" height:2px;border:none;border-top:2px dotted #185598;" />
        <asp:Button ID="Button3" runat="server" OnClick="btnExportSwitch_Click" Text="<%$ Resources:export_switch%>" />
        <span style="font-size: 9pt;color:red"><asp:Literal runat="server" Text="<%$ Resources:export_switch%>" /><%= Session["export_switch"].ToString() %></span>
        <br/>
        <asp:Button ID="Button2" runat="server" OnClick="btnExport_Click" Text="<%$ Resources:export%>" />
        </div>
        </form>       
    </div>
</body>
</html>
