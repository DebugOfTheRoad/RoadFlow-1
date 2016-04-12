using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Dictionary
{
    public partial class Body : Common.BasePage
    {
        RoadFlow.Platform.Dictionary bdict = new RoadFlow.Platform.Dictionary();
        RoadFlow.Data.Model.Dictionary dict = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            this.Code.Attributes.Add("validate_url", "CheckCode.ashx?id=" + id);
            if (id.IsGuid())
            {
                dict = bdict.Get(id.ToGuid());
            }
            if (dict == null)
            {
                dict = bdict.GetRoot();
            }
            if (IsPostBack && dict != null)
            {
                string refreshID = dict.ParentID == Guid.Empty ? dict.ID.ToString() : dict.ParentID.ToString();
                //删除
                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    int i = bdict.DeleteAndAllChilds(dict.ID);
                    bdict.RefreshCache();
                    RoadFlow.Platform.Log.Add("删除了数据字典及其下级共" + i.ToString() + "项", dict.Serialize(), RoadFlow.Platform.Log.Types.数据字典);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('删除成功!');parent.frames[0].reLoad('" + refreshID + "');window.location='Body.aspx?id=" + dict.ParentID.ToString() + "&appid=" + Request.QueryString["appid"] + "';", true);
                }
                //保存
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    string title = Request.Form["Title1"];
                    string code = Request.Form["Code"];
                    string values = Request.Form["Values"];
                    string note = Request.Form["Note"];
                    string other = Request.Form["Other"];
                    string oldXML = dict.Serialize();

                    dict.Code = code.IsNullOrEmpty() ? null : code.Trim();
                    dict.Note = note.IsNullOrEmpty() ? null : note.Trim();
                    dict.Other = other.IsNullOrEmpty() ? null : other.Trim();
                    dict.Title = title.Trim();
                    dict.Value = values.IsNullOrEmpty() ? null : values.Trim();

                    bdict.Update(dict);
                    bdict.RefreshCache();
                    RoadFlow.Platform.Log.Add("修改了数据字典项", "", RoadFlow.Platform.Log.Types.数据字典, oldXML, dict.Serialize());
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');parent.frames[0].reLoad('" + refreshID + "');", true);
                }
            }
            if (dict != null)
            {
                this.Title1.Value = dict.Title;
                this.Code.Value = dict.Code;
                this.Values.Value = dict.Value;
                this.Note.Value = dict.Note;
                this.Other.Value = dict.Other;
            }
        }
    }
}