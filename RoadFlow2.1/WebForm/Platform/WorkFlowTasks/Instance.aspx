<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instance.aspx.cs" Inherits="WebForm.Platform.WorkFlowTasks.Instance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding: 0; overflow: hidden;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width: 200px; vertical-align: top; padding: 5px 5px 0 5px;">
                <iframe id="Iframe1" frameborder="0" scrolling="auto" src="InstanceTree.aspx?<%=query %>" style="width:100%;margin:0;padding:0;"></iframe>
            </td>
            <td class="organizesplit" style="padding: 0;">
                <iframe id="Iframe2" frameborder="0" scrolling="auto" src="InstanceList.aspx?<%=query %>" style="width:100%;margin:0;padding:0;"></iframe>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function ()
        {
            var height = $(window).height();
            $('#Iframe1').attr('height', height);
            $('#Iframe2').attr('height', height);
        });
    </script>
    </form>
</body>
</html>
