using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;

namespace WebForm.Controls.SelectIco
{
    /// <summary>
    /// File 的摘要说明
    /// </summary>
    public class File : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.ContentType = "text/xml";

            XElement rootElement = new XElement("Root");
            string Path = context.Request["Path"];
            string Path1 = Common.Tools.BaseUrl + Path;

            string showType = ",.jpg,.gif,.png,";

            if (!Directory.Exists(context.Server.MapPath(Path1)))
            {
                rootElement.Save(context.Response.Output);
                context.Response.End();
            }

            DirectoryInfo folder = new DirectoryInfo(context.Server.MapPath(Path1));

            XElement element;
            foreach (var item in folder.GetFiles().Where(p => (p.Attributes & FileAttributes.Hidden) == 0))
            {
                if (showType.IndexOf("," + item.Extension.ToLower() + ",") != -1)
                {
                    element = new XElement("Icon");
                    rootElement.Add(element);
                    element.SetAttributeValue("title", item.Name);
                    element.SetAttributeValue("path", Path1.Replace(@"\", "/") + "/" + item.Name);
                    element.SetAttributeValue("path1", Path.Replace(@"\", "/") + "/" + item.Name);
                }
            }

            rootElement.Save(context.Response.Output);
            context.Response.End(); 
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