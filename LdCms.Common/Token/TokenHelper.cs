using System;

namespace LdCms.Common.Token
{
    using LdCms.Common.Security;
    using LdCms.Common.Time;

    /// <summary>
    /// 接口Token帮助类
    /// </summary>
    public class TokenHelper
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string Token { get; set; }
        public TokenHelper() { }
        public TokenHelper(string token) : this("", "", token) { }
        public TokenHelper(string appId, string appSecret) : this(appId, appSecret, "") { }
        public TokenHelper(string appId, string appSecret, string token)
        {
            AppId = appId;
            AppSecret = appSecret;
            Token = token;
        }
        public string GetToken()
        {
            return CreateToken();
        }
        public string GetToken(int getNum)
        {
            try
            {
                int length = 128;
                int totalNumber = getNum == 0 ? 1 : getNum + 1;
                return CreateToken(totalNumber).PadRight(length, '0');
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsToken(int createTimestamp, int expiresIn)
        {
            try
            {
                int indexTimestamp = TimeHelper.GetUnixTimestamp();
                createTimestamp = Math.Abs(createTimestamp);
                expiresIn = Math.Abs(expiresIn);
                if ((indexTimestamp - createTimestamp) < expiresIn)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string CreateToken()
        {
            try
            {
                string strGuid = GeneralCodeHelper.GetGuid(GeneralCodeHelper.GuIdFormat.N);
                string intGuid = GeneralCodeHelper.GuidTo16String();
                string randomStr = GeneralCodeHelper.RandomString(16);
                return string.Format("{0}{1}{2}", strGuid, intGuid, randomStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string CreateToken(int getNum)
        {
            try
            {
                int randomLength = 47 - getNum.ToString().Length;
                string strGuid = GeneralCodeHelper.GetGuid(GeneralCodeHelper.GuIdFormat.N);
                string intGuid = GeneralCodeHelper.GuidTo16String();
                string aRandomStr = GeneralCodeHelper.RandomString(randomLength, true, true, true, true);
                string bRandomStr = GeneralCodeHelper.RandomString(32, true, true, true, true);
                return string.Format("{0}_{1}{2}{3}{4}", getNum, aRandomStr, strGuid, intGuid, bRandomStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
