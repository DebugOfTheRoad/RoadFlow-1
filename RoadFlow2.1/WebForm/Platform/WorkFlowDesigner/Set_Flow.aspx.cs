using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowDesigner
{
    public partial class Set_Flow : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected RoadFlow.Platform.DBConnection bdbConn = new RoadFlow.Platform.DBConnection();
        protected string base_TypesOptions = string.Empty;
        protected string link_DBConnOptions = string.Empty;
        protected bool isAdd = false;
        protected string flowID = string.Empty;
        protected string defaultManager = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            base_TypesOptions = bworkFlow.GetTypeOptions();
            link_DBConnOptions = bdbConn.GetAllOptions();
            isAdd = "1" == Request.QueryString["isadd"];
            flowID = Request.QueryString["flowid"].IsGuid() && !isAdd ? Request.QueryString["flowid"] : Guid.NewGuid().ToString();
            defaultManager = RoadFlow.Platform.Users.PREFIX + RoadFlow.Platform.Users.CurrentUserID.ToString();
        }
    }
}