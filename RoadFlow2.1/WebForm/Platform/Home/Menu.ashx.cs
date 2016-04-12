using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.Home
{
    /// <summary>
    /// Menu 的摘要说明
    /// </summary>
    public class Menu : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string roleID = context.Request.QueryString["roleid"];
            string userID = context.Request.QueryString["userid"];
            Guid gid, uid;
            if (!roleID.IsGuid(out gid) || !userID.IsGuid(out uid))
            {
                context.Response.Write("[]");
            }
            else
            {
                context.Response.Write(new RoadFlow.Platform.RoleApp().GetRoleAppJsonString(gid, uid, Common.Tools.BaseUrl));
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