<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddApp.aspx.cs" Inherits="WebForm.Platform.UserApp.AddApp" %>

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
            <td><input type="text" id="Name" name="Name" class="mytext" value="" validate="empty" style="width:75%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">关联程序：</th>
            <td><select id="Type" name="Type" onchange="loadApp(this.value);" style="width:130px;" class="myselect" style="margin-right:5px"><option value=""></option><%=AppTypesOptions %></select>
            <select onclick="appidchange(this.value);" style="width:188px;" class="myselect" id="AppID" name="AppID"></select></td>
        </tr>
        <tr>
            <th style="width:80px;">相关参数：</th>
            <td><input type="text" id="Params" name="Params" value="" class="mytext" style="width:75%"/></td>
        </tr>
        <tr>
            <th style="width:80px;">图标：</th>
            <td><input type="text" name="Ico" id="Ico" class="myico" source="/Images/ico" value="" style="width: 75%"/></td>
        </tr>
    </table>
    <div class="buttondiv">
        <input type="submit" value="保存" class="mybutton" name="Save" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
        <input type="button" value="返回" class="mybutton" onclick="window.location='<%=Request.QueryString["Page"]%>'+'<%=Request.Url.Query%>';" />
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
            $.ajax({ url: "GetApps.aspx", type: "post", data: { "type": value }, dataType: "text", async: false, cache: false, success: function (txt)
            {
                var $appid = $("#AppID");
                $("option", $appid).remove();
                $appid.append('<option value=""></option>'+txt);
            }
            });
        }
    </script>

</body>
</html>
