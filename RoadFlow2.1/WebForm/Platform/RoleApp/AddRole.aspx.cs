using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.RoleApp
{
    public partial class AddRole : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string name = Request.Form["Name"];
                string note = Request.Form["Note"];
                string useMember = Request.Form["UseMember"];
                RoadFlow.Data.Model.Role role = new RoadFlow.Data.Model.Role();
                using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
                {
                    role.ID = Guid.NewGuid();
                    role.Name = name.Trim();
                    if (!useMember.IsNullOrEmpty())
                    {
                        role.UseMember = useMember;
                        RoadFlow.Platform.UsersRole busersRole = new RoadFlow.Platform.UsersRole();
                        var users = new RoadFlow.Platform.Organize().GetAllUsers(useMember);
                        foreach (var user in users)
                        {
                            RoadFlow.Data.Model.UsersRole ur = new RoadFlow.Data.Model.UsersRole();
                            ur.IsDefault = true;
                            ur.MemberID = user.ID;
                            ur.RoleID = role.ID;
                            busersRole.Add(ur);
                        }
                    }
                    if (!note.IsNullOrEmpty())
                    {
                        role.Note = note.Trim();
                    }
                    new RoadFlow.Platform.Role().Add(role);

                    //添加一个根应用
                    RoadFlow.Data.Model.RoleApp roleApp = new RoadFlow.Data.Model.RoleApp();
                    roleApp.ID = Guid.NewGuid();
                    roleApp.ParentID = Guid.Empty;
                    roleApp.RoleID = role.ID;
                    roleApp.Sort = 1;
                    roleApp.Title = "管理目录";
                    new RoadFlow.Platform.RoleApp().Add(roleApp);
                    trans.Complete();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }

            }
        }
    }
}