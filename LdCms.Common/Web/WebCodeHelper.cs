using System;

namespace LdCms.Common.Web
{
    /// <summary>
    /// 编码操作类
    /// </summary>
    public class WebCodeHelper
    {
        #region  Unicode编码转中文
        /// <summary>
        /// Unicode编码转中文
        /// </summary>
        /// <param name="s">Unicode编码字符串</param>
        /// <returns></returns>
        public static string UnicodeToGb(string s)
        {
            System.Text.RegularExpressions.Regex reUnicode = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})", System.Text.RegularExpressions.RegexOptions.Compiled);
            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }
        #endregion
    }
}
