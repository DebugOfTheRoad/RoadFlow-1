<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sort.aspx.cs" Inherits="WebForm.Platform.Members.Sort" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div style="width:82%; margin:0 auto; height:auto;" id="sortdiv">
        <%foreach (var org in Orgs){%>
        <ul class="sortul">
            <input type="hidden" value="<%=org.ID %>" name="sort" />
            <%=org.Name %>
        </ul>
        <%}%> 
    </div>
    <div style="width:400px; text-align:center; margin:0 auto; margin-top:10px;">
        <input type="submit" class="mybutton" name="save" value="保存排序" />
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
            window.location = "Body.aspx" + '<%=Request.Url.Query%>';
        }
    </script>
</body>
</html>
