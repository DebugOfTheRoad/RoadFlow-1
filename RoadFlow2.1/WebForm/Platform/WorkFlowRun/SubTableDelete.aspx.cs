using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.WorkFlowRun
{
    public partial class SubTableDelete : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string secondtableconnid = Request.QueryString["secondtableconnid"];
            string secondtable = Request.QueryString["secondtable"];
            string secondtableprimarykey = Request.QueryString["secondtableprimarykey"];
            string secondtablepkvalue = Request.QueryString["secondtablepkvalue"];
            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();

            int i = bdbconn.DeleteData(secondtableconnid.ToGuid(), secondtable, secondtableprimarykey, secondtablepkvalue);
            if (i > 0)
            {
                Response.Write("删除成功!");
            }
            else
            {
                Response.Write("删除失败!");
            }
        }
    }
}