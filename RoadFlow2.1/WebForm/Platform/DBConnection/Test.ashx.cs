using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.DBConnection
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string connid = context.Request.QueryString["id"];
            Guid cid;
            if (!connid.IsGuid(out cid))
            {
                context.Response.Write("参数错误");
                return;
            }
            context.Response.Write(new RoadFlow.Platform.DBConnection().Test(cid));
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