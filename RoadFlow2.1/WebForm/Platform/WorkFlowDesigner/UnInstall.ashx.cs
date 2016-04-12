using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.WorkFlowDesigner
{
    /// <summary>
    /// UnInstall 的摘要说明
    /// </summary>
    public class UnInstall : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.Form["id"];
            string type = context.Request.Form["type"];

            RoadFlow.Platform.WorkFlow bworkFlow = new RoadFlow.Platform.WorkFlow();
            var flow = bworkFlow.Get(id.ToGuid());
            if (flow == null)
            {
                context.Response.Write("该流程还未保存!");
                context.Response.End();
            }
            else
            {
                if ("0" == type)
                {
                    flow.Status = 3;
                    bworkFlow.Update(flow);
                    bworkFlow.RefreshWrokFlowCache(flow.ID);
                    RoadFlow.Platform.Log.Add("卸载了流程", flow.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
                    context.Response.Write("1");
                    context.Response.End();
                }
                else if ("1" == type)
                {
                    flow.Status = 4;
                    bworkFlow.Update(flow);
                    //bworkFlow.ClearWorkFlowCache(flow.ID);
                    RoadFlow.Platform.AppLibrary APP = new RoadFlow.Platform.AppLibrary();
                    var app = APP.GetByCode(flow.ID.ToString());
                    if (app != null)
                    {
                        APP.Delete(app.ID);
                        new RoadFlow.Platform.RoleApp().DeleteByAppID(app.ID);
                    }
                    RoadFlow.Platform.Log.Add("删除了流程", flow.Serialize(), RoadFlow.Platform.Log.Types.流程相关);

                    context.Response.Write("1");
                    context.Response.End();
                }
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