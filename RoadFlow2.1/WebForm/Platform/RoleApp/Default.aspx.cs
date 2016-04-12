using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.RoleApp
{
    public partial class Default : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.Role> RoleList = new List<RoadFlow.Data.Model.Role>();
        protected void Page_Load(object sender, EventArgs e)
        {
            RoleList = new RoadFlow.Platform.Role().GetAll();
            if (IsPostBack && !Request.Form["Search"].IsNullOrEmpty())
            {
                string name = Request.Form["Name"];
                if (!name.IsNullOrEmpty())
                {
                    RoleList = RoleList.Where(p => p.Name.Contains(name)).ToList();
                }
            }
        }
    }
}