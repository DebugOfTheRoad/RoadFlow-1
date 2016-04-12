<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tree.aspx.cs" Inherits="WebForm.Platform.UserApp.Tree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form method="post">
<div style="margin:3px 0 3px 0;">
    <select id="role" onchange="loadTree(this.value);" class="myselect" style="width:100px;">
        <%=RoleOptions %>
    </select>
</div>
<div id="menu"></div>
</form>
<script type="text/javascript">
    var AppID = '<%=Request.QueryString["appid"]%>';
    var UserID = '<%=Request.QueryString["id"]%>';
    var roadTree = null;
    $(function ()
    {
        loadTree($('#role').val());
    });
    function loadTree(id)
    {
        $("#menu").html('');
        if (id.length > 0)
        {
            roadTree = new RoadUI.Tree({ id: "menu", path: "Tree1.aspx?roleid=" + id, refreshpath: "TreeRefresh.aspx?userid=" + UserID, onclick: openUrl });
        }
    }
    function reLoad(id)
    {
        if (roadTree != null)
        {
            roadTree.refresh(id);
        }
    }
    function openUrl(json)
    {
        var url = json.type == 0 ? "Body.aspx" : "Body1.aspx";
        parent.frames[1].location = url + "?id=" + json.id + "&appid=" + AppID + "&roleid=" + $("#role").val() + "&userid=" + UserID;
    }
</script>
</body>
</html>
