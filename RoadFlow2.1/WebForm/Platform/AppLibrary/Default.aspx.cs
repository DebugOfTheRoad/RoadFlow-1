using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.AppLibrary
{
    public partial class Default : Common.BasePage
    {
        protected string Query = string.Empty;
        protected string Iframeid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Query = "appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["appid"];
            Iframeid = Request.QueryString["appid"] + "_iframe";
        }
    }
}