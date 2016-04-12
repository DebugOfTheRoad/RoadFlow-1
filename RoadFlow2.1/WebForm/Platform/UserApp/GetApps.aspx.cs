using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class GetApps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["type"];
            string appid = Request.Form["value"];
            Response.Write(new RoadFlow.Platform.AppLibrary().GetAppsOptions(type.ToGuid(), appid));
        }
    }
}