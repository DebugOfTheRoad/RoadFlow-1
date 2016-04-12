using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebForm.Controls.SelectDictionary
{
    /// <summary>
    /// GetJson_SQL 的摘要说明
    /// </summary>
    public class GetJson_SQL : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!Common.Tools.CheckLogin(false))
            {
                context.Response.Write("{}");
                context.Response.End();
                return;
            }
            string dbconn = context.Request.QueryString["dbconn"];
            string sql = context.Request.QueryString["sql"];
            RoadFlow.Platform.DBConnection conn = new RoadFlow.Platform.DBConnection();
            var conn1 = conn.Get(dbconn.ToGuid());
            System.Data.DataTable dt = conn.GetDataTable(conn1, sql.UrlDecode().ReplaceSelectSql());
            System.Text.StringBuilder json = new System.Text.StringBuilder(1000);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string value = dr[0].ToString();
                string title = dt.Columns.Count > 1 ? dr[1].ToString() : value;
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", value);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty.ToString());
                json.AppendFormat("\"title\":\"{0}\",", title);
                json.AppendFormat("\"type\":\"{0}\",", "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                json.Append("\"childs\":[]},");
            }
            context.Response.Write("[" + json.ToString().TrimEnd(',') + "]");
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