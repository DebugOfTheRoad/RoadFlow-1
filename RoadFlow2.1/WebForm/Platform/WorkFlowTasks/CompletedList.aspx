<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompletedList.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.CompletedList" %>

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
                    接收时间：<input type="text" class="mycalendar" style="width:90px;" id="Date1" name="Date1" runat="server" /> 至 <input type="text" style="width:90px;" class="mycalendar" id="Date2" name="Date2" runat="server" />
                    <input type="submit" name="Search" value="查询" class="mybutton" />
                </td>
            </tr>
        </table>
    </div>

    <table class="listtable">
        <thead>
            <tr>
                <th width="28%">任务标题</th>
                <th width="10%">流程</th>
                <th width="10%">步骤</th>
                <th width="8%">发送人</th>
                <th width="13%">接收时间</th>
                <th width="13%">完成时间</th>
                <th width="8%">状态</th>
                <th width="12%" sort="0"></th>
            </tr>
        </thead>
        <tbody>
        <%foreach (var task in taskList)
        {
            string flowName;
            string stepName = bworkFlow.GetStepName(task.StepID, task.FlowID, out flowName);
            string query1 = string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}&display=1",
                task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, Request.QueryString["appid"]
                );
         %>
            <tr>
                <td><a href="javascript:void(0);" onclick="openTask('<%=WebForm.Common.Tools.BaseUrl %>/Platform/WorkFlowRun/Default.aspx?<%=query1 %>','<%=task.Title %>','<%=task.ID %>');return false;" class="blue"><%=task.Title %></a></td>
                <td><%=flowName %></td>
                <td><%=stepName %></td>
                <td><%=task.SenderName %></td>
                <td><%=task.ReceiveTime.ToString().ToDateTimeStringS() %></td>
                <td><%=task.CompletedTime1.HasValue?task.CompletedTime1.Value.ToDateTimeStringS():"" %></td>
                <td><%=bworkFlowTask.GetStatusTitle(task.Status) %></td>
                <td>
                    <a class="viewlink" href="javascript:void(0);" onclick="detail('<%=task.FlowID %>','<%=task.GroupID %>');">查看</a>
                    <%if (task.Status==2 && bworkFlowTask.HasWithdraw(task.ID)){ %>
                    <a style="background:url(<%=WebForm.Common.Tools.BaseUrl %>/Images/ico/arrow_medium_left.png) no-repeat left center; padding-left:18px;" href="javascript:void(0);" onclick="withdraw('<%=task.ID %>');">收回</a>
                    <%}%>
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
        function detail(flowid, groupid)
        {
            top.mainDialog.open({
                url: top.rootdir + '/Platform/WorkFlowTasks/Detail.aspx?flowid1=' + flowid + "&groupid=" + groupid + '<%=query%>',
                width: 1024, height: 550, title: "查看流程处理过程"
            });
        }
        function withdraw(taskID)
        {
            if (confirm("您真的要收回该任务吗?"))
            {
                $.ajax({
                    url: top.rootdir + "/Platform/WorkFlowTasks/Withdraw.aspx?taskid=" + taskID + '<%=query%>', cache: false, async: false, success: function (txt)
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
