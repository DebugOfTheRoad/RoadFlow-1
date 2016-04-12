﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowBack.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.FlowBack" %>

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
    string taskid = Request.QueryString["taskid"];

    RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
    RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
    var wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
    if(wfInstalled==null)
    {
        Response.Write("未找到流程运行时实体");
        Response.End();
    }
    Guid taskID;
    if(!taskid.IsGuid(out taskID))
    {
        Response.Write("未找到当前任务");
        Response.End();
    }

    var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
    if (steps.Count() == 0)
    {
        Response.Write("未找到当前步骤");
        Response.End();
    }
    var currentStep = steps.First();
   
    int backModel = currentStep.Behavior.BackModel;//退回策略
    if (backModel == 0)
    {
        //Response.Write("<script type=\"text/javascript\">alert(\"当前步骤设置为不能退回!\");</script>");
        //Response.End();
    }
    
    int backType = currentStep.Behavior.BackType;//退回类型
    var prevSteps = btask.GetBackSteps(taskID, backType, currentStep.ID, wfInstalled);
    int i=0;
    %>
    <table cellpadding="0" cellspacing="1" border="0" width="95%" align="center" style="margin-top:6px;">
        <%
        foreach (var step in prevSteps)
        {
            string checked1 = string.Empty;
            if ((backType == 2 && step.Key == currentStep.Behavior.BackStepID) || currentStep.Behavior.Countersignature != 0 || backType == 0)
            {
                checked1 = "checked=\"checked\""; i++;
            }
            else
            {
                checked1 = !step.Key.ToString().IsNullOrEmpty() && i++ == 0 ? "checked=\"checked\"" : "";
            }
            string disabled = step.Key.ToString().IsNullOrEmpty() || currentStep.Behavior.Countersignature != 0 || backType == 0 ? "disabled=\"disabled\"" : "";
         %>
        <tr>
            <td style="padding:9px 0 2px 0;">
            <input type="hidden" name="nextstepid" value="@step" />
            <input type="checkbox" value="<%=step.Key %>" <%=checked1 %> <%=disabled %> name="stepid" id="step_<%=step.Key %>" style="vertical-align:middle;" />
            <label for="step_<%=step.Key %>" style="vertical-align:middle;"><%=step.Value %></label>
            </td>
        </tr>
        <tr><td style="height:6px; border-bottom:1px dashed #e8e8e8;"></td></tr>
        <%}%>
    </table>
    <div style="width:95%; margin:12px auto 0 auto; text-align:center;">
        <input type="submit" class="mybutton" onclick="return confirm1();" name="Save" value="&nbsp;确&nbsp;定&nbsp;" style="margin-right:5px;" />
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
            if ("0" == "<%=backModel%>")//退回策略为不能退回
            {
                alert("当前步骤设置为不能退回!");
                new RoadUI.Window().close();
                return;
            }
            var opts = {};
            opts.type = "back";
            opts.steps = [];
            var isSubmit = true;
            $(":checked[name='stepid']").each(function ()
            {
                var step = $(this).val();
                opts.steps.push({ id: step, member: "" });
            });
            if (opts.steps.length == 0)
            {
                alert("没有选择要退回的步骤!");
                return false;
            }
            if (isSubmit)
            {
                frame.formSubmit(opts);
                new RoadUI.Window().close();
            }
        }
    </script>
</form>
</body>
</html>
