using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Common
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.CheckUrl();
            this.CheckLogin();
            this.CheckApp();
            this.InitInclude();
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        protected virtual void InitInclude()
        {
            if (Page.Header != null)
            {
                Page.Header.Controls.AddAt(Page.Header.Controls.Count - 1, new System.Web.UI.WebControls.Literal() { Text = Tools.IncludeFiles });
            }
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckLogin(bool isRedirect = true)
        {
            return Tools.CheckLogin(isRedirect);
        }

        /// <summary>
        /// 检查访问地址
        /// </summary>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        protected virtual bool CheckUrl(bool isEnd = true)
        {
            return Tools.CheckReferrer(isEnd);
        }

        /// <summary>
        /// 检查应用权限
        /// </summary>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        protected virtual bool CheckApp()
        {
            string msg;
            return Tools.CheckApp(out msg);
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static RoadFlow.Data.Model.Users CurrentUser
        {
            get
            {
                return RoadFlow.Platform.Users.CurrentUser;
            }
        }
        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public static Guid CurrentUserID
        {
            get
            {
                return RoadFlow.Platform.Users.CurrentUserID;
            }
        }
        /// <summary>
        /// 当前用户姓名
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                return RoadFlow.Platform.Users.CurrentUserName;
            }
        }
        /// <summary>
        /// 应用程序路径
        /// </summary>
        public static string SitePath
        {
            get
            {
                return Tools.BaseUrl;
            }
        }
    }
}