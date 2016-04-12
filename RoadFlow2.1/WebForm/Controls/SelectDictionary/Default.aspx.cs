using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebForm.Controls.SelectDictionary
{
    public partial class Default : Common.BasePage
    {
        protected string defaultValuesString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.Dictionary Dict = new RoadFlow.Platform.Dictionary();

            string values = Request.QueryString["values"];
            string rootid = Request.QueryString["rootid"];
            string datasource = Request.QueryString["datasource"];
            string sql = Request.QueryString["sql"];

            DataTable SqlDataTable = new DataTable();
            if ("1" == datasource)
            {
                string dbconn = Request.QueryString["dbconn"];
                RoadFlow.Platform.DBConnection conn = new RoadFlow.Platform.DBConnection();
                var conn1 = conn.Get(dbconn.ToGuid());
                SqlDataTable = conn.GetDataTable(conn1, sql.UrlDecode().ReplaceSelectSql());
            }
           
            System.Text.StringBuilder defautlSB = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                switch (datasource)
                {
                    case "0":
                    default:
                        Guid id;
                        if (!value.IsGuid(out id))
                        {
                            continue;
                        }
                        defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                        defautlSB.Append(Dict.GetTitle(id));
                        defautlSB.Append("</div>");
                        break;
                    case "1"://SQL
                        string title1 = string.Empty;
                        foreach (DataRow dr in SqlDataTable.Rows)
                        {
                            if (value == dr[0].ToString())
                            {
                                title1 = SqlDataTable.Columns.Count > 1 ? dr[1].ToString() : value;
                                break;
                            }
                        }
                        defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                        defautlSB.Append(title1);
                        defautlSB.Append("</div>");
                        break;
                    case "2"://url
                        string url2 = Request.QueryString["url2"];
                        if (!url2.IsNullOrEmpty())
                        {
                            url2 = url2.IndexOf('?') >= 0 ? url2 + "&values=" + value : url2 + "?values=" + value;
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            try
                            {
                                System.IO.TextWriter tw = new System.IO.StringWriter(sb);
                                Server.Execute(url2, tw);
                            }
                            catch (Exception err) { }
                            defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                            defautlSB.Append(sb.ToString());
                            defautlSB.Append("</div>");
                        }
                        break;
                    case "3"://table
                        string dbconn = Request.QueryString["dbconn"];
                        string dbtable = Request.QueryString["dbtable"];
                        string valuefield = Request.QueryString["valuefield"];
                        string titlefield = Request.QueryString["titlefield"];
                        string parentfield = Request.QueryString["parentfield"];
                        string where = Request.QueryString["where"];
                        RoadFlow.Platform.DBConnection bdbconn = new RoadFlow.Platform.DBConnection();
                        var conn = bdbconn.Get(dbconn.ToGuid());
                        string sql2 = "select " + titlefield + " from " + dbtable + " where " + valuefield + "='" + value + "'";
                        DataTable dt = bdbconn.GetDataTable(conn, sql2.ReplaceSelectSql());
                        string title3 = string.Empty;
                        if (dt.Rows.Count > 0)
                        {
                            title3 = dt.Rows[0][0].ToString();
                        }
                        defautlSB.AppendFormat("<div onclick=\"currentDel=this;showinfo('{0}');\" class=\"selectorDiv\" ondblclick=\"currentDel=this;del();\" value=\"{0}\">", value);
                        defautlSB.Append(title3);
                        defautlSB.Append("</div>");
                        break;
                }
            }
            defaultValuesString = defautlSB.ToString();
        }

        protected override bool CheckApp()
        {
            return true;//base.CheckApp();
        }
    }
}