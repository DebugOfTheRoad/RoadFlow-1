using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Platform.WorkFlowDesigner
{
    /// <summary>
    /// TestLineSqlWhere 的摘要说明
    /// </summary>
    public class TestLineSqlWhere : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string connid = context.Request["connid"];
            string table = context.Request["table"];
            string tablepk = context.Request["tablepk"];
            string where = context.Request["where"];

            RoadFlow.Platform.DBConnection dbconn = new RoadFlow.Platform.DBConnection();

            if (!connid.IsGuid())
            {
                context.Response.Write("流程未设置数据连接!");
                return;
            }
            var conn = dbconn.Get(connid.ToGuid());
            if (conn == null)
            {
                context.Response.Write("未找到连接!");
                return;
            }
            string sql = "SELECT * FROM " + table + " WHERE 1=1 AND " + where;
            if (dbconn.TestSql(conn, sql))
            {
                context.Response.Write("SQL条件正确!");
                return;
            }
            else
            {
                context.Response.Write("SQL条件错误!");
                return;
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