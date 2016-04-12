using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadFlow.Utility
{
    public class DateTimeNew
    {

        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 短日期格式(yyyy/MM/dd)
        /// </summary>
        public static string ShortDate
        {
            get
            {
                return Now.ToString(Utility.Config.DateFormat);
            }
        }
        /// <summary>
        /// 长日期格式(yyyy月MM日dd日)
        /// </summary>
        public static string LongDate
        {
            get
            {
                return Now.ToString("yyyy月MM日dd日");
            }
        }
        /// <summary>
        /// 日期时间(yyyy/MM/dd HH:mm)
        /// </summary>
        public static string ShortDateTime
        {
            get
            {
                return Now.ToString(Utility.Config.DateTimeFormat);
            }
        }
        /// <summary>
        /// 日期时间(yyyy/MM/dd HH:mm:ss)
        /// </summary>
        public static string ShortDateTimeS
        {
            get
            {
                return Now.ToString(Utility.Config.DateTimeFormatS);
            }
        }
        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分)
        /// </summary>
        public static string LongDateTime
        {
            get
            {
                return Now.ToString("yyyy年MM月dd日 HH时mm分");
            }
        }
        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分ss秒)
        /// </summary>
        public static string LongDateTimeS
        {
            get
            {
                return Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
            }
        }
        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分)
        /// </summary>
        public static string LongTime
        {
            get
            {
                return Now.ToString("HH时mm分");
            }
        }

        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分)
        /// </summary>
        public static string ShortTime
        {
            get
            {
                return Now.ToString("HH:mm");
            }
        }
    }
}
