;$(window).load(function ()
{
    var roadInit = new RoadUI.Init();
    roadInit.calendar();
    roadInit.file();
    roadInit.member();
    roadInit.dict();
    roadInit.select();
    roadInit.combox();
    roadInit.table();
    roadInit.selectIco();
    roadInit.editor();
    roadInit.text();
    roadInit.textarea();
    roadInit.button();
    roadInit.validate();
    roadInit.textFocus();
});

;RoadUI.Init = function ()
{
    this.validate = function ()
    {
        new RoadUI.Validate().bind($("[validate]"));
    };

    this.textFocus = function ()
    {
        var $txt = $('<input type="text" style="height:0; width:0;" />');
        try
        {
            $("body").prepend($txt);
            $txt.get(0).focus();
            $txt.remove();
        } catch (e) { }
    };

    this.text = function ()
    {
        new RoadUI.Text().init($(".mytext"));
    };

    this.textarea = function ()
    {
        new RoadUI.Textarea().init($(".mytextarea"));
    };

    this.editor = function ()
    {
        new RoadUI.Editor().init($(".myeditor"));
    };

    this.calendar = function ()
    {
        new RoadUI.Calendar().init($(".mycalendar"));
    };

    this.select = function ()
    {
        new RoadUI.Select().init($(".myselect"));
    };

    this.combox = function ()
    {
        new RoadUI.Combox().init($(".mycombox"));
    };

    this.button = function ()
    {
        new RoadUI.Button().init($(".mybutton"));
    };

    this.file = function ()
    {
        new RoadUI.File().init($(".myfile"));
    };

    this.member = function ()
    {
        new RoadUI.Member().init($(".mymember"));
    };

    this.dict = function ()
    {
        new RoadUI.Dict().init($(".mydict,.mylrselect"));
    };

    this.selectIco = function ()
    {
        $(".myico").each(function ()
        {
            new RoadUI.SelectIco({ obj: $(this) });
        });

    };

    this.table = function ()
    {
        $(".listtable tbody tr:even td").removeClass().addClass("listtabletrout");
        $(".listtable tbody tr:odd td").removeClass().addClass("listtabletrover");
    };
}