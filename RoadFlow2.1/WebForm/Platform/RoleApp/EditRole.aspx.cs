using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.RoleApp
{
    public partial class EditRole : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.Role brole = new RoadFlow.Platform.Role();
            RoadFlow.Data.Model.Role role = null;
            string roleID = Request.QueryString["roleid"];
            Guid roleGID;
            string name = string.Empty;
            string useMember = string.Empty;
            string note = string.Empty;

            if (roleID.IsGuid(out roleGID))
            {
                role = brole.Get(roleGID);
            }

            if (IsPostBack)
            {
                if (!Request.Form["Copy"].IsNullOrEmpty())
                {
                    string tpl = Request.Form["ToTpl"];
                    if (tpl.IsGuid())
                    {
                        new RoadFlow.Platform.RoleApp().CopyRoleApp(roleGID, tpl.ToGuid());
                        RoadFlow.Platform.Log.Add("复制了模板应用", "源：" + roleID + "复制给：" + tpl, RoadFlow.Platform.Log.Types.角色应用);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('复制成功!');", true);
                    }
                }

                if (!Request.Form["Save"].IsNullOrEmpty() && role != null)
                {
                    RoadFlow.Platform.UsersRole busersRole = new RoadFlow.Platform.UsersRole();
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        name = Request.Form["Name"];
                        useMember = Request.Form["UseMember"];
                        note = Request.Form["Note"];

                        role.Name = name.Trim();
                        role.Note = note.IsNullOrEmpty() ? null : note.Trim();
                        role.UseMember = useMember.IsNullOrEmpty() ? null : useMember;
                        brole.Update(role);
                        busersRole.DeleteByRoleID(role.ID);
                        if (!useMember.IsNullOrEmpty())
                        {
                            busersRole.DeleteByRoleID(role.ID);
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
                        scope.Complete();
                    }
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }

                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        brole.Delete(roleGID);
                        new RoadFlow.Platform.RoleApp().DeleteByRoleID(roleGID);
                        new RoadFlow.Platform.UsersRole().DeleteByRoleID(roleGID);
                        scope.Complete();
                    }
                    RoadFlow.Platform.Log.Add("删除的角色其及相关数据", roleID, RoadFlow.Platform.Log.Types.角色应用);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }
            }
            if (role != null)
            {
                this.Name.Value = role.Name;
                this.UseMember.Value = role.UseMember;
                this.Note.Value = role.Note;
            }
            this.RoleOptions.Text = brole.GetRoleOptions("", roleID);
        }
    }
}