using System;
using System.Collections.Generic;

namespace LdCms.Plugins.WeChat.PublicPlatform
{
    using Common.Extension;
    using Common.Json;
    using Common.Security;
    using Common.Utility;
    using Common.Web;

    /// <summary>
    /// 微信网页授权 帮助类
    /// </summary>
    public class AuthorizeHelper
    {
        /*
         * 微信网页授权类 调用方法：
         * WxUserInfo wx = new WxUserInfo();
         * var userinfo = wx.GetUserInfo();
         * 
         * 单例模式 调用方法：
         * 第一种：
         * AuthorizeHelper.GetInstance().UserInfo(appid, appsecret);
         * 第二种
         * AuthorizeHelper t = AuthorizeHelper.GetInstance();
         * t.UserInfo(appid, appsecret);
         */

        /// <summary>
        /// 定义一个静态变量来保存类的实例
        /// </summary>
        private static AuthorizeHelper uniqueInstance;
        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>
        /// 定义私有构造函数，使外界不能创建该类实例
        /// </summary>
        private AuthorizeHelper() { }
        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static AuthorizeHelper GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new AuthorizeHelper();
                    }
                }
            }
            return uniqueInstance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public string CreateAuthorizeUrl(string appId, string appSecret,string state)
        {
            WxUserInfo t = new WxUserInfo();
            t.AppID = appId;
            t.AppSecret = appSecret;
            t.Scope = ScopeType.snsapi_base;
            return t.GoAuthorizeUrl(state);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public string GetOpenId(string appId, string appSecret)
        {
            WxUserInfo t = new WxUserInfo();
            t.AppID = appId;
            t.AppSecret = appSecret;
            t.Scope = ScopeType.snsapi_base;
            return t.GetOpenId();
        }
        /// <summary>
        /// 拉取微信用户信息
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public UserInfoResult GetUserInfo(string appId, string appSecret)
        {
            WxUserInfo t = new WxUserInfo();
            t.AppID = appId;
            t.AppSecret = appSecret;
            t.Scope = ScopeType.snsapi_userinfo;
            return t.GetUserInfo();
        }
    }

    /// <summary>
    /// 生成微信网页授权 API URL 地址、参数
    /// </summary>
    public class AuthorizeUri 
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
        /// 应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
        /// </summary>
        public ScopeType Scope { get; set; }
        /// <summary>
        /// 用户同意授权，获取code URL
        /// </summary>
        private string AuthorizeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";
        /// <summary>
        /// 通过code换取网页授权access_token URL
        /// </summary>
        private string AccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
        /// <summary>
        /// 刷新access_token（如果需要）URL
        /// </summary>
        private string RefreshTokenUrl = "https://api.weixin.qq.com/sns/oauth2/refresh_token";
        /// <summary>
        /// 拉取用户信息(需scope为 snsapi_userinfo) URL
        /// </summary>
        private string UserInfoUrl = "https://api.weixin.qq.com/sns/userinfo";
        /// <summary>
        /// 检验授权凭证（access_token）是否有效 URL
        /// </summary>
        private string AuthUrl = "https://api.weixin.qq.com/sns/auth";

        /// <summary>
        /// 获取当前 URL
        /// </summary>
        protected string RedirectUrl = System.Web.HttpUtility.UrlEncode(HttpContext.Current.Request.GetAbsoluteUri());
        /// <summary>
        /// 生成 Authorize Url
        /// </summary>
        /// <param name="redirectUrl">成功回调地址，默认返回当前地址</param>
        /// <returns></returns>
        public string CreateAuthorizeUrl(string state, string redirectUrl = "")
        {
            string redirect_url = string.IsNullOrEmpty(redirectUrl) ? RedirectUrl : System.Web.HttpUtility.UrlEncode(redirectUrl);
            var authorize_parameter = new
            {
                appid = AppID,
                redirect_uri = FilterRedirectUri(redirect_url),
                response_type = "code",
                scope = Scope.ToString(),
                state = string.Format("{0}#wechat_redirect", state)
            };
            string parameter_str = Utility.ObjToUrlParams(authorize_parameter);
            return string.Format("{0}?{1}", AuthorizeUrl, parameter_str);
        }
        /// <summary>
        /// 生成 access_token url
        /// </summary>
        /// <param name="code">获取 第一步返回的 code ，默认自动获取当前地址当的code</param>
        /// <returns></returns>
        public string CreateAccessTokenUrl(string code = "")
        {
            string getCode = HttpContext.Current.Request.GetQueryString("code");
            string strCode = string.IsNullOrEmpty(code) ? getCode : code;
            var access_token_parameter = new
            {
                appid = AppID,
                secret = AppSecret,
                code = strCode,
                grant_type = "authorization_code"
            };
            string parameter_str = Utility.ObjToUrlParams(access_token_parameter);
            return string.Format("{0}?{1}", AccessTokenUrl, parameter_str);
        }
        /// <summary>
        /// 生成 刷新 access_token url
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public string CreateRefreshTokenUrl(string refreshToken)
        {
            var refresh_token_parameter = new
            {
                appid = AppID,
                grant_type = "refresh_token",
                refresh_token = refreshToken
            };
            string parameter_str = Utility.ObjToUrlParams(refresh_token_parameter);
            return string.Format("{0}?{1}", RefreshTokenUrl, parameter_str);
        }
        /// <summary>
        /// 生成拉取用户信息 userinfo url
        /// </summary>
        /// <param name="accessToken">access_token</param>
        /// <param name="openid">openid</param>
        /// <returns></returns>
        public string CreateUserInfoUrl(string accessToken, string openid)
        {
            var user_info_parameter = new
            {
                access_token = accessToken,
                openid = openid,
                lang = "zh_CN"
            };
            string parameter_str = Utility.ObjToUrlParams(user_info_parameter);
            return string.Format("{0}?{1}", UserInfoUrl, parameter_str);
        }
        /// <summary>
        /// 生成 检验授权凭证（access_token）是否有效 url
        /// </summary>
        /// <param name="accessToken">access_token</param>
        /// <param name="openid">openid</param>
        /// <returns></returns>
        public string CreateAuthUrl(string accessToken, string openid)
        {
            var userInfoParameter = new
            {
                access_token = accessToken,
                openid = openid,
            };
            string parameterStr = Utility.ObjToUrlParams(userInfoParameter);
            return string.Format("{0}?{1}", AuthUrl, parameterStr);
        }

        #region 辅助方法
        /// <summary>
        /// 分析一个url将参数存入到dictionary
        /// </summary>
        /// <param name="strHref"></param>
        /// <returns></returns>
        private Dictionary<string, string> UrlParameterToDictionary(string strHref)
        {
            Dictionary<string, string> dicInfo = new Dictionary<string, string>();
            int intPos = strHref.IndexOf("?");
            if (intPos < 1) return dicInfo;

            string strRight = strHref.Substring(intPos + 1);
            string[] arrParams = strRight.Split('&');
            for (int i = 0; i < arrParams.Length; i++)
            {
                string[] arrSingle = arrParams[i].Split('=');
                if (arrSingle.Length > 1)
                {
                    string keyInfo = arrSingle[0].Trim();
                    string valueInfo = System.Web.HttpUtility.UrlDecode(arrSingle[1].Trim());
                    if (!dicInfo.ContainsKey(keyInfo))
                    {
                        dicInfo.Add(keyInfo, valueInfo);
                    }
                }
            }
            return dicInfo;
        }
        /// <summary>
        /// 过虑 授权回调地址
        /// </summary>
        /// <param name="uri">回调地址</param>
        /// <returns></returns>
        private string FilterRedirectUri(string uri)
        {
            string result_url = System.Web.HttpUtility.UrlDecode(uri);
            string[] f = result_url.Split('?');
            if (f.Length >= 2)
            {
                string url = f[0];
                var parameter_dictionary =  UrlParameterToDictionary(result_url);
                string query_string = "";
                int i = 0;
                foreach (var m in parameter_dictionary)
                {
                    if (m.Key.ToLower() != "code" && m.Key.ToLower() != "state")
                    {
                        i++;
                        var s = i == 1 ? "" : "&";
                        query_string += string.Format("{0}{1}={2}", s, m.Key.ToLower(), m.Value);
                    }
                }
                result_url = string.Format("{0}?{1}", url, query_string);
            }
            return System.Web.HttpUtility.UrlEncode(result_url);
        }
        #endregion

    }

    /// <summary>
    /// 生成微信网页授权 API 请求操作
    /// </summary>
    public abstract class AuthorizeOperation : AuthorizeUri
    {
        /// <summary>
        /// 用户同意授权，获取code 回调URI
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// 定义公共 access_token
        /// </summary>
        protected AccessTokenResult AccessTokenModel { get; set; }
        /// <summary>
        /// 获取OPENID
        /// </summary>
        /// <returns></returns>
        public string GetOpenId()
        {
            var getCode = HttpContext.Current.Request.GetQueryString("code");
            if (string.IsNullOrEmpty(getCode))
                return string.Empty;
            AccessTokenModel = this.GetAccessToken();
            if (AccessTokenModel == null)
                return string.Empty;
            else
                return AccessTokenModel.openid;
        }
        /// <summary>
        /// 拉取微信用户信息
        /// </summary>
        public UserInfoResult GetUserInfo()
        {
            var getCode = HttpContext.Current.Request.GetQueryString("code");
            if (string.IsNullOrEmpty(getCode))
                return null;
            else
            {
                AccessTokenModel = this.GetAccessToken();
                if (AccessTokenModel == null) { return null; }
            }
            string accessToken = AccessTokenModel.access_token;
            string openid = AccessTokenModel.openid;
            string refreshToken = AccessTokenModel.refresh_token;

            bool isAccessToken = IsAccessToken(accessToken, openid);
            if (!isAccessToken)
            {
                AccessTokenModel = this.RefreshAccessToken(refreshToken);
            }
            var result = this.UserInfoTemplate();
            return result;
        }

        /// <summary>
        /// 抽象 获取用户信息出来
        /// </summary>
        /// <returns></returns>
        public abstract UserInfoResult UserInfoTemplate();

        /// <summary>
        /// 第一步：用户同意授权，获取code
        /// </summary>
        public string GoAuthorizeUrl(string state)
        {
            //string authorizeUrl = CreateAuthorizeUrl(RedirectUri);
            //HttpContext.Current.Response.Redirect(authorize_url, true);
            return CreateAuthorizeUrl(state, RedirectUri);
        }
        /// <summary>
        /// 第二步：通过code换取网页授权access_token
        /// </summary>
        /// <returns></returns>
        public AccessTokenResult GetAccessToken()
        {
            string accessTokenUrl = CreateAccessTokenUrl();
            string getResultJson = HttpHelper.GetToUrl(accessTokenUrl, "utf-8");
            bool isError = IsJsonKey(getResultJson, "errcode");
            if (isError) { return null; }
            bool isJsonKey = IsJsonKey(getResultJson, "access_token");
            if (isJsonKey)
                return getResultJson.ToObject<AccessTokenResult>();
            return null;
        }
        /// <summary>
        /// 第三步：刷新access_token（如果需要）
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public AccessTokenResult RefreshAccessToken(string refreshToken)
        {
            string refreshTokenUrl = CreateRefreshTokenUrl(refreshToken);
            string getResultJson = HttpHelper.GetToUrl(refreshTokenUrl, "utf-8");
            bool requestResult = IsJsonKey(getResultJson, "access_token");
            if (requestResult)
                return getResultJson.ToObject<AccessTokenResult>();
            return null;
        }
        /// <summary>
        /// 第四步：拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public UserInfoResult GetUserInfo(string accessToken, string openid)
        {
            string userInfoUrl = CreateUserInfoUrl(accessToken, openid);
            string getResultJson = HttpHelper.GetToUrl(userInfoUrl, "utf-8");
            bool requestResult = IsJsonKey(getResultJson, "openid");
            if (requestResult)
                return getResultJson.ToObject<UserInfoResult>();
            return null;
        }


        /// <summary>
        /// 判断 access_token 是否已过期 true/false 可用/不可用
        /// </summary>
        /// <param name="accessToken">access_token</param>
        /// <param name="openid">openid</param>
        /// <returns>true/false 可用/不可用</returns>
        private bool IsAccessToken(string accessToken, string openid)
        {
            string authUrl = CreateAuthUrl(accessToken, openid);
            string getResultJsonstr = HttpHelper.GetToUrl(authUrl, "utf-8");
            Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(getResultJsonstr);
            int errCode = (int)jo["errcode"];
            string errMsg = (String)jo["errmsg"];
            return errCode == 0 ? true : false;
        }
        /// <summary>
        /// 判断JSON 字符串 某个节点是否存在 true/false 存在/不存在
        /// </summary>
        /// <param name="jsonStr">JSON字符串</param>
        /// <param name="keyName">KYE名称</param>
        /// <returns>true/false 存在/不存在</returns>
        private bool IsJsonKey(string jsonStr, string keyName)
        {
            Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(jsonStr);
            if (jo.Property(keyName) == null || jo.Property(keyName).ToString() == "")
                return false;
            else
                return true;
        }

    }

    /// <summary>
    /// 提取微信用户信息 抽象模板类
    /// </summary>
    public class WxUserInfo : AuthorizeOperation
    {
        /// <summary>
        /// 获取微信用户信息 抽象类
        /// </summary>
        /// <returns></returns>
        public override UserInfoResult UserInfoTemplate()
        {
            return GetUserInfo(AccessTokenModel.access_token, AccessTokenModel.openid);
        }
    }

    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum ScopeType
    {
        /// <summary>
        /// snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid）
        /// </summary>
        snsapi_base,
        /// <summary>
        /// snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
        /// </summary>
        snsapi_userinfo
    }
    /// <summary>
    /// access_token 结果模型
    /// </summary>
    public class AccessTokenResult
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string scope { get; set; }
    }
    /// <summary>
    /// 用户信息 结果模型
    /// </summary>
    public class UserInfoResult
    {
        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public string[] privilege { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionid { get; set; }

    }

}