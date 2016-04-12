using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;

namespace WebForm.Controls.SelectDictionary
{
    /// <summary>
    /// GetJson_TableRefresh 的摘要说明
    /// </summary>
    public class GetJson_TableRefresh : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dbconn = context.Request.QueryString["dbconn"];
            string dbtable = context.Request.QueryString["dbtable"];
            string valuefield = context.Request.QueryString["valuefield"];
            string titlefield = context.Request.QueryString["titlefield"];
            string parentfield = context.Request.QueryString["parentfield"];
            string where = context.Request.QueryString["where"];
            string id = context.Request.QueryString["refreshid"];

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var conn = bdbconn.Get(dbconn.ToGuid());
            string sql = "select " + valuefield + "," + titlefield + " from " + dbtable + " where " + parentfield + "='" + id + "'";
            DataTable dt = bdbconn.GetDataTable(conn, sql.ReplaceSelectSql());
            System.Text.StringBuilder json = new System.Text.StringBuilder(1000);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string value = dr[0].ToString();
                string title = dt.Columns.Count > 1 ? dr[1].ToString() : value;
                string sql1 = "select * from " + dbtable + " where " + parentfield + "='" + value + "'";
                bool hasChilds = bdbconn.GetDataTable(conn, sql1.ReplaceSelectSql()).Rows.Count > 0;
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", value);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty.ToString());
                json.AppendFormat("\"title\":\"{0}\",", title);
                json.AppendFormat("\"type\":\"{0}\",", hasChilds ? "1" : "2"); //类型：0根 1父 2子
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", hasChilds ? "1" : "0");
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