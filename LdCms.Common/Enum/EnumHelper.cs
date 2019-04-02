using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Enum
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumHelper
    {
        
        public static List<int> ValueToArr<T>()
        {
            try
            {
                List<int> result = new List<int>();
                foreach (var suit in System.Enum.GetValues(typeof(T)))
                    result.Add((int)suit);
                if (result.Count == 0)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<string> NameToArr<T>()
        {
            try
            {
                List<string> result = new List<string>();
                foreach (var suit in System.Enum.GetValues(typeof(T)))
                    result.Add(suit.ToString());
                if (result.Count == 0)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool IsKey<T>(string key)
        {
            try
            {
                return System.Enum.IsDefined(typeof(T), key);//若key=10，则b=false；
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetName<T>(int Value)
        {
            try
            {
                return System.Enum.GetName(typeof(T), Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int GetValue<T>(string name)
        {
            try
            {
                return (int)System.Enum.Parse(typeof(T), name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
