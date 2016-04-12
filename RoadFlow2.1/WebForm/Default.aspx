<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        html,body {overflow:hidden; }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div class="mainTop">
    <div class="mainTopLeft"></div>
    <div class="mainTopRight">
        <div style="height:9px;"></div>
        <div style="padding-right:20px;">
            <div>
                <span class="indexwelcome">欢迎您：<asp:Literal ID="UserName" runat="server"></asp:Literal></span>
                <span style="margin-right:6px;"></span>
                <span style="margin-right:6px;"><select id="roleselect" onchange="roleChange(this.value)" class="roleselect"><asp:Literal ID="RoleOptions" runat="server"></asp:Literal></select></span>
                <span style="margin-right:6px;">日期：<span id="CurrentDateTimeSpan"><asp:Literal ID="CurrentTime" runat="server"></asp:Literal></span></span>
                <span style="">主题：</span>
                <span class="mainTheme_blue" onclick="changeTheme('Blue', true);"></span>
                <span class="mainTheme_green" onclick="changeTheme('Green', true);"></span>
                <span class="mainTheme_gray" onclick="changeTheme('Gray', true);"></span>
            </div>
            <div style="margin-top:4px;">
                <span style="margin-right:4px;"><a href="http://www.cqroad.cn" class="white" target="_blank">官方网站</a></span>
                <span style="margin-right:6px;">|</span>
                <span style="margin-right:6px;"><a href="javascript:void(0);" onclick="openApp('Platform/Home/Default.aspx',0,'首页','index'); return false;" class="white" >平台首页</a></span>
                <span style="margin-right:6px;">|</span>
                <span style="margin-right:6px;"><a href="javascript:void(0);" onclick="openApp('Platform/UserInfo/EditPass.aspx',2,'修改密码','index_editpass',500,210); return false;" class="white" >修改密码</a></span>
                <span style="margin-right:6px;">|</span>
                <span style="margin-right:4px;"><a href="javascript:void(0);" onclick="if(confirm('您真的要退出系统吗?')){window.location='Logout.ashx';} return false;" class="white" >退出系统</a></span>
            </div>
        </div>
    </div>
    <div style="clear:both;"></div>
</div>
<div class="mainDiv">
<table cellpadding="0" cellspacing="0" width="100%" border="0" style="table-layout:fixed;">
    <tr>
        <td class="mainMenutd" id="mainMenutd" >
            <div class="menuDiv">
                <div class="menuDivLeft">
                    <div id="menuDiv0">
                        
                    </div>
                    <div id="menuDiv1"></div>
                    <div id="menuDiv2">
                        <!--
                        <div menuid="quit" type="system" onclick="if(confirm('您真的要退出系统吗?')){window.location='Login/Quit';}">
                            <div style="height:5px;"></div>
                            <div class="menuDivLeftIco" style="background-image:url(Images/ico/direction.png)"></div>
                            <div class="menuDivLeftTitle">退出系统</div>
                            <div class="menuDivLeftSplit"></div>
                        </div>
                        -->
                    </div>
                </div>
                <div class="menuDivRight">
                    <div style="padding:10px 1px 1px 10px; overflow:auto;">
                        <div id="treeDiv" style="margin:0; overflow:auto;"></div>
                    </div>
                </div>
            </div>
        </td>
        <td class="mainSplittd" id="mainSplittd">
            <div class="mainSplittdImg" onclick="switchMenu(this);"></div>
        </td>
        <td style="vertical-align:top;">
            <div class="tab_top"></div>
            <div id="mainTabDiv" class="mainTabDiv"></div>
        </td>
    </tr>
