<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm.Platform.WorkFlowButtons.Edit" %>

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
            <th style="width: 80px;">
                按钮名称：
            </th>
            <td>
                <input type="text" name="Title1" id="Title1" class="mytext" runat="server" validate="empty" style="width: 75%" />
            </td>
        </tr>
        <tr>
            <th>
                按钮图标：
            </th>
            <td>
                <input type="text" name="Ico" id="Ico" class="myico" source="/Images/ico" runat="server" style="width: 75%"/>
            </td>
        </tr>
        <tr>
            <th>
                执行脚本：
            </th>
            <td>
                <textarea class="mytext" name="Script" id="Script" rows="1" cols="1" style="width:90%; height:180px; line-height:16px; color:Blue; font-family:Courier New; padding:5px;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <th>
                按钮说明：
            </th>
            <td>
                <textarea class="mytext" name="Note" id="Note" rows="1" cols="1" style="width:90%; height:50px;" runat="server"></textarea>
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
