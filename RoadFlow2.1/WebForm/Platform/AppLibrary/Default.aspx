<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.AppLibrary.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding:0; overflow:hidden;">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width:170px; vertical-align:top; padding:5px 5px 0 5px;">
                <iframe id="Iframe1" frameborder="0" scrolling="auto" src="Tree.aspx?<%=Query %>" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
            <td class="organizesplit" style="padding:0;">
                <iframe id="<%=Iframeid %>" frameborder="0" scrolling="auto" src="List.aspx?<%=Query %>" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function ()
        {
            var height = $(window).height();
            $('#Iframe1').attr('height', height);
            $('#<%=Iframeid %>').attr('height', height);
        });
    </script>
</body>
</html>
