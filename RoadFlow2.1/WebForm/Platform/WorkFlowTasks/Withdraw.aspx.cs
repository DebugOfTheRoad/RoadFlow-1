using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class Withdraw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskid = Request.QueryString["taskid"];
            Guid tid;
            if (!taskid.IsGuid(out tid))
            {
                Response.Write("参数错误!");
                Response.End();
            }
            else if (new RoadFlow.Platform.WorkFlowTask().HasWithdraw(tid))
            {
                bool success = new RoadFlow.Platform.WorkFlowTask().WithdrawTask(tid);
                if (success)
                {
                    RoadFlow.Platform.Log.Add("收回了任务", "任务ID：" + taskid, RoadFlow.Platform.Log.Types.流程相关);
                    Response.Write("收回成功!");
                    Response.End();
                }
                else
                {
                    Response.Write("收回失败!");
                    Response.End();
                }
            }
            else
            {
                Response.Write("该任务不能收回!");
                Response.End();
            }
        }
    }
}