</table>
</div>
</form>
<script type="text/javascript">
    var mainTab = null;
    var mainTree = null;
    var mainDialog = new RoadUI.Window();
    var defaultRoleID = "<%=DefaultRoleID%>";
    var currentDateTimeSpan = $("#CurrentDateTimeSpan");
    var rolesLength = <%=RoleLength%>;
    var userID = '<%=CurrentUserID%>';
    var rootdir = '<%=SitePath%>';
    $(function ()
    {
        var windowheight=$(window).height()-58;
        $('#mainTabDiv').height(windowheight-3);
        $('.menuDivLeft').height(windowheight);
        $('.menuDivRight').height(windowheight);
        $('.menuDivRight>div>div').height(windowheight-11);

        $(window).bind('resize', function ()
        {
            var height=$(window).height()-58;
            $('#mainTabDiv').height(height-3);
            $('.menuDivLeft').height(height);
            $('.menuDivRight').height(height);
            $('.menuDivRight>div>div').height(height-11);
            mainTab.topResize(height);
        });

        mainTab = new RoadUI.Tab({ id: "mainTabDiv", replace: true });
        roleChange(defaultRoleID);
        openApp("Platform/Home/Default.aspx", 0, "首页", "index");

        //初始化主题按钮样式
        var theme = $.cookies.get("theme_platform") || "Blue";
        changeTheme(theme, false);
    });

    function update()
    {
        $.ajax({ url: "", cache: false, async: true, success: function (txt)
        {
            currentDateTimeSpan.html(txt);
            window.setTimeout(update, 10000);
        },
            error: function ()
            {
                window.setTimeout(update, 20000);
            }
        });
    }

    function treeClick(json)
    {
        if (json)
        {
            openApp(json.link, json.model, json.title, json.id, parseInt(json.width), parseInt(json.height), true);
        }
    }

    function openApp(url, model, title, id, width, height, isAppendParams)
    {
        if (!url || url.toString().length == 0)
        {
            return;
        }
        if (!id)
        {
            id = RoadUI.Core.newid();
        }
        if (width == 0) width = undefined;
        if (height == 0) height = undefined;
        if (isAppendParams)
        {
            url += url.indexOf('?') >= 0 ? "&appid=" + id : "?appid=" + id;
        }
        url = url.substr(0,1) == "/" ? url : rootdir + url;
       
        switch (parseInt(model))
        {
            case 0:
                mainTab.addTab({ id: "tab_" + id.replaceAll('-', ''), title: title, src: url });
                break;
            case 1:
                mainDialog.open({ id: "window_" + id.replaceAll('-', ''), title: title, url: url, width: width || 800, height: height || 460, ismodal: false });
                break;
            case 2:
                mainDialog.open({ id: "window_" + id.replaceAll('-', ''), title: title, url: url, width: width || 800, height: height || 460, ismodal: true });
                break;
            case 3:
                RoadUI.Core.open(url,width || 800,height || 460, title);
                break;
            case 4:
                window.showModalDialog(url,null,"dialogWidth="+(width || 800)+"px;dialogHeight="+(height || 460)+"px;center=1");
                break;
            case 5:
                window.open(url);
                break;
        }
    }

    function switchMenu(div)
    {
        var flag="mainSplittdImg"==$(div).attr("class");
        if (flag)
        {
            $("#mainMenutd").hide(200);
            $(div).removeClass().addClass("mainSplittdImg1");
        }
        else
        {
            $("#mainMenutd").show(200);
            $(div).removeClass().addClass("mainSplittdImg");
        }
    }

    function roleChange(roleID)
    {
        $("#treeDiv").html("");
        $.ajax({url: rootdir + "/Platform/Home/Menu.ashx?roleid=" + roleID + "&userid=" + userID, async:false, cache:true, dataType:"json", success:function(json){
            if(json && json.loginstatus && -1 == json.loginstatus)
            {
                login();
                return;
            }
            if(json.length>0 && json[0].childs)
            {
                var html='';
                for(var i=0;i<json[0].childs.length;i++)
                {
                    var child=json[0].childs[i];
                    html+='<div '+(i==0?'class="menuDivLeft1"':'')+' type="menu" menuid="'+child.id+'" onclick="loadMenu(\''+child.id+'\',\''+roleID+'\', this); '+(child.url&&$.trim(child.url).length>0?'openApp(\''+child.url+'\', '+child.model+', \''+child.title+'\', \''+child.id+'\', parseInt('+child.width+'), parseInt('+child.height+'), true);"':'"')+'>';
                    html+='<div style="height:5px;font-size:0px;"></div>';
                    html+='<div class="menuDivLeftIco" '+(child.ico&&$.trim(child.ico).length>0?'style="background-image:url('+child.ico+')"':'')+'></div>';
                    html+='<div class="menuDivLeftTitle">'+child.title+'</div>';
                    html+='<div class="menuDivLeftSplit"></div>';
                    html+='</div>';
                }
                $("#menuDiv1").html(html);
                mainTree = new RoadUI.Tree({ id: "treeDiv", path: rootdir + "/Platform/Home/MenuRefresh.ashx?roleid=" + roleID + "&userid=" + userID + "&refreshid="+json[0].childs[0].id, refreshpath: rootdir + "/Platform/Home/MenuRefresh.ashx?roleid=" + roleID + "&userid=" + userID, showroot: true, showline:true, onclick: treeClick });
            }
        }});
    }

    function loadMenu(id, roleID, div)
    {
        if($(div).attr('class')=="menuDivLeft1") 
        {
            return false;
        }
        $("#menuDiv1>div,#menuDiv2>div").each(function(){
            $(this).removeClass();
        });
        $(div).removeClass().addClass("menuDivLeft1");
        mainTree = new RoadUI.Tree({ id: "treeDiv", path: rootdir + "/Platform/Home/MenuRefresh.ashx?roleid=" + roleID + "&userid=" + userID + "&refreshid=" + id, refreshpath: rootdir + "/Platform/Home/MenuRefresh.ashx?roleid=" + roleID + "&userid=" + userID, showroot: true, showline:true, onclick: treeClick });
    }

    function changeTheme(themeName, isCng)
    {
        if (!themeName || themeName.toString().trim().length == 0)
        {
            themeName = $.cookies.get("theme_platform")
        }

        $("span[class^='mainTheme_']").each(function ()
        {
            var cssName = $(this).attr("class");
            $(this).removeClass().addClass(cssName.replace("1", ""));
            
        });
        try
        {
            themeName=themeName.toLowerCase();
            var current=$(".mainTheme_" + themeName)||$(".mainTheme_" + themeName+"1");
            current.removeClass().addClass("mainTheme_" + themeName + "1");
        }
        catch(e){}
        if(isCng)
        {
            RoadUI.Core.allFrames = [];
            RoadUI.Core.getAllFrames();
            for (var i = 0; i < RoadUI.Core.allFrames.length; i++)
            {
                $("#style_style", RoadUI.Core.allFrames[i].document).attr("href", rootdir + "/Themes/" + themeName + "/Style/style.css");
                $("#style_ui", RoadUI.Core.allFrames[i].document).attr("href", rootdir + "/Themes/" + themeName + "/Style/ui.css");
            }
            $.cookies.set("theme_platform", themeName, { expiresAt: new Date(2099, 1, 1) });
        }
    }

    function login()
    {
        openApp(rootdir + "/Login1.aspx?session=1",1,"用户登录","login",400,230);
    }
</script>
</body>
</html>
