using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowButtons
{
    public partial class Default : Common.BasePage
    {
        protected IEnumerable<RoadFlow.Data.Model.WorkFlowButtons> workFlowButtonsList;
        protected string Query1 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = string.Empty;
            Query1 = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            RoadFlow.Platform.WorkFlowButtons bworkFlowButtons = new RoadFlow.Platform.WorkFlowButtons();
            
            if (IsPostBack)
            {
                if (!Request.Form["DeleteBut"].IsNullOrEmpty())
                {
                    string ids = Request.Form["checkbox_app"];
                    foreach (string id in ids.Split(','))
                    {
                        Guid bid;
                        if (!id.IsGuid(out bid))
                        {
                            continue;
                        }
                        var but = bworkFlowButtons.Get(bid);
                        if (but != null)
                        {
                            bworkFlowButtons.Delete(bid);
                            RoadFlow.Platform.Log.Add("删除了流程按钮", but.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
                        }
                    }
                    bworkFlowButtons.ClearCache();

                }
                workFlowButtonsList = bworkFlowButtons.GetAll();

                if (!Request.Form["Search"].IsNullOrEmpty())
                {
                    name = Request.Form["Name"];
                    if (!name.IsNullOrEmpty())
                    {
                        workFlowButtonsList = workFlowButtonsList.Where(p => p.Title.IndexOf(name) >= 0);
                    }
                }
            }
            else
            {
                workFlowButtonsList = bworkFlowButtons.GetAll();
            }
           
        }
    }
}