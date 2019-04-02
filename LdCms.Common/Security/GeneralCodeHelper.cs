using System;

namespace LdCms.Common.Security
{
    using LdCms.Common.Extension;

    /// <summary>
    /// 随机字符串类
    /// </summary>
    public static class GeneralCodeHelper
    {
        /// <summary>
        /// 定义锁
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>生成随机字符串</summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，true=包含，默认为包含</param>
        /// <param name="useLow">是否包含小写字母，true=包含，默认为包含</param>
        /// <param name="useUpp">是否包含大定字母，true=包含，默认为包含</param>
        /// <param name="useSpe">是否包含特殊字符，true=包含，默认为不包含 _-</param>
        /// <param name="custom">要包含的自写义字符，直接输入要包含的字符列表</param>
        /// <returns>指定长度的随机字符串</returns>
        public static string RandomString(int length, bool useNum = true, bool useLow = false, bool useUpp = false, bool useSpe = false)
        {
            string custom = "";
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "_-"; }
            for (int i = 0; i < length; i++) { s += str.Substring(r.Next(0, str.Length - 1), 1); }
            return s;
        }
        /// <summary>
        /// 随时字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            return RandomString(length, true, true, true);
        }
        /// <summary>
        /// 随机单数据字符串 不足前面补0
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomInt(int length)
        {
            return RandomString(length, true).ToString().PadLeft(length, '0');
        }
        /// <summary>
        /// 随机单数据字符串 不足补0 右边补0 第一位不会出现0
        /// </summary>
        /// <param name="length"></param>
        /// <param name="pad"></param>
        /// <returns></returns>
        public static string GetRandomInt(int length, RandomIntPad pad)
        {
            string randomString = RandomString(length, true);
            if (pad.ToString().ToUpper() == "L")
                return randomString.PadLeft(length, '0');
            else
            {
                string formatString = randomString.ToInt().ToString();
                return formatString.PadRight(length, '0');
            }
        }
        /// <summary>
        /// 根据日期创建订单号
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderNumber()
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssff");
            string strRandomResult = GetRandomInt(3).ToString();
            return string.Format("{0}{1}", strDateTimeNumber, strRandomResult);
        }
        /// <summary>
        /// 返回订单号
        /// </summary>
        /// <param name="Platform">平台代码</param>
        /// <param name="OrderType">下单类型</param>
        /// <param name="PaymentType">支付类型</param>
        /// <returns></returns>
        public static string CreateOrderNumber(int platform, int orderType, int paymentType)
        {
            try
            {
                lock (locker)
                {
                    try
                    {
                        int time_stamp = Time.TimeHelper.GetUnixTimestamp();
                        return string.Format("{0}{1}{2}{3}", platform, orderType, paymentType, time_stamp);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="headString"></param>
        /// <param name="randomLength"></param>
        /// <returns></returns>
        public static string CreateOrderNumber(string headString, int totalLength = 16)
        {
            try
            {
                lock (locker)
                {
                    int default_len = 16;
                    string time_str = DateTime.Now.ToString("yyyyMMddHHmmss");
                    int random_length = (totalLength <= default_len ? default_len : totalLength) - time_str.Length;
                    string random_str = RandomString(random_length);
                    return string.Format("{0}{1}{2}", headString, time_str, random_str);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 由连字符分隔的32位数字
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }
        /// <summary>
        /// 获到GUID
        /// </summary>
        /// <param name="format">
        /// N格式：a53a7186b583483aa4580519034e8095
        /// D格式：5ae7f002-a989-4345-864b-3bcfbe09e1da
        /// B格式：{d9762660-8461-4c44-b714-8ffad6e1b79c}
        /// P格式：(694ce704-0a7d-41d5-a25a-4eaedf7db50d)
        /// X格式：{0x75198f26,0xac4e,0x42c8,{0x96,0x88,0xcc,0x91,0xe0,0xa6,0x9b,0x21}
        /// </param>
        /// <param name="ToUpper">是否大写 true/false</param>
        /// <returns></returns>
        public static string GetGuid(GuIdFormat format, bool ToUpper = false)
        {
            try
            {
                lock (locker)
                {
                    string result = string.Empty;
                    if (string.IsNullOrWhiteSpace(format.ToString()))
                        result = System.Guid.NewGuid().ToString();
                    else
                        result = System.Guid.NewGuid().ToString(format.ToString());
                    return ToUpper ? result.ToUpper() : result.ToLower();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name="guid"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>
        /// 根据GUID获取16位的唯一字符串  加前缀
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GuidTo16String(string str)
        {
            return string.Format("{0}{1}", str, GuidTo16String());
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        /// <summary>
        /// 根据GUID获取19位的唯一数字序列 加前缀
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GuidToLongID(string str)
        {
            return string.Format("{0}{1}", str, GuidToLongID());
        }
        /// <summary>
        /// GUID类型
        /// </summary>
        public enum GuIdFormat
        {
            N, D, B, P, X
        }
        public enum RandomIntPad
        {
            L,R
        }


    }
}
