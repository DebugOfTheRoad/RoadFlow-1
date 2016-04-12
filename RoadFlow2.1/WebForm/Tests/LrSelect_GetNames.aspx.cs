using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Tests
{
    public partial class LrSelect_GetNames : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string values = Request.QueryString["values"];
            RoadFlow.Platform.Dictionary Dict = new RoadFlow.Platform.Dictionary();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string value in values.Split(','))
            {
                var dict = Dict.Get(value.ToGuid(), true);
                if (dict != null)
                {
                    sb.Append(dict.Title);
                    sb.Append(',');
                }
            }
            Response.Write(sb.ToString().TrimEnd(','));
        }
    }
}