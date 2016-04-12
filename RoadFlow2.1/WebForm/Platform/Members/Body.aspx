<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Body.aspx.cs" Inherits="WebForm.Platform.Members.Body" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        var win = new RoadUI.Window();
        var validate = new RoadUI.Validate();
    </script>
    <form id="form1" runat="server">
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
        <%if("1"!=Request.QueryString["type"]){%>
        <tr>
            <th style="width:100px;">分管领导：</th>
            <td><input type="text" class="mymember" runat="server" unit="0" dept="0" station="0" user="1" group="0" id="ChargeLeader" name="ChargeLeader" /></td>
        </tr>
        <tr>
            <th style="width:80px;">部门/岗位领导：</th>
            <td><input type="text" class="mymember" runat="server" unit="0" dept="0" station="0" user="1" group="0" id="Leader" name="Leader" /></td>
        </tr>
        <%}%>
        <tr>
            <th style="width:80px;">备注：</th>
            <td><textarea id="Note" name="Note" class="mytext" runat="server" style="width:90%; height:50px;"></textarea></td>
        </tr>

        <tr id="deptmove_tr" style="display:none;">
            <th style="width:80px;">移动到：</th>
            <td>
                <table cellpadding="0" cellspacing="1" border="0">
                    <tr>
                        <td><input type="text" style="width:220px;" class="mymember" id="deptmove" name="deptmove" more="false" user="false" station="true" dept="true" unit="true"/></td>
                        <td><input type="submit" class="mybutton" onclick="return deptmove3();" name="Move1" value="确定移动" /></td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
    <div style="width:95%; margin:10px auto 10px auto; text-align:center;">
        <input type="button" class="mybutton" onclick="window.location='BodyAdd.aspx'+'<%=Request.Url.Query%>';" value="添加单位/部门/岗位" id="addChild" />
        <input type="button" class="mybutton" onclick="window.location='UserAdd.aspx'+'<%=Request.Url.Query%>';" value="添加人员" id="addUser" />
        <input type="button" class="mybutton" value="移动" id="move" name="move" onclick="deptmove2();" />
        <input type="button" class="mybutton" value="排序" id="sort" onclick="sort1();" />
        <input type="submit" class="mybutton" onclick="return validate.validateForm(document.forms[0]);" name="Save" value="保存" />
        <input type="submit" class="mybutton" name="Delete" onclick="return confirm('您真的要删除该机构及其下级机构吗?');" value="删除" />
    </div>
    </form>

    <script type="text/javascript">
        var showType = '';
        $(function(){
            
        });
        function del()
        {
            return confirm('真的要删除吗?');
        }
        function deptmove3()
        {
            if ($.trim($("#deptmove").val()) == "")
            {
                alert("请选择要移动到的机构!");
                return false;
            }
            else if ($.trim($("#deptmove").val()) == '<%=Request.QueryString["id"]%>')
            {
                alert("不能将机构移动到自己!");
                return false;
            }
            return true;
        }
        function deptmove2()
        {
            $('#deptmove_tr').toggle();
        }

        function sort1()
        {
            window.location = 'Sort.aspx' + '<%=Request.Url.Query%>';
        }
    </script>

</body>
</html>
