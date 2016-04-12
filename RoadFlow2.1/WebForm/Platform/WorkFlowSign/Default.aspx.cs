using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowSign
{
    public partial class Default : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (FileUpload1.HasFile)
                {
                    var file = FileUpload1.PostedFile;
                    if (file.ContentLength > 0)
                    {
                        string extname = System.IO.Path.GetExtension(file.FileName);
                        if (extname.IsNullOrEmpty() || (extname.ToLower() != ".gif" && extname.ToLower() != ".jpg") && extname.ToLower() != ".png")
                        {
                            Response.Write("<script>alert('只能上传gif,jpg,png类型的图片文件!'); window.location = window.location;</script>");
                            Response.End();
                        }
                        string filename = string.Concat(Server.MapPath(WebForm.Common.Tools.BaseUrl + "/Files/UserSigns/"), RoadFlow.Platform.Users.CurrentUserID, ".gif");
                        FileUpload1.SaveAs(filename);
                        RoadFlow.Platform.Log.Add("修改了签名", filename, RoadFlow.Platform.Log.Types.流程相关);
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('上传成功!'); window.location = window.location;", true);
                    }
                }

                if (!Request.Form["reset"].IsNullOrEmpty())
                {
                    string filename = string.Concat(Server.MapPath(WebForm.Common.Tools.BaseUrl + "/Files/UserSigns/"), RoadFlow.Platform.Users.CurrentUserID, ".gif");
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                        RoadFlow.Platform.Log.Add("恢复了签名", filename, RoadFlow.Platform.Log.Types.流程相关);
                    }
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "alert('已恢复为默认签名!'); window.location = window.location;", true);
                }
            }
        }
    }
}