<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Body.aspx.cs" Inherits="WebForm.Platform.Dictionary.Body" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
        <tr>
            <th style="width:80px;">标题：</th>
            <td><input type="text" id="Title1" name="Title1" class="mytext" runat="server" validate="empty" maxlength="100" style="width:70%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">唯一代码：</th>
            <td><input type="text" id="Code" name="Code" class="mytext" runat="server" validate="canempty,ajax" maxlength="100" style="width:70%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">值：</th>
            <td><input type="text" id="Values" name="Values" class="mytext" runat="server" maxlength="100" style="width:70%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" runat="server" style="width:90%; height:50px;"></textarea></td>
        </tr>
        <tr>
            <th style="width:80px;">其它：</th>
            <td><textarea id="Other" name="Other" class="mytext" runat="server" style="width:90%; height:50px;"></textarea></td>
        </tr>
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="button" class="mybutton" value="添加子项" onclick="window.location='Add.aspx<%=Request.Url.Query%>';" />
        <input type="button" class="mybutton" value="排序" onclick="sort1();" />
        <input type="submit" class="mybutton" name="Save" value="保存" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
        <input type="submit" class="mybutton" name="Delete" value="删除" onclick="return confirm('您真的要删除该字典项吗?');" />
    </div>
    </form>
    <script type="text/javascript">
        $(window).load(function ()
        {
           
        });
        function sort1()
        {
            window.location = "Sort.aspx" + "<%=Request.Url.Query%>";
        }
    </script>
</body>
</html>
