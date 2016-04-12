using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Log
{
    public partial class Detail : Common.BasePage
    {
        protected RoadFlow.Data.Model.Log log;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                log = new RoadFlow.Platform.Log().Get(id.ToGuid());
                if (log != null)
                {
                    this.Title1.Text = log.Title;
                    this.Type.Text = log.Type;
                    this.WriteTime.Text = log.WriteTime.ToDateTimeStringS();
                    this.UserName.Text = log.UserName;
                    this.IPAddress.Text = log.IPAddress;
                    this.URL.Text = log.URL;
                    this.Others.Text = log.Others;
                    this.Contents.Text = log.Contents.HtmlEncode();
                    this.OldXml.Text = log.OldXml.HtmlEncode();
                    this.NewXml.Text = log.NewXml.HtmlEncode();
                    if (log.Contents.IsNullOrEmpty())
                    {
                        this.contentstr.Visible = false;
                    }
                    if (log.OldXml.IsNullOrEmpty())
                    {
                        this.oldxmlstr.Visible = false;
                    }
                    if (log.NewXml.IsNullOrEmpty())
                    {
                        this.newxmlstr.Visible = false;
                    }
                }
            }
           
        }
    }
}