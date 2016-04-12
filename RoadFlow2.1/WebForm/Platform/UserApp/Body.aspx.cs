using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class Body : Common.BasePage
    {
        protected string query = string.Empty;
        protected RoadFlow.Platform.AppLibrary bappLibrary = new RoadFlow.Platform.AppLibrary();
        protected RoadFlow.Platform.RoleApp broleApp = new RoadFlow.Platform.RoleApp();
        protected RoadFlow.Data.Model.RoleApp roleApp = null;
        protected string name = string.Empty;
        protected string type = string.Empty;
        protected string appid = string.Empty;
        protected string params1 = string.Empty;
        protected string ico = string.Empty;
        protected Guid parentID = Guid.Empty;
        protected string AppID = string.Empty;
        protected string AppTypesOptions = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            query = "&id=" + Request.QueryString["id"] + "&appid=" + Request.QueryString["appid"] + "&roleid=" + Request.QueryString["roleid"] + "&userid=" + Request.QueryString["userid"];
            string id = Request.QueryString["id"];
            Guid appID;
            if (id.IsGuid(out appID))
            {
                roleApp = broleApp.Get(appID);
                if (roleApp != null)
                {
                    name = roleApp.Title;
                    type = roleApp.AppID.HasValue ? bappLibrary.GetTypeByID(roleApp.AppID.Value) : "";
                    appid = roleApp.AppID.ToString();
                    params1 = roleApp.Params;
                    ico = roleApp.Ico;
                    parentID = roleApp.ParentID;
                }
            }
            AppID = appid;
            AppTypesOptions = bappLibrary.GetTypeOptions(type);
        }
    }
}