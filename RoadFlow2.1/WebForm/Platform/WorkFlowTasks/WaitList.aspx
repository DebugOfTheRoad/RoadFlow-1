<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitList.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.WaitList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="querybar">
    <table cellpadding="0" cellspacing="1" border="0" width="100%">
        <tr>
            <td>
                名称：<input type="text" class="mytext" id="Title1" name="Title1" runat="server" />
                所属流程：<select class="mycombox" style="width:150px;" width1="166" more="1" id="FlowID" name="FlowID"><asp:Literal ID="flowOptions" runat="server"></asp:Literal></select>
                发送人：<input type="text" class="mymember" id="SenderID" unit="0" dept="0" station="0" user="1" group="0" more="0" name="SenderID" runat="server"/>
                接收时间：<input type="text" class="mycalendar" style="width:90px;" runat="server" id="Date1" name="Date1" /> 至 <input type="text" runat="server" style="width:90px;" class="mycalendar" id="Date2" name="Date2" />
                <input type="submit" name="Search" value="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" class="mybutton" />
            </td>
        </tr>
    </table>
    </div>

    <table class="listtable">
        <thead>
            <tr>
                <th width="33%">任务标题</th>
                <th width="10%">流程</th>
                <th width="10%">步骤</th>
                <th width="8%">发送人</th>
                <th width="13%">接收时间</th>
                <th width="8%"">状态</th>
                <th width="10%">备注</th>
                <th width="8%" sort="0"></th>
            </tr>
        </thead>
        <tbody>
        <%foreach (var task in taskList)
        {
            string flowName;
            string stepName = bworkFlow.GetStepName(task.StepID, task.FlowID, out flowName);
            string query1 = string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}",
                task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, Request.QueryString["appid"]
                );
        %>
            <tr>
                <td><a href="javascript:void(0);" onclick="openTask('<%=WebForm.Common.Tools.BaseUrl %>/Platform/WorkFlowRun/Default.aspx?<%=query1 %>','<%=task.Title %>','<%=task.ID %>');return false;" class="blue"><%=task.Title %></a></td>
                <td><%=flowName %></td>
                <td><%=stepName %></td>
                <td><%=task.SenderName %></td>
                <td><%=task.ReceiveTime.ToString().ToDateTimeStringS() %></td>
                <td><%=bworkFlowTask.GetStatusTitle(task.Status) %></td>
                <td><%=task.Note %></td>
                <td><a class="viewlink" href="javascript:void(0);" onclick="detail('<%=task.FlowID %>','<%=task.GroupID %>','<%=task.ID %>');">查看</a></td>
            </tr>
        <%}%>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        function openTask(url, title, id)
        {
            top.openApp(url, 0, title, "tab_" + id, 0, 0, false);
        }
        function detail(flowid, groupid, taskid)
        {
            top.mainDialog.open({
                url: top.rootdir + '/Platform/WorkFlowTasks/Detail.aspx?flowid1=' + flowid + "&groupid=" + groupid + "&taskid=" + taskid + '<%=query%>',
                width: 1024, height: 550, title: "查看流程处理过程"
            });
        }
    </script>

</body>
</html>
