using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdCms.Common.Extension
{
    /// <summary>
    /// 常用扩展方法
    /// 
    /// 
    /// 
    /// </summary>
    public static partial class ExtConvert
    {
        #region 数值转换
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt(this object data)
        {
            if (data == null)
                return 0;
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success)
                return result;
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble(this object data)
        {
            if (data == null)
                return 0;
            double result;
            return double.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == null)
                return null;
            double result;
            bool isValid = double.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal(this object data)
        {
            if (data == null)
                return 0;
            decimal result;
            return decimal.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object data, int digits)
        {
            return Math.Round(ToDecimal(data), digits);
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == null)
                return null;
            decimal result;
            bool isValid = decimal.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
                return null;
            return Math.Round(result.Value, digits);
        }

        #endregion

        #region byte转换

        public static byte ToByte(this object data)
        {
            try
            {
                return Convert.ToByte(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static byte? ToByteOrNull(this object data)
        {
            try
            {
                if (data == null)
                    return null;
                byte result;
                bool isValid = byte.TryParse(data.ToString(), out result);
                if (isValid)
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(this object data)
        {
            if (data == null)
                return DateTime.MinValue;
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result) ? result : DateTime.MinValue;
        }
        public static DateTime ToSqlDate(this object data)
        {
            if (data == null)
                return SqlDateTime.MinValue.ToDate();
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result) ? result : SqlDateTime.MinValue.ToDate();
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(this object data)
        {
            if (data == null)
                return null;
            DateTime result;
            bool isValid = DateTime.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为Unix时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ToUnixTime(this DateTime date)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (date - startTime).TotalSeconds;
        }

        /// <summary>
        /// Unix时间转换为系统时间
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime UnixToDate(this double unixTime)
        {
            if (unixTime < 1) return DateTime.MinValue;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = 0;
            if (!long.TryParse((unixTime + "0000000"), out lTime))
            {
                return DateTime.MinValue;
            }
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
        #endregion

        #region 布尔转换
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null)
                return false;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(this object data)
        {
            if (data == null)
                return null;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            bool isValid = bool.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 字符串转换
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString(this object data)
        {
            return data == null ? string.Empty : data.ToString().Trim();
        }
        public static object ToDBNull(this object data)
        {
            if (data == null)
                return DBNull.Value;
            if (data.ToDate() == DateTime.MinValue)
                return DBNull.Value;
            if (data.ToSqlDate() == SqlDateTime.MaxValue)
                return DBNull.Value;
            return data;
        }
        #endregion

        #region 字符串操作
        /// <summary>
        /// 从左到右提取字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(this string str, int len)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            if (str.Length > len)
            {
                return str.Substring(0, len);
            }
            else
            {
                return str.Substring(0, str.Length);
            }
        }
        /// <summary>
        /// 从右到左提取字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string Right(this string str, int len)
        {
            return str.Substring(str.Length - len);
        }
        #endregion

        #region 验证是否为空
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid? value)
        {
            if (value == null)
                return true;
            return IsEmpty(value.Value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
            if (value == Guid.Empty)
                return true;
            return false;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this object value)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 泛型列表转换
        public static List<T> ToLists<T>(this List<T> lists)
        {
            try
            {
                if (lists == null)
                    return null;
                if (lists.Count == 0)
                    return null;
                if (lists != null && lists.Count > 0)
                    return lists;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 其它
        /// <summary>
        /// 枚举转换成泛型
        /// </summary>
        /// <param name="enumType">枚举</param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(this Type enumType)
        {
            var allItem = System.Enum.GetNames(enumType);
            Dictionary<int, string> dc = new Dictionary<int, string>();
            for (var i = 1; i <= allItem.Length; i++)
            {
                dc.Add(i, allItem[i - 1]);
            }
            return dc;
        }
        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        #endregion

        /// <summary>
        /// 三目运算 输入字符串为空则车输出结果值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="result">结果</param>
        /// <returns></returns>
        public static string IIF(this string str, string result)
        {
            try
            {
                return string.IsNullOrEmpty(str) ? result : str;
            }
            catch (Exception)
            {
                return result;
            }
        }
        /// <summary>
        /// 三目运算
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        public static string IIF(this bool obj, string trueValue, string falseValue)
        {
            try
            {
                return obj ? trueValue : falseValue;
            }
            catch (Exception)
            {
                return falseValue;
            }
        }

    }
}
