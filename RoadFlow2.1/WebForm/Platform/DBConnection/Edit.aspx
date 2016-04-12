<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm.Platform.DBConnection.Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return new RoadUI.Validate().validateForm(document.forms[0]);">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
    <tr>
        <th style="width: 80px;">
            连接名称：
        </th>
        <td>
            <input type="text" name="Name" id="Name" class="mytext" runat="server" validate="empty" style="width: 75%" />
        </td>
    </tr>
    <tr>
        <th>
            连接类型：
        </th>
        <td>
            <select id="LinkType" class="myselect" name="LinkType" validate="empty"><option value=""></option><asp:Literal ID="TypeOptions" runat="server"></asp:Literal></select>
        </td>
    </tr>
    <tr>
        <th>
            连接字符串：
        </th>
        <td>
            <textarea class="mytext" name="ConnStr" id="ConnStr" cols="1" rows="1" style="width: 95%; height: 50px;" runat="server"></textarea>
        </td>
    </tr>
    <tr>
        <th>
            备注：
        </th>
        <td>
            <textarea class="mytext" name="Note" id="Note" cols="1" rows="1" style="width: 95%; height: 50px;" runat="server"></textarea>
        </td>
    </tr>
    </table>
    <div class="buttondiv">
        <input type="submit" value="确定保存" class="mybutton" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="closewin();" />
    </div>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        $(window).load(function ()
        {

        });
        function closewin()
        {
            win.close();
            return false;
        }
    </script>
    </form>
</body>
</html>
