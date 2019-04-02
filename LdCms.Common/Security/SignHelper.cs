using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Security
{
    using Json;

    /// <summary>
    /// 签名计算类
    ///     1、计算签名
    ///     2、验证签名
    ///     
    /// 
    /// 
    /// </summary>
    public class SignHelper
    {
        public static bool IsSign(SortedDictionary<string, string> sParams, string key, string sign)
        {
            try
            {
                string strSign = GetSign(sParams, key);
                return strSign == sign ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool IsSign(string str, string key, StringType stringType, string sign)
        {
            try
            {
                string strSign = GetSign(str, key, stringType);
                return strSign == sign ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetSign(SortedDictionary<string, string> sParams, string key)
        {
            try
            {
                string signKey = GetSignParams(sParams, key);
                return AlgorithmHelper.MD5(signKey);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetSign(string str, string key, StringType stringType)
        {
            try
            {
                SortedDictionary<string, string> sdParams = new SortedDictionary<string, string>();
                if (stringType == StringType.Json)
                    sdParams = JsonToSortedDictionary(str);
                if (stringType == StringType.Xml)
                    sdParams = XmlToSortedDictionary(str);
                return GetSign(sdParams, key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetSignParams(SortedDictionary<string, string> sParams, string key)
        {
            try
            {
                int i = 0;
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> temp in sParams)
                {
                    if (string.IsNullOrEmpty(temp.Value) || temp.Key.ToLower() == "sign")
                        continue;
                    i++;
                    sb.Append(string.Format("{0}={1}&", temp.Key.Trim(), temp.Value.Trim()));
                }
                sb.Append(string.Format("key={0}", key.Trim()));
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static SortedDictionary<string, string> JsonToSortedDictionary(string jsonString)
        {
            try
            {
                string str = jsonString;
                SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
                var jo = Newtonsoft.Json.Linq.JObject.Parse(str);
                foreach (Newtonsoft.Json.Linq.JToken child in jo.Children())
                {
                    var property = child as Newtonsoft.Json.Linq.JProperty;
                    var keyType = property.Value.Type;
                    string keyName = property.Name.ToString().Trim();
                    string keyValue = string.Empty;
                    switch (keyType)
                    {
                        case Newtonsoft.Json.Linq.JTokenType.Object:
                        case Newtonsoft.Json.Linq.JTokenType.Array:
                            keyValue = property.Value.ToJson().Trim();
                            break;
                        case Newtonsoft.Json.Linq.JTokenType.Boolean:
                            keyValue = property.Value.ToString().Trim().ToLower();
                            break;
                        case Newtonsoft.Json.Linq.JTokenType.Null:
                            keyValue = string.Empty;
                            break;
                        default:
                            keyValue = property.Value.ToString().Trim();
                            break;
                    }
                    sParams.Add(keyName, keyValue);
                }
                return sParams;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static SortedDictionary<string, string> XmlToSortedDictionary(string xmlString)
        {
            try
            {
                SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(xmlString);
                System.Xml.XmlElement root = doc.DocumentElement;
                int len = root.ChildNodes.Count;
                for (int i = 0; i < len; i++)
                {
                    string name = root.ChildNodes[i].Name;
                    if (!sParams.ContainsKey(name))
                    {
                        sParams.Add(name.Trim(), root.ChildNodes[i].InnerText.Trim());
                    }
                }
                return sParams;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public enum StringType
        {
            /// <summary>
            /// Json源串
            /// </summary>
            Json = 1,
            /// <summary>
            /// Xml源串
            /// </summary>
            Xml = 2
        }

    }
}
