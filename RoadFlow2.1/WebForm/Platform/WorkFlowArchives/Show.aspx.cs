using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowArchives
{
    public partial class Show : Common.BasePage
    {
        protected string Contents = string.Empty;
        protected string Comments = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (!id.IsGuid())
            {
                Contents = "无内容";
                return;
            }
            var archives = new RoadFlow.Platform.WorkFlowArchives().Get(id.ToGuid());
            if (archives != null)
            {
                Contents = archives.Contents;
                Comments = archives.Comments;
            }
            else
            {
                Contents = "无内容";
            }
        }
    }
}