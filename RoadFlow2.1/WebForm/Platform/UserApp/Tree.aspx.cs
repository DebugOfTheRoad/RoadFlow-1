using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class Tree : Common.BasePage
    {
        protected string RoleOptions = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Request.QueryString["id"];
            RoadFlow.Platform.Role brole = new RoadFlow.Platform.Role();
            var roles = new RoadFlow.Platform.UsersRole().GetByUserID(userid.ToGuid());
            List<RoadFlow.Data.Model.Role> roleList = new List<RoadFlow.Data.Model.Role>();
            foreach (var role in roles)
            {
                var role1 = brole.Get(role.RoleID);
                if (role1 == null)
                {
                    continue;
                }
                roleList.Add(role1);
            }

            RoleOptions = new RoadFlow.Platform.Role().GetRoleOptions(Request.QueryString["roleid"], "", roleList);

        }
    }
}