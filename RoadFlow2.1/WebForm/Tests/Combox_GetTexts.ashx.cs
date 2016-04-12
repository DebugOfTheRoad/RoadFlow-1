using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Tests
{
    /// <summary>
    /// Combox_GetTexts 的摘要说明
    /// </summary>
    public class Combox_GetTexts : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string values = context.Request.QueryString["values"];
            System.Text.StringBuilder texts = new System.Text.StringBuilder();
            RoadFlow.Platform.Log Log = new RoadFlow.Platform.Log();
            foreach (string value in values.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (value.IsGuid())
                {
                    var log = Log.Get(value.ToGuid());
                    if (log != null)
                    {
                        texts.Append(log.Title);
                        texts.Append(",");
                    }
                }
            }
            context.Response.Write(texts.ToString().TrimEnd(','));
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