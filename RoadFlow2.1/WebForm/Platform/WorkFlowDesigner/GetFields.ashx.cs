using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowDesigner
{
    /// <summary>
    /// GetFields 的摘要说明
    /// </summary>
    public class GetFields : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string table = context.Request.QueryString["table"];
            string connid = context.Request.QueryString["connid"];

            if (table.IsNullOrEmpty() || !connid.IsGuid())
            {
                context.Response.Write("[]");
            }
            Dictionary<string, string> fields = new RoadFlow.Platform.DBConnection().GetFields(connid.ToGuid(), table);
            System.Text.StringBuilder sb = new System.Text.StringBuilder("[", 1000);

            foreach (var field in fields)
            {
                sb.Append("{");
                sb.AppendFormat("\"name\":\"{0}\",\"note\":\"{1}\"", field.Key, field.Value);
                sb.Append("},");
            }
            context.Response.Write(sb.ToString().TrimEnd(',') + "]");
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