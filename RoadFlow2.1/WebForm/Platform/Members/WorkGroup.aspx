<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkGroup.aspx.cs" Inherits="WebForm.Platform.Members.WorkGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var win = new RoadUI.Window();
        var validate = new RoadUI.Validate();
    </script>
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
        <tr>
            <th style="width:80px;">名称：</th>
            <td><input type="text" id="Name" name="Name" class="mytext" validate="empty,minmax" value="<%=name %>" max="100" style="width:75%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">成员：</th>
            <td><input type="text" id="Members" name="Members" class="mymember" validate="empty" value="<%=members %>" style="width:65%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;"><%=note %></textarea></td>
        </tr>
        <tr>
            <th style="width:80px;">人员：</th>
            <td><%=users %></td>
        </tr>
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="submit" class="mybutton" onclick="return validate.validateForm(document.forms[0]);" name="Save" value="保存" />
        <input type="submit" class="mybutton" name="DeleteBut" onclick="return confirm('您真的要删除该工作组吗?');" value="删除" />
    </div>
    </form>
</body>
</html>
