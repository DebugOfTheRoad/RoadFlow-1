using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class Login : WebForm.Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override bool CheckApp()
        {
            return true;
        }

        protected override bool CheckUrl(bool isEnd = true)
        {
            return true;
        }

        protected override bool CheckLogin(bool isRedirect = true)
        {
            return true;
        }
    }
}