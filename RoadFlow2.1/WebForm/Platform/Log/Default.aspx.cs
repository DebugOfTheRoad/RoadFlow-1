using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Log
{
    public partial class Default : Common.BasePage
    {
        protected string Query = string.Empty;
        protected System.Data.DataTable Dt = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = string.Empty;
            string type = string.Empty;
            string date1 = string.Empty;
            string date2 = string.Empty;
            string userid = string.Empty;
            if (!IsPostBack)
            {
                title = Request.QueryString["Title1"];
                type = Request.QueryString["Type"];
                date1 = Request.QueryString["Date1"];
                date2 = Request.QueryString["Date2"];
                userid = Request.QueryString["UserID"];
            }
            else
            {
                title = Request.Form["Title"];
                type = Request.Form["Type"];
                date1 = Request.Form["Date1"];
                date2 = Request.Form["Date2"];
                userid = Request.Form["UserID"];
            }

            RoadFlow.Platform.Log blog = new RoadFlow.Platform.Log();
            Query = string.Format("&appid={0}&tabid={1}&Title={2}&Type={3}&Date1={4}&Date2={5}&UserID={6}",
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                title.UrlEncode(),
                type.UrlEncode(),
                date1,
                date2,
                userid
                );
            string pager;
            Dt = blog.GetPagerData(out pager, Query, title, type, date1, date2, userid);
            this.TypeOptions.Text = blog.GetTypeOptions(type);
            this.Pager.Text = pager;
        }
    }
}