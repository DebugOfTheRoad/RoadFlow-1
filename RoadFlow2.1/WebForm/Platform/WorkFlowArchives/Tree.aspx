<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tree.aspx.cs" Inherits="WebForm.Platform.WorkFlowArchives.Tree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <%string rootid = new RoadFlow.Platform.Dictionary().GetIDByCode("FlowTypes").ToString(); %>
    <form id="form1" runat="server">
        <div id="tree"></div>
    </form>
    <script type="text/javascript">
        var AppID = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["iframeid"]%>';
        var openerid = '<%=Request.QueryString["openerid"]%>';
        var roadTree = null;
        $(function ()
        {
            roadTree = new RoadUI.Tree({ id: "tree", path: top.rootdir + "/Platform/Dictionary/Tree1.ashx?root=<%=rootid%>", refreshpath: top.rootdir + "/Platform/Dictionary/TreeRefresh.ashx", onclick: openUrl });
        });
        function openUrl(json)
        {
            parent.frames[1].location = "List.aspx?typeid=" + json.id + "&appid=" + AppID + "&iframeid=" + iframeid + "&openerid=" + openerid;
        }
        function reLoad(id)
        {
            roadTree.refresh(id);
        }
    </script>

</body>
</html>
