using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Tests
{
    /// <summary>
    /// Combox_GetOptions 的摘要说明
    /// </summary>
    public class Combox_GetOptions : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string values=context.Request.QueryString["values"];
            string xx;
            var logs = new RoadFlow.Platform.Log().GetPagerData(out xx, "", "", "", "", "", "");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (System.Data.DataRow dr in logs.Rows)
            {
                string selected= (","+values+",").Contains(","+ dr["ID"].ToString()+",", StringComparison.CurrentCultureIgnoreCase)
                    ?" selected=\"selected\"":"";
                sb.AppendFormat("<option value=\"{0}\"{1}>{2}</option>", dr["ID"], selected, dr["Title"]);
            }
            context.Response.Write(sb.ToString());
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