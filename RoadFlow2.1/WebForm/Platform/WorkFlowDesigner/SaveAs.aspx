<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaveAs.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.SaveAs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" width="96%" cellspacing="0" border="0" align="center" style="margin-top:12px;">
            <tr>
                <td height="30">新流程名称：</td>
            </tr>
            <tr>
                <td height="30"><input type="text" class="mytext" style="width:75%" validate="empty" id="NewFlowName" name="NewFlowName" /></td>
            </tr>
            <tr>
                <td height="30">
                    <input type="checkbox" value="1" style="vertical-align:middle;" id="SaveOpen" name="SaveOpen" />
                    <label for="SaveOpen" style="vertical-align:middle;">另存后立即打开</label>
                </td>
            </tr>
        </table>
        <div style="width:100%; margin:20px auto 10px auto; text-align:center;">
            <input type="submit" class="mybutton" name="save" style="margin-right:5px;" value=" 确 定 " onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
            <input type="button" class="mybutton" value=" 取 消 " onclick="new RoadUI.Window().close();" />
        </div>
    </form>
    <script type="text/javascript">
        var frame=null;
        var openerid = "<%=Request.QueryString["openerid"]%>";
        var isOpen = "1" == "<%=saveOpen%>";
        var newFlowID = "<%=newid%>";
        $(function(){
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                if (iframes[i].name == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            if (frame == null) return;

            if(isOpen)
            {
                frame.openFlow1(newFlowID);
                new RoadUI.Window().close();
            }
        });
    </script>
</body>
</html>
