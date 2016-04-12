using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowDesigner
{
    /// <summary>
    /// GetTables 的摘要说明
    /// </summary>
    public class GetTables : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            string connID = context.Request.QueryString["connid"];
            if (!connID.IsGuid())
            {
                context.Response.Write("[]");
            }
            List<string> tables = new RoadFlow.Platform.DBConnection().GetTables(connID.ToGuid());
            System.Text.StringBuilder sb = new System.Text.StringBuilder("[", 1000);
            foreach (string table in tables)
            {
                sb.Append("{\"name\":");
                sb.AppendFormat("\"{0}\"", table);
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