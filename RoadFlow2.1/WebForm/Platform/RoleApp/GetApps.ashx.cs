using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.RoleApp
{
    /// <summary>
    /// GetApps 的摘要说明
    /// </summary>
    public class GetApps : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.Form["type"];
            string appid = context.Request.Form["value"];
            context.Response.Write(new RoadFlow.Platform.AppLibrary().GetAppsOptions(type.ToGuid(), appid));
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