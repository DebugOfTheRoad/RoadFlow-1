<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="WebForm.Platform.RoleApp.EditRole" %>

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
            <td><input type="text" id="Name" name="Name" class="mytext" validate="empty" runat="server" style="width:65%"/></td>
        </tr>
        <tr>
            <th style="width:80px;">角色成员：</th>
            <td><input type="text" id="UseMember" name="UseMember"  class="mymember" runat="server" style="width:65%"/></td>
        </tr>
        <tr>
            <th style="width:80px;">备注说明：</th>
            <td><textarea class="mytext" id="Note" name="Note" cols="1" rows="1" style="width:95%; height:50px;" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width:80px;">复制：</th>
            <td>将此角色的应用复制给：<select id="ToTpl" name="ToTpl" class="myselect"><option value=""></option>
                <asp:Literal ID="RoleOptions" runat="server"></asp:Literal>
                           </select>
            <input type="submit" name="Copy" value="确认复制" onclick="return copy();" class="mybutton" /></td>
        </tr>
    </table>
    <div class="buttondiv">
       <input type="submit" name="Save" value="确认保存" onclick="return new RoadUI.Validate().validateForm(this);" class="mybutton" />
       <input type="submit" name="Delete" value="删除角色" onclick="return confirm('您真的要删除该角色及其应用吗?');" class="mybutton" />
    </div>
    <script type="text/javascript">
        function copy()
        {
            if ($("#ToTpl").val().length == 0)
            {
                alert("请选择要复制到的模板!");
                return false;
            }
            return true;
        }
    </script>
    </form>
</body>
</html>
