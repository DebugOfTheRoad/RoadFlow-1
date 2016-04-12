<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WebForm.Platform.Members.User" %>

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
            <td><input type="text" id="Name" name="Name" class="mytext" runat="server" validate="empty,min,max" max="50" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">帐号：</th>
            <td><input type="text" id="Account" name="Account" class="mytext" runat="server" validate="empty,max,ajax" max="20" style="width:160px;" /></td>
        </tr>
        <tr>
            <th style="width:80px;">状态：</th>
            <td><asp:Literal runat="server" ID="StatusRadios"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" style="width:90%; height:50px;" runat="server"></textarea></td>
        </tr>
        <tr>
            <th style="width:80px;">所在组织：</th>
            <td><asp:Literal ID="ParentString" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th style="width:80px;">所属角色：</th>
            <td><asp:Literal ID="RoleString" runat="server"></asp:Literal></td>
        </tr>
        <tr id="StationMove_tr" style="display:none;">
            <th style="width:80px;">调往组织：</th>
            <td>
            <table cellpadding="0" cellspacing="1" border="0"><tr>
                <td><input type="text" style="width:180px;" title="选择要调往的组织：" class="mymember" id="movetostation" name="movetostation" more="false" user="false" station="true" dept="true" unit="true" runat="server"/>
                    <input type="checkbox" name="movetostationjz" id="movetostationjz" style="vertical-align:middle;" value="1" runat="server"/><label for="movetostationjz" style="vertical-align:middle;">兼职</label>
                </td>
                <td><input type="submit" class="mybutton" name="Move1" onclick="return stationMove1();" value="确定调动" /></td>
            </tr></table>
            </td>
        </tr>
        
    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="button" value="调动" class="mybutton" onclick="stationMove();" />
        <input type="button" class="mybutton" value="排序" id="sort" onclick="sort1('@id');" runat="server" />
        <input type="submit" class="mybutton" onclick="return confirm('您真的要初始化密码吗?');" name="InitPass" value="初始密码" />
        <input type="button" class="mybutton" value="设置应用" onclick="setUserApp();"/>
        <input type="submit" class="mybutton" name="Save" onclick="return validate.validateForm(document.forms[0]);" value="保存" />
        <input type="submit" class="mybutton" onclick="return confirm('您真的要删除该用户吗?');" name="DeleteBut" value="删除" />
    </div>
    </form>
    <script type="text/javascript">
        $(function(){
            
        });
        function stationMove()
        {
            $('#StationMove_tr').toggle();
        }

        function stationMove1()
        {
            if ($.trim($("#movetostation").val()).length == 0)
            {
                alert("请选择要调往的组织!");
                return false;
            }
            return true;
        }
        
        function sort1()
        {
            window.location = 'SortUsers.aspx' + '<%=Request.Url.Query%>';
        }

        function setUserApp()
        {
            top.mainDialog.open({url: top.rootdir + "/Platform/UserApp/Default.aspx"+"<%=Request.Url.Query%>",width:760,height:530,title:"设置人员应用"});
            return false;
        }
    </script>
</body>
</html>
