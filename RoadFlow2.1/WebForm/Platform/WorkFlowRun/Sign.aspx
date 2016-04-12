<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sign.aspx.cs" Inherits="WebForm.Platform.WorkFlowRun.Sign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <%
    WebForm.Common.Tools.CheckLogin(false);
    if (!Request.Form["save"].IsNullOrEmpty())
    {
        string pass = Request.Form["pass"];
        var user = RoadFlow.Platform.Users.CurrentUser;
        string openerid=Request.QueryString["openerid"];
        if(string.Compare(user.Password, new RoadFlow.Platform.Users().GetUserEncryptionPassword(user.ID.ToString(),pass.Trim()), false)==0)
        {
    %>
            <script type="text/javascript">
                var frame = null;
                var openerid = '<%=openerid%>';
                var iframes = top.frames;
                for (var i = 0; i < iframes.length; i++)
                {
                    if (iframes[i].name == openerid + "_iframe")
                    {
                        frame = iframes[i]; break;
                    }
                }
                if (frame != null)
                {
                    frame.setSign();
                }
                new RoadUI.Window().close();
            </script>
    <%
        }
        else
        {
    %>
            <script type="text/javascript">
                alert("密码错误!");
            </script>
    <%
        }
    }
    %>
    <form action="" method="post">
        <div style="text-align:center; padding-top:30px;">
            <input type="password" class="mytext" style="width:140px;" id="pass" name="pass" validate="empty" />
            <input type="submit" class="mybutton" value="确&nbsp;定" name="save" onclick="return new RoadUI.Validate().validateForm(document.forms[0]);" />
            <span type="msg"></span>
        </div>
    </form>
</body>
</html>
