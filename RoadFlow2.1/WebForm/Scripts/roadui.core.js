RoadUI = function () { };
RoadUI.Core = {
    allFrames: [],
    getAllFrames: function (frame)
    {
        if (!frame)
        {
            frame = top;
            this.allFrames.push(frame);
        }
        var frames = frame.frames;
        for (var i = 0; i < frames.length; i++)
        {
            this.allFrames.push(frames[i]);
            this.getAllFrames(frames[i]);
        }
    },
    newid: function (isMiddline)
    {
        var guid = "";
        isMiddline = isMiddline == undefined ? true : isMiddline;
        for (var i = 1; i <= 32; i++)
        {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if (isMiddline && (i == 8 || i == 12 || i == 16 || i == 20))
            {
                guid += "-";
            }
        }
        return guid;
    },
    rooturl: function ()
    {
        var curWwwPath = window.document.location.href;
        var pathName = window.document.location.pathname;
        var pos = curWwwPath.indexOf(pathName);
        var localhostPaht = curWwwPath.substring(0, pos);
        var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
        //alert(projectName);
        return projectName + "/";
    },
    query: function (name)
    {
        var search = document.location.search;
        var pattern = new RegExp("[?&]" + name + "\=([^&]+)", "g");
        var matcher = pattern.exec(search);
        var items = null;
        if (null != matcher)
        {
            try
            {
                items = decodeURIComponent(decodeURIComponent(matcher[1]));
            } catch (e)
            {
                try
                {
                    items = decodeURIComponent(matcher[1]);
                } catch (e)
                {
                    items = matcher[1];
                }
            }
        }
        return items;
    },
    open: function (url, width, height, name)//弹出居中窗口
    {
        //弹出窗口的宽度
        var iWidth = width || 700;
        //弹出窗口的高度
        var iHeight = height || 500;
        var y = (window.screen.availHeight - 30 - iHeight) / 2;    //获得窗口的垂直位置;
        var x = (window.screen.availWidth - 10 - iWidth) / 2;      //获得窗口的水平位置;
        return window.open(url, name || "newwindow_" + RoadUI.Core.newid(false), 'height=' + iHeight + ',width=' + iWidth + ',top=' + y + ',left=' + x + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no');
    },
    formatDate: function (date, fmt)
    {
        if (!date) return;
        if (!fmt) fmt = "yyyy-MM-dd";
        switch (typeof date)
        {
            case "string":
                date = new Date(date.replace(/-/, "/"));
                break;
            case "number":
                date = new Date(date);
                break;
        }
        if (!date instanceof Date) return;
        var o = {
            "M+": date.getMonth() + 1, //月份     
            "d+": date.getDate(), //日     
            "h+": date.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时     
            "H+": date.getHours(), //小时     
            "m+": date.getMinutes(), //分     
            "s+": date.getSeconds(), //秒     
            "q+": Math.floor((date.getMonth() + 3) / 3), //季度     
            "S": date.getMilliseconds() //毫秒     
        };
        var week = {
            "0": "\u65e5",
            "1": "\u4e00",
            "2": "\u4e8c",
            "3": "\u4e09",
            "4": "\u56db",
            "5": "\u4e94",
            "6": "\u516d"
        };
        if (/(y+)/.test(fmt))
        {
            fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        if (/(E+)/.test(fmt))
        {
            fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[date.getDay() + ""]);
        }
        for (var k in o)
        {
            if (new RegExp("(" + k + ")").test(fmt))
            {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    },
    decodeUri: function (uri)
    {
        if (!uri || $.trim(uri).length == 0)
        {
            return "";
        }
        try
        {
            return decodeURI(uri);
        }
        catch (e)
        {
            return uri;
        }
    },
    accDiv: function (arg1, arg2)//返回值：arg1除以arg2的精确结果
    {
        var t1 = 0, t2 = 0, r1, r2;
        try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
        try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
        with (Math)
        {
            r1 = Number(arg1.toString().replace(".", ""))
            r2 = Number(arg2.toString().replace(".", ""))
            return (r1 / r2) * pow(10, t2 - t1);
        }
    },
    accMul: function (arg1, arg2)//返回值：arg1乘以 arg2的精确结果
    {
        var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
        try { m += s1.split(".")[1].length } catch (e) { }
        try { m += s2.split(".")[1].length } catch (e) { }
        return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
    },
    accAdd: function (arg1, arg2)// 返回值：arg1加上arg2的精确结果
    {
        var r1, r2, m, c;
        try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
        try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
        c = Math.abs(r1 - r2);
        m = Math.pow(10, Math.max(r1, r2))
        if (c > 0)
        {
            var cm = Math.pow(10, c);
            if (r1 > r2)
            {
                arg1 = Number(arg1.toString().replace(".", ""));
                arg2 = Number(arg2.toString().replace(".", "")) * cm;
            }
            else
            {
                arg1 = Number(arg1.toString().replace(".", "")) * cm;
                arg2 = Number(arg2.toString().replace(".", ""));
            }
        }
        else
        {
            arg1 = Number(arg1.toString().replace(".", ""));
            arg2 = Number(arg2.toString().replace(".", ""));
        }
        return (arg1 + arg2) / m
    },
    accSub: function (arg1, arg2)// 返回值：arg1减去arg2的精确结果
    {
        var r1, r2, m, n;
        try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
        try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
        m = Math.pow(10, Math.max(r1, r2));
        //last modify by deeka
        //动态控制精度长度
        n = (r1 >= r2) ? r1 : r2;
        return ((arg1 * m - arg2 * m) / m).toFixed(n);
    },
    isIe6Or7: function ()
    {
        return !+'\v1' && !'1'[0];
    },
    isIE: function ()
    {
        return !!window.ActiveXObject;
    },
    isIE6: function ()
    {
        return this.isIE() && !window.XMLHttpRequest;
    },
    isIE7: function ()
    {
        return this.isIE() && !this.isIE6() && !this.isIE8();
    },
    isIE8: function ()
    {
        return this.isIE() && !!document.documentMode;
    },
    intDiv: function (number1, number2)//整除
    {
        var num1 = Math.round(number1);
        var num2 = Math.round(number2);
        var result = num1 / num2;
        if (result >= 0)
        {
            result = Math.floor(result);
        }
        else
        {
            result = Math.ceil(result);
        }
        return result;
    },
    offsetTop:function (elements){
        var top = elements.offsetTop;
        var parent = elements.offsetParent;
        while (parent)
        {
            top += parent.offsetTop;
            parent = parent.offsetParent;
        };
        return top;
    },
    offsetLeft:function (elements){
        var left = elements.offsetLeft;
        var parent = elements.offsetParent;
        while (parent)
        {
            left += parent.offsetLeft;
            parent = parent.offsetParent;
        };
        return left;
    },
   
    getPager:function (count, size, number, param, loadDataFunName, eleId)
    {
        eleId = eleId || "";
        var pager = '';
        size = size || 15;
        number = number || 1;
        //得到共有多少页
        var pageCount = count <= 0 ? 1 : count % size == 0 ? parseInt(count / size) : parseInt(count / size) + 1;
        if (pageCount <= 1)//只有一页则返回空
        {
             return "";
        }
        if (number < 1)
        {
            number = 1;
        }
        else if (number > pageCount)
        {
            number = pageCount;
        }
        //构造分页字符串
        var displaySize = 10;//中间显示的页数
        pager += "<div>";
        pager += "<span style='margin-right:15px;'>共 " + count.toString() + " 条  每页 " + size.toString() + " 条</span>";
        pager += "<a class=\"b\" style=\"margin-right:10px;\" href=\"javascript:eval('" + loadDataFunName + "(" + size + "," + 1 + ",\\'" + eleId + "\\')');\">首页</a>";
        pager += "<a class=\"b\" style=\"margin-right:10px;\" href=\"javascript:eval('" + loadDataFunName + "(" + size + "," + (number - 1) + ",\\'" + eleId + "\\')');\">上一页</a>";
        pager += "<a class=\"b\" style=\"margin-right:10px;\" href=\"javascript:eval('" + loadDataFunName + "(" + size + "," + (number + 1) + ",\\'" + eleId + "\\')');\">下一页</a>";
        pager += "<a class=\"b\" href=\"javascript:eval('" + loadDataFunName + "(" + size + "," + pageCount + ",\\'" + eleId + "\\')');\">尾页</a>";  
        pager += "</div>";
        return pager;
    }
};

RoadUI.Xml = {
    getXmlDom: function ()
    {
        var xmldoc;
        if (window.ActiveXObject)
        {
            xmldoc = new ActiveXObject("Microsoft.XMLDOM");
        }
        else
        {
            if (document.implementation && document.implementation.createDocument)
            {
                xmldoc = document.implementation.createDocument("", "doc", null);
            }
        }
        return xmldoc;
    },
    loadXML: function (xml)
    {
        var xmldoc = RoadUI.Xml.getXmlDom();
        xmldoc.async = false;
        try
        {
            xmldoc.loadXML(xml);
        }
        catch (e)
        {
            xmldoc = new DOMParser().parseFromString(xml, "text/xml");
        }
        return xmldoc;
    },
    getElementValue: function (elements)
    {
        return elements && elements.length > 0 && elements[0].firstChild ? elements[0].firstChild.nodeValue : "";
    }
};

String.prototype.isInteger = function ()
{
    return (new RegExp(/^\d+$/).test(this));
};
String.prototype.isNumber = function (value, element)
{
    return (new RegExp(/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/).test(this));
};
String.prototype.trim = function ()
{
    return this.replace(/(^\s*)|(\s*$)/g, "");
};
String.prototype.isNullOrEmpty =function()
{
    return !this || this.length == 0 || this.trim().length == 0;
};
String.prototype.startWith = function (pattern)
{
    return this.indexOf(pattern) === 0;
};
String.prototype.endWith = function (pattern)
{
    var d = this.length - pattern.length;
    return d >= 0 && this.lastIndexOf(pattern) === d;
};
String.prototype.encodeTXT = function ()
{
    return (this).replaceAll('&', '&amp;').replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll(" ", "&nbsp;");
};
String.prototype.isMail = function ()
{
    return (new RegExp(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/).test(this.trim()));
};
String.prototype.isPhone = function ()
{
    return (new RegExp(/(^([0-9]{3,4}[-])?\d{3,8}(-\d{1,6})?$)|(^\([0-9]{3,4}\)\d{3,8}(\(\d{1,6}\))?$)|(^\d{3,8}$)/).test(this));
};
String.prototype.isUrl = function ()
{
    return (new RegExp(/^[a-zA-z]+:\/\/([a-zA-Z0-9\-\.]+)([-\w .\/?%&=:]*)$/).test(this));
};
String.prototype.isExternalUrl = function ()
{
    return this.isUrl() && this.indexOf("://" + document.domain) == -1;
};
String.prototype.replaceAll = function (s1, s2, ignoreCase)
{
    var str = this;
    if ('.' == s1)
    {
        while (str.indexOf(s1) != -1)
        {
            str = str.replace(s1, s2);
        }
        return str;
    }
    else
    {
        if (!RegExp.prototype.isPrototypeOf(s1))
        {
            return str.replace(new RegExp(s1, (ignoreCase ? "gi" : "g")), s2);
        }
        else
        {
            return str.replace(s1, s2);
        }
    }
};
String.prototype.isDate = function ()
{
    var str = this;
    var reg = /^(\d+)-(\d{1,2})-(\d{1,2})$/;
    var r = str.match(reg);
    if (r == null)
    {
        reg = /^(\d+)\/(\d{1,2})\/(\d{1,2})$/;
        r = str.match(reg);
    }
    if (r == null) return false;
    r[2] = r[2] - 1;
    var d = new Date(r[1], r[2], r[3]);
    if (d.getFullYear() != r[1]) return false;
    if (d.getMonth() != r[2]) return false;
    if (d.getDate() != r[3]) return false;
    return true;
}
String.prototype.isDateTime = function ()
{
    var str = this;
    var reg = /^(\d+)-(\d{1,2})-(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
    var r = str.match(reg);
    if (r == null)
    {
        reg = /^(\d+)\/(\d{1,2})\/(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        r = str.match(reg);
    }
    if (r == null) return false;
    r[2] = r[2] - 1;
    var d = new Date(r[1], r[2], r[3], r[4], r[5], r[6]);
    if (d.getFullYear() != r[1]) return false;
    if (d.getMonth() != r[2]) return false;
    if (d.getDate() != r[3]) return false;
    if (d.getHours() != r[4]) return false;
    if (d.getMinutes() != r[5]) return false;
    if (d.getSeconds() != r[6]) return false;
    return true;
}
String.prototype.removeHtml = function ()
{
    var str = this;
    return str.replace(/<[^>]+>/g, "");//去掉所有的html标记
};
Date.prototype.format = function (format)
{
    var o = {
        "M+": this.getMonth() + 1, 
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S": this.getMilliseconds()
    }
    if (/(y+)/.test(format))
    {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o)
    {
        if (new RegExp("(" + k + ")").test(format))
        {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};
Array.prototype.unique = function ()
{
    var res = [];
    var json = {};
    for (var i = 0; i < this.length; i++)
    {
        if (!json[this[i]])
        {
            res.push(this[i]);
            json[this[i]] = 1;
        }
    }
    return res;
};

var currentFocusObj = null; //当前焦点对象
function initElement($elements, type)
{
    if (!$elements || $elements.size() == 0)
    {
        return;
    }
    var cssType = type;
    $elements.addClass(cssType + "1")
    .bind("mouseover", function ()
    {
        $(this).removeClass().addClass(cssType + "2");
    }).bind("mouseout", function ()
    {
        if (currentFocusObj == null || $(this).get(0) !== currentFocusObj)
        {
            $(this).removeClass().addClass(cssType + "1");
        }
    }).bind("focus", function ()
    {
        if (currentFocusObj != null)
        {
            var css = $(currentFocusObj).attr("class").replace("1", "").replace("2", "");
            $(currentFocusObj).removeClass().addClass(css + "1");
        }
        $(this).removeClass().addClass(cssType + "2");

        currentFocusObj = $(this).get(0);
    });
}