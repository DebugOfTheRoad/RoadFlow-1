using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Dictionary
{
    /// <summary>
    /// Tree1 的摘要说明
    /// </summary>
    public class Tree1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            RoadFlow.Platform.Dictionary BDict = new RoadFlow.Platform.Dictionary();

            string rootid = context.Request.QueryString["root"];
            bool ischild = "1" == context.Request.QueryString["ischild"];//是否要加载下级节点

            Guid rootID = Guid.Empty;
            if (!rootid.IsGuid(out rootID))
            {
                if (!rootid.IsGuid(out rootID))
                {
                    var dict = BDict.GetByCode(rootid);
                    if (dict != null)
                    {
                        rootID = dict.ID;
                    }
                }
            }

            var root = rootID != Guid.Empty ? BDict.Get(rootID) : BDict.GetRoot();
            var rootHasChild = BDict.HasChilds(root.ID);
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);
            json.Append("{");
            json.AppendFormat("\"id\":\"{0}\",", root.ID);
            json.AppendFormat("\"parentID\":\"{0}\",", root.ParentID);
            json.AppendFormat("\"title\":\"{0}\",", root.Title);
            json.AppendFormat("\"type\":\"{0}\",", rootHasChild ? "0" : "2"); //类型：0根 1父 2子
            json.AppendFormat("\"ico\":\"{0}\",", Common.Tools.BaseUrl + "/images/ico/role.gif");
            json.AppendFormat("\"hasChilds\":\"{0}\",", rootHasChild ? "1" : "0");
            json.Append("\"childs\":[");
           
            var childs = BDict.GetChilds(root.ID);
            int i = 0;
            int count = childs.Count;
            foreach (var child in childs)
            {
                var hasChild = ischild && BDict.HasChilds(child.ID);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", child.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", child.ParentID);
                json.AppendFormat("\"title\":\"{0}\",", child.Title);
                json.AppendFormat("\"type\":\"{0}\",", hasChild ? "1" : "2");
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", hasChild ? "1" : "0");
                json.Append("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }
           
            json.Append("]");
            json.Append("}");
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