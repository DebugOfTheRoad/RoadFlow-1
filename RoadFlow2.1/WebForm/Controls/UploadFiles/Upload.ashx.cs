using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebForm.Controls.UploadFiles
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str1 = context.Request.Form["str1"];
            string str2 = context.Request.Form["str2"];
            string filetype = context.Request.Form["filetype"];

            var obj = RoadFlow.Cache.IO.Opation.Get(str1 ?? "");
            if (str1.IsNullOrEmpty() || str2.IsNullOrEmpty() || obj == null || obj.ToString() != str2)
            {
                context.Response.Write("您不能上传文件");
                return;
            }

            //接收上传后的文件
            HttpPostedFile file = context.Request.Files["Filedata"];

            if (filetype.IsNullOrEmpty())
            {
                if (!RoadFlow.Utility.Config.UploadFileType.Contains(Path.GetExtension(file.FileName).TrimStart('.'), StringComparison.CurrentCultureIgnoreCase))
                {
                    context.Response.Write("您上传的文件类型不被允许");
                    return;
                }
            }
            else
            {
                if (!filetype.Contains(Path.GetExtension(file.FileName).TrimStart('.'), StringComparison.CurrentCultureIgnoreCase))
                {
                    context.Response.Write("您上传的文件类型不被允许");
                    return;
                }
            }

            //获取文件的保存路径
            string uploadPath;
            string uploadFullPath = context.Server.MapPath(getFilePath(out uploadPath));

            //判断上传的文件是否为空
            if (file != null)
            {
                if (!Directory.Exists(uploadFullPath))
                {
                    Directory.CreateDirectory(uploadFullPath);
                }
                //保存文件
                string newFileName = getFileName(uploadFullPath, file.FileName);
                string newFileFullPath = uploadFullPath + newFileName;
                try
                {
                    int fileLength = file.ContentLength;
                    file.SaveAs(newFileFullPath);
                    context.Response.Write("1|" + (uploadPath + newFileName) + "|" + (fileLength / 1000).ToString("###,###") + "|" + newFileName);
                    return;
                }
                catch
                {
                    context.Response.Write("上传文件发生了错误");
                    return;
                }
            }
            else
            {
                context.Response.Write("上传文件为空");
                return;
            }
        }

        /// <summary>
        /// 得到上传文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string getFileName(string filePath, string fileName)
        {
            while (System.IO.File.Exists(filePath + fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + RoadFlow.Utility.Tools.GetRandomString() + Path.GetExtension(fileName);
            }
            return fileName;
        }

        /// <summary>
        /// 得到文件保存路径
        /// </summary>
        /// <returns></returns>
        private string getFilePath(out string path1)
        {
            DateTime date = RoadFlow.Utility.DateTimeNew.Now;
            path1 = WebForm.Common.Tools.BaseUrl + "/Files/UploadFiles/" + date.ToString("yyyyMM") + "/" + date.ToString("dd") + "/";
            return path1;
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