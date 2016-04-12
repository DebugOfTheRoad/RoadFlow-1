using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Common
{
    public class Tools
    {
        /// <summary>
        /// 包含文件
        /// </summary>
        public static string IncludeFiles
        {
            get
            {
                return
                    string.Format(@"<link href=""{0}/Themes/Common.css"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}/Themes/{1}/Style/style.css"" id=""style_style"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}/Themes/{1}/Style/ui.css"" id=""style_ui"" rel=""stylesheet"" type=""text/css"" media=""screen""/> 
    <script type=""text/javascript"" src=""{0}/Scripts/My97DatePicker/WdatePicker.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/jquery-1.11.1.min.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/jquery.cookie.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/json.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.core.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.button.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.calendar.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.file.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.member.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.dict.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.menu.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.select.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.combox.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.tab.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.text.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.textarea.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.editor.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.tree.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.validate.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.window.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.dragsort.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.selectico.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.accordion.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.grid.js""></script>
    <script type=""text/javascript"" src=""{0}/Scripts/roadui.init.js""></script>"
    , BaseUrl, RoadFlow.Utility.Config.Theme);
            }
        }

        public static string BaseUrl
        {
            get
            {
                return "";
            }
        }

        public static bool CheckLogin(out string msg)
        {
            msg = "";
            object session = System.Web.HttpContext.Current.Session[RoadFlow.Utility.Keys.SessionKeys.UserID.ToString()];
            Guid uid;
            if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
            {
                return false;
            }

            //#if DEBUG
            return true; //正式使用时请注释掉这一行
            //#endif

            string uniqueIDSessionKey = RoadFlow.Utility.Keys.SessionKeys.UserUniqueID.ToString();
            var user = new RoadFlow.Platform.OnlineUsers().Get(uid);
            if (user == null)
            {
                return false;
            }
            else if (System.Web.HttpContext.Current.Session[uniqueIDSessionKey] == null)
            {
                return false;
            }
            else if (string.Compare(System.Web.HttpContext.Current.Session[uniqueIDSessionKey].ToString(), user.UniqueID.ToString(), true) != 0)
            {
                msg = string.Format("您的帐号在{0}登录,您被迫下线!", user.IP);
                return false;
            }
            return true;
        }

        public static bool CheckLogin(bool redirect = true)
        {
            string msg;
            if (!CheckLogin(out msg))
            {
                if (!redirect)
                {
                    System.Web.HttpContext.Current.Response.Write("登录验证失败!");
                    System.Web.HttpContext.Current.Response.End();
                    return false;
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<script>top.login();</script>");
                    System.Web.HttpContext.Current.Response.End();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查应用程序权限
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static bool CheckApp(out string msg, string appid = "")
        {
            msg = "";
            appid = appid.IsNullOrEmpty() ? System.Web.HttpContext.Current.Request.QueryString["appid"] : appid;
            Guid appGuid;
            if (!appid.IsGuid(out appGuid))
            {
                return false;
            }
            var app = new RoadFlow.Platform.RoleApp().GetFromCache(appid);
            if (app != null)
            {
                var roles = RoadFlow.Platform.Users.CurrentUserRoles;
                if (roles.Contains(app["RoleID"].ToString().ToGuid()))
                {
                    return true;
                }
                else
                {
                    msg = "<script>top.login();</script>";
                }
            }
            else
            {
                var userID = RoadFlow.Platform.Users.CurrentUserID;
                if (userID.IsEmptyGuid())
                {
                    msg = "<script>top.login();</script>";
                    return false;
                }
                var userApp = new RoadFlow.Platform.UsersApp().GetUserDataRows(userID);
                foreach (System.Data.DataRow dr in userApp)
                {
                    if (dr["ID"].ToString().ToGuid() == appGuid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 检查访问地址
        /// </summary>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        public static bool CheckReferrer(bool isEnd = true)
        {
            bool IsUri = HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.Url.Host.Equals(HttpContext.Current.Request.UrlReferrer.Host, StringComparison.CurrentCultureIgnoreCase);
            if (!IsUri && isEnd)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("访问地址错误!");
                HttpContext.Current.Response.End();
            }
            return IsUri;
        }
        
    }
}