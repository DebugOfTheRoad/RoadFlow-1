<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Controls.SelectMember.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .SelectBorder{border:1px solid #cccccc; padding:3px 3px 3px 3px;}
        body { overflow:hidden;}
    </style>
    <script type="text/javascript">
        var win = new RoadUI.Window();
    </script>
</head>
<body>
    <% 
        RoadFlow.Platform.Organize borganizename = new RoadFlow.Platform.Organize();
        string userPrefix = RoadFlow.Platform.Users.PREFIX;
        string workgroupPrefix = RoadFlow.Platform.WorkGroup.PREFIX;
        string values = Request.QueryString["values"];
        string rootid = Request.QueryString["rootid"];
        string defaultValuesString = "";
        bool isChangeType = "1" == Request.QueryString["isChangeType"];
        System.Text.StringBuilder defautlSB = new System.Text.StringBuilder();
        foreach (string value in values.Split(','))
        {
            if (value.IsNullOrEmpty())
            {
                continue;
            }
            string name = borganizename.GetName(value);
            if (name.IsNullOrEmpty())
            {
                continue;
            }
            defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
            defautlSB.Append(name);
            defautlSB.Append("</div>");
        }
        defaultValuesString = defautlSB.ToString();    
    %>
    <table border="0" cellpadding="0" cellspacing="0" align="center" style="margin-top:4px;">
        <tr>
            <td valign="top">
                <div style="margin-bottom:4px;">
                    显示类型：<select onchange="treecng(this.value);" <%=isChangeType?"disabled='disabled'":"" %> id="showtype" class="myselect" style="width:158px;">
                        <option value="0">组织机构</option>
                        <option value="1">工作组</option>
                    </select>
                </div>
                <div id="Organize" style="width:210px; height:392px; overflow:auto;" class="SelectBorder"></div>
            </td>
            <td align="center" style="padding:0px 6px;" valign="middle">
                <div style="margin-bottom:12px;"><button class="mybutton" onclick="add();">添加</button></div>
                <div style="margin-bottom:12px;"><button class="mybutton" onclick="del();">删除</button></div>
                <div style="margin-bottom:12px;"><button class="mybutton" onclick="confirm1();">确定</button></div>
                <div><button class="mybutton" onclick="win.close();">取消</button></div>
            </td>
            <td valign="top">
                <div id="SelectNote" class="SelectBorder" style="width:200px; height:40px; overflow:auto; margin-bottom:5px;">
                    <span style="color:#ccc;">单击已选择项可显示该项详细信息</span>
                </div>
                <div id="SelectDiv" style="width:200px; height:367px; overflow:auto;" class="SelectBorder">
                    <%=defaultValuesString %>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var isdept = '<%=Request.QueryString["isdept"]%>';
        var isunit = '<%=Request.QueryString["isunit"]%>';
        var isstation = '<%=Request.QueryString["isstation"]%>';
        var isuser = '<%=Request.QueryString["isuser"]%>';
        var ismore = '<%=Request.QueryString["ismore"]%>';
        var isall = '<%=Request.QueryString["isall"]%>';
        var isgroup = '<%=Request.QueryString["isgroup"]%>';
        var onlyunit = '<%=Request.QueryString["onlyunit"]%>';
        var eid = '<%=Request.QueryString["eid"]%>';
        var rootid = '<%=rootid%>';
        var values = '<%=values%>';
        var userBefor = '<%=userPrefix%>';
        var userWorkGroup = '<%=workgroupPrefix%>';
        var orgTree = null;
        var current = null;
        var currentDel = null;
        $(function ()
        {
            orgTree = new RoadUI.Tree({
                id: "Organize", path: top.rootdir + "/Platform/Members/Tree1.ashx?showtype=0&isall=" + isall + "&onlyunit=" + onlyunit + "&rootid=" + rootid, refreshpath: top.rootdir + "/platform/Members/TreeRefresh.ashx?showtype=0",
                onclick: click, ondblclick: dblclick
            });
        });

        function treecng(val)
        {
            if (!val)
            {
                val = $("#showtype").val();
            }
            orgTree = new RoadUI.Tree({
                id: "Organize", path: top.rootdir + "/Platform/Members/Tree1.ashx?showtype=" + val + "&isall=" + isall + "&onlyunit=" + onlyunit + "&rootid=" + rootid, refreshpath: top.rootdir + "/Platform/Members/TreeRefresh.ashx?showtype=" + val,
                onclick: click, ondblclick: dblclick
            });
        }

        function click(json)
        {
            current = json;
        }
        function dblclick(json)
        {
            click(json);
            add();
        }
        function add()
        {
            if (!current)
            {
                alert("没有选择要添加的项"); return;
            }

            if (("0" == ismore || "false" == ismore.toLowerCase()) && $("#SelectDiv").children("div").size() >= 1)
            {
                alert("当前设置最多只能选择一项!"); return;
            }
            if (current.type == 1 && ("0" == isunit || "false" == isunit.toLowerCase()))
            {
                alert("当前设置不允许选择单位!"); return;
            }
            if (current.type == 2 && ("0" == isdept || "false" == isdept.toLowerCase()))
            {
                alert("当前设置不允许选择部门!"); return;
            }
            if (current.type == 3 && ("0" == isstation || "false" == isstation.toLowerCase()))
            {
                alert("当前设置不允许选择岗位!"); return;
            }
            if (current.type == 4 && ("0" == isuser || "false" == isuser.toLowerCase()))
            {
                alert("当前设置不允许选择人员!"); return;
            }
            if (current.type == 5 && ("0" == isgroup || "false" == isgroup.toLowerCase()))
            {
                alert("当前设置不允许选择工作组!"); return;
            }
            if ($("#SelectDiv div[value$='" + current.id + "']").size() > 0)
            {
                alert(current.title + "已经选择了!"); return;
            }
            var value = current.id;
            if (current.type == 4)
            {
                value = userBefor + value;
            }
            else if (current.type == 5)
            {
                value = userWorkGroup + value;
            }
            $("#SelectDiv").append('<div onclick="currentDel=this;showinfo(\'' + value + '\');" class="selectorDiv" ondblclick="currentDel=this;del();" value="' + value + '">' + current.title + '</div>');
        }
        function showinfo(id)
        {
            $.ajax({
                url: top.rootdir + '/Controls/SelectMember/GetNote.ashx?id=' + id, async: true, cache: true, success: function (txt)
                {
                    $("#SelectNote").html(txt);
                }
            });
        }
        function del()
        {
            if (!currentDel)
            {
                alert("没有选择要删除的项");
            }
            $(currentDel).remove();
            window.setTimeout('$("#SelectNote").html(\'<span style="color:#ccc;">单击已选择项可显示该项详细信息</span>\')', 1);
        }
        function confirm1()
        {
            var value = [];
            var title = [];
            var objs = $("#SelectDiv div");
            for (var i = 0; i < objs.size() ; i++)
            {
                value.push(objs.eq(i).attr("value"));
                title.push(objs.eq(i).text());
            }

            var ele = win.getOpenerElement(eid);
            var ele1 = win.getOpenerElement(eid + "_text");
            if (ele1 != null && ele1.size() > 0)
            {
                ele1.val(title.join(','));
            }
            if (ele != null && ele.size() > 0)
            {
                ele.val(value.join(','));
            }

            win.close();
        }
    </script>

</body>
</html>
