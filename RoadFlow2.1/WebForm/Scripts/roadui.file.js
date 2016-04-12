//文件上传
; RoadUI.File = function ()
{
    var instance = this;
    this.init = function ($files)
    {
        $files.each(function ()
        {
            $file = $(this);
            var id = $file.attr("id") || "";
            var name = $file.attr("name") || "";
            var filetype = $file.attr("filetype") || "";
            var value = $file.val();

            if (name.length == 0)
            {
                name = id;
            }
            var $hide = $('<input type="hidden" id="' + id + '" name="' + name + '" value="" />');
            var $but = $('<input type="button" class="mybutton" style="margin:0;" value="附件" />');
            $file.attr("id", id + "_text");
            $file.attr("name", name + "_text");
            $file.attr("readonly", "readonly");
            $file.removeClass().addClass("mytext");
            $file.css({ "border-top": "1px solid #b7b6b4", "border-left": "1px solid #b7b6b4", "border-bottom": "1px solid #b7b6b4", "border-right": "0" });
            $hide.val(value);
            if (value.length > 0)
            {
                $file.val('共' + value.split('|').length + '个文件');
            }
            $but.bind("click", function ()
            {
                var val = $(this).prev().prev().val();
                new RoadUI.Window().open({ id: "file_" + id, url: (top.rootdir || "") + "/Controls/UploadFiles/Default.aspx?eid=" + id + "&files=" + val + "&filetype=" + filetype, width: 500, height: 400, title: "附件管理", showclose: false });
            });
            $file.after($but).before($hide);
        });
    };
}