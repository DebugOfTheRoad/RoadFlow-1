using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class WaitList : Common.BasePage
    {
        protected string query = string.Empty;
        protected RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected IEnumerable<RoadFlow.Data.Model.WorkFlowTask> taskList;
        protected void Page_Load(object sender1, EventArgs e)
        {
            string title = "";
            string flowid = "";
            string sender = "";
            string date1 = "";
            string date2 = "";

            if (IsPostBack)
            {
                title = Request.Form["Title1"];
                flowid = Request.Form["FlowID"];
                sender = Request.Form["SenderID"];
                date1 = Request.Form["Date1"];
                date2 = Request.Form["Date2"];
            }
            else
            {
                title = Request.QueryString["title"];
                flowid = Request.QueryString["flowid"];
                sender = Request.QueryString["sender"];
                date1 = Request.QueryString["date1"];
                date2 = Request.QueryString["date2"];
            }
            
            query = string.Format("&appid={0}&tabid={1}&title={2}&flowid={3}&sender={4}&date1={5}&date2={6}",
                Request.QueryString["appid"], Request.QueryString["tabid"], title.UrlEncode(), flowid, sender, date1, date2);
            string pager;
            taskList = bworkFlowTask.GetTasks(RoadFlow.Platform.Users.CurrentUserID,
               out pager, query, title, flowid, sender, date1, date2);

            this.flowOptions.Text = bworkFlow.GetOptions(flowid);
            this.Pager.Text = pager;
        }
    }
}