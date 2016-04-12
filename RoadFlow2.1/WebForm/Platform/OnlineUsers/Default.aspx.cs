using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Platform.OnlineUsers
{
    public partial class Default : Common.BasePage
    {
        protected List<RoadFlow.Data.Model.OnlineUsers> UserList = new List<RoadFlow.Data.Model.OnlineUsers>();
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadFlow.Platform.OnlineUsers bou = new RoadFlow.Platform.OnlineUsers();
            string name = string.Empty;
            if (IsPostBack)
            {
                name = Request.Form["Name"];
                if (!Request.Form["ClearAll"].IsNullOrEmpty())
                {
                    bou.RemoveAll();
                }

                if (!Request.Form["ClearSelect"].IsNullOrEmpty())
                {
                    string userids = Request.Form["checkbox_app"];
                    if (!userids.IsNullOrEmpty())
                    {
                        foreach (string userid in userids.Split(','))
                        {
                            Guid uid;
                            if (userid.IsGuid(out uid))
                            {
                                bou.Remove(uid);
                            }
                        }
                    }
                }
            }
            else
            {
                name = Request.QueryString["Name"];
            }
            UserList = bou.GetAll();
            this.Count.Text = UserList.Count.ToString();
            if (!name.IsNullOrEmpty())
            {
                UserList = UserList.Where(p => p.UserName.IndexOf(name) >= 0).ToList();
            }
            
        }
    }
}