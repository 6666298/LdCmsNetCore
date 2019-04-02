using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LdCms.Common.Utility
{
    /// <summary>
    /// 常用方法函数
    /// 
    /// 
    /// </summary>
    public static partial class Utility
    {
        /// <summary>
        /// 三目运算 输入字符串为空则车输出结果值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="result">结果</param>
        /// <returns></returns>
        public static string IIF(string str, string result)
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
        public static string IIF(bool obj, string trueValue, string falseValue)
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
        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        public static bool IsNumber(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            const string pattern = "^[0-9]*$";
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(pattern);
            return rx.IsMatch(str);
        }
        /// <summary>
        /// 验证字符是否为数字类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是/否 true/false</returns>
        public static bool IsNumeric(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 验证字符是否为数字类型，否则返回结果值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="result">返回结果</param>
        /// <returns></returns>
        public static int IsNum(string str, int result)
        {
            if (IsNumeric(str))
                return Convert.ToInt32(str);
            else
                return result;
        }
        /// <summary>
        /// 验证文件是不是图片格式
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static bool IsPic(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            switch (fileExtension.Substring(1))
            {
                case "jpg": return true;
                case "gif": return true;
                case "peg": return true;
                case "bmp": return true;
                case "png": return true;
                default: return false;
            }
        }
        public static bool IsOfficeFile(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            switch (fileExtension.Substring(1))
            {
                case "txt": return true;
                case "pdf": return true;
                case "doc": return true;
                case "docx": return true;
                case "xls": return true;
                case "xlsx": return true;
                case "ppt": return true;
                case "pptx": return true;
                default: return false;
            }
        }
        public static bool IsVideoFile(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            switch (fileExtension.Substring(1))
            {
                case "mp3": return true;
                case "m4a": return true;
                case "flac": return true;
                case "ogg": return true;
                case "wav": return true;

                case "mp4": return true;
                case "ts": return true;
                case "flv": return true;
                case "wmv": return true;
                case "asf": return true;
                case "rm": return true;
                case "rmvb": return true;
                case "mpg": return true;
                case "mpeg": return true;
                case "3gp": return true;
                case "mov": return true;
                case "webm": return true;
                case "mkv": return true;
                case "avi": return true;
                default: return false;
            }
        }
        /// <summary>
        /// 验证字符串里是否中文字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsChina(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(str.Substring(i, 1))) > Convert.ToInt32(Convert.ToChar(128)))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^1[34578]\\d{9}$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 验证字符串是否为邮箱格式
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            return r.IsMatch(email);
        }
        /// <summary>
        /// 验证JSON字符串格式是否法
        /// </summary>
        /// <param name="str">JSON字符串</param>
        /// <returns></returns>
        public static bool IsJson(string str)
        {
            try
            {
                Newtonsoft.Json.Linq.JObject.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 验证JSON节点是否存在
        /// </summary>
        /// <param name="jsonStr">JSON字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static bool IsJsonNodeName(string jsonStr, string nodeName)
        {
            try
            {
                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(jsonStr);
                if (jo.Property(nodeName) == null || jo.Property(nodeName).ToString() == "")
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 从左到右提取字符串内容
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">数量</param>
        /// <returns></returns>
        public static string Left(string str, int len)
        {
            if (str.Length > len)
                return str.Substring(0, len);
            else
                return str.Substring(0, str.Length);
        }
        /// <summary>
        /// 从右到左提取字符串内空
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">数量</param>
        /// <returns></returns>
        public static string Right(string str, int len)
        {
            return str.Substring(str.Length - len);
        }
        /// <summary>
        /// 过虑字符串，转换内容，防止意外
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string FilterText(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            string Result = "";
            Result = str.Replace("&", "&amp;");
            Result = Result.Replace("'", "&#39;");
            Result = Result.Replace("\"", "&#34;");
            Result = Result.Replace("<", "&lt;");
            Result = Result.Replace(">", "&gt;");
            return Result;
        }
        /// <summary>
        /// 反转换内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ContentDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            string code = str.Replace("&amp;", "&");
            code = code.Replace("&#39;", "'");
            code = code.Replace("&#34;", "\"");
            code = code.Replace("&lt;", "<");
            code = code.Replace("&gt;", ">");
            return code;
        }
        /// <summary>
        /// 对象转换URL参数字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ObjToUrlParams(object obj)
        {
            string result = string.Empty;
            if (obj == null) { return result; }
            int i = 0;
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                i++;
                var s = i == 1 ? "" : "&";
                result += string.Format("{0}{1}={2}", s, p.Name, p.GetValue(obj, null));
            }
            return result;
        }
        /// <summary>
        /// SortedDictionary 转换URL参数字符串
        /// </summary>
        /// <param name="sParams"></param>
        /// <returns></returns>
        public static string DictionaryToUrlParams(SortedDictionary<string, object> sParams)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (KeyValuePair<string, object> temp in sParams)
            {
                if (temp.Value==null)
                    continue;
                i++;
                var strSplit = i == 1 ? "" : "&";
                sb.Append(string.Format("{0}{1}={2}", strSplit, temp.Key.Trim(), temp.Value.ToString().Trim()));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 分析一个url将参数存入到dictionary
        /// </summary>
        /// <param name="strHref"></param>
        /// <returns></returns>
        private static Dictionary<string, string> UrlParamsToDictionary(string strHref)
        {
            Dictionary<string, string> dicInfo = new Dictionary<string, string>();
            int intPos = strHref.IndexOf("?");
            if (intPos < 1) return dicInfo;

            string strRight = strHref.Substring(intPos + 1);
            string[] arrParams = strRight.Split('&');
            for (int i = 0; i < arrParams.Length; i++)
            {
                string[] arrSingle = arrParams[i].Split('=');
                if (arrSingle.Length > 1)
                {
                    string keyInfo = arrSingle[0].Trim();
                    string valueInfo = System.Web.HttpUtility.UrlDecode(arrSingle[1].Trim());
                    if (!dicInfo.ContainsKey(keyInfo))
                    {
                        dicInfo.Add(keyInfo, valueInfo);
                    }
                }
            }
            return dicInfo;
        }

        /// <summary>
        /// 高亮关键字
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="key">高亮关键字</param>
        /// <returns></returns>
        public static string Highlight(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) { return str; }
            if (string.IsNullOrWhiteSpace(str)) { return str; }
            if (key.Length == 0) { return str; }
            return str.Replace(key, string.Format("<font color='red'>{0}</font>", key));
        }
        /// <summary>
        /// 生成重复字符串
        /// </summary>
        /// <param name="str">要重复字符串</param>
        /// <param name="n">生成数量</param>
        /// <returns></returns>
        public static string StringRepeat(string str, int n)
        {
            if (string.IsNullOrEmpty(str) || n <= 0)
                return string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            while (n > 0)
            {
                sb.Append(str);
                n--;
            }
            return sb.ToString();
        }
        /// <summary>
        /// 字符串中间插入字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string InsertString(string str,int n,string key)
        {
            return str.Insert(n, key);
        }




 




    }
}
