using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Dictionary
{
    /// <summary>
    /// CheckCode 的摘要说明
    /// </summary>
    public class CheckCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string code = context.Request.Form["value"];
            string id = context.Request["id"];
            context.Response.Write(new RoadFlow.Platform.Dictionary().HasCode(code, id) ? "唯一代码重复" : "1");
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