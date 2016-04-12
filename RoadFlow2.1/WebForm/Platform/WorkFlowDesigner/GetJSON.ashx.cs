using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowDesigner
{
    /// <summary>
    /// GetJSON 的摘要说明
    /// </summary>
    public class GetJSON : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string flowid = context.Request.QueryString["flowid"];
            string type = context.Request.QueryString["type"];
            if (!flowid.IsGuid())
            {
                context.Response.Write("{}");
                return;
            }
            var flow = new RoadFlow.Platform.WorkFlow().Get(flowid.ToGuid());
            if (flow == null)
            {
                context.Response.Write("{}");
            }
            else
            {
                context.Response.Write("0" == type ? flow.RunJSON : flow.DesignJSON);
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