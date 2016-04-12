using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    /// <summary>
    /// GetHtml 的摘要说明
    /// </summary>
    public class GetHtml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];
            Guid gid;
            if (!id.IsGuid(out gid))
            {
                context.Response.Write("");
                return;
            }

            var wff = new RoadFlow.Platform.WorkFlowForm().Get(gid);
            if (wff == null)
            {
                context.Response.Write("");
            }
            else
            {
                context.Response.Write(wff.Html);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}