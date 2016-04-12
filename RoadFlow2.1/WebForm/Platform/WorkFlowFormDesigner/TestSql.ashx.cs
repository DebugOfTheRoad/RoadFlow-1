using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowFormDesigner
{
    /// <summary>
    /// TestSql 的摘要说明
    /// </summary>
    public class TestSql : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string sql = context.Request["sql"];
            string dbconn = context.Request["dbconn"];

            if (sql.IsNullOrEmpty() || !dbconn.IsGuid())
            {
                context.Response.Write("SQL语句为空或未设置数据连接");
                return;
            }

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var dbconn1 = bdbconn.Get(dbconn.ToGuid());
            if (bdbconn.TestSql(dbconn1, sql))
            {
                context.Response.Write("SQL语句测试正确");
            }
            else
            {
                context.Response.Write("SQL语句测试错误");
            }
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