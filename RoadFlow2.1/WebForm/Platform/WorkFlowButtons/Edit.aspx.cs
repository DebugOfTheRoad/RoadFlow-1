using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowButtons
{
    public partial class Edit : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.WorkFlowButtons bworkFlowButtons = new RoadFlow.Platform.WorkFlowButtons();
            RoadFlow.Data.Model.WorkFlowButtons workFlowButton = null;
            string id = Request.QueryString["id"];

            string title = string.Empty;
            string ico = string.Empty;
            string script = string.Empty;
            string note = string.Empty;

            Guid buttionID;
            if (id.IsGuid(out buttionID))
            {
                workFlowButton = bworkFlowButtons.Get(buttionID);
            }
            string oldXML = workFlowButton.Serialize();
            if (IsPostBack)
            {
                title = Request.Form["Title1"];
                ico = Request.Form["Ico"];
                script = Request.Form["Script"];
                note = Request.Form["Note"];

                bool isAdd = !id.IsGuid();
                if (workFlowButton == null)
                {
                    workFlowButton = new RoadFlow.Data.Model.WorkFlowButtons();
                    workFlowButton.ID = Guid.NewGuid();
                    workFlowButton.Sort = bworkFlowButtons.GetMaxSort();
                }

                workFlowButton.Ico = ico.IsNullOrEmpty() ? null : ico.Trim();
                workFlowButton.Note = note.IsNullOrEmpty() ? null : note.Trim();
                workFlowButton.Script = script.IsNullOrEmpty() ? null : script;
                workFlowButton.Title = title.Trim();

                if (isAdd)
                {
                    bworkFlowButtons.Add(workFlowButton);
                    RoadFlow.Platform.Log.Add("添加了流程按钮", workFlowButton.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
                }
                else
                {
                    bworkFlowButtons.Update(workFlowButton);
                    RoadFlow.Platform.Log.Add("修改了流程按钮", "", RoadFlow.Platform.Log.Types.流程相关, oldXML, workFlowButton.Serialize());
                }
                bworkFlowButtons.ClearCache();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "new RoadUI.Window().reloadOpener();alert('保存成功!');new RoadUI.Window().close();", true);
            }
            if (workFlowButton != null)
            {
                this.Title1.Value = workFlowButton.Title;
                this.Ico.Value = workFlowButton.Ico;
                this.Script.Value = workFlowButton.Script;
                this.Note.Value = workFlowButton.Note;
            }
        }
    }
}