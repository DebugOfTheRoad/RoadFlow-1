using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.DBConnection
{
    public partial class Edit : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string editid = Request.QueryString["id"];
            RoadFlow.Platform.DBConnection bdbConn = new RoadFlow.Platform.DBConnection();
            RoadFlow.Data.Model.DBConnection dbconn = null;
            if (editid.IsGuid())
            {
                dbconn = bdbConn.Get(editid.ToGuid());
            }
            bool isAdd = !editid.IsGuid();
            string oldXML = string.Empty;
            if (dbconn == null)
            {
                dbconn = new RoadFlow.Data.Model.DBConnection();
                dbconn.ID = Guid.NewGuid();
            }
            else
            {
                oldXML = dbconn.Serialize();
            }

            if (IsPostBack)
            {
                string Name = Request.Form["Name"];
                string LinkType = Request.Form["LinkType"];
                string ConnStr = Request.Form["ConnStr"];
                string Note = Request.Form["Note"];
                dbconn.Name = Name.Trim();
                dbconn.Type = LinkType;
                dbconn.ConnectionString = ConnStr;
                dbconn.Note = Note;

                if (isAdd)
                {
                    bdbConn.Add(dbconn);
                    RoadFlow.Platform.Log.Add("添加了应用程序库", dbconn.Serialize(), RoadFlow.Platform.Log.Types.角色应用);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('添加成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }
                else
                {
                    bdbConn.Update(dbconn);
                    RoadFlow.Platform.Log.Add("修改了应用程序库", "", RoadFlow.Platform.Log.Types.角色应用, oldXML, dbconn.Serialize());
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('修改成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }
                bdbConn.ClearCache();
            }
            if (dbconn != null)
            {
                this.Name.Value = dbconn.Name;
                this.ConnStr.Value = dbconn.ConnectionString;
                this.Note.Value = dbconn.Note;
            }
            this.TypeOptions.Text = bdbConn.GetAllTypeOptions(dbconn.Type);


        }
    }
}