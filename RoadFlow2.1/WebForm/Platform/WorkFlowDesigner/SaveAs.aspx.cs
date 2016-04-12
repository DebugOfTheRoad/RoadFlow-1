using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDesigner
{
    public partial class SaveAs : Common.BasePage
    {
        protected string saveOpen = string.Empty;
        protected string newid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string flowid = Request.QueryString["flowid"];
            if (IsPostBack && flowid.IsGuid() && !Request.Form["save"].IsNullOrEmpty())
            {
                string newName = Request.Form["NewFlowName"];
                saveOpen = Request.Form["SaveOpen"];
                var wf = new RoadFlow.Platform.WorkFlow().SaveAs(flowid.ToGuid(), newName);
                if(wf!=null)
                {
                    newid=wf.ID.ToString();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('另存成功!');", true);
                }
            }
        }
    }
}