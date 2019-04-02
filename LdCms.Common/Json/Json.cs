using System.Collections.Generic;
using System.Data;

namespace LdCms.Common.Json
{
    using Newtonsoft.Json.Linq;
    /// <summary>
    /// Json 操作类
    /// 
    /// 作者：小草
    /// 功能：
    ///     1
    /// 
    /// </summary>
    public static class Json
    {
        public static bool IsJson(this string str)
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
        public static object ToJson(this string json)
        {
            return json == null ? null : Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }
        public static string ToJson(this object obj)
        {
            var timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static T ToObject<T>(this string json)
        {
            return json == null ? default(T) : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static T ToObject<T>(this object obj)
        {
            string json = obj.ToJson();
            return json == null ? default(T) : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static List<T> ToList<T>(this string json)
        {
            return json == null ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
        }
        public static DataTable ToTable(this string json)
        {
            return json == null ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
        }
        public static JObject ToJObject(this string json)
        {
            return json == null ? Newtonsoft.Json.Linq.JObject.Parse("{}") : Newtonsoft.Json.Linq.JObject.Parse(json.Replace("&nbsp;", ""));
        }
    }
}
