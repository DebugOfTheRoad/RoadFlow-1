using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Controls.SelectDictionary
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
            string note = "";
            if (id.IsGuid(out gid))
            {
                var dict = new RoadFlow.Platform.Dictionary().Get(gid, true);
                if (dict != null)
                {
                    note = dict.Note;
                }
            }
            context.Response.Write(note);
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