<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.DBConnection.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:0;">
        <a href="javascript:void(0);" onclick="add();return false;"><span style="background-image:url(<%=WebForm.Common.Tools.BaseUrl%>/Images/ico/folder_classic_stuffed_add.png);">添加连接</span></a>
        <span class="toolbarsplit">&nbsp;</span>
        <input type="submit" style="display:none;" value="d" id="DeleteBut" name="DeleteBut" />
        <a href="javascript:void(0);" onclick="del();return false;"><span style="background-image:url(<%=WebForm.Common.Tools.BaseUrl%>/Images/ico/folder_classic_stuffed_remove.png);">删除所选</span></a>
    </div>
    <table class="listtable">
        <thead>
            <tr>
                <th width="3%" sort="0"><input type="checkbox" onclick="checkAll(this.checked);" style="vertical-align:middle;" /></th>
                <th width="15%">连接名称</th>
                <th width="10%">连接类型</th>
                <th width="42%">连接字符串</th>
                <th width="15%">备注</th>
                <th width="15%" sort="0">操作</th>
            </tr>
        </thead>
        <tbody>
        <% 
        foreach (var conn in ConnList)
        {%>
            <tr>
                <td><input type="checkbox" value="<%=conn.ID %>" name="checkbox_app" /></td>
                <td><%=conn.Name %></td>
                <td><%=conn.Type %></td>
                <td style="word-break:break-all;word-wrap:break-word;"><%=conn.ConnectionString %></td>
                <td><%=conn.Note %></td>
                <td>
                    <a class="editlink" href="javascript:edit('<%=conn.ID %>');">编辑</a>
                    <a onclick="test('<%=conn.ID %>');" style="background:url(<%=WebForm.Common.Tools.BaseUrl%>/Images/ico/hammer_screwdriver.png) no-repeat left center; padding-left:18px; margin-left:5px;" href="javascript:void(0);">测试</a>
                </td>
            </tr>
        <% }%>
        </tbody>
    </table>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
        function add()
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "添加连接", width: 700, height: 320, url: top.rootdir + '/Platform/DBConnection/Edit.aspx?1=1' + '<%=Query1%>', openerid: iframeid });
        }
        function edit(id)
        {
            dialog.open({ id: "window_" + appid.replaceAll('-', ''), title: "编辑连接", width: 700, height: 320, url: top.rootdir + '/Platform/DBConnection/Edit.aspx?id=' + id + '<%=Query1%>', openerid: iframeid });
        }
        function test(id)
        {
            $.ajax({
                url: "Test.ashx?id=" + id + "&appid=" + appid, cache: false, async: false, success: function (txt)
                {
                    alert(txt);
                }
            });
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
            if (!confirm('您真的要删除所选连接吗?'))
            {
                return false;
            }
            $("#DeleteBut").click();
            return true;
        }
    </script>
</form>
</body>
</html>
