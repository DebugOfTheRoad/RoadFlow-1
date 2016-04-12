﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Opation.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.Opation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <% 
        string op = Request.QueryString["op"];
        string title = string.Empty;
        switch (op)
        {
            case "save":
                title = "正在保存...";
                break;
            case "install":
                title = "正在安装...";
                break;
            case "uninstall":
                title = "正在卸载...";
                break;
            case "delete":
                title = "正在删除...";
                break;
        }    
    %>
    <div style="margin:0 auto; text-align:center; padding-top:28px;">
        <div>
            <img src="../../Images/loading/load1.gif" alt="" />
        </div>
        <div style="margin-top:5px;">
            <%=title %>
        </div>
    </div>
    <script type="text/javascript">
        var op = "<%=op%>";
        var openerid = '<%=Request.QueryString["openerid"]%>';
        var frame = null;
        $(function ()
        {
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                if (iframes[i].name == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            if (frame == null) return;
            switch (op)
            {
                case "save":
                    save();
                    break;
                case "install":
                    install();
                    break;
                case "uninstall":
                    uninstall(0);
                    break;
                case "delete":
                    uninstall(1);
                    break;
            }
        });

        function save()
        {
            var json = JSON.stringify(frame.wf_json);
            $.ajax({
                url: "Save.ashx", type: "post", async: true, dataType: "text", data: { json: json }, success: function (txt)
                {
                    if (1 == txt)
                    {
                        alert("保存成功!");
                    }
                    else
                    {
                        alert(txt);
                    }
                    window.setTimeout('new RoadUI.Window().close();', 1);
                }, error: function (obj) { alert(obj.responseText); window.setTimeout('new RoadUI.Window().close();', 1); }
            });

        }
        function install()
        {
            var json = JSON.stringify(frame.wf_json);
            $.ajax({
                url: "Install.ashx", type: "post", async: true, dataType: "text", data: { json: json }, success: function (txt)
                {
                    if (1 == txt)
                    {
                        alert("安装成功!");
                    }
                    else
                    {
                        alert(txt);
                    }
                    window.setTimeout('new RoadUI.Window().close();', 1);
                }, error: function (obj) { alert(obj.responseText); window.setTimeout('new RoadUI.Window().close();', 1); }
            });
        }
        function uninstall(type)
        {
            var json = frame.wf_json;
            $.ajax({
                url: "UnInstall.ashx", type: "post", async: true, dataType: "text", data: { id: json.id, type: type }, success: function (txt)
                {
                    if (1 == txt)
                    {
                        alert((type == 0 ? "卸载" : "删除") + "成功!");
                    }
                    else
                    {
                        alert(txt);
                    }
                    window.setTimeout('new RoadUI.Window().close();', 1);
                }, error: function (obj) { alert(obj.responseText); window.setTimeout('new RoadUI.Window().close();', 1); }
            });
        }
    </script>
</body>
</html>
