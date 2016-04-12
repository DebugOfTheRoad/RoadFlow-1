using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    /// <summary>
    /// Save 的摘要说明
    /// </summary>
    public class Save : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string html = context.Request["html"];
            string name = context.Request["name"];
            string att = context.Request["att"];
            string id = context.Request["id"];
            string type = context.Request["type"];
            string subtable = context.Request["subtable"];
            string formEvents = context.Request["formEvents"];
            if (name.IsNullOrEmpty())
            {
                context.Response.Write("表单名称不能为空!");
                return;
            }

            Guid formID;
            if (!id.IsGuid(out formID))
            {
                context.Response.Write("表单ID无效!");
                return;
            }

            RoadFlow.Platform.WorkFlowForm WFF = new RoadFlow.Platform.WorkFlowForm();
            RoadFlow.Data.Model.WorkFlowForm wff = WFF.Get(formID);
            bool isAdd = false;
            string oldXML = string.Empty;
            if (wff == null)
            {
                wff = new RoadFlow.Data.Model.WorkFlowForm();
                wff.ID = formID;
                wff.CreateUserID = RoadFlow.Platform.Users.CurrentUserID;
                wff.CreateUserName = RoadFlow.Platform.Users.CurrentUserName;
                wff.CreateTime = RoadFlow.Utility.DateTimeNew.Now;
                wff.Status = 0;
                isAdd = true;
            }
            else
            {
                oldXML = wff.Serialize();
            }

            wff.Type = type.ToGuid();
            wff.Attribute = att;
            wff.Html = html;
            wff.LastModifyTime = RoadFlow.Utility.DateTimeNew.Now;
            wff.Name = name;
            wff.SubTableJson = subtable;
            wff.EventsJson = formEvents;

            if (isAdd)
            {
                WFF.Add(wff);
                RoadFlow.Platform.Log.Add("添加了流程表单", wff.Serialize(), RoadFlow.Platform.Log.Types.流程相关);
            }
            else
            {
                WFF.Update(wff);
                RoadFlow.Platform.Log.Add("修改了流程表单", "", RoadFlow.Platform.Log.Types.流程相关, oldXML, wff.Serialize());
            }
            context.Response.Write("保存成功!");
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