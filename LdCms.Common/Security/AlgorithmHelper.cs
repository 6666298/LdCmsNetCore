using System;
using System.Security.Cryptography;
using System.Text;

namespace LdCms.Common.Security
{
    /// <summary>
    /// 常用算法类
    /// </summary>
    public static class AlgorithmHelper
    {
        #region SHA1算法
        /// <summary>
        /// SHA1算法
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// 使用示列:
        /// Algorithm.SHA1("str");
        public static string SHA1(string str)
        {
            try
            {
                byte[] cleanBytes = System.Text.Encoding.Default.GetBytes(str);
                byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
                return BitConverter.ToString(hashedBytes).Replace("-", "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region MD5加密算法
        public static string MD5(string encypString)
        {
            return MD5(encypString, "UTF-8");
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="encypStr">加密字符串</param>
        /// <param name="CodeType">编码</param>
        /// <returns></returns>
        public static string MD5(string encypString, string codeType)
        {
            try
            {
                MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
                byte[] inputBye = Encoding.GetEncoding(codeType).GetBytes(encypString);
                byte[] outputBye = m5.ComputeHash(inputBye);
                string result = System.BitConverter.ToString(outputBye);
                return result.Replace("-", "").ToUpper();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string encypString)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(encypString)), 4, 8);
                return result.Replace("-", "").ToUpper();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        #endregion

    }
}
