using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Dictionary
{
    public partial class Sort : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.Dictionary> DictList = new List<RoadFlow.Data.Model.Dictionary>();
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();
            string id = Request.QueryString["id"];
            string refreshID = "";
            Guid dictid;
            if (id.IsGuid(out dictid))
            {
                var dict = BDict.Get(dictid);
                if (dict != null)
                {
                    DictList = BDict.GetChilds(dict.ParentID);
                    refreshID = dict.ParentID.ToString();
                }
            }

            if (IsPostBack)
            {
                string sortdict = Request.Form["sort"];
                if (sortdict.IsNullOrEmpty())
                {
                    return;
                }
                string[] sortArray = sortdict.Split(',');
                int i = 1;
                foreach (string id1 in sortArray)
                {
                    Guid gid;
                    if (id1.IsGuid(out gid))
                    {
                        BDict.UpdateSort(gid, i++);
                    }
                }
                BDict.RefreshCache();
                RoadFlow.Platform.Log.Add("保存了数据字典排序", "保存了ID为：" + id + "的同级排序", RoadFlow.Platform.Log.Types.数据字典);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + refreshID + "');", true);
                DictList = BDict.GetChilds(refreshID.ToGuid());
            }
        }
    }
}