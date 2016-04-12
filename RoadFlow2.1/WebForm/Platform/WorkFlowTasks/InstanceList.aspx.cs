using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class InstanceList : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected IEnumerable<RoadFlow.Data.Model.WorkFlowTask> taskList;
        protected string query = string.Empty;
        protected void Page_Load(object sender1, EventArgs e)
        {
            string title = "";
            string flowid = "";
            string sender = "";
            string date1 = "";
            string date2 = "";
            string status = "";
            string typeid = Request.QueryString["typeid"];

            if (IsPostBack)
            {
                title = Request.Form["Title1"];
                flowid = Request.Form["FlowID"];
                sender = Request.Form["SenderID"];
                date1 = Request.Form["Date1"];
                date2 = Request.Form["Date2"];
                status = Request.Form["Status"];
            }
            else
            {
                title = Request.QueryString["Title"];
                flowid = Request.QueryString["FlowID"];
                sender = Request.QueryString["SenderID"];
                date1 = Request.QueryString["Date1"];
                date2 = Request.QueryString["Date2"];
                status = Request.QueryString["Status"];
            }

            string query1 = string.Format("&appid={0}&tabid={1}&title={2}&flowid={3}&sender={4}&date1={5}&date2={6}&status={7}&typeid={8}",
                Request.QueryString["appid"], Request.QueryString["tabid"], title.UrlEncode(), flowid, sender, date1, date2, status, typeid);

            query = string.Format("{0}&pagesize={1}&pagenumber={2}", query1, Request.QueryString["pagesize"], Request.QueryString["pagenumber"]);

            string pager;

            List<System.Web.UI.WebControls.ListItem> statusItems = new List<System.Web.UI.WebControls.ListItem>();
            statusItems.Add(new System.Web.UI.WebControls.ListItem() { Text = "==全部==", Value = "0", Selected = "0" == status });
            statusItems.Add(new System.Web.UI.WebControls.ListItem() { Text = "未完成", Value = "1", Selected = "1" == status });
            statusItems.Add(new System.Web.UI.WebControls.ListItem() { Text = "已完成", Value = "2", Selected = "2" == status });
            this.Status.Items.AddRange(statusItems.ToArray());


            //可管理的流程ID数组
            var flows = bworkFlow.GetInstanceManageFlowIDList(RoadFlow.Platform.Users.CurrentUserID, typeid);
            List<Guid> flowids = new List<Guid>();
            foreach (var flow in flows.OrderBy(p => p.Value))
            {
                flowids.Add(flow.Key);
            }
            Guid[] manageFlows = flowids.ToArray();

            this.FlowOptions.Text = bworkFlow.GetOptions(flows, typeid, flowid);

            taskList = bworkFlowTask.GetInstances(manageFlows, new Guid[] { },
                sender.IsNullOrEmpty() ? new Guid[] { } : new Guid[] { sender.Replace(RoadFlow.Platform.Users.PREFIX, "").ToGuid() },
                out pager, query1, title, flowid, date1, date2, status.ToInt());
            this.Pager.Text = pager;
        }
    }
}