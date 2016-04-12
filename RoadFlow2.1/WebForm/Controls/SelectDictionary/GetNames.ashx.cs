using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Controls.SelectDictionary
{
    /// <summary>
    /// GetNames 的摘要说明
    /// </summary>
    public class GetNames : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string values = context.Request.QueryString["values"] ?? "";
            RoadFlow.Platform.Dictionary Dict = new RoadFlow.Platform.Dictionary();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                var dict = Dict.Get(value.ToGuid(), true);
                if (dict != null)
                {
                    sb.Append(dict.Title);
                    sb.Append(',');
                }
            }
            context.Response.Write(sb.ToString().TrimEnd(','));
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