using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.RoleApp
{
    public partial class Body : Common.BasePage
    {
        protected string AppID = string.Empty;
        protected RoadFlow.Data.Model.RoleApp roleApp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.AppLibrary bappLibrary = new RoadFlow.Platform.AppLibrary();
            RoadFlow.Platform.RoleApp broleApp = new RoadFlow.Platform.RoleApp();
            
            string id = Request.QueryString["id"];
            string name = string.Empty;
            string type = string.Empty;
            string appid = string.Empty;
            string params1 = string.Empty;
            string ico = string.Empty;
            Guid appID;
            if (id.IsGuid(out appID))
            {
                roleApp = broleApp.Get(appID);
            }

            if (IsPostBack)
            {
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    name = Request.Form["Name"];
                    type = Request.Form["Type"];
                    appid = Request.Form["AppID"];
                    params1 = Request.Form["Params"];
                    ico = Request.Form["Ico"];

                    string oldXML = roleApp.Serialize();
                    roleApp.Title = name.Trim();
                    if (appid.IsGuid())
                    {
                        roleApp.AppID = appid.ToGuid();
                    }
                    else
                    {
                        roleApp.AppID = null;
                    }
                    roleApp.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                    if (!ico.IsNullOrEmpty())
                    {
                        roleApp.Ico = ico;
                    }
                    else
                    {
                        roleApp.Ico = null;
                    }

                    broleApp.Update(roleApp);
                    broleApp.ClearAllDataTableCache();
                    RoadFlow.Platform.Log.Add("修改了应用模板", "", RoadFlow.Platform.Log.Types.角色应用, oldXML, roleApp.Serialize());
                    string refreshID = roleApp.ParentID == Guid.Empty ? roleApp.ID.ToString() : roleApp.ParentID.ToString();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + refreshID + "');alert('保存成功!');", true);
                }

                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    int i = broleApp.DeleteAndAllChilds(roleApp.ID);
                    broleApp.ClearAllDataTableCache();
                    RoadFlow.Platform.Log.Add("删除了模板及其所有下级共" + i.ToString() + "项", roleApp.Serialize(), RoadFlow.Platform.Log.Types.角色应用);
                    string refreshID = roleApp.ParentID == Guid.Empty ? roleApp.ID.ToString() : roleApp.ParentID.ToString();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + refreshID + "');window.location='Body.aspx?id=" + refreshID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "';", true);
                }
            }
            if (roleApp != null && roleApp.AppID.HasValue)
            {
                var app = new RoadFlow.Platform.AppLibrary().Get(roleApp.AppID.Value);
                if (app != null)
                {
                    type = app.Type.ToString();
                }
            }
            if(roleApp!=null)
            {
                this.Name.Value = roleApp.Title;
                this.Params.Value = roleApp.Params;
                this.Ico.Value = roleApp.Ico;
            }
            this.AppTypesOptions.Text = bappLibrary.GetTypeOptions(type);
            AppID = roleApp.AppID.ToString();
        }
    }
}