using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Members
{
    /// <summary>
    /// TreeRefresh 的摘要说明
    /// </summary>
    public class TreeRefresh : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.QueryString["refreshid"] ?? "";
            string showtype = context.Request.QueryString["showtype"] ?? "";
            string type = context.Request.QueryString["type"] ?? "";

            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);

            if ("1" == showtype)
            {
                #region 显示工作组
                RoadFlow.Platform.WorkGroup BWorkGroup = new RoadFlow.Platform.WorkGroup();
                var workGroups = BWorkGroup.GetAll();

                int countwg = workGroups.Count;
                int iwg = 0;
                foreach (var wg in workGroups)
                {
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", wg.ID);
                    json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"title\":\"{0}\",", wg.Name);
                    json.AppendFormat("\"ico\":\"{0}\",", "");
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", 5);
                    json.AppendFormat("\"hasChilds\":\"{0}\",", 0);
                    json.Append("\"childs\":[");
                    json.Append("]");
                    json.Append("}");
                    if (iwg++ < countwg - 1)
                    {
                        json.Append(",");
                    }
                }

                json.Append("]");
                json.Append("}");
                context.Response.Write(json.ToString());
                context.Response.End();
                #endregion
            }

            Guid orgID;
            if (!id.IsGuid(out orgID))
            {
                json.Append("]");
                context.Response.Write(json.ToString());
            }

            RoadFlow.Platform.Organize BOrganize = new RoadFlow.Platform.Organize();
            var childOrgs = BOrganize.GetChilds(orgID);

            int count = childOrgs.Count;
            int i = 0;
            foreach (var org in childOrgs)
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", org.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", id);
                json.AppendFormat("\"title\":\"{0}\",", org.Name);
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", org.Type);
                json.AppendFormat("\"hasChilds\":\"{0}\",", org.ChildsLength);
                json.Append("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }

            var userRelations = new RoadFlow.Platform.UsersRelation().GetAllByOrganizeID(orgID);
            var users = "5" == type ? BOrganize.GetAllUsers(RoadFlow.Platform.WorkGroup.PREFIX + id) 
                : new RoadFlow.Platform.Users().GetAllByOrganizeID(orgID);
            int count1 = users.Count;
            if (count1 > 0 && count > 0)
            {
                json.Append(",");
            }
            int j = 0;
            foreach (var user in users)
            {
                var ur = userRelations.Find(p => p.UserID == user.ID);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", user.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", id);
                json.AppendFormat("\"title\":\"{0}{1}\",", user.Name, ur != null && ur.IsMain == 0 ? "<span style='color:#999;'>[兼职]</span>" : "");
                json.AppendFormat("\"ico\":\"{0}\",", Common.Tools.BaseUrl + "/images/ico/contact_grey.png");
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", "4");
                json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                json.Append("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (j++ < count1 - 1)
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