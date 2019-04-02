using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Utility
{
    using LdCms.Common.Json;
    public partial class Utility
    {
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

    }
}
