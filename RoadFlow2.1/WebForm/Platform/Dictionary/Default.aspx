<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.Dictionary.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding:2px 0; overflow:hidden;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="1" border="0" width="100%">
        <tr>
            <td style="width:230px; vertical-align:top; padding:5px 5px 0 5px;">
                <iframe id="Iframe1" frameborder="0" scrolling="auto" src="Tree.aspx?appid=<%=Request.QueryString["appid"] %>" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
            <td class="organizesplit">
                <iframe id="Iframe2" frameborder="0" scrolling="auto" src="Body.aspx?appid=<%=Request.QueryString["appid"] %>" style="width:100%;margin:0;padding:0;"></iframe> 
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(function ()
        {
            var height = $(window).height();
            $('#Iframe1').attr('height', height - 10);
            $('#Iframe2').attr('height', height - 10);
        });
    </script>
    </form>
</body>
</html>
