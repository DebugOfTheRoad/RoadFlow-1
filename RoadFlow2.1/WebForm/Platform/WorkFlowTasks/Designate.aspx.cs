using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowTasks
{
    public partial class Designate : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string taskid = Request.QueryString["taskid"];
                Guid taskID;
                if (taskid.IsGuid(out taskID))
                {
                    string user = Request.Form["user"];
                    string openerid = Request.QueryString["openerid"];

                    RoadFlow.Platform.WorkFlowTask btask = new RoadFlow.Platform.WorkFlowTask();
                    var users = new RoadFlow.Platform.Organize().GetAllUsers(user);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var user1 in users)
                    {
                        btask.DesignateTask(taskID, user1);
                        RoadFlow.Platform.Log.Add("管理员指派了流程任务", "将任务" + taskID + "指派给了：" + user1.Name + user1.ID, RoadFlow.Platform.Log.Types.流程相关);

                        sb.Append(user1.Name);
                        sb.Append(",");
                    }
                    string userNames = sb.ToString().TrimEnd(',');
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('已成功指派给：" + userNames + "!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();", true);
                }
            }
        }
    }
}