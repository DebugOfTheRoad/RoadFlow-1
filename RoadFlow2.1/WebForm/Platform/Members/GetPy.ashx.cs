using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Members
{
    /// <summary>
    /// GetPy 的摘要说明
    /// </summary>
    public class GetPy : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string name = context.Request.Form["name"];
            string account = name.ToChineseSpell();
            context.Response.Write(account.IsNullOrEmpty() ? "" : new RoadFlow.Platform.Users().GetAccount(account.Trim()));
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