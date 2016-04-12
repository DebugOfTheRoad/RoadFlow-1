using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class BodyAdd : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.Organize borganize = new RoadFlow.Platform.Organize();
            RoadFlow.Data.Model.Organize org = null;
            string id = Request.QueryString["id"];
            string name = string.Empty;
            string type = string.Empty;
            string status = string.Empty;
            string note = string.Empty;

            Guid orgID;
            if (id.IsGuid(out orgID))
            {
                org = borganize.Get(orgID);
            }

            if (IsPostBack && org != null)
            {
                name = Request.Form["Name"];
                type = Request.Form["Type"];
                status = Request.Form["Status"];
                note = Request.Form["note"];

                RoadFlow.Data.Model.Organize org1 = new RoadFlow.Data.Model.Organize();
                Guid org1ID = Guid.NewGuid();
                org1.ID = org1ID;
                org1.Name = name.Trim();
                org1.Note = note.IsNullOrEmpty() ? null : note.Trim();
                org1.Number = org.Number + "," + org1ID.ToString().ToLower();
                org1.ParentID = org.ID;
                org1.Sort = borganize.GetMaxSort(org.ID);
                org1.Status = status.IsInt() ? status.ToInt() : 0;
                org1.Type = type.ToInt();
                org1.Depth = org.Depth + 1;

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    borganize.Add(org1);
                    //更新父级[ChildsLength]字段
                    borganize.UpdateChildsLength(org.ID);
                    scope.Complete();
                }

                RoadFlow.Platform.Log.Add("添加了组织机构", org1.Serialize(), RoadFlow.Platform.Log.Types.组织机构);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');parent.frames[0].reLoad('" + id + "');window.location=window.location;", true);
            }
            
            this.TypeRadios.Text = borganize.GetTypeRadio("Type", type, "validate=\"radio\"");
            this.StatusRadios.Text = borganize.GetStatusRadio("Status", "0", "validate=\"radio\"");
        }
    }
}