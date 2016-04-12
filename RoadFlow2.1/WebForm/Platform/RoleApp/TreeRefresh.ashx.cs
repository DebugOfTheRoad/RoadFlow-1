using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.RoleApp
{
    /// <summary>
    /// TreeRefresh 的摘要说明
    /// </summary>
    public class TreeRefresh : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["refreshid"];
            if (!id.IsGuid())
            {
                context.Response.Write("[]");
                return;
            }
            RoadFlow.Platform.RoleApp BRoleApp = new RoadFlow.Platform.RoleApp();
            var childs = BRoleApp.GetChild(id.ToGuid());
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", childs.Count * 50);
            int count = childs.Count;
            int i = 0;
            foreach (var child in childs)
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", child.ID);
                json.AppendFormat("\"title\":\"{0}\",", child.Title);
                json.AppendFormat("\"ico\":\"{0}\",", child.Ico.IsNullOrEmpty() ? "" : Common.Tools.BaseUrl + child.Ico);
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", "0");
                json.AppendFormat("\"model\":\"{0}\",", "");
                json.AppendFormat("\"width\":\"{0}\",", "");
                json.AppendFormat("\"height\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", BRoleApp.HasChild(child.ID) ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            context.Response.Write(json.ToString());
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