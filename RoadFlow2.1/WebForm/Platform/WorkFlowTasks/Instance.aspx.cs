using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class Instance : Common.BasePage
    {
        protected string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            query = "appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];
        }
    }
}