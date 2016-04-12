﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Controls.SelectIco.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding:6px 5px 5px 5px;">
    <style type="text/css">
        .fileItem1 {text-align:center; float:left; display:inline-block; margin:3px 7px 3px 7px; cursor:pointer; width:93px; height:40px; overflow:hidden;}
        .fileItem1 span {line-height:25px; padding:2px 2px 2px 2px; -moz-user-select:none; color:#555;}
        .fileItem2 {text-align:center; }
        .fileItem2 span {background:#ccc; padding:2px 2px 2px 2px; color:#fff;}
    </style>
    <table width="100%" cellpadding="0" cellspacing="1" border="0" align="center">
        <tr>
            <td>
                <div id="selectList" style="border:1px solid #ccc; overflow:auto; height: 308px; width:100%; text-align:center;"></div>
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom" style="height:32px;">
                <input type="button" id="btnOK" onclick="OK_Click();" value=" 确 定 " class="mybutton" />
                <input type="button" id="btnCancel" onclick="new RoadUI.Window().close();" value=" 取 消 " class="mybutton" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var path = '<%=Request.QueryString["source"]%>';
        var id = '<%=Request.QueryString["id"]%>';
        var curSelectName = '';
        var curSelectPath = '';
        var icoTree = null;
        $(function ()
        {
            getFiles(path);
        });
        function OK_Click()
        {
            var win = new RoadUI.Window();
            var ele = win.getOpenerElement(id);
            if (ele != null && ele.size() > 0)
            {
                $(ele).val(curSelectPath);
            }
            win.close();
        }

        function getFiles(folderValue)
        {
            $.ajax({
                type: "get", url: "File.ashx?path=" + folderValue, dataType: "xml", async: true, cache: false,
                success: function (xml) { showFiles(xml); }
            });
        }
        var showFiles = function (xmlDom)
        {
            $element = $("#selectList");
            $element.children().remove();
            if (xmlDom == null || xmlDom.documentElement.childNodes.length == 0)
            {
                return;
            }
            nodeList = xmlDom.documentElement.childNodes;
            for (var i = 0; i < nodeList.length; i++)
            {
                var title = getNodeAtt(nodeList[i], "title");
                var path = getNodeAtt(nodeList[i], "path");
                var path1 = getNodeAtt(nodeList[i], "path1");
                if (path == "") { continue; }

                var html = '<div class="fileItem1" title="' + title + '"'
                + ' onclick="$(\'#selectList\').children().removeClass(\'fileItem2\');$(this).addClass(\'fileItem2\');curSelectName=\'' + title + '\';curSelectPath=\'' + path1 + '\';" '
                + ' ondblclick="OK_Click();" '
                + '><img src="' + path + '" border="0" /><br/><span onselectstart="return false" >' + title + '</span></div>';
                $element.append(html);
            }
        }
        var getNodeAtt = function (node, att)
        {
            try { return $.trim(node.attributes.getNamedItem(att).nodeValue); } catch (e) { return ''; }
        }
    </script>
</body>
</html>
