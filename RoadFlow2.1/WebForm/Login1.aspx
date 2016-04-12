<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="WebForm.Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <input type="hidden" id="Force" name="Force" value="0" />
    <table cellpadding="0" cellspacing="1" border="0" style="width:95%; margin:0 auto;">
        <tr>
            <td style="width:70px; height:45px; text-align:right;">帐号：</td>
            <td><input type="text" class="mytext" id="Account" name="Account" value="" maxlength="50" style="width:170px;" /></td>
        </tr>
        <tr>
            <td style="height:45px; text-align:right;">密码：</td>
            <td><input type="password" class="mytext" id="Password" name="Password" maxlength="50" style="width:170px;" /></td>
        </tr>
        <tr id="novcode" style="display:none;">
            <td style="height:45px; text-align:right;">验证码：</td>
            <td><input type="text" class="mytext" id="VCode" name="VCode" maxlength="4" style="width:80px;" />
                <img alt="" src="VCode.ashx?<%=DateTime.Now.Ticks %>" onclick="cngimg();" style="vertical-align:middle;" id="VcodeImg" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <input type="submit" value="&nbsp;&nbsp;登&nbsp;&nbsp;录&nbsp;&nbsp;" id="loginbutton" name="loginbutton" onclick="return checkForm();" class="mybutton" />
            </td>
        </tr>
        <%if("1"!=Request.QueryString["session"]){%>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align:left; vertical-align:bottom; height:23px;"><span style="color:blue; margin-right:10px;">用户名:xh 密码:111</span><a href="http://www.cqroad.cn" target="_blank" ><span style="color:blue;">官网:cqroad.cn</span></a></td>
        </tr>
        <%} %>
    </table>
    </form>
    <script type="text/javascript">
        var isVcode = '1' == '<%=Session[RoadFlow.Utility.Keys.SessionKeys.IsValidateCode.ToString()]%>';
        var isSessionLost = "1" == '<%=Request.QueryString["session"]%>';
        $(window).load(function ()
        {
            if (isVcode)
            {
                if (!isSessionLost)
                {
                    top.win.resize(300, 250);
                }
                $("#novcode").show();
            }
            <%=Script%>
        });
        function checkForm()
        {
            var form1 = document.forms[0];
            if ($.trim(form1.Account.value).length == 0)
            {
                alert("帐号不能为空!");
                form1.Account.focus();
                return false;
            }
            if ($.trim(form1.Password.value).length == 0)
            {
                alert("密码不能为空!");
                form1.Password.focus();
                return false;
            }
            if (isVcode && form1.VCode && $.trim(form1.VCode.value).length == 0)
            {
                alert("验证码不能为空!");
                form1.VCode.focus();
                return false;
            }
            return true;
        }
        function cngimg()
        {
            $('#VcodeImg').attr('src', 'VCode.ashx?' + new Date().toString());
        }
    </script>
</body>
</html>
