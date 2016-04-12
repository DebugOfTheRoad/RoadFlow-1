<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm.Platform.AppLibrary.Edit" %>

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
        <th style="width: 80px;">应用名称：</th>
        <td><input type="text" id="Title1" name="Title1" class="mytext" runat="server" validate="empty" style="width: 75%"/></td>
    </tr>
    <tr>
        <th>应用地址：</th>
        <td><input type="text" id="Address" name="Address" class="mytext" runat="server" validate="empty" style="width: 75%"/></td>
    </tr>
    <tr>
        <th>应用分类：</th>
        <td>
            <select name="Type" id="Type" class="myselect" validate="empty">
                <asp:Literal ID="TypeOptions" runat="server"></asp:Literal>
            </select>
            <span style="msg"></span>
        </td>
    </tr>
    <tr>
        <th>打开方式：</th>
        <td>
            <select name="OpenModel" id="OpenModel" class="myselect" onchange="openModelChange(this.value);">
                <asp:Literal ID="OpenModelOptions" runat="server"></asp:Literal>
            </select>
        </td>
    </tr>
    <tr id="winsizetr" style="display: none;">
        <th>窗口大小：</th>
        <td>
            宽度：<input type="text" id="Width" name="Width" class="mytext" runat="server" validate="int,canempty" style="width: 80px;"/>
            高度：<input type="text" id="Height" name="Height" class="mytext" runat="server" validate="int,canempty" style="width: 80px;"/>
        </td>
    </tr>
    <tr>
        <th>相关参数：</th>
        <td><input type="text" id="Params" name="Params" class="mytext" runat="server" style="width: 95%"/></td>
    </tr>
    <tr>
        <th>使用人员：</th>
        <td><input type="text" id="UseMember" name="UseMember" class="mymember" runat="server" style="width: 89%"/></td>
    </tr>
    <tr>
        <th>备注说明：</th>
        <td><textarea class="mytext" id="Note" name="Note" cols="1" rows="1" runat="server" style="width: 95%; height: 50px;"></textarea></td>
    </tr>
    </table>
    <div class="buttondiv">
        <asp:Button ID="Button1" runat="server" Text="确定保存" OnClientClick="return new RoadUI.Validate().validateForm(document.forms[0]);" CssClass="mybutton" OnClick="Button1_Click" />
        <input type="button" class="mybutton" value="取消关闭" style="margin-left: 5px;" onclick="closewin();" />
    </div>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        $(window).load(function ()
        {
            $("#OpenModel").change();
        });
        function openModelChange(value)
        {
            if ("0" == value)
            {
                $("#winsizetr").hide();
            }
            else
            {
                $("#winsizetr").show();
            }
        }
        function closewin()
        {
            win.close();
            return false;
        }
    </script>
    </form>
</body>
</html>
