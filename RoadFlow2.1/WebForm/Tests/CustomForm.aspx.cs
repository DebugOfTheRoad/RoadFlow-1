using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Tests
{
    public partial class CustomForm : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["instanceid"];
            if (id.IsInt())
            {
                System.Data.DataTable dt = new RoadFlow.Data.MSSQL.DBHelper().GetDataTable("select * from TempTest_CustomForm where id=" + id);
                if (dt.Rows.Count > 0)
                {
                    this.title1.Value = dt.Rows[0]["title"].ToString();
                    this.contents.Value = dt.Rows[0]["contents"].ToString();
                }
            }
        }
    }
}