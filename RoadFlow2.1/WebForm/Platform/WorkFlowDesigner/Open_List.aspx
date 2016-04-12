<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open_List.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.Open_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="99%" align="center">
        <tr>
            <td align="left" height="35">
                名称：<input type="text" class="mytext" style="width:160px;" id="flow_name" runat="server" name="flow_name" />
                <input type="submit" class="mybutton" value=" 查询 "/>
            </td>
        </tr>
    </table>

    <table class="listtable">
        <thead>
        <tr>
            <th width="40%">流程名称</th>
            <th width="22%">创建时间</th>
            <th width="12%">创建人</th>
            <th width="11%">状态</th>
            <th width="10%" sort="0"></th>
        </tr>
        </thead>
        <tbody>
        <%
        foreach (var flow in flows.OrderBy(p => p.Name))
        {
            if (!borg.GetAllUsers(flow.Manager).Exists(p => p.ID == RoadFlow.Platform.Users.CurrentUserID))
            {
                continue;
            }
            if (type.IsGuid() && !bwf.GetAllChildsIDString(type.ToGuid()).Contains(flow.Type.ToString(),  StringComparison.CurrentCultureIgnoreCase))
            {
                continue;
            }
         %>
            <tr>
                <td><%=flow.Name %></td>
                <td><%=flow.CreateDate.ToDateTimeStringS() %></td>
                <td><%=busers.GetName(flow.CreateUserID) %></td>
                <td><%=bwf.GetStatusTitle(flow.Status) %></td>
                <td>
                    <a href="javascript:void(0);" onclick="openflow('<%=flow.ID %>');return false;">
                    <img src="../../Images/ico/folder_classic_opened.png" alt="" style="vertical-align:middle; border:0;" />
                    <span style="vertical-align:middle;">打开</span>
                    </a>
                </td>
            </tr>
        <%}%>       
        </tbody>
    </table>
    </form>   
    <script type="text/javascript">
        var frame = null;
        var openerid = '<%=Request.QueryString["openerid"]%>';
        $(window).load(function ()
        {
            //var dataGrid = new RoadUI.Grid({ table: $(".mygrid"), showpager: false, height: 350 });
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
        function typechange(type)
        {

        }
        function openflow(id)
        {
            frame.openFlow1(id);
            new RoadUI.Window().close();
        }
    </script>
</body>
</html>
