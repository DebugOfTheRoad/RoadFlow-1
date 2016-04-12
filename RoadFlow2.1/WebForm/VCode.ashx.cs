using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm
{
    /// <summary>
    /// VCode 的摘要说明
    /// </summary>
    public class VCode : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string code;
            System.IO.MemoryStream ms = RoadFlow.Utility.Tools.GetValidateImg(out code, "~/Images/vcodebg.png");
            context.Session[RoadFlow.Utility.Keys.SessionKeys.ValidateCode.ToString()] = code;
            context.Response.ClearContent();
            context.Response.ContentType = "image/gif";
            context.Response.BinaryWrite(ms.ToArray());
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