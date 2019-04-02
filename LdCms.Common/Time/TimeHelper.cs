using System;

namespace LdCms.Common.Time
{
    using Extension;
    using Utility;

    /// <summary>
    /// 时间操作类
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 判断字符串是否为时间格式 true为时间
        /// </summary>
        /// <param name="strDate">字符串</param>
        /// <returns>true为时间</returns>
        public static bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime StringToDate(string str)
        {
            try
            {
                if (str.Length < 7)
                    throw new Exception("字任串长度错误！");
                if (str.Length > 14)
                    str = str.Left(14);
                else
                    str = string.Format("{0}{1}", str, Utility.StringRepeat("0", 14 - str.Length));
                string strTime = str;
                strTime = strTime.Insert(4, "-");  //20181120113232
                strTime = strTime.Insert(7, "-");
                strTime = strTime.Insert(10, " ");
                strTime = strTime.Insert(13, ":");
                strTime = strTime.Insert(16, ":");
                if (IsDate(strTime))
                    return strTime.ToDate();
                else
                    throw new Exception("字任串不是时间格式！");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 获取时间
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetUnixTimestamp()
        {
            return DateTimeToTimestamp(DateTime.Now);
        }
        /// <summary>
        /// C#格式时间转为时间戳
        /// </summary>
        /// <param name="timeStamp">C#格式时间</param>
        /// <returns>C#格式时间</returns>
        public static int DateTimeToTimestamp(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime TimestampToDateTime(string timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 返回当前时间的标准日期格式
        /// </summary>
        /// <returns>yyyy-MM-dd</returns>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回当前时间的标准时间格式string
        /// </summary>
        /// <returns>HH:mm:ss</returns>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回当前时间的标准时间格式string
        /// </summary>
        /// <returns>yyyy-MM-dd HH:mm:ss</returns>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region 当前时间星期几
        /// <summary>
        /// 返回当前日期的星期名称
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>星期名称</returns>
        public static string GetWeekNameOfDay(DateTime idt)
        {
            string dt, week = "";
            dt = idt.DayOfWeek.ToString();
            switch (dt)
            {
                case "Mondy":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
            }
            return week;
        }
        /// <summary>
        /// 返回当前日期的星期编号
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>星期数字编号</returns>
        public static string GetWeekNumberOfDay(DateTime idt)
        {
            string dt, week = "";
            dt = idt.DayOfWeek.ToString();
            switch (dt)
            {
                case "Mondy":
                    week = "1";
                    break;
                case "Tuesday":
                    week = "2";
                    break;
                case "Wednesday":
                    week = "3";
                    break;
                case "Thursday":
                    week = "4";
                    break;
                case "Friday":
                    week = "5";
                    break;
                case "Saturday":
                    week = "6";
                    break;
                case "Sunday":
                    week = "7";
                    break;
            }
            return week;
        }
        #endregion

        #region 获取两个日期之间的差值 可返回年 月 日 小时 分钟 秒
        /// <summary>
        /// 获取两个日期之间的差值
        /// </summary>
        /// <param name="howtocompare">比较的方式可为：year month day hour minute second</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>时间差</returns>
        public static double DateDiff(CompareType howtoCompare, DateTime startDate, DateTime endDate)
        {
            double diff = 0;
            try
            {
                TimeSpan TS = new TimeSpan(endDate.Ticks - startDate.Ticks);
                switch (howtoCompare.ToString().ToLower())
                {
                    case "year":
                        diff = Convert.ToDouble(TS.TotalDays / 365);
                        break;
                    case "month":
                        diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                        break;
                    case "day":
                        diff = Convert.ToDouble(TS.TotalDays);
                        break;
                    case "hour":
                        diff = Convert.ToDouble(TS.TotalHours);
                        break;
                    case "minute":
                        diff = Convert.ToDouble(TS.TotalMinutes);
                        break;
                    case "second":
                        diff = Convert.ToDouble(TS.TotalSeconds);
                        break;
                }
            }
            catch (Exception)
            {
                diff = 0;
            }
            return diff;
        }
        /// <summary>
        /// 如何比较类型
        /// </summary>
        public enum CompareType
        {
            /// <summary>
            /// 年
            /// </summary>
            Year,
            /// <summary>
            /// 月
            /// </summary>
            Month,
            /// <summary>
            /// 天
            /// </summary>
            Day,
            /// <summary>
            /// 时
            /// </summary>
            Hour,
            /// <summary>
            /// 分
            /// </summary>
            Minute,
            /// <summary>
            /// 秒
            /// </summary>
            Second
        }
        #endregion





    }
}
