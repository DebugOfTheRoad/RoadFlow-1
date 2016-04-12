using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.Members
{
    public partial class SortUsers : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.Users> Users = new List<RoadFlow.Data.Model.Users>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string parentID = Request.QueryString["parentid"];
            if (IsPostBack)
            {
                string sort = Request.Form["sort"] ?? "";
                string[] sortArray = sort.Split(',');
                RoadFlow.Platform.Users busers = new RoadFlow.Platform.Users();
                for (int i = 0; i < sortArray.Length; i++)
                {
                    Guid gid;
                    if (!sortArray[i].IsGuid(out gid))
                    {
                        continue;
                    }
                    busers.UpdateSort(gid, i + 1);
                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ok", "parent.frames[0].reLoad('" + parentID + "');", true);
            }
            Users = new RoadFlow.Platform.Organize().GetAllUsers(parentID.ToGuid());
        }
    }
}