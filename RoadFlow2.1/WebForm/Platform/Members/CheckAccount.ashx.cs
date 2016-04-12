using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Members
{
    /// <summary>
    /// CheckAccount 的摘要说明
    /// </summary>
    public class CheckAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string account = context.Request.Form["value"];
            string id = context.Request["id"];
            context.Response.Write(new RoadFlow.Platform.Users().HasAccount(account, id) ? "帐号已经被使用了" : "1");
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