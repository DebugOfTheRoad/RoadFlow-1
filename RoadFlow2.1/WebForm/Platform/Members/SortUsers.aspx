<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortUsers.aspx.cs" Inherits="WebForm.Platform.Members.SortUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:82%; margin:0 auto; height:auto;" id="sortdiv">
        <%
        foreach (var user in Users)
        {
        %>
        <ul class="sortul">
            <input type="hidden" value="<%=user.ID %>" name="sort" />
            <%=user.Name %>
        </ul>
        <%}%>
    </div>
    <div style="width:90%; text-align:center; margin:0 auto; margin-top:10px;">
        <input type="submit" class="mybutton" value="保存排序" onclick="" />
        <input type="button" class="mybutton" value="返回" onclick="re();" />
    </div>
    </form>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        $(function ()
        {
            new RoadUI.DragSort($("#sortdiv"));
        });
        function re()
        {
            window.location = "User.aspx" + '<%=Request.Url.Query%>';
        }
    </script>
</body>
</html>
