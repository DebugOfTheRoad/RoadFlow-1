<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Body.aspx.cs" Inherits="WebForm.Platform.UserApp.Body" %>

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
            <th style="width:80px;">应用名称：</th>
            <td><input type="text" id="Name" name="Name" disabled="disabled" class="mytext" value="<%=name %>" validate="empty" style="width:75%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">关联程序：</th>
            <td><select id="Type" name="Type" onchange="loadApp(this.value);" disabled="disabled" style="width:130px;" class="myselect" style="margin-right:5px"><option value=""></option><%=AppTypesOptions %></select>
            <select onclick="appidchange(this.value);" class="myselect" disabled="disabled" style="width:188px;" id="AppID" name="AppID"></select></td>
        </tr>
        <tr>
            <th style="width:80px;">相关参数：</th>
            <td><input type="text" id="Params" name="Params" disabled="disabled" value="<%=params1 %>" class="mytext" style="width:75%"/></td>
        </tr>
        <tr>
            <th style="width:80px;">图标：</th>
            <td><input type="text" name="Ico" id="Ico" disabled="disabled" class="myico" source="/Images/ico" value="<%=ico %>" style="width: 75%"/></td>
        </tr>
    </table>
    <div class="buttondiv">
        <input type="button" value="添加子项" class="mybutton" onclick="window.location = 'AddApp.aspx?page=Body.aspx<%=query%>';" />
        <input type="submit" value="保存" disabled="disabled" class="mybutton" name="Save" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
         <% if (parentID != Guid.Empty){%>
            <input type="submit" value="删除" disabled="disabled" class="mybutton" name="Delete" onclick="return confirm('真的要删除该角色应用及其所有下级应用吗?');" />
            <input type="button" value="排序" disabled="disabled" class="mybutton" onclick="sort();" />
         <%}%> 
    </div>
    </form>
    <script type="text/javascript">
        $(function ()
        {
            loadApp($("#Type").val());
        });
        function appidchange(value)
        {
            var options = $("#AppID option");
            for (var i = 0; i < options.length; i++)
            {
                if (value && options.eq(i).val() == value)
                {
                    $("#Name").val(options.eq(i).text());
                }
            }
        }
        function loadApp(value)
        {
            if (!value)
            {
                return false;
            }
            $.ajax({
                url: "GetApps.aspx", type: "post", data: { "type": value, "value": "<%=AppID%>" }, dataType: "text", async: false, cache: false, success: function (txt)
                {
                    var $appid = $("#AppID");
                    $("option", $appid).remove();
                    $appid.append('<option value=""></option>' + txt);
                }
            });
        }
        function sort()
        {
            window.location = "Sort.aspx<%=Request.Url.Query%>";
        }
    </script>
</body>
</html>
