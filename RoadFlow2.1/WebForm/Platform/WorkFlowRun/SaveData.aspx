<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaveData.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.SaveData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <% 
        WebForm.Common.Tools.CheckLogin(false);
        string flowid = Request.QueryString["flowid"];
        string instanceid = Request.QueryString["instanceid"];
        string taskid = Request.QueryString["taskid"];
        string stepid = Request.QueryString["stepid"];
        string groupid = Request.QueryString["groupid"];
        string opation = Request.QueryString["opation"];

        if (instanceid.IsNullOrEmpty())
        {
            instanceid = Request.Form["instanceid"];
        }

        RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        RoadFlow.Data.Model.WorkFlowCustomEventParams eventParams = new RoadFlow.Data.Model.WorkFlowCustomEventParams();
        eventParams.FlowID = flowid.ToGuid();
        eventParams.GroupID = groupid.ToGuid();
        eventParams.StepID = stepid.ToGuid();
        eventParams.TaskID = taskid.ToGuid();
        eventParams.InstanceID = instanceid;
        string instanceid1 = bworkFlow.SaveFromData(instanceid, eventParams);
        if (instanceid.IsNullOrEmpty())
        {
            instanceid = instanceid1;
            eventParams.InstanceID = instanceid1;
        }
        Response.Write("<script>new RoadUI.Window().close();$('#instanceid',parent.document).val('" + instanceid + "');parent." + opation + "(true);</script>");
    %>
    </div>
    </form>
</body>
</html>
