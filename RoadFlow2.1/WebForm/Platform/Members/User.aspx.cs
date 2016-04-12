using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class User : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
            RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
            RoadFlow.Platform.UsersRelation buserRelation = new RoadFlow.Platform.UsersRelation();
            RoadFlow.Data.Model.Users user = null;
            RoadFlow.Data.Model.Organize organize = null;
            string id = Request.QueryString["id"];
            string parentID = Request.QueryString["parentid"];
            
            string parentString = string.Empty;
            this.Account.Attributes.Add("validate_url", "CheckAccount.ashx?id=" + id);
            Guid userID, organizeID;
            if (id.IsGuid(out userID))
            {
                user = busers.Get(userID);
                if (user != null)
                {
                    //所在组织字符串
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    var userRelations = buserRelation.GetAllByUserID(user.ID).OrderByDescending(p => p.IsMain);
                    foreach (var userRelation in userRelations)
                    {
                        sb.Append("<div style='margin:3px 0;'>");
                        sb.Append(borganize.GetAllParentNames(userRelation.OrganizeID, true));
                        if (userRelation.IsMain == 0)
                        {
                            sb.Append("<span style='color:#999'> [兼职]</span>");
                        }
                        sb.Append("</div>");

                    }
                    this.ParentString.Text = sb.ToString();
                    var roles = new RoadFlow.Platform.UsersRole().GetByUserIDFromCache(userID);
                    RoadFlow.Platform.Role brole = new RoadFlow.Platform.Role();
                    System.Text.StringBuilder rolesb = new System.Text.StringBuilder();
                    foreach (var role in roles)
                    {
                        var role1 = brole.Get(role.RoleID);
                        if (role1 == null) continue;
                        rolesb.Append(role1.Name);
                        rolesb.Append("，");
                    }
                    this.RoleString.Text = rolesb.ToString().TrimEnd('，');
                }
            }
            if (parentID.IsGuid(out organizeID))
            {
                organize = borganize.Get(organizeID);
            }

            if (IsPostBack)
            {
                #region 保存
                if (!Request.Form["Save"].IsNullOrEmpty() && user != null)
                {
                    string name = Request.Form["Name"];
                    string account = Request.Form["Account"];
                    string status = Request.Form["Status"];
                    string note = Request.Form["Note"];

                    string oldXML = user.Serialize();

                    user.Name = name.Trim();
                    user.Account = account.Trim();
                    user.Status = status.ToInt(1);
                    user.Note = note.IsNullOrEmpty() ? null : note.Trim();

                    busers.Update(user);
                    RoadFlow.Platform.Log.Add("修改了用户", "", RoadFlow.Platform.Log.Types.组织机构, oldXML, user.Serialize());
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');parent.frames[0].reLoad('" + parentID + "');", true);
                }
                #endregion

                #region 删除用户
                if (!Request.Form["DeleteBut"].IsNullOrEmpty() && user != null && organize != null)
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        var urs = buserRelation.GetAllByUserID(user.ID);
                        busers.Delete(user.ID);

                        buserRelation.DeleteByUserID(user.ID);

                        new RoadFlow.Platform.UsersInfo().Delete(user.ID);
                        new RoadFlow.Platform.UsersRole().DeleteByUserID(user.ID);

                        //更新父级[ChildsLength]字段
                        foreach (var ur in urs)
                        {
                            borganize.UpdateChildsLength(ur.OrganizeID);
                        }

                        scope.Complete();
                    }

                    string refreshID = parentID;
                    string url = string.Empty;
                    var users = borganize.GetAllUsers(refreshID.ToGuid());
                    if (users.Count > 0)
                    {
                        url = "User.aspx?id=" + users.Last().ID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&parentid=" + parentID;
                    }
                    else
                    {
                        refreshID = organize.ParentID == Guid.Empty ? organize.ID.ToString() : organize.ParentID.ToString();
                        url = "Body.aspx?id=" + parentID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&parentid=" + organize.ParentID;
                    }
                    RoadFlow.Platform.Log.Add("删除了用户", user.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('删除成功');parent.frames[0].reLoad('" + refreshID + "');window.location='" + url + "'", true);
                    new RoadFlow.Platform.AppLibrary().ClearUseMemberCache();
                }
                #endregion

                #region 初始化密码
                if (!Request.Form["InitPass"].IsNullOrEmpty() && user != null)
                {
                    string initpass = busers.GetInitPassword();
                    busers.InitPassword(user.ID);
                    RoadFlow.Platform.Log.Add("初始化了用户密码", user.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('密码已初始化为：" + initpass + "');", true);
                }
                #endregion

                #region 调动
                if (!Request.Form["Move1"].IsNullOrEmpty() && user != null)
                {
                    string moveto = Request.Form["movetostation"];
                    string movetostationjz = Request.Form["movetostationjz"];
                    Guid moveToID;
                    if (moveto.IsGuid(out moveToID))
                    {
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            var us = buserRelation.GetAllByUserID(user.ID);
                            if ("1" != movetostationjz)
                            {
                                buserRelation.DeleteByUserID(user.ID);
                            }

                            RoadFlow.Data.Model.UsersRelation ur = new RoadFlow.Data.Model.UsersRelation();
                            ur.UserID = user.ID;
                            ur.OrganizeID = moveToID;
                            ur.IsMain = "1" == movetostationjz ? 0 : 1;
                            ur.Sort = buserRelation.GetMaxSort(moveToID);
                            buserRelation.Add(ur);

                            foreach (var u in us)
                            {
                                borganize.UpdateChildsLength(u.OrganizeID);
                            }

                            borganize.UpdateChildsLength(organizeID);
                            borganize.UpdateChildsLength(moveToID);

                            scope.Complete();
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('调动成功!');parent.frames[0].reLoad('" + parentID + "');parent.frames[0].reLoad('" + moveto + "')", true);
                        }

                        RoadFlow.Platform.Log.Add(("1" == movetostationjz ? "兼职" : "全职") + "调动了人员的岗位", "将人员调往岗位(" + moveto + ")", RoadFlow.Platform.Log.Types.组织机构);
                        new RoadFlow.Platform.AppLibrary().ClearUseMemberCache();
                    }
                }
                #endregion
            }
            if (user != null)
            {
                this.Name.Value = user.Name;
                this.Account.Value = user.Account;
                this.Note.Value = user.Note;
            }
            this.StatusRadios.Text = borganize.GetStatusRadio("Status", user != null ? user.Status.ToString() : "", "validate=\"radio\"");
        }
    }
}