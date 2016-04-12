<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Line.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.Set_Line" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <%
        string lineID = Request.QueryString["id"];
        string fromID = Request.QueryString["from"];
        string toID = Request.QueryString["to"];    
    %>
    <form id="form1" runat="server">
    <div id="tabdiv">
    <div id="div_sql" style="width:96%; margin:8px auto 0 auto;" title="&nbsp;&nbsp;SQL条件&nbsp;&nbsp;">
        <div>SQL条件：</div>
        <div style="margin-top:8px;">
            <textarea id="div_sql_value" style="width:98%; height:100px; font-family:Verdana;" rows="1" cols="1" class="mytextarea"></textarea>
        </div>
        <table border="0" style="width:99%;" align="center">
            <tr>
                <td>
                    <div style="margin-top:8px; line-height:21px;">
                        <div>1.条件对应的表为流程对应的主表</div>
                        <div>2.条件对应的字段为流程主表字段</div>
                        <div>3.示例：a=1 and b='1'</div>
                    </div>
                </td>
                <td style="text-align:right; padding:15px 4px 0 0; vertical-align:top;"><input type="button" class="mybutton" value="测试SQL条件" onclick="test();" /></td>
            </tr>
        </table>
        
        <div style="padding:8px;">条件标签：<input type="text" class="mytext" style="width:400px;" id="div_sql_title" /></div>
    </div>

    <div id="div_organize" style="width:99%; margin:8px auto 0 auto;" title="&nbsp;&nbsp;组织机构&nbsp;&nbsp;">
        <div style="margin-top:8px;">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
            <tr>
                <th style="width:100px">发送者属于：</th>
                <td>
                   <input type="text" class="mymember" id="organize_senderin" style="width:400px;" />
                </td>
            </tr>
            <tr>
                <th style="width:80px">发送者不属于：</th>
                <td>
                   <input type="text" class="mymember" id="organize_sendernotin" style="width:400px;" />
                </td>
            </tr>
            <tr>
                <th style="width:100px">发起者属于：</th>
                <td>
                   <input type="text" class="mymember" id="organize_sponsorin" style="width:400px;" />
                </td>
            </tr>
            <tr>
                <th style="width:80px">发起者不属于：</th>
                <td>
                   <input type="text" class="mymember" id="organize_sponsornotin" style="width:400px;" />
                </td>
            </tr>
            <tr>
                <th style="width:80px">是领导：</th>
                <td>
                   <input type="checkbox" id="organize_senderleader" style="vertical-align:middle;" /><label for="organize_senderleader">发送者是部门领导</label> 
                   <input type="checkbox" id="organize_senderchargeleader" style="vertical-align:middle;" /><label for="organize_senderchargeleader">发送者是部门分管领导</label> 
                   <input type="checkbox" id="organize_sponsorleader" style="vertical-align:middle;" /><label for="organize_sponsorleader">发起者是部门领导</label> 
                   <input type="checkbox" id="organize_sponsorchargeleader" style="vertical-align:middle;" /><label for="organize_sponsorchargeleader">发起者是部门分管领导</label> 
                </td>
            </tr>
            <tr>
                <th style="width:80px">不是领导：</th>
                <td>
                   <input type="checkbox" id="organize_notsenderleader" style="vertical-align:middle;" /><label for="organize_notsenderleader">发送者不是部门领导</label> 
                   <input type="checkbox" id="organize_notsenderchargeleader" style="vertical-align:middle;" /><label for="organize_notsenderchargeleader">发送者不是部门分管领导</label> 
                   <input type="checkbox" id="organize_notsponsorleader" style="vertical-align:middle;" /><label for="organize_notsponsorleader">发起者不是部门领导</label> 
                   <input type="checkbox" id="organize_notsponsorchargeleader" style="vertical-align:middle;" /><label for="organize_notsponsorchargeleader">发起者不是部门分管领导</label> 
                </td>
            </tr>
            </table>
        </div>
    </div>

    <div id="div_custom" style="width:96%; margin:8px auto 0 auto;" title="&nbsp;&nbsp;自定义方法&nbsp;&nbsp;">
        <div>
            <div>方法名称：</div><div style="margin-top:8px;"><input type="text" class="mytext" id="custom_method" style="width:90%;" /></div>
        </div>
        <div style="height:10px;"></div>
        <!--
        <div>
            <div>条件不满足提示信息：</div><div style="margin-top:8px;"><input type="text" class="mytext" id="custom_msg" style="width:90%;" /></div>
        </div>
        -->
        <div style="margin-top:10px; font-weight:bold;">方法说明：</div>
        <div style="line-height:21px; padding-left:12px;">
            <div>1.方法名称格式为：DLL名称.命名空间.类名.方法名（例：WebMvc.Common.CustomFormSave.Test）</div>
            <div>2.方法返回类型为 bool 类型的 True 时条件满足,返回其它类型且字符串值不为 "1" 时条件不满足</div>
            <div>3.方法返回类型为 bool 类型时提示信息为上面输入的信息，否则提示返回的值</div>
            <div>4.方法访问限定符为 Public</div>
        </div>
    </div>
    </div>
    <div style="width:100%; margin:30px auto 10px auto; text-align:center;">
        <input type="button" class="mybutton" value=" 确 定 " onclick="confirm1();" />
        <input type="button" class="mybutton" value=" 取 消 " onclick="new RoadUI.Window().close();" />
    </div>
    </form>

    <script type="text/javascript">
        var fieldsOptions = '';
        var frame=null;
        var openerid = "<%=Request.QueryString["openerid"]%>";
        var lineid="<%=lineID%>";
        var fromid="<%=fromID%>";
        var toid = "<%=toID%>";
        var table = "";
        var dbconnid = "";
        var dbtable = "";
        var dbtablepk = "";
        $(function ()
        {
            new RoadUI.Tab({ id: "tabdiv", replace: true, contextmenu: false });
            var iframes = top.frames;
            for (var i = 0; i < iframes.length; i++)
            {
                if (iframes[i].name == openerid + "_iframe")
                {
                    frame = iframes[i]; break;
                }
            }
            if (frame == null) return;

            var json=frame.wf_json;
            var line=null;
            if(json)
            {
                var lines=json.lines;
                if(lines && lines.length>0)
                {
                    for(var i=0;i<json.lines.length;i++)
                    {
                        if(json.lines[i].id==lineid)
                        {
                            line=json.lines[i];
                            break;
                        }
                    }
                }
                var databases = json.databases;
                if (databases && databases.length>0)
                {
                    dbconnid = databases[0].link;
                    dbtable = databases[0].table;
                    dbtablepk = databases[0].primaryKey;
                }
            }
            if(line)
            {
                $("#custom_method").val(line.customMethod);
                $("#div_sql_value").val(line.sql);
                $("#div_sql_title").val(line.text);
                $("#organize_senderin").val(line.organize_senderin);
                $("#organize_sendernotin").val(line.organize_sendernotin);
                $("#organize_sponsorin").val(line.organize_sponsorin);
                $("#organize_sponsornotin").val(line.organize_sponsornotin);
                new RoadUI.Member().setValue($("#organize_senderin"));
                new RoadUI.Member().setValue($("#organize_sendernotin"));
                new RoadUI.Member().setValue($("#organize_sponsorin"));
                new RoadUI.Member().setValue($("#organize_sponsornotin"));
                $("#organize_senderleader").prop("checked", "1" == line.organize_senderleader);
                $("#organize_senderchargeleader").prop("checked","1"==line.organize_senderchargeleader);
                $("#organize_sponsorleader").prop("checked","1"==line.organize_sponsorleader);
                $("#organize_sponsorchargeleader").prop("checked", "1" == line.organize_sponsorchargeleader);
                $("#organize_notsenderleader").prop("checked", "1" == line.organize_notsenderleader);
                $("#organize_notsenderchargeleader").prop("checked", "1" == line.organize_notsenderchargeleader);
                $("#organize_notsponsorleader").prop("checked", "1" == line.organize_notsponsorleader);
                $("#organize_notsponsorchargeleader").prop("checked", "1" == line.organize_notsponsorchargeleader);
            }
        });
        
        function confirm1()
        {
            var line={};
            line.id=lineid;
            line.from=fromid;
            line.to=toid;
            line.customMethod = $("#custom_method").val() || "";
            line.text = $("#div_sql_title").val() || "";
            line.sql = $("#div_sql_value").val() || "";
            line.organize_senderin = $("#organize_senderin").val() || "";
            line.organize_sendernotin = $("#organize_sendernotin").val() || "";
            line.organize_sponsorin = $("#organize_sponsorin").val() || "";
            line.organize_sponsornotin = $("#organize_sponsornotin").val() || "";
            line.organize_senderleader = $("#organize_senderleader").prop("checked") ? "1" : "0";
            line.organize_senderchargeleader = $("#organize_senderchargeleader").prop("checked") ? "1" : "0";
            line.organize_sponsorleader = $("#organize_sponsorleader").prop("checked") ? "1" : "0";
            line.organize_sponsorchargeleader = $("#organize_sponsorchargeleader").prop("checked") ? "1" : "0";
            line.organize_notsenderleader = $("#organize_notsenderleader").prop("checked") ? "1" : "0";
            line.organize_notsenderchargeleader = $("#organize_notsenderchargeleader").prop("checked") ? "1" : "0";
            line.organize_notsponsorleader = $("#organize_notsponsorleader").prop("checked") ? "1" : "0";
            line.organize_notsponsorchargeleader = $("#organize_notsponsorchargeleader").prop("checked") ? "1" : "0";

            frame.addLine(line);
            new RoadUI.Window().close();
        }

        function test()
        {
            var where = $("#div_sql_value").val();
            if ($.trim(where).length == 0)
            {
                alert("条件为空!");
                return;
            }
            $.ajax({
                url: "TestLineSqlWhere.ashx", method: "POST",
                data: { "connid": dbconnid, "table": dbtable, "tablepk": dbtablepk, "where": where },
                async: false, cache: false, success: function (txt)
                {
                    alert(txt);
                }
            });
        }
    </script>
</body>
</html>
