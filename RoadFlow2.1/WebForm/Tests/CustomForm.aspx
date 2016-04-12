<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomForm.aspx.cs" Inherits="WebForm.Tests.CustomForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" action="CustomFormSave.aspx<%=Request.Url.Query %>" method="post">
    <div>
        <table class="formtable" style="width:98%; margin: 15px auto 0 auto;" cellpadding="0" cellspacing="1">
            <tr>
                <th style="width:60px;">标题：</th>
                <td><input type="text" class="mytext" id="title1" name="title1" runat="server" style="width:90%" validate="empty" errmsg="标题不能为空" /></td>
            </tr>
            <tr>
                <th>内容：</th>
                <td><textarea class="mytextarea" id="contents" name="contents" runat="server" style="width:99%;height:80px;"></textarea></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
