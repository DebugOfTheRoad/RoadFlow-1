﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.UserApp
{
    public partial class Tree1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string roleID = Request.QueryString["roleid"];
            Guid roleGuid;
            if (!roleID.IsGuid(out roleGuid))
            {
                Response.Write("[]");
                Response.End();
            }

            RoadFlow.Platform.RoleApp BRoleApp = new RoadFlow.Platform.RoleApp();
            var appDt = BRoleApp.GetAllDataTableByRoleID(roleGuid);
            if (appDt.Rows.Count == 0)
            {
                Response.Write("[]");
                Response.End();
            }

            var root = appDt.Select("ParentID='" + Guid.Empty.ToString() + "'");
            if (root.Length == 0)
            {
                Response.Write("[]");
                Response.End();
            }

            var apps = appDt.Select("ParentID='" + root[0]["ID"].ToString() + "'");
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);
            System.Data.DataRow rootDr = root[0];
            json.Append("{");
            json.AppendFormat("\"id\":\"{0}\",", rootDr["ID"]);
            json.AppendFormat("\"title\":\"{0}\",", rootDr["Title"]);
            json.AppendFormat("\"ico\":\"{0}\",", rootDr["Ico"]);
            json.AppendFormat("\"link\":\"{0}\",", rootDr["Address"]);
            json.AppendFormat("\"type\":\"{0}\",", "0");
            json.AppendFormat("\"model\":\"{0}\",", rootDr["OpenMode"]);
            json.AppendFormat("\"width\":\"{0}\",", rootDr["Width"]);
            json.AppendFormat("\"height\":\"{0}\",", rootDr["Height"]);
            json.AppendFormat("\"hasChilds\":\"{0}\",", apps.Length > 0 ? "1" : "0");
            json.AppendFormat("\"childs\":[");

            for (int i = 0; i < apps.Length; i++)
            {
                System.Data.DataRow dr = apps[i];
                var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", dr["ID"]);
                json.AppendFormat("\"title\":\"{0}\",", dr["Title"]);
                json.AppendFormat("\"ico\":\"{0}\",", dr["Ico"]);
                json.AppendFormat("\"link\":\"{0}\",", dr["Address"]);
                json.AppendFormat("\"type\":\"{0}\",", "0");
                json.AppendFormat("\"model\":\"{0}\",", dr["OpenMode"]);
                json.AppendFormat("\"width\":\"{0}\",", dr["Width"]);
                json.AppendFormat("\"height\":\"{0}\",", dr["Height"]);
                json.AppendFormat("\"hasChilds\":\"{0}\",", childs.Length > 0 ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i < apps.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            json.Append("}");
            json.Append("]");

            Response.Write(json.ToString());
        }
    }
}