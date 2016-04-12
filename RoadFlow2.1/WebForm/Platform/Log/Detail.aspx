<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebForm.Platform.Log.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="99%" class="formtable">
        <tr>
            <th style="width:100px;">日志标题：</th>
            <td style="word-break:break-all;"><asp:Literal ID="Title1" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>日志类型：</th>
            <td><asp:Literal ID="Type" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>发生时间：</th>
            <td><asp:Literal ID="WriteTime" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>操作人员：</th>
            <td><asp:Literal ID="UserName" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>发生IP：</th>
            <td><asp:Literal ID="IPAddress" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>操作地址：</th>
            <td style="word-break:break-all;"><asp:Literal ID="URL" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th>客户端信息：</th>
            <td><asp:Literal ID="Others" runat="server"></asp:Literal></td>
        </tr>
        
        <tr id="contentstr" runat="server">
            <th>日志内容：</th>
            <td><pre style="word-break:break-all; font-family:'Courier New'; white-space:pre-wrap;"><asp:Literal ID="Contents" runat="server"></asp:Literal></pre></td>
        </tr>
        
        <tr id="oldxmlstr" runat="server">
            <th>修改前：</th>
            <td><pre style="word-break:break-all; font-family:'Courier New'; white-space:pre-wrap;"><asp:Literal ID="OldXml" runat="server"></asp:Literal></pre></td>
        </tr>
        <tr id="newxmlstr" runat="server">
            <th>修改后：</th>
            <td><pre style="word-break:break-all; font-family:'Courier New'; white-space:pre-wrap;"><asp:Literal ID="NewXml" runat="server"></asp:Literal></pre></td>
        </tr>
    </table>
    <div class="buttondiv">
        <input type="button" class="mybutton" value="关闭窗口" style="margin-left:5px;" onclick="new RoadUI.Window().close();" />
    </div>
</body>
</html>
