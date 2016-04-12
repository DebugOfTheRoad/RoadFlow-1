<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubTableEdit.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.SubTableEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        var win = new RoadUI.Window();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto; height:26px;">
        <div>
            <asp:LinkButton ID="LinkButton1" OnClientClick="return save();" runat="server" OnClick="LinkButton1_Click">
                <span style="background:url(/Images/ico/save.gif) no-repeat left center;">确认保存</span>
            </asp:LinkButton>
            <span class="toolbarsplit">&nbsp;</span>
            <a href="#" onclick="new RoadUI.Window().close();return false;" title="">
                <span style="background:url(/Images/ico/cancel.gif) no-repeat left center;">取消关闭</span>
            </a>
        </div>
    </div>
    <% 
        string secondtableeditform = Request.QueryString["secondtableeditform"];
        string secondtablerelationfield = Request.QueryString["secondtablerelationfield"];
    %>
    <div style="padding-top:10px;">
        <input type="hidden" name="<%=secondtablerelationfield %>" id="<%=secondtablerelationfield %>" value="<%=Request.QueryString["primarytableid"] %>" />
         <%
             var form = new RoadFlow.Platform.AppLibrary().Get(secondtableeditform.ToGuid());
             if (form != null)
             {
                 string src = form.Address;
                 if (!src.IsNullOrEmpty())
                 {
                     System.Text.StringBuilder sb = new System.Text.StringBuilder();
                     System.IO.TextWriter tw = new System.IO.StringWriter(sb);
                     try
                     {
                         Server.Execute(src, tw);
                         Response.Write(sb.ToString().RemovePageTag());
                     }
                     catch (Exception err)
                     {
                         Response.Write(err.Message);
                     }
                 }
             }
        %>
    </div>
    </form>
    <script type="text/javascript">
        function save()
        {
            $("#contents").remove();
            var validateAlertType = $("#Form_ValidateAlertType").size() > 0 && !isNaN($("#Form_ValidateAlertType").val()) ? parseInt($("#Form_ValidateAlertType").val()) : 1;
            var v = new RoadUI.Validate().validateForm(document.forms[0], validateAlertType);
            return v;
        }
    </script>
</body>
</html>