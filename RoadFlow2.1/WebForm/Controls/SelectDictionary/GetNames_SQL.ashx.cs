using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace WebForm.Controls.SelectDictionary
{
    /// <summary>
    /// GetNames_SQL 的摘要说明
    /// </summary>
    public class GetNames_SQL : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dbconn = context.Request.QueryString["dbconn"];
            string sql = context.Request.QueryString["sql"];
            RoadFlow.Platform.DBConnection conn = new RoadFlow.Platform.DBConnection();
            var conn1 = conn.Get(dbconn.ToGuid());
            DataTable dt = conn.GetDataTable(conn1, sql.UrlDecode().ReplaceSelectSql());

            string values = context.Request.QueryString["values"] ?? "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                string value1 = string.Empty;
                string title1 = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    value1 = dr[0].ToString();
                    if (value == value1)
                    {
                        title1 = dt.Columns.Count > 1 ? dr[1].ToString() : value1;
                        break;
                    }
                }
                sb.Append(title1);
                sb.Append(',');
            }
            context.Response.Write(sb.ToString().TrimEnd(','));
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