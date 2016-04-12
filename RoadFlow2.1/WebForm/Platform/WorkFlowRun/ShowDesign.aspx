<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowDesign.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.ShowDesign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%
        RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
    %>
    <script type="text/javascript">
        var isshowDesign = true;
        var taskJSON = {};
    </script>
    <div id="flowdiv" style="margin:0; padding:0; overflow:auto;"></div>
    <script src="../WorkFlowDesigner/Scripts/draw-min.js" type="text/javascript"></script>
    <script src="../WorkFlowDesigner/Scripts/workflow-show.js" type="text/javascript"></script>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        $(function ()
        {
            openFlow1('<%=Request.QueryString["FlowID"]%>');
        });
    </script>
    </form>
</body>
</html>
