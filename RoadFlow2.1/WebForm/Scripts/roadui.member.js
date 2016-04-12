﻿//组织机构选择
; RoadUI.Member = function ()
{
    var instance = this;
    this.init = function ($members)
    {
        $members.each(function (index)
        {
            var $_member = $members.eq(index);
            var id = $_member.attr("id") || "";
            var name = $_member.attr("name") || "";
            var value = $_member.val() || "";
            var title = $_member.attr("title") || "";

            var disabled = $_member.prop("disabled");

            $_member.prop("readonly", true);
            var $hide = $('<input type="hidden" id="' + id + '" name="' + name + '" value="' + (value || "") + '" />');
            var $but = $('<input type="button" ' + (disabled ? 'disabled="disabled"' : '') + ' title="' + title + '" class="mybutton" style="margin:0;" value="选择" />');
            $_member.attr("id", id + "_text");
            $_member.attr("name", name + "_text");
            $_member.css({ "border-top": "1px solid #b7b6b4", "border-left": "1px solid #b7b6b4", "border-bottom": "1px solid #b7b6b4", "border-right": "0" });
            $_member.removeClass().addClass("mytext");
            if (value && value.length > 0)
            {
                $.ajax({
                    url: (top.rootdir || "") + "/Controls/SelectMember/GetNames.ashx?values=" + value, type: "get", async: false, cache: false, success: function (txt)
                    {
                        $_member.val(txt);
                    }
                });
            }

            if ($_member.prop("disabled"))
            {
                $but.prop("disabled", true);
                $but.removeClass().addClass("buttondisabled");
            }
            else
            {
                $but.bind("click", function ()
                {
                    var $obj = $(this).prev().prev();
                    var val = $obj.val();
                    var $obj1 = $(this).prev();
                    var isunit = $obj1.attr("unit") || "";
                    var isdept = $obj1.attr("dept") || "";
                    var isstation = $obj1.attr("station") || "";
                    var isuser = $obj1.attr("user") || "";
                    var ismore = $obj1.attr("more") || "";
                    var isgroup = $obj1.attr("group") || "";
                    var rootid = $obj1.attr("rootid") || "";
                    var isall = $obj1.attr("all") || "";
                    var isexecuteflow = $obj1.attr("isChangeType") || "";
                    var isChangeType = "1" == isexecuteflow ? "1" : "0";

                    var params = "eid=" + id + "&isunit=" + isunit + "&isdept=" + isdept + "&isstation=" + isstation + "&isuser=" + isuser + "&ismore=" + ismore + "&isall=" + isall + "&isgroup=" + isgroup + "&rootid=" + rootid + "&values=" + val + "&isChangeType=" + isChangeType;
                    new RoadUI.Window().open({ id: "member_" + id, url: (top.rootdir || "") + "/Controls/SelectMember/Default.aspx?" + params, width: 500, height: 470, resize: false, title: "选择组织机构成员", openerid: RoadUI.Core.query("tabid") || "" });
                });
            }

            $_member.after($but).before($hide);
        });
    };
    this.setValue = function (objorid)
    {
        var $obj;
        if (typeof (objorid) == "string")
        {
            $obj = $("#" + objorid);
        }
        else
        {
            $obj = $(objorid);
        }
    
        if (!$obj || $obj.size() == 0) return;
        var value = $obj.val();
        if (value && value.length > 0)
        {
            $.ajax({
                url: (top.rootdir || "") + "/Controls/SelectMember/GetNames.ashx?values=" + value, type: "get", async: false, cache: false, success: function (txt)
            {
                $obj.next().val(txt);
            }
            });
        }
        else
        {
            $obj.next().val('');
        }

    };
}