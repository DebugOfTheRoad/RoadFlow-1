﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Platform.WorkFlowDesigner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
    <div class="toolbar" style="margin-top:0; border-top:none 0;">
        <a href="#" onclick="openFlow(); return false;">
            <span style="background:url(../../Images/ico/folder_classic.png) no-repeat left center;">打开</span>
        </a>
        <a href="#" onclick="addFlow(); return false;">
            <span style="background:url(../../Images/ico/folder_classic_add.png) no-repeat left center;">新建</span>
        </a>
        <a href="#" onclick="flowAttrSetting(); return false;">
            <span style="background:url(../../Images/ico/hammer_screwdriver.png) no-repeat left center;">属性</span>
        </a>

        <span class="toolbarsplit">&nbsp;</span>

        <a href="#" onclick="addStep(); return false;">
            <span style="background:url(../../Images/ico/shape_aling_left.png) no-repeat left center;">步骤</span>
        </a>
    
        <a href="#" onclick="addSubFlow(); return false;">
            <span style="background:url(../../Images/ico/shape_aling_center.png) no-repeat left center;">子流程</span>
        </a>
   
        <a href="#" onclick="addConn(); return false;">
            <span style="background:url(../../Images/ico/vector.png) no-repeat left center;">连线</span>
        </a>
        <a href="#" onclick="copyStep(); return false;">
            <span style="background:url(../../Images/ico/ui_saccordion.png) no-repeat left center;">复制</span>
        </a>
        <a href="#" onclick="removeObj(); return false;" title="删除当前选定对象">
            <span style="background:url(../../Images/ico/cross.png) no-repeat left center;">删除</span>
        </a>
        <span class="toolbarsplit">&nbsp;</span>

        <a href="#" onclick="saveFlow('save'); return false;">
            <span style="background:url(../../Images/ico/save.gif) no-repeat left center;">保存</span>
        </a>
        <a href="#" onclick="saveAs(); return false;">
            <span style="background:url(../../Images/ico/saveas.gif) no-repeat left center;">另存为</span>
        </a>
       
        <span class="toolbarsplit">&nbsp;</span>

        <a href="#" onclick="saveFlow('install'); return false;">
            <span style="background:url(../../Images/ico/folder_classic_up.png) no-repeat left center;">安装</span>
        </a>
        <a href="#" onclick="saveFlow('uninstall'); return false;">
            <span style="background:url(../../Images/ico/folder_classic_down.png) no-repeat left center;">卸载</span>
        </a>
        <a href="#" onclick="saveFlow('delete'); return false;" title="删除流程">
            <span style="background:url(../../Images/ico/folder_classic_stuffed_remove.png) no-repeat left center;">删除</span>
        </a>
    </div>

    <div id="flowdiv" style="margin:0; padding:0;"></div>
    <script src="Scripts/draw-min.js" type="text/javascript"></script>
    <script src="Scripts/workflow.js" type="text/javascript"></script>
    <script type="text/javascript">
        var appid = '<%=Request.QueryString["appid"]%>';
        var iframeid = '<%=Request.QueryString["tabid"]%>';
        var dialog = top.mainDialog;
    </script>
    </form>
</body>
</html>
