using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class Body : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Data.Model.Organize org = null;
            RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                org = borganize.Get(id.ToGuid());
            }

            if (IsPostBack)
            {
                //保存
                if (!Request.Form["Save"].IsNullOrEmpty() && org != null)
                {
                    string name = Request.Form["Name"];
                    string type = Request.Form["Type"];
                    string status = Request.Form["Status"];
                    string chargeLeader = Request.Form["ChargeLeader"];
                    string leader = Request.Form["Leader"];
                    string note = Request.Form["note"];
                    string oldXML = org.Serialize();
                    org.Name = name.Trim();
                    org.Type = type.ToInt(1);
                    org.Status = status.ToInt(0);
                    org.ChargeLeader = chargeLeader;
                    org.Leader = leader;
                    org.Note = note.IsNullOrEmpty() ? null : note.Trim();

                    borganize.Update(org);
                    RoadFlow.Platform.Log.Add("修改了组织机构", "", RoadFlow.Platform.Log.Types.组织机构, oldXML, org.Serialize());
                    string rid = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('保存成功!');parent.frames[0].reLoad('" + rid + "');", true);
                }

                //移动
                if (!Request.Form["Move1"].IsNullOrEmpty() && org != null)
                {
                    string toOrgID = Request.Form["deptmove"];
                    Guid toID;
                    if (toOrgID.IsGuid(out toID) && borganize.Move(org.ID, toID))
                    {
                        RoadFlow.Platform.Log.Add("移动了组织机构", "将机构：" + org.ID + "移动到了：" + toID, RoadFlow.Platform.Log.Types.组织机构);
                        string refreshID = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('移动成功!');parent.frames[0].reLoad('" + refreshID + "');parent.frames[0].reLoad('" + toOrgID + "')", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('移动失败!');", true);
                    }
                }

                //删除
                if (!Request.Form["Delete"].IsNullOrEmpty())
                {
                    int i = borganize.DeleteAndAllChilds(org.ID);
                    RoadFlow.Platform.Log.Add("删除了组织机构及其所有下级共" + i.ToString() + "项", org.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
                    string refreshID = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('共删除了" + i.ToString() + "项!');parent.frames[0].reLoad('" + refreshID + "');", true);
                }
            }



            if (org != null)
            {
                this.Name.Value = org.Name;
                this.TypeRadios.Text = borganize.GetTypeRadio("Type", org.Type.ToString(), "validate=\"radio\"");
                this.StatusRadios.Text = borganize.GetStatusRadio("Status", org.Status.ToString(), "validate=\"radio\"");
                this.ChargeLeader.Value = org.ChargeLeader;
                this.Leader.Value = org.Leader;
                this.Note.Value = org.Note;

            }
            else
            {
                this.TypeRadios.Text = borganize.GetTypeRadio("Type", "", "validate=\"radio\"");
                this.StatusRadios.Text = borganize.GetStatusRadio("Status", "", "validate=\"radio\"");
            }

        }
    }
}