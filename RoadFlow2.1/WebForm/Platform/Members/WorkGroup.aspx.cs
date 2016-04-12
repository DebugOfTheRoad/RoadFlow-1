using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class WorkGroup : Common.BasePage
    {
        protected RoadFlow.Platform.WorkGroup bwg = new RoadFlow.Platform.WorkGroup();
        protected string name = string.Empty;
        protected string members = string.Empty;
        protected string note = string.Empty;
        protected string users = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            Guid wid;
            RoadFlow.Data.Model.WorkGroup wg = null;
            name = string.Empty;
            members = string.Empty;
            note = string.Empty;
            users = string.Empty;

            if (!id.IsGuid(out wid) || wid == Guid.Empty)
            {
                Response.End();
            }

            wg = bwg.Get(wid);
            if (wg != null)
            {
                name = wg.Name;
                members = wg.Members;
                note = wg.Note;
                users = bwg.GetUsersNames(wg.Members, '、');
            }

            if (!Request.Form["Save"].IsNullOrEmpty() && IsPostBack && wg != null)
            {
                string oldxml = wg.Serialize();
                name = Request.Form["Name"];
                members = Request.Form["Members"];
                note = Request.Form["Note"];
                wg.Name = name.Trim();
                wg.Members = members;
                if (!note.IsNullOrEmpty())
                {
                    wg.Note = note;
                }

                bwg.Update(wg);
                users = bwg.GetUsersNames(wg.Members, '、');
                string query = Request.Url.Query;
                RoadFlow.Platform.Log.Add("修改了工作组", "修改了工作组", RoadFlow.Platform.Log.Types.组织机构, oldxml, wg.Serialize());
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "OK", "alert('保存成功!');", true);
            }

            //删除
            if (!Request.Form["DeleteBut"].IsNullOrEmpty() && IsPostBack && wg != null)
            {
                string oldxml = wg.Serialize();
                bwg.Delete(wg.ID);
                string query = Request.Url.Query;
                RoadFlow.Platform.Log.Add("删除了工作组", oldxml, RoadFlow.Platform.Log.Types.组织机构);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "OK", "parent.frames[0].treecng('1');alert('删除成功!');window.location = 'Empty.aspx' + '" + query + "';", true);
            }
        }
    }
}