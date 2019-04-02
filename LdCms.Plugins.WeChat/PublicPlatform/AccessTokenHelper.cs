using System;

namespace LdCms.Plugins.WeChat.PublicPlatform
{
    using Common.Json;
    using Common.Time;
    using Common.Utility;
    using Common.Web;

    /// <summary>
    /// 获取微信全局 access_token
    /// </summary>
    public class AccessTokenHelper
    {
        /// <summary>
        /// 定义一个静态变量来保存类的实例
        /// </summary>
        private static AccessTokenHelper uniqueInstance;
        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>
        /// 定义私有构造函数，使外界不能创建该类实例
        /// </summary>
        private AccessTokenHelper() { }
        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static AccessTokenHelper GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new AccessTokenHelper();
                    }
                }
            }
            return uniqueInstance;
        }
        /// <summary>
        /// 新请求微信全局公共AccessToken 返回原生JSON
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="appSecret">appSecret</param>
        /// <returns></returns>
        public string GetAccessToken(string appId, string appSecret)
        {
            try
            {
                return new AccessTokenOperation(appId, appSecret).GetAccessToken();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 查询微信全局公共AccessToken 只返回access_token字符串
        /// </summary>
        /// <param name="appId">微信公众号appid</param>
        /// <param name="appSecret">微信公众号appSecret</param>
        /// <param name="entity">Access_Token Entity 如果全新请求 传入null</param>
        /// <returns></returns>
        public string GetAccessToken(string appId, string appSecret, AccessTokenEntity entity)
        {
            try
            {
                var result = GetAccessTokenPro(appId, appSecret, entity);
                return result.access_token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 查询微信全局公共AccessToken
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="entity"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public AccessTokenEntity GetAccessTokenPro(string appId, string appSecret, AccessTokenEntity entity)
        {
            try
            {
                return new AccessTokenOperation(appId, appSecret).PlatformAccessToken(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    /// <summary>
    /// 生成微信公众号全局access_token API URL 地址、参数
    /// </summary>
    public class AccessTokenUri
    {
        /// <summary>
        /// 微信公众号AppID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 微信公众号AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 微信公众号全局access_token URL
        /// </summary>
        private string AccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token";
        /// <summary>
        /// 生成微信公众号全局access_token 参数 URL
        /// </summary>
        /// <returns></returns>
        public string CreateAccessTokenUrl()
        {
            var accessTokenParams = new
            {
                grant_type = "client_credential",
                appid = AppID,
                secret = AppSecret
            };
            string urlParams = Utility.ObjToUrlParams(accessTokenParams);
            return string.Format("{0}?{1}", AccessTokenUrl, urlParams);
        }
    }
    /// <summary>
    /// 全局AccessToken操作类
    /// </summary>
    public class AccessTokenOperation : AccessTokenUri
    {
        public AccessTokenOperation() { }
        public AccessTokenOperation(string appId, string appSecret)
        {
            AppID = appId;
            AppSecret = appSecret;
        }

        public string GetAccessToken()
        {
            try
            {
                string accessTokenUrl = CreateAccessTokenUrl();
                return HttpHelper.GetToUrl(accessTokenUrl);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取AccessToken API接口
        /// </summary>
        /// <returns></returns>
        public AccessTokenResult GetAccessTokenPro()
        {
            try
            {
                string resultJson = GetAccessToken();
                bool isCode = IsJsonKey(resultJson, "errcode");
                if (!isCode)
                {
                    bool isKey = IsJsonKey(resultJson, "access_token");
                    if (isKey)
                        return resultJson.ToObject<AccessTokenResult>();
                    else
                        return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///  获取平台全局公共AccessToken
        /// </summary>
        /// <returns></returns>
        public AccessTokenEntity PlatformAccessToken(AccessTokenEntity entity)
        {
            try
            {
                int currentTimeint = TimeHelper.GetUnixTimestamp();
                if (entity == null)
                {
                    var getAccessTokenResult = GetAccessTokenPro();
                    return new AccessTokenEntity()
                    {
                        access_token = getAccessTokenResult.access_token,
                        expires_in = getAccessTokenResult.expires_in,
                        timestamp = currentTimeint
                    };
                }
                else
                {
                    string strAccessToken = entity.access_token;
                    int expiresIn = entity.expires_in - 300;
                    int timestamp = entity.timestamp;
                    if (currentTimeint - timestamp > expiresIn)
                    {
                        var getAccessTokenResult = GetAccessTokenPro();
                        return new AccessTokenEntity()
                        {
                            access_token = getAccessTokenResult.access_token,
                            expires_in = getAccessTokenResult.expires_in,
                            timestamp = currentTimeint
                        };
                    }
                    else
                    {
                        return entity;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 判断JSON 字符串 某个节点是否存在 true/false 存在/不存在
        /// </summary>
        /// <param name="jsonStr">JSON字符串</param>
        /// <param name="keyName">KYE名称</param>
        /// <returns>true/false 存在/不存在</returns>
        private bool IsJsonKey(string jsonStr, string keyName)
        {
            try
            {
                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(jsonStr);
                if (jo.Property(keyName) == null || jo.Property(keyName).ToString() == "")
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    /// <summary>
    /// 微信全局AccessToken实体
    /// </summary>
    public class AccessTokenEntity
    {
        /// <summary>
        /// access_token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 有效秒
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public int timestamp { get; set; }
    }



}