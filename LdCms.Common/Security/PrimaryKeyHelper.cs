using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Security
{
    /// <summary>
    /// 生成唯一主建
    /// </summary>
    public class PrimaryKeyHelper
    {
        /// <summary>
        /// 只读状态锁对象
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>
        /// 生成唯一主建
        /// </summary>
        /// <param name="keyType">主建类型</param>
        /// <returns></returns>
        public static string MakePrimaryKey(PrimaryKeyType keyType)
        {
            return MakePrimaryKey(keyType, PrimaryKeyLen.V2);
        }
        /// <summary>
        /// 生成唯一主建
        /// </summary>
        /// <param name="keyType">主建类型</param>
        /// <param name="keyLen">主建长度</param>
        /// <returns></returns>
        public static string MakePrimaryKey(PrimaryKeyType keyType, PrimaryKeyLen keyLen)
        {
            var primaryKey = string.Empty;
            lock (locker)
            {
                switch (keyType)
                {
                    case PrimaryKeyType.Other:
                        if (keyLen == PrimaryKeyLen.V4)
                            primaryKey = GetGuidString();
                        else
                            primaryKey = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
                        break;
                    default:
                        if (keyLen == PrimaryKeyLen.V1)
                            primaryKey = GetKeyString(keyType);
                        else
                            primaryKey = GetKeyString(keyType, (int)keyLen);
                        break;
                }
            }
            return primaryKey;
        }
        /// <summary>
        /// 创建key字符串 10位
        /// </summary>
        /// <param name="keyType">主建类型</param>
        /// <returns></returns>
        private static string GetKeyString(PrimaryKeyType keyType)
        {
            try
            {
                int keyLen = 10;
                var primaryKey = string.Empty;
                lock (locker)
                {
                    var k = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
                    if (k < 0) k = -k;
                    primaryKey = (int)keyType + k.ToString();
                    while (primaryKey.Length < keyLen)
                    {
                        primaryKey += new Random(k).Next();
                    }
                    primaryKey = primaryKey.Substring(0, keyLen);
                }
                return primaryKey;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 创建key字符串 16、19位
        /// </summary>
        /// <param name="keyType">主建类型</param>
        /// <param name="len">主建长度</param>
        /// <returns></returns>
        private static string GetKeyString(PrimaryKeyType keyType, int len)
        {
            try
            {
                int keyLen = len;
                var primaryKey = string.Empty;
                lock (locker)
                {
                    var k = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
                    if (k < 0) k = -k;
                    primaryKey = (int)keyType + DateTime.Now.ToString("yyMM") + k;
                    while (primaryKey.Length < keyLen)
                    {
                        primaryKey += new Random(k).Next();
                    }
                    primaryKey = primaryKey.Substring(0, keyLen);
                }
                return primaryKey;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 创建GUID 字符串
        /// </summary>
        /// <returns></returns>
        private static string GetGuidString()
        {
            return GeneralCodeHelper.GetGuid(GeneralCodeHelper.GuIdFormat.N);
        }

        /// <summary>
        /// 主建类型枚举
        /// </summary>
        public enum PrimaryKeyType
        {
            MemberAccount = 10,
            MemberAddress = 11,
            MemberInvoice = 12,

            ServiceMessageBoard = 20,
            BasicsMedia = 30,

            InfoNoticeCategory = 40,
            InfoNotice = 41,
            InfoBlock = 42,
            InfoClass = 43,
            InfoPage = 44,
            InfoArtice = 45,

            ExtendAdvertisement = 50,
            ExtendAdvertisementDetails = 51,
            ExtendLinkGroup=52,
            ExtendLink=53,


            Table = 88,
            Other = 99
        }
        /// <summary>
        /// 主建长度枚举
        /// </summary>
        public enum PrimaryKeyLen
        {
            /// <summary>
            /// 版本V1 10位长度纯数字
            /// </summary>
            V1 = 10,
            /// <summary>
            /// 版本V2 16位长度纯数字
            /// </summary>
            V2 = 16,
            /// <summary>
            /// 版本V3 19位长度纯数字
            /// </summary>
            V3 = 19,
            /// <summary>
            /// 版本V4 32位长度字符串
            /// </summary>
            V4 = 32
        }

    }
}
