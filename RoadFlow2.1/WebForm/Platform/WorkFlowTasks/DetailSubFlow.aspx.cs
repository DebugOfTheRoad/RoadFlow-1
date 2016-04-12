using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class DetailSubFlow : Common.BasePage
    {
        protected RoadFlow.Platform.WorkFlowTask bworkFlowTask = new RoadFlow.Platform.WorkFlowTask();
        protected RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
        protected string query = string.Empty;
        protected string flowid = string.Empty;
        protected string groupid = string.Empty;
        protected string displayModel = string.Empty;
        protected string taskid = string.Empty;
        protected RoadFlow.Data.Model.WorkFlowInstalled wfInstall = null;
        protected IOrderedEnumerable<RoadFlow.Data.Model.WorkFlowTask> tasks;
        protected void Page_Load(object sender, EventArgs e)
        {
            query = string.Format("&flowid1={0}&groupid={1}&appid={2}&tabid={3}&title={4}&flowid={5}&sender={6}&date1={7}&date2={8}&iframeid={9}&openerid={10}&taskid={11}",
                Request.QueryString["flowid"],
                Request.QueryString["groupid"],
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                Request.QueryString["title"].UrlEncode(),
                Request.QueryString["flowid"],
                Request.QueryString["sender"],
                Request.QueryString["date1"],
                Request.QueryString["date2"],
                Request.QueryString["iframeid"],
                Request.QueryString["openerid"],
                Request.QueryString["taskid"]
                );
            flowid = Request.QueryString["flowid"];
            groupid = Request.QueryString["groupid"];
            taskid = Request.QueryString["taskid"];
            displayModel = Request.QueryString["displaymodel"];
            if (!taskid.IsGuid())
            {
                return;
            }
            var task = bworkFlowTask.Get(taskid.ToGuid());

            if (task == null || !task.SubFlowGroupID.HasValue)
            {
                return;
            }
            var subFlowTasks = bworkFlowTask.GetTaskList(Guid.Empty, task.SubFlowGroupID.Value);
            if (subFlowTasks.Count == 0)
            {
                return;
            }

            wfInstall = bworkFlow.GetWorkFlowRunModel(subFlowTasks.First().FlowID);
            tasks = subFlowTasks.OrderBy(p => p.Sort).ThenBy(p => p.ReceiveTime);
            flowid = subFlowTasks.First().FlowID.ToString();
        }
    }
}