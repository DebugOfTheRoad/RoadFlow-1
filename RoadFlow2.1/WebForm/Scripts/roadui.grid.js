; RoadUI.Grid = function (options)
{
    var defaults = {
        table: $(), //源表格
        tableID: '', //源表格ID
        pager: $(),//分页html
        showpager: true, //是否显示分页
        width: '', //列表宽度
        height: '', //列表高度
        sort: true, //是否可以排序
        sorttype: 'desc',//初始排序方式
        oddcolor: true, //是否隔行换色
        clickColor: false, //是否点击换色
        resizeCol: true, //是否可以调整列宽
        instanceName: '' //实例名称
    };
    this.opts = $.extend(defaults, options);
    if (!this.opts.table || this.opts.table.size() == 0)
    {
        var tableID = this.opts.tableID;
        if (tableID && $.trim(tableID).length > 0)
        {
            this.opts.table = $("#" + $.trim(tableID));
        }
    }
    if (!this.opts.table || this.opts.table.size() == 0)
    {
        throw "要初始化的表格为空";
        return false;
    }
    if (RoadUI.Core.isIE6())
    {
        this.opts.resizeCol = false;
    }
    var instance = this;
    
    this.init = function ()
    {
        var $div = $('<div class="grid"></div>');
        var $divhead = $('<div class="gridhead"></div>');
        var $divbody = $('<div class="gridbody"></div>');
        var $divpager = $('<div class="gridpager"></div>');
        var $divlist = $('<div class="gridlist"></div>');
        instance.opts.container = $div;
        var tableHtml = instance.opts.table.get(0).outerHTML;
        $divhead.append(tableHtml);
        $('tbody', $divhead).remove();
        $divbody.append(tableHtml);
        $('thead', $divbody).remove();
        $divlist.append($divhead, $divbody);
        $div.append($divlist);

        var $headTable = $('table', $divhead);
        $headTable.attr('border', '0');
        $headTable.attr('cellpadding', '0');
        $headTable.attr('cellspacing', '0');
        $headTable.css('width', '100%');
        $headTable.removeAttr('id');

        var $bodyTable = $('table', $divbody);
        $bodyTable.attr('border', '0');
        $bodyTable.attr('cellpadding', '0');
        $bodyTable.attr('cellspacing', '0');
        $bodyTable.css('width', '100%');
        $bodyTable.removeAttr('id');
        if (instance.opts.showpager)
        {
            $div.append($divpager);
            var $pager = instance.opts.pager;
            var pagerhtml = "";
            if ($pager && $pager > 0)
            {
                pagerhtml = $pager.html();
            }
            else
            {
                $pager = instance.opts.table.next();
                pagerhtml = $pager.html();
            }
            if ($.trim(pagerhtml).length > 0)
            {
                $divpager.html(pagerhtml);
            }
            $pager.remove();
        }
        $divhead.wrap('<div class="gridheadwrap"></div>');
        instance.opts.table.before($div);

        if (!instance.opts.height)
        {
            //instance.opts.height = $(window).height() - $div.get(0).offsetTop;
        }
        //alert($div.get(0).offsetTop);

        instance.opts.table.remove();
        
        if (instance.opts.height)
        {
            $div.height(instance.opts.height);
            $divbody.height(instance.opts.height - 28);
        }
        if (instance.opts.width)
        {
            $divhead.width(instance.opts.width);
            $divbody.width(instance.opts.width);
        }
        else
        {
            $divhead.css("width", '100%');
            $divbody.css("width", '100%');
        }
        $(window.document.body).css("overflow-x", "hidden");
        $divbody.bind("scroll", function ()
        {
            var left = $divbody.get(0).scrollLeft;
            $divhead.css('left', left - left * 2);
        });
        if ($divbody.children('table').height() > $divbody.height())
        {
            $divhead.width($divhead.width() - 17);
            $('table', $divbody).css('width', '100%');
        }
        var $tds = $("tbody tr:first td", $divbody);
        var $ths = $("thead tr th", $divhead);
        if ($tds.size() == $ths.size())
        {
            for (var i = 0; i < $ths.size() ; i++)
            {
                var txt = $ths.eq(i).html();
                var sort = $ths.eq(i).attr("sort") || "1";
                var title = '<div class="gridheadtitle">' + txt + '</div>';
                title += '<div class="gridheadsort">';
                if (instance.opts.sort && "1" == sort)
                {
                    title += '<div class="gridheadsortempty" colindex="' + i.toString() + '"></div>';
                }
                if (instance.opts.resizeCol)
                {
                    title += '<div class="gridheadresize" colindex="' + i.toString() + '">&nbsp;</div>';
                }
                title += '</div>';
                $ths.eq(i).html(title);

                $('.gridheadresize', $ths.eq(i)).bind('mousedown', function (e)
                {
                    var $divlist = $('.gridlist', instance.opts.container);
                    var $divline = $('<div class="gridheadresizeline" colindex="' + $(this).attr('colindex') + '"></div>');
                    var $maskdiv = $('<div class="gridmask"></div>');

                    $divline.css('left', (event || e).clientX);
                    $divlist.append($divline);
                    $divlist.append($maskdiv);
                    $divline.bind('mouseup', function (e)
                    {
                        var $ths = $('table thead tr th', $divhead);
                        var idx = parseInt($(this).attr('colindex'));
                        var $headTable = $("table", $divhead);
                        var $bodyTable = $("table", $divbody);
                        var thsWidth = $ths.eq(idx).width() + $ths.eq(idx).get(0).offsetLeft;
                        var addWidth = (event || e).clientX - thsWidth;
                        var tableWidth = $headTable.width() + addWidth;
                        $divhead.width(tableWidth);
                        $('table', $divhead).width(tableWidth);
                        $('table', $divbody).width(tableWidth);
                        $ths.eq(idx).width($ths.eq(idx).width() + addWidth);
                        $('.gridmask', $div).remove();
                        $(this).remove();
                        instance.resetWidth(false);
                    });
                    $maskdiv.bind('mousemove', function (e)
                    {
                        $(this).css('cursor', 'e-resize');
                        $('.gridheadresizeline', instance.opts.container).css({ "left": (event || e).clientX });
                    });
                });

                if (instance.opts.sort && "1" == sort)
                {
                    $(".gridheadtitle", $ths.eq(i)).bind('click', function ()
                    {
                        var $sortdiv = $(".gridheadsort div[class^='gridheadsort']", $(this).parent());
                        var className = $sortdiv.attr('class');
                        $sortdiv.removeClass().addClass(className == "gridheadsortdesc" || className == "gridheadsortdesc1" || className == "gridheadsortempty" ? "gridheadsortasc1" : "gridheadsortdesc1");
                        var $tds2 = $('.gridhead table thead tr th', instance.opts.container)
                        var index = 0;
                        for (var i = 0; i < $tds2.size() ; i++)
                        {
                            if ($tds2.eq(i).get(0) === $(this).parent().get(0))
                            {
                                index = i;
                            }
                            else
                            {
                                $(".gridheadsort div[class^='gridheadsort']", $tds2.eq(i)).removeClass().addClass("gridheadsortempty");
                            }
                        }
                        instance.sort($('.gridbody table', instance.opts.container), index);
                        instance.oddColor();
                        instance.resetWidth(true);
                    }).css("cursor", "pointer");
                }
            }
        }
        instance.resetWidth(false);
        instance.oddColor();
    };

    this.sort = function ($table, Idx)
    {
        var table = $table.get(0);
        var tbody = $('tbody', $table).get(0);
        var tr = tbody.rows;

        var trValue = new Array();
        for (var i = 0; i < tr.length; i++)
        {
            trValue[i] = tr[i];
        }
        if (tbody.sortCol == Idx)
        {
            trValue.reverse();
        }
        else
        {
            trValue.sort(function (tr1, tr2)
            {
                var param1 = tr1.cells[Idx].innerHTML.removeHtml();
                var param2 = tr2.cells[Idx].innerHTML.removeHtml();
                if (!isNaN(param1) && isNaN(param2))//如果参数1为数字，参数2为字符串
                {
                    return -1;
                }
                else if (isNaN(param1) && !isNaN(param2))//如果参数1为字符串，参数2为数字
                {
                    return 1;
                }
                else if (!isNaN(param1) && !isNaN(param2))//如果两个参数均为数字
                {
                    if (parseFloat(param1) > parseFloat(param2)) return 1;
                    if (parseFloat(param1) == parseFloat(param2)) return 0;
                    if (parseFloat(param1) < parseFloat(param2)) return -1;
                }
                else if ((param1.isDate() || param1.isDateTime()) && (param2.isDate() || param2.isDateTime()))//如果两个参数均为日期时间
                {
                    var date1 = Date.parse(param1.replaceAll('/', '-'));
                    var date2 = Date.parse(param2.replaceAll('/', '-'));
                    if (date1 > date2) return 1;
                    if (date1 == date2) return 0;
                    if (date1 < date2) return -1;
                }
                else//如果两个参数均为字符串类型
                {
                    return param1.localeCompare(param2);
                }
            });
        }

        var fragment = document.createDocumentFragment();
        for (var i = 0; i < trValue.length; i++)
        {
            fragment.appendChild(trValue[i]);
        }

        tbody.appendChild(fragment);
        tbody.sortCol = Idx;
    };

    this.resetWidth = function (isSort)
    {
        var $headtable = $('.gridhead table', instance.opts.container);
        var $bodytable = $('.gridbody table', instance.opts.container)
        var $ths = $('thead tr th', $headtable);
        var $tds = $('tbody tr:first td', $bodytable);

        for (var i = 0; i < $ths.size() ; i++)
        {
            if (!isSort)
            {
                //$ths.eq(i).width($ths.eq(i).width());
            }
            $tds.eq(i).width($ths.eq(i).width());
        }
    };

    this.oddColor = function ()
    {
        var $table = $('.gridbody table', instance.opts.container);
        $("tbody tr:odd td", $table).removeClass().addClass("gridbodytrodd");
        $("tbody tr:even td", $table).removeClass().addClass("gridbodytreven");
    };

    this.init();
}