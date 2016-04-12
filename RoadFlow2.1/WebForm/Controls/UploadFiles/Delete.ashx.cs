using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;

namespace WebForm.Controls.UploadFiles
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler,IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str1 = context.Request.QueryString["str1"];
            string str2 = context.Request.QueryString["str2"];
            var obj = RoadFlow.Cache.IO.Opation.Get(str1 ?? "");
            if (str1.IsNullOrEmpty() || str2.IsNullOrEmpty() || obj == null || obj.ToString() != str2)
            {
                context.Response.Write("var json = {\"success\":0,\"message\":\"您不能删除文件\"}");
                return;
            }
            string file = context.Request.QueryString["file"];
            if (!file.IsNullOrEmpty())
            {
                try
                {
                    System.IO.File.Delete(context.Server.MapPath(Path.Combine("/Files/UploadFiles/", file)));
                    context.Response.Write("var json = {\"success\":1,\"message\":\"\"}");
                }
                catch (Exception e)
                {
                    context.Response.Write("var json = {\"success\":0,\"message\":\"" + e.Message + "\"}");
                }
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