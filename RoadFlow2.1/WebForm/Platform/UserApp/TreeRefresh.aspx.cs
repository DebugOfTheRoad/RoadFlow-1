using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class TreeRefresh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["refreshid"];
            string userID = Request.QueryString["userid"];
            Guid rid;
            if (!id.IsGuid(out rid))
            {
                Response.Write("[]");
                Response.End();
            }
            RoadFlow.Platform.RoleApp BRoleApp = new RoadFlow.Platform.RoleApp();
            RoadFlow.Platform.UsersApp BUsersApp = new RoadFlow.Platform.UsersApp();
            var childs = BRoleApp.GetChild(rid);

            //加载个人应用
            if (userID.IsGuid())
            {
                BUsersApp.AppendUserApps(userID.ToGuid(), rid, childs);
            }

            System.Text.StringBuilder json = new System.Text.StringBuilder("[", childs.Count * 50);
            int count = childs.Count;
            int i = 0;
            foreach (var child in childs.OrderBy(p => p.Sort))
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", child.ID.ToString());
                json.AppendFormat("\"title\":\"{0}\",", child.Title);
                json.AppendFormat("\"ico\":\"{0}\",", child.Ico);
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", child.Type);
                json.AppendFormat("\"model\":\"{0}\",", "");
                json.AppendFormat("\"width\":\"{0}\",", "");
                json.AppendFormat("\"height\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", BRoleApp.HasChild(child.ID) || BUsersApp.HasChild(child.ID) ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            Response.Write(json.ToString());
        }
    }
}