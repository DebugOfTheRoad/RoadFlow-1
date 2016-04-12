using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class Test : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(
                new RoadFlow.Platform.WorkFlowTask().StartFlow
                ("A6509C1B-F49F-47A6-829D-EC43B9210EB2".ToGuid(),
                new List<RoadFlow.Data.Model.Users>() { CurrentUser }, "xxxxxxxxxxxxxxxxxxxxxx", 
                "8542C297-39FE-4622-B537-FDC54F58C0E9"));
        }

        protected override bool CheckApp()
        {
            return true;//return base.CheckApp();
        }
        protected override bool CheckUrl(bool isEnd = true)
        {
            return true; //base.CheckUrl(isEnd);
        }
        protected override bool CheckLogin(bool isRedirect = true)
        {
            return true; //base.CheckLogin(isRedirect);
        }
    }
}