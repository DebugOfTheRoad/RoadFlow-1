using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebForm.Tests
{
    public partial class CustomFormSave : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string title = Request.Form["title1"];
            string contents = Request.Form["contents"];
            string instanceid = Request.QueryString["instanceid"];
            string sql = string.Empty;
            SqlParameter[] parameters = { };
            if (instanceid.IsInt())
            {
                sql = "update TempTest_CustomForm set Title=@Title,Contents=@Contents where ID=@ID";
                parameters = new SqlParameter[]{
                    new SqlParameter("@Title",title),
                    new SqlParameter("@Contents",contents),
                    new SqlParameter("@ID",instanceid)
                };
            }
            else
            {
                sql = "insert into TempTest_CustomForm(Title,Contents) values(@Title,@Contents);SELECT SCOPE_IDENTITY();";
                parameters = new SqlParameter[]{
                    new SqlParameter("@Title",title),
                    new SqlParameter("@Contents",contents)
                };
            }
            
            using (SqlConnection conn = new SqlConnection(RoadFlow.Utility.Config.PlatformConnectionStringMSSQL))
            {
                
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        instanceid = obj.ToString();
                    }
                }
            }

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok",
                 "$('#instanceid',parent.document).val('" + instanceid + "');" +
                 "$('#customformtitle',parent.document).val('" + title + ")');parent.flowSaveIframe(true);",
                 true);
        }
    }
}