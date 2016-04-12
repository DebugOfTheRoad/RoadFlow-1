using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class Body1 : Common.BasePage
    {
        protected string query = string.Empty;
        protected string AppID = string.Empty;
        protected string AppTypesOptions = string.Empty;
        protected Guid ParentID = Guid.Empty;
        protected string name = string.Empty;
        protected string type = string.Empty;
        protected string appid = string.Empty;
        protected string params1 = string.Empty;
        protected string ico = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            query = "&id=" + Request.QueryString["id"] + "&appid=" + Request.QueryString["appid"] + "&roleid=" + Request.QueryString["roleid"] + "&userid=" + Request.QueryString["userid"];
            RoadFlow.Platform.AppLibrary bappLibrary = new RoadFlow.Platform.AppLibrary();
            RoadFlow.Platform.RoleApp broleApp = new RoadFlow.Platform.RoleApp();
            RoadFlow.Platform.UsersApp buserApp = new RoadFlow.Platform.UsersApp();
            RoadFlow.Data.Model.UsersApp usersApp = null;

            string id = Request.QueryString["id"];

            Guid appID;
            if (id.IsGuid(out appID))
            {
                usersApp = buserApp.Get(appID);
                if (usersApp != null)
                {
                    name = usersApp.Title;
                    type = usersApp.AppID.HasValue ? bappLibrary.GetTypeByID(usersApp.AppID.Value) : "";
                    appid = usersApp.AppID.ToString();
                    params1 = usersApp.Params;
                    ico = usersApp.Ico;
                    ParentID = usersApp.ParentID;
                }
            }


            if (IsPostBack && usersApp != null)
            {
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    name = Request.Form["Name"];
                    type = Request.Form["Type"];
                    appid = Request.Form["AppID"];
                    params1 = Request.Form["Params"];
                    ico = Request.Form["Ico"];

                    string oldXML = usersApp.Serialize();
                    usersApp.Title = name.Trim();
                    if (appid.IsGuid())
                    {
                        usersApp.AppID = appid.ToGuid();
                    }
                    else
                    {
                        usersApp.AppID = null;
                    }
                    usersApp.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                    if (!ico.IsNullOrEmpty())
                    {
                        usersApp.Ico = ico;
                    }
                    else
                    {
                        usersApp.Ico = null;
                    }

                    buserApp.Update(usersApp);
                    buserApp.ClearCache();
                    RoadFlow.Platform.Log.Add("修改了个人应用", "", RoadFlow.Platform.Log.Types.角色应用, oldXML, usersApp.Serialize());
                    string refreshID = usersApp.ParentID.ToString();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!'); parent.frames[0].reLoad('" + refreshID + "')", true);
                }

                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    int i = buserApp.DeleteAndAllChilds(usersApp.ID);
                    buserApp.ClearCache();
                    RoadFlow.Platform.Log.Add("删除了个人应用", usersApp.Serialize(), RoadFlow.Platform.Log.Types.角色应用);
                    string refreshID = usersApp.ParentID.ToString();
                    var parent = buserApp.Get(usersApp.ParentID);
                    string page = parent == null ? "Body.aspx" : "Body1.aspx";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + refreshID + "');window.location='" + page + "?id=" + refreshID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&userid=" + Request.QueryString["userid"] + "';", true);
                }
            }
            AppID = appid;
            AppTypesOptions = bappLibrary.GetTypeOptions(type);
        }
    }
}