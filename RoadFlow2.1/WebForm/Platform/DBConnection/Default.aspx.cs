using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.DBConnection
{
    public partial class Default : Common.BasePage
    {
        protected string Query1 = string.Empty;
        protected List<RoadFlow.Data.Model.DBConnection> ConnList = new List<RoadFlow.Data.Model.DBConnection>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string query1 = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();

            if (IsPostBack)
            {
                if (!Request.Form["DeleteBut"].IsNullOrEmpty())
                {
                    string deleteID = Request.Form["checkbox_app"];
                    System.Text.StringBuilder delxml = new System.Text.StringBuilder();
                    foreach (string id in deleteID.Split(','))
                    {
                        Guid gid;
                        if (id.IsGuid(out gid))
                        {
                            delxml.Append(bdbconn.Get(gid).Serialize());
                            bdbconn.Delete(gid);
                        }
                    }
                    bdbconn.ClearCache();
                    RoadFlow.Platform.Log.Add("删除了数据连接", delxml.ToString(), RoadFlow.Platform.Log.Types.流程相关);
                }
            }

            ConnList = bdbconn.GetAll();
            Query1 = query1;
        }
    }
}