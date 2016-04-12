using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Controls.SelectMember
{
    /// <summary>
    /// GetNote 的摘要说明
    /// </summary>
    public class GetNote : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.QueryString["id"];
            Guid gid;
            if (id.IsNullOrEmpty())
            {
                context.Response.Write("");
            }
            RoadFlow.Platform.Organize borg = new RoadFlow.Platform.Organize();
            RoadFlow.Platform.Users buser = new RoadFlow.Platform.Users();
            if (id.StartsWith(RoadFlow.Platform.Users.PREFIX))
            {
                Guid uid = buser.RemovePrefix1(id).ToGuid();
                context.Response.Write(string.Concat(borg.GetAllParentNames(buser.GetMainStation(uid)), " / ", buser.GetName(uid)));
            }
            else if (id.StartsWith(RoadFlow.Platform.WorkGroup.PREFIX))
            {
                context.Response.Write(new RoadFlow.Platform.WorkGroup().GetUsersNames(RoadFlow.Platform.WorkGroup.RemovePrefix(id).ToGuid(), '、'));
            }
            else if (id.IsGuid(out gid))
            {
                context.Response.Write(borg.GetAllParentNames(gid));
            }
            context.Response.Write("");
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