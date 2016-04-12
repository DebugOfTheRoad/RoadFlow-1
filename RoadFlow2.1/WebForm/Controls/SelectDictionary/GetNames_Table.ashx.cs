using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebForm.Controls.SelectDictionary
{
    /// <summary>
    /// GetNames_Table 的摘要说明
    /// </summary>
    public class GetNames_Table : IHttpHandler
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
            string values = context.Request.QueryString["values"] ?? "";

            RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
            var conn = bdbconn.Get(dbconn.ToGuid());
            System.Text.StringBuilder names = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                if (value.IsNullOrEmpty())
                {
                    continue;
                }
                string sql = "select " + titlefield + " from " + dbtable + " where " + valuefield + "='" + value + "'";
                DataTable dt = bdbconn.GetDataTable(conn, sql.ReplaceSelectSql());
                if (dt.Rows.Count > 0)
                {
                    names.Append(dt.Rows[0][0].ToString());
                    names.Append(",");
                }
            }
            context.Response.Write(names.ToString().TrimEnd(','));
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