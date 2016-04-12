<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sort.aspx.cs" Inherits="WebForm.Platform.Dictionary.Sort" %>

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
        <%foreach (var dict in DictList){ %>
        <ul class="sortul">
            <input type="hidden" value="<%=dict.ID %>" name="sort" />
            <%=dict.Title %>
        </ul>
        <%} %>
    </div>
        <div class="buttondiv">
            <input type="submit" class="mybutton" value="保存排序" />
            <input type="button" class="mybutton" value="返回" onclick="window.location='Body.aspx<%=Request.Url.Query%>';" />
        </div>
    </form>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        $(function ()
        {
            new RoadUI.DragSort($("#sortdiv"));
        });
    </script>
</body>
</html>
