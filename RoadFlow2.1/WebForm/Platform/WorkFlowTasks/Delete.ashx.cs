using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.WorkFlowTasks
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string flowid = context.Request.QueryString["flowid1"];
            string groupid = context.Request.QueryString["groupid"];

            Guid fid, gid;
            if (flowid.IsGuid(out fid) && groupid.IsGuid(out gid))
            {
                System.Text.StringBuilder delxml = new System.Text.StringBuilder();
                var tasks = new RoadFlow.Platform.WorkFlowTask().GetTaskList(fid, gid);
                foreach (var task in tasks)
                {
                    delxml.Append(task.Serialize());
                }
                new RoadFlow.Platform.WorkFlowTask().DeleteInstance(fid, gid);
                RoadFlow.Platform.Log.Add("管理员删除了流程实例", delxml.ToString(), RoadFlow.Platform.Log.Types.流程相关);
                context.Response.Write("删除成功!");
            }
            else
            {
                context.Response.Write("参数错误!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}