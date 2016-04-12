<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="WebForm.Platform.RoleApp.AddRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width:80px;">角色名称：</th>
            <td><input type="text" id="Name" name="Name" class="mytext" runat="server" validate="empty" style="width:75%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">角色成员：</th>
            <td><input type="text" id="UseMember" name="UseMember" runat="server" class="mymember" value="" style="width:65%"/></td>
        </tr>
        <tr>
            <th style="width:80px;">备注说明：</th>
            <td><textarea class="mytext" id="Note" name="Note" cols="1" rows="1" style="width:95%; height:50px;" runat="server"></textarea></td>
        </tr>
    </table>
    <div class="buttondiv">
        <input type="submit" value="确认保存" class="mybutton" />
    </div>
    </form>
</body>
</html>
