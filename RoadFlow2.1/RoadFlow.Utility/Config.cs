using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadFlow.Utility
{
    public class Config
    {
        /// <summary>
        /// 平台连接字符串
        /// </summary>
        public static string PlatformConnectionStringMSSQL
        {
            get
            {
                 return System.Configuration.ConfigurationManager.ConnectionStrings["PlatformConnection"].ConnectionString;
            }
        }
        /// <summary>
        /// 平台连接字符串
        /// </summary>
        public static string PlatformConnectionStringORACLE
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["PlatformConnectionOracle"].ConnectionString;
            }
        }
    
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DataBaseType
        {
            get
            {
                string dbType = System.Configuration.ConfigurationManager.AppSettings["DatabaseType"];
                return dbType.IsNullOrEmpty() ? "MSSQL" : dbType.ToUpper();
            }
        }
        /// <summary>
        /// 系统初始密码
        /// </summary>
        public static string SystemInitPassword
        {
            get
            {
                string pass = System.Configuration.ConfigurationManager.AppSettings["InitPassword"];
                return pass.IsNullOrEmpty() ? "111" : pass.Trim();
            }
        }
        /// <summary>
        /// 得到当前主题
        /// </summary>
        public static string Theme
        {
            get
            {
                var cookie = System.Web.HttpContext.Current.Request.Cookies["theme_platform"];
                return cookie != null && !cookie.Value.IsNullOrEmpty() ? cookie.Value : "Blue";
            }
        }

        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public static string UploadFileType
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UploadFileType"];
            }
        }
        /// <summary>
        /// 日期格式
        /// </summary>
        public static string DateFormat
        {
            get
            {
                return "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public static string DateTimeFormat
        {
            get
            {
                return "yyyy-MM-dd HH:mm";
            }
        }

        /// <summary>
        /// 日期时间格式(带秒)
        /// </summary>
        public static string DateTimeFormatS
        {
            get
            {
                return "yyyy-MM-dd HH:mm:ss";
            }
        }
    }
}
