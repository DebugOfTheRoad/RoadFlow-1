using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;

namespace WebForm.Platform.Members
{
    /// <summary>
    /// Tree1 的摘要说明
    /// </summary>
    public class Tree1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string rootid = context.Request.QueryString["rootid"] ?? "";
            string showtype = context.Request.QueryString["showtype"] ?? "";
            RoadFlow.Platform.Organize BOrganize = new RoadFlow.Platform.Organize();
            RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
            RoadFlow.Platform.WorkGroup BWorkGroup = new RoadFlow.Platform.WorkGroup();
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);

            if ("1" == showtype)
            {
                #region 显示工作组
                
                var workGroups = BWorkGroup.GetAll();
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", Guid.Empty);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                json.AppendFormat("\"title\":\"{0}\",", "工作组");
                json.AppendFormat("\"ico\":\"{0}\",", Common.Tools.BaseUrl + "/images/ico/group.gif");
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", 5);
                json.AppendFormat("\"hasChilds\":\"{0}\",", workGroups.Count);
                json.Append("\"childs\":[");

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
                json.Append("]");
                context.Response.Write(json.ToString());
                context.Response.End();
                #endregion
            }
            if (rootid.IsNullOrEmpty())
            {
                rootid = BOrganize.GetRoot().ID.ToString();
            }
            string[] rootIDArray = rootid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int m = 0;
            foreach (string rootID in rootIDArray)
            {
                List<RoadFlow.Data.Model.Users> users = new List<RoadFlow.Data.Model.Users>();
                Guid rootGuid = Guid.Empty;
                if (rootID.IsGuid(out rootGuid))
                {
                    var root = BOrganize.Get(rootGuid);
                    if (root != null)
                    {
                        users = busers.GetAllByOrganizeID(rootGuid);
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", root.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", root.ParentID);
                        json.AppendFormat("\"title\":\"{0}\",", root.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", rootIDArray.Length == 1 ? Common.Tools.BaseUrl + "/images/ico/icon_site.gif" : "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", root.Type);
                        json.AppendFormat("\"hasChilds\":\"{0}\",", root.ChildsLength == 0 && users.Count == 0 ? "0" : "1");
                        json.Append("\"childs\":[");
                    }
                }
                else if (rootID.StartsWith(RoadFlow.Platform.Users.PREFIX))
                {
                    var root = busers.Get(busers.RemovePrefix1(rootID).ToGuid());
                    if (root != null)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", root.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", root.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", Common.Tools.BaseUrl + "/images/ico/contact_grey.png");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", "4");
                        json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                        json.Append("\"childs\":[");
                    }
                }
                else if (rootID.StartsWith(RoadFlow.Platform.WorkGroup.PREFIX))
                {
                    var root = BWorkGroup.Get(BWorkGroup.RemovePrefix1(rootID).ToGuid());
                    if (root != null)
                    {
                        users = BOrganize.GetAllUsers(rootID);
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", root.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", root.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", "5");
                        json.AppendFormat("\"hasChilds\":\"{0}\",", users.Count > 0 ? "1" : "0");
                        json.Append("\"childs\":[");
                    }
                }

                #region 只有一个根时显示二级
                if (rootIDArray.Length == 1)
                {
                    List<RoadFlow.Data.Model.Organize> orgs = rootID.IsGuid() ? BOrganize.GetChilds(rootGuid) 
                        : new List<RoadFlow.Data.Model.Organize>(); 
                    int count = orgs.Count;
                    int i = 0;
                    foreach (var org in orgs)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", org.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", org.ParentID);
                        json.AppendFormat("\"title\":\"{0}\",", org.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", org.Type);
                        json.AppendFormat("\"hasChilds\":\"{0}\",", org.ChildsLength);
                        json.Append("\"childs\":[");
                        json.Append("]");
                        json.Append("}");
                        if (i++ < count - 1 || users.Count > 0)
                        {
                            json.Append(",");
                        }
                    }

                    if (users.Count > 0)
                    {
                        var userRelations = new RoadFlow.Platform.UsersRelation().GetAllByOrganizeID(rootGuid);
                        int count1 = users.Count;
                        int j = 0;
                        foreach (var user in users)
                        {
                            var ur = userRelations.Find(p => p.UserID == user.ID);
                            json.Append("{");
                            json.AppendFormat("\"id\":\"{0}\",", user.ID);
                            json.AppendFormat("\"parentID\":\"{0}\",", rootGuid);
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
                    }
                }
                #endregion

                json.Append("]");
                json.Append("}");
                if (m++ < rootIDArray.Length - 1)
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