<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstanceTree.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.InstanceTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <% 
        string rootid = new RoadFlow.Platform.Dictionary().GetIDByCode("FlowTypes").ToString();    
    %>
    <form id="form1" runat="server">
        <div id="tree"></div>
    </form>
    <script type="text/javascript">
        var AppID = '<%=Request.QueryString["appid"]%>';
        var tabid = '<%=Request.QueryString["tabid"]%>';
        var roadTree = null;
        $(function ()
        {
            roadTree = new RoadUI.Tree({ id: "tree", path: top.rootdir + "/Platform/Dictionary/Tree1.ashx?root=<%=rootid%>", refreshpath: top.rootdir + "/Platform/Dictionary/TreeRefresh.ashx", onclick: openUrl });
        });
        function openUrl(json)
        {
            parent.frames[1].location = "InstanceList.aspx?typeid=" + json.id + "&appid=" + AppID + "&tabid=" + tabid;
        }
        function reLoad(id)
        {
            roadTree.refresh(id);
        }
    </script>
</body>
</html>
