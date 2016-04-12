<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkFlowDelegation.Default" %>

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
                委托人：<input type="text" class="mymember" style="width:150px;" id="S_UserID" name="S_UserID" runat="server" />
                开始时间：<input type="text" class="mycalendar" style="width:100px;" id="S_StartTime" name="S_StartTime" runat="server" />
                结束时间：<input type="text" class="mycalendar" style="width:100px;" id="S_EndTime" name="S_EndTime" runat="server" />
                <input type="submit" name="Search" value="&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;" class="mybutton" />
                <input type="button" onclick="add(); return false;" value="添加委托" class="mybutton" />
                <input type="submit" onclick="return del();" name="DeleteBut" value="删除所选" class="mybutton" />
            </td>
        </tr>
    </table>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="30" sort="0"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th>委托人</th>
                <th>被委托人</th>
                <th>委托流程</th>
                <th>开始时间</th>
                <th>结束时间</th>
                <th>备注说明</th>
                <th>状态</th>
                <th sort="0">编辑</th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var deletation in workFlowDelegationList.OrderByDescending(p => p.WriteTime)){%> 
            <tr>
                <td><input type="checkbox" value="<%=deletation.ID %>" name="checkbox_app" /></td>
                <td><%=busers.GetName(deletation.UserID) %></td>
                <td><%=busers.GetName(deletation.ToUserID) %></td>
                <td><%=deletation.FlowID.HasValue && !deletation.FlowID.Value.IsEmptyGuid()? bworkFlow.GetFlowName(deletation.FlowID.Value):"所有流程" %></td>
                <td><%=deletation.StartTime.ToDateTimeStringS() %></td>
                <td><%=deletation.EndTime.ToDateTimeStringS() %></td>
                <td><%=deletation.Note %></td>
                <td><%=deletation.EndTime>=RoadFlow.Utility.DateTimeNew.Now?"委托中":"已失效" %></td>
                <td><a class="editlink" href="javascript:edit('<%=deletation.ID %>');">编辑</a></td>
            </tr>
        <%} %>
        </tbody>
    </table>
    <div class="buttondiv"><asp:Literal ID="Pager" runat="server"></asp:Literal></div>
    </form>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
        
        function add()
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "添加委托", width: 700, height: 360, url: top.rootdir + '/Platform/WorkFlowDelegation/Edit.aspx?1=1' + '<%=Query1%>', openerid: iframeid });
        }
        function edit(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "编辑委托", width: 700, height: 360, url: top.rootdir + '/Platform/WorkFlowDelegation/Edit.aspx?id=' + id + '<%=Query1%>', openerid: iframeid });
        }
        function checkAll(checked)
        {
            $("input[name='checkbox_app']").prop("checked", checked);
        }
        function del()
        {
            if ($(":checked[name='checkbox_app']").size() == 0)
            {
                alert("您没有选择要删除的项!");
                return false;
            }
            return confirm('您真的要删除所选委托吗?');
        }
    </script>

</body>
</html>
