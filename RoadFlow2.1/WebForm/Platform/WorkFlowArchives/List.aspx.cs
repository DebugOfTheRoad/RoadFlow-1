using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowArchives
{
    public partial class List : Common.BasePage
    {
        protected System.Data.DataTable Dt = new System.Data.DataTable();
        protected string query1 = string.Empty;
        protected string appid = string.Empty;
        protected string tabid = string.Empty;
        protected string title = string.Empty;
        protected string typeid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            appid = Request.QueryString["appid"];
            tabid = Request.QueryString["tabid"];
            typeid = Request.QueryString["typeid"];
            title = string.Empty;

            RoadFlow.Platform.WorkFlowArchives BWFA = new RoadFlow.Platform.WorkFlowArchives();
            RoadFlow.Platform.WorkFlow BWF = new RoadFlow.Platform.WorkFlow();
            if (IsPostBack)
            {
                title = Request.Form["Title1"];
            }
            else
            {
                title = Request.QueryString["Title"];
            }

            string query = string.Format("&appid={0}&tabid={1}&Title={2}&typeid={3}",
                        Request.QueryString["appid"],
                        Request.QueryString["tabid"],
                        title.UrlEncode(), typeid
                        );
            query1 = string.Format("{0}&pagesize={1}&pagenumber={2}", query, Request.QueryString["pagesize"], Request.QueryString["pagenumber"]);
            string pager;
            Dt = BWFA.GetPagerData(out pager, query, title, BWF.GetFlowIDFromType(typeid.ToGuid()));
            this.Pager.Text = pager;
           
        }
    }
}