<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstanceList.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.InstanceList" %>

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
                标题：<input type="text" class="mytext" id="Title1" style="width:100px;" name="Title1" runat="server" />
                流程：<select class="mycombox" style="width:100px;" width1="156" more="1" id="FlowID" name="FlowID"><asp:Literal ID="FlowOptions" runat="server"></asp:Literal></select>
                发送人：<input type="text" style="width:80px;"  class="mymember" id="SenderID" unit="0" dept="0" station="0" user="1" group="0" more="0" name="SenderID" runat="server"  />
                发送时间：<input type="text" class="mycalendar" runat="server" style="width:80px;" name="Date1" /> 至 <input type="text" style="width:80px;" runat="server" class="mycalendar" name="Date2" />
                状态：<select class="myselect" style="width:80px;" id="Status" name="Status" runat="server"></select>
                <input type="submit" name="Search" value="&nbsp;&nbsp;查询&nbsp;&nbsp;" class="mybutton" />
            </td>
        </tr>
    </table>
</div>
<table class="listtable">
    <thead>
        <tr>
            <th width="25%">标题</th>
            <th width="13%">所属流程</th>
            <th width="12%">所在步骤</th>
            <th width="10%">处理者</th>
            <th width="15%">接收时间</th>
            <th width="10%">当前状态</th>
            <th width="15%" sort="0"></th>
        </tr>
    </thead>
    <tbody>
    <%
        foreach (var task in taskList)
        { 
        string flowName;
        string stepName = bworkFlow.GetStepName(task.StepID, task.FlowID, out flowName);
        string query1 = string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}&display=1",
             task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, Request.QueryString["appid"]
             );
        string timeout = string.Empty;// task.CompletedTime.HasValue && task.CompletedTime.Value < RoadFlow.Utility.DateTimeNew.Now ? "<span style='color:red;'>(已超时)<span>" : "";
    %>
        <tr>
            <td><a href="javascript:void(0);" onclick="openTask('<%=WebForm.Common.Tools.BaseUrl %>/Platform/WorkFlowRun/Default.aspx?<%=query1 %>','<%=task.Title %>','<%=task.ID %>');return false;" class="blue"><%=task.Title %></a></td>
            <td><%=flowName %></td>
            <td><%=stepName %></td>
            <td><%=task.ReceiveName %></td>
            <td><%=task.ReceiveTime.ToDateTimeStringS() %></td>
            <td><%=bworkFlowTask.GetStatusTitle(task.Status) %><%=timeout %></td>
            <td>
                <a style="margin-right:3px; background:url(<%=WebForm.Common.Tools.BaseUrl%>/Images/ico/Properties.png) no-repeat left center; padding-left:18px;" href="javascript:void(0);" onclick="manage('<%=task.FlowID %>','<%=task.GroupID %>');">管理</a>
                <%if(task.Status.In(0,1)){%>
                <a style="margin-right:3px; background:url(<%=WebForm.Common.Tools.BaseUrl%>/Images/ico/trash.gif) no-repeat left center; padding-left:18px;" href="javascript:void(0);" onclick="delete1('<%=task.FlowID %>','<%=task.GroupID %>');">删除</a>
                <% }%>
            </td>
        </tr>
    <%}%>
    </tbody>
</table>
<div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>
</form>
<script type="text/javascript">
    function openTask(url, title, id)
    {
        top.openApp(url, 0, title, id, 0, 0, false);
    }
    function manage(flowid, groupid)
    {
        top.mainDialog.open({
            url: top.rootdir + '/Platform/WorkFlowTasks/InstanceManage.aspx?flowid1=' + flowid + "&groupid=" + groupid + '<%=query%>',
            width: 800, height: 400, title: "管理流程实例"
        });
    }
    function delete1(flowid, groupid)
    {
        if (confirm("您真的要删除该流程实例吗?"))
        {
            $.ajax({
                url: top.rootdir + "/Platform/WorkFlowTasks/Delete.ashx?flowid1=" + flowid + "&groupid=" + groupid, async: false, cache: false, success: function (txt)
                {
                    alert(txt);
                    window.location = window.location;
                }
            });
        }
    }
</script>
</body>
</html>
