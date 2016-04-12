<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm.Platform.WorkFlowComments.Edit" %>

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
        <th>
            意见：
        </th>
        <td>
            <textarea class="mytext" name="Comment" id="Comment" validate="empty" style="width:75%; height:60px;" runat="server"></textarea>
        </td>
    </tr>
    <tr id="usermemberid" runat="server">
        <th style="width: 80px;">
            使用成员：
        </th>
        <td>
            <input type="text" name="Member" id="Member" class="mymember" style="width:60%" runat="server" /> //为空表示给所有成员
        </td>
    </tr>
    <tr>
        <th style="width: 80px;">
            序号：
        </th>
        <td>
            <input type="text" name="Sort" id="Sort" class="mytext" validate="canempty,int" runat="server" />
        </td>
    </tr>
    </table>
    <div class="buttondiv">
        <input type="submit" value="确定保存" class="mybutton" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="new RoadUI.Window().close();" />
    </div>
    </form>
</body>
</html>
