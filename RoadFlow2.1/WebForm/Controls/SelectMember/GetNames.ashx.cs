using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Controls.SelectMember
{
    /// <summary>
    /// GetNames 的摘要说明
    /// </summary>
    public class GetNames : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string values = context.Request.QueryString["values"];
            context.Response.Write(new RoadFlow.Platform.Organize().GetNames(values));
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