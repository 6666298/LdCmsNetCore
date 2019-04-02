using System;
using System.Text;

namespace LdCms.Common.Security
{
    /// <summary>
    /// DES加密、解密类
    /// 
    /// 
    /// </summary>
    public class DESEncryptHelper
    {
        /// <summary>
        /// 默认KEY
        /// </summary>
        private static string DESKey = AlgorithmHelper.MD5Encrypt16("LdCms_V1.0");
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static readonly byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        private static string IsDESKey(string key)
        {
            try
            {
                string result = DESKey;
                if (string.IsNullOrEmpty(key))
                    return result;
                if (key.Length != 16)
                    return result;
                else
                    return key;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #region DES加密
        /// <summary>
        /// DES加密字符串 
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns></returns>
        public static string EncryptDES(string encryptString)
        {
            return EncryptDES(encryptString, DESKey);
        }
        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <param name="encryptKey">加密密钥,要求为16位</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回空错误信息</returns> 
        public static string EncryptDES(string encryptString, string key)
        {
            try
            {
                string encryptKey = IsDESKey(key);
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var DCSP = System.Security.Cryptography.Aes.Create();
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                System.Security.Cryptography.CryptoStream cStream = new System.Security.Cryptography.CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), System.Security.Cryptography.CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  DES解密
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns></returns>
        public static string DecryptDES(string decryptString)
        {
            return DecryptDES(decryptString, DESKey);
        }
        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥,要求为16位,和加密密钥相同</param> 
        /// <returns>解密成功返回解密后的字符串，失败返回null</returns> 
        public static string DecryptDES(string decryptString, string key)
        {
            try
            {
                string decryptKey = IsDESKey(key);
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = System.Security.Cryptography.Aes.Create();
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                System.Security.Cryptography.CryptoStream cStream = new System.Security.Cryptography.CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), System.Security.Cryptography.CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion 
    }
}
