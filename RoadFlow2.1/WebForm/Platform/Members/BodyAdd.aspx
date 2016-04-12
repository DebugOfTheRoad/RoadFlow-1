<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BodyAdd.aspx.cs" Inherits="WebForm.Platform.Members.BodyAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        var validate = new RoadUI.Validate();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
        <tr>
            <th style="width:80px;">名称：</th>
            <td><input type="text" id="Name" name="Name" class="mytext" validate="empty,minmax" runat="server" max="100" style="width:75%" /></td>
        </tr>
        <tr>
            <th style="width:80px;">类型：</th>
            <td><asp:Literal ID="TypeRadios" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">状态：</th>
            <td><asp:Literal ID="StatusRadios" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;" runat="server"></textarea></td>
        </tr>

        <tr id="deptmove_tr" style="display:none;">
            <th style="width:80px;">移动到：</th>
            <td>
                <table cellpadding="0" cellspacing="1" border="0">
                    <tr>
                        <td><input type="text" style="width:220px;" class="mymember" id="deptmove" more="false" user="false" station="true" dept="true" unit="true" runat="server"/></td>
                        <td><input type="submit" class="mybutton" name="Move" value="确定移动" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="submit" class="mybutton" onclick="return validate.validateForm(document.forms[0]);" name="Save" value="保存" />
        <input type="button" class="mybutton" value="返回" onclick="window.location='Body.aspx'+'<%=Request.Url.Query%>';" />
    </div>
    </form>
</body>
</html>
