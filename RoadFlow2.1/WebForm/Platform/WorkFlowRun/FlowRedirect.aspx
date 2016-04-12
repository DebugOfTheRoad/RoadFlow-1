<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowRedirect.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.FlowRedirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%
    WebForm.Common.Tools.CheckLogin(false);
    string flowid = Request.QueryString["flowid"];
    string stepid = Request.QueryString["stepid"];
    string groupid = Request.QueryString["groupid"];
    string instanceid = Request.QueryString["instanceid"];
    RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
    RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
    RoadFlow.Data.Model.WorkFlowInstalled wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
    if (wfInstalled == null)
    {
        Response.Write("未找到流程运行实体");
        Response.End();
    }

    var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
    if (steps.Count() == 0)
    {
        Response.Write("未找到当前步骤");
        Response.End();
    }
    %>
    <br /><br />
    <div style="text-align:center;">
        接收人：<input type="text" id="user" class="mymember" style="width:200px;" />
    </div><br />
    <div style="text-align:center;">
        <input type="button" class="mybutton" value="&nbsp;确&nbsp;定&nbsp;" onclick="confirm1();" />
        <input type="button" class="mybutton" value="&nbsp;取&nbsp;消&nbsp;" onclick="new RoadUI.Window().close();" />
    </div>
    <script type="text/javascript">
        var frame = null;
        var openerid = '<%=Request.QueryString["openerid"]%>';
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
        });
        function confirm1()
        {
            var opts = {};
            opts.type = "redirect";
            opts.steps = [];
            var member = $("#user").val() || "";
            if (member.length == 0)
            {
                alert("没有选择接收人员!");
                isSubmit = false;
                return false;
            }
            else
            {
                opts.steps.push({ id: "", member: member });
            }
            frame.formSubmit(opts);
            new RoadUI.Window().close();
        }
    </script>
    </form>
</body>
</html>
