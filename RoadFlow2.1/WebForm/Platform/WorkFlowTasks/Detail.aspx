<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="toolbar" style="margin-top:0; border-top:0;">
        <a href="javascript:void(0);" onclick="window.location='Detail.aspx?displaymodel=1'+'<%=query %>';return false;"><span style="background-image:url(/Images/ico/shape_aling_left.png);">图形方式</span></a>
        <input type="submit" style="display:none;" value="d" id="DeleteBut" name="DeleteBut" />
        <a href="javascript:void(0);" onclick="window.location='Detail.aspx?displaymodel=0'+'<%=query %>';return false;"><span style="background-image:url(/Images/ico/table.gif);">列表方式</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <a href="javascript:void(0);" onclick="new RoadUI.Window().close();return false;"><span style="background-image:url(/Images/ico/application_osx_remove.png);">关闭窗口</span></a>
    </div>
    <%if ("1" != displayModel){ %>
    <table cellpadding="0" cellspacing="1" border="0" class="listtable" style="width:100%; margin-top:0px;">
        <thead>
            <tr>
                <th>步骤名称</th>
                <th>发送人</th>
                <th>发送时间</th>
                <th>处理人</th>
                <th>完成时间</th>
                <th>状态</th>
                <th>意见</th>
                <th>备注</th>
            </tr>
        </thead>
        <tbody>
        <%foreach (var task in tasks){ %>
            <tr>
                <td><%=bworkFlow.GetStepName(task.StepID, wfInstall) %></td>    
                <td><%=task.SenderName %></td>
                <td><%=task.SenderTime.ToDateTimeStringS() %></td>
                <td><%=task.ReceiveName %></td>
                <td><%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></td>
                <td><%=bworkFlowTask.GetStatusTitle(task.Status) %></td>
                <td><%=task.Comment %></td>
                <td><%=task.Note %></td>
            </tr>
            <%if(task.SubFlowGroupID.HasValue){ %>
                <tr>
                <td colspan="8" style="padding:3px;">
                    <div style="">
                        <table cellpadding="0" cellspacing="1" border="0" class="listtable" style="width:99%;">
                            <thead>
                                <tr>
                                    <th>步骤名称</th>
                                    <th>发送人</th>
                                    <th>发送时间</th>
                                    <th>处理人</th>
                                    <th>完成时间</th>
                                    <th>状态</th>
                                    <th>意见</th>
                                    <th>备注</th>
                                </tr>
                            </thead>
                            <tbody>
                    
                <%
                var tasks1 = bworkFlowTask.GetTaskList(Guid.Empty, task.SubFlowGroupID.Value);
                var wfInstall1 = tasks1.Count > 0 ? bworkFlow.GetWorkFlowRunModel(tasks1.First().FlowID) : null;
                foreach (var task1 in tasks1)
                { %>
                        <tr>
                            <td><%=bworkFlow.GetStepName(task1.StepID, wfInstall1) %></td> 
                            <td><%=task1.SenderName %></td>
                            <td><%=task1.SenderTime.ToDateTimeStringS() %></td>
                            <td><%=task1.ReceiveName %></td>
                            <td><%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></td>
                            <td><%=bworkFlowTask.GetStatusTitle(task1.Status) %></td>
                            <td><%=task1.Comment %></td>
                            <td><%=task1.Note %></td> 
                        </tr>
                <%} %>  
                        </tbody>
                        </table>
                    </div>
                </td>
                </tr>
            <%}%>
        <%}%>
        </tbody>
    </table>
    <script type="text/javascript">
        function showSubFlow()
        {
            top.mainDialog.open({title: '查看子流程处理过程', 
                url: top.rootdir + '/Platform/WorkFlowTasks/DetailSubFlow.aspx?1=1' + '<%=query%>', width: 1000, height: 500 });
        }
    </script>
    <%}
    else
    {
        int taskCount = tasks.Count();
        int i = 0;
        System.Text.StringBuilder tasksjson = new System.Text.StringBuilder("[", tasks.Count() * 60);
        foreach (var task in tasks)
        {
            tasksjson.Append("{");
            tasksjson.AppendFormat("\"stepid\":\"{0}\",\"prevstepid\":\"{1}\",\"status\":\"{2}\"",
                task.StepID, task.PrevStepID, task.Status);
            tasksjson.Append("}");
            if (i++ < taskCount - 1)
            {
                tasksjson.Append(",");
            }
        }
        tasksjson.Append("]");
     %>
        <div style="padding:8px 5px 0 10px;">
            <span style="display:inline-block; height:12px; width:12px; background:#4fba4f; margin-left:6px; vertical-align:middle;"></span>
            <label style="vertical-align:middle;">已完成步骤</label>
            <span style="display:inline-block; height:12px; width:12px; background:#ff9001; margin-left:6px; vertical-align:middle;"></span>
            <label style="vertical-align:middle;">待处理步骤</label>
            <span style="display:inline-block; height:12px; width:12px; background:#7e7e7f; margin-left:6px; vertical-align:middle;"></span>
            <label style="vertical-align:middle;">未经过步骤</label>
        </div>
        <div id="flowdiv" style="margin:0; padding:0; overflow:auto;"></div>
        <script type="text/javascript">
            var isshowDesign = false;
            var taskJSON=<%=tasksjson.ToString()%>;
        </script>
        <script src="../WorkFlowDesigner/Scripts/draw-min.js" type="text/javascript"></script>
        <script src="../WorkFlowDesigner/Scripts/workflow-show.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function ()
            {
                openFlow1('<%=flowid%>');
            });
        </script>
   <% }%>
</body>
</html>
