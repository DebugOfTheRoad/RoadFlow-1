<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="WebForm.Platform.Members.UserAdd" %>

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
            <th style="width:80px;">姓名：</th>
            <td><input type="text" id="Name" name="Name" class="mytext" runat="server" onchange="getPy(this.value);" validate="empty,min,max" max="50" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">帐号：</th>
            <td><input type="text" id="Account" name="Account" class="mytext" runat="server" validate="empty,max,ajax" max="20" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">状态：</th>
            <td><asp:Literal ID="StatusRadios" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;" runat="server"></textarea></td>
        </tr>
        
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="submit" class="mybutton" name="Save" value="保存" onclick="return validate.validateForm(document.forms[0]);" />
        <input type="button" class="mybutton" onclick="window.location='Body.aspx'+'<%=Request.Url.Query%>';" value="返回" />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
           
        });
        function getPy(v)
        {
            $.ajax({ url: 'GetPy.ashx', data: { 'name': v }, dataType: 'text', type: 'post', cache: false, success: function (txt)
            {
                $('#Account').val(txt);
            }
            });
        }
    </script>
</body>
</html>
