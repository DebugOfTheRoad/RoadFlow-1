using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Log
{
    /// <summary>
    /// GetData 的摘要说明
    /// </summary>
    public class GetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            long count=0;
            string title = context.Request.QueryString["title"];
            string type = context.Request.QueryString["type"];
            string date1 = context.Request.QueryString["date1"];
            string date2 = context.Request.QueryString["date2"];
            string userid = context.Request.QueryString["userid"];
            string size = context.Request.QueryString["pagesize"];
            string number = context.Request.QueryString["pagenumber"];
            RoadFlow.Platform.Log blog = new RoadFlow.Platform.Log();
            System.Data.DataTable dt = new System.Data.DataTable(); //blog.GetPagerData(out count, size.ToInt(), number.ToInt(), "", title, type, date1, date2, userid);
            string data = RoadFlow.Utility.Tools.DataTableToJsonString(dt);
            context.Response.Write("{\"count\":" + count.ToString() + ",\"data\":" + data + "}");
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