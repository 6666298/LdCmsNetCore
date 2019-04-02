using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;

namespace LdCms.Web.Controllers.API.Member.V2
{
    using LdCms.IBLL.Member;
    using LdCms.Web.Services;
    using LdCms.Common.Utility;
    using LdCms.Common.Security;
    using LdCms.Common.Net;
    using LdCms.Common.Time;
    /// <summary>
    /// 前后端分离API
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    public partial class MemberController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IAccountService AccountService;
        private readonly IAccessTokenService AccessTokenService;
        public MemberController(IBaseApiManager BaseApiManager, IAccountService AccountService, IAccessTokenService AccessTokenService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.AccountService = AccountService;
            this.AccessTokenService = AccessTokenService;
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns>
        /// cgi-bin/v2/member/index?uuid=460e64203493444ba27d4fc7ad7efae8
        /// </returns>
        [HttpGet]
        [ActionName("index")]
        public IActionResult Index(string uuid)
        {
            try
            {
                string index = "index";
                var data = new { index, uuid };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="fromValue">{"phone":"18666376363","password":"123456"}</param>
        /// <returns>
        /// cgi-bin/v2/member/register?uuid=460e64203493444ba27d4fc7ad7efae8
        /// </returns>
        [HttpPost]
        [ActionName("register")]
        public IActionResult Register(string uuid, [FromBody]JObject fromValue)
        {
            long logId = 0;
            try
            {
                int systemId = SystemID;
                logId = BaseApiManager.SaveLogs(uuid, fromValue);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                bool isParams = IsRegisterParams(fromValue);
                var entity = GetInterfaceAccountByUuid(uuid);
                string companyId = entity.CompanyID;
                string memberId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.MemberAccount);
                string phone = GetJObjectValue(fromValue, "phone");
                string password = GetJObjectValue(fromValue, "password");
                string ipAddress = Net.Ip;
                if (!Utility.IsMobilePhone(phone))
                    return Error(logId, "verify phone fail！");

                var result = AccountService.SaveAccountRegisterPro(systemId, companyId, memberId, AlgorithmHelper.MD5(password), phone, ipAddress);
                if (result)
                    return Success(logId, "ok");
                else
                    return Error(logId, "fail");
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="uuid">460e64203493444ba27d4fc7ad7efae8</param>
        /// <param name="fromValue">{"account":"18666376363","password":"123456"}</param>
        /// <returns>
        /// cgi-bin/v2/member/login?uuid=460e64203493444ba27d4fc7ad7efae8
        /// </returns>
        [HttpPost]
        [ActionName("login")]
        public IActionResult Login(string uuid, [FromBody]JObject fromValue)
        {
            long logId = 0;
            try
            {
                int systemId = SystemID;
                logId = BaseApiManager.SaveLogs(uuid, fromValue);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                bool isParams = IsLoginParams(fromValue);
                var entity = GetInterfaceAccountByUuid(uuid);
                string companyId = entity.CompanyID;
                string account = GetJObjectValue(fromValue, "account");
                string password = GetJObjectValue(fromValue, "password");
                var result = AccountService.VerifyAccountLoginPro(systemId, companyId, account, AlgorithmHelper.MD5(password));
                if (result)
                {
                    var entityMember = AccountService.GetAccountByUserName(systemId, companyId, account);
                    string memberId = entityMember.MemberID;
                    var tokenService = new Common.Token.TokenHelper();
                    string token = tokenService.GetToken();
                    string refreshToken = tokenService.GetToken();
                    int expiresIn = AccessTokenExpiresIn;
                    string ipAddress = Net.Ip;
                    int createTimestamp = TimeHelper.GetUnixTimestamp();
                    var createResult = AccessTokenService.SaveAccessTokenPro(token, refreshToken, systemId, companyId, memberId, uuid, expiresIn, RefreshTokenExpiresIn, ipAddress, createTimestamp);
                    if (createResult)
                    {
                        return Result(logId, new
                        {
                            access_token = token,
                            expiresin = expiresIn,
                            refresh_token = refreshToken,
                            memberid = memberId,
                            scope = "scope"
                        });
                    }
                    return Error(logId, "fail");
                }
                return Error(logId, "login fail！");
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        /// <summary>
        /// 获取会员资料
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("get")]
        public IActionResult GetMember(string access_token)
        {
            long logId = 0;
            try
            {
                int systemId = SystemID;
                logId = BaseApiManager.SaveLogs(access_token);
                if (!IsAccessToken(access_token))
                    return Error(logId, "验证access_token失败！");
                var result = AccountService.GetAccountByAccessTokenPro(systemId, access_token);
                var data = new
                {
                    memberid = result.MemberID,
                    nickname = result.NickName
                };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        /// <summary>
        /// 刷新access_token
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="refresh_token"></param>
        /// <returns>
        /// cgi-bin/v2/member/token/refresh?uuid=460e64203493444ba27d4fc7ad7efae8&refresh_token=
        /// </returns>
        [HttpGet("refresh")]
        [ActionName("token")]
        public IActionResult RefreshToken(string uuid, string refresh_token)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid))
                    return Error(logId, "verify uuid fail！");
                if (string.IsNullOrWhiteSpace(refresh_token))
                    return Error(logId, "refresh token not empty！");
                var tokenService = new Common.Token.TokenHelper();
                string token = tokenService.GetToken();
                string refreshToken = tokenService.GetToken();
                int expiresIn = AccessTokenExpiresIn;
                int refreshTokenExpiresIn = RefreshTokenExpiresIn;
                string ipAddress = Net.Ip;
                int createTimestamp = TimeHelper.GetUnixTimestamp();
                var createResult = AccessTokenService.SaveRefreshTokenPro(refresh_token, token, refreshToken, expiresIn, refreshTokenExpiresIn, ipAddress, createTimestamp);
                if (createResult)
                {
                    var entityMember = AccessTokenService.GetAccessToken(token);
                    return Result(new
                    {
                        access_token = token,
                        expiresin = expiresIn,
                        refresh_token = refreshToken,
                        memberid = entityMember.MemberID,
                        scope = "scope"
                    });
                }
                return Error(logId, "fail");
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// cgi-bin/v2/member/token/verify?access_token=token
        /// </returns>
        [HttpGet("verify")]
        [ActionName("token")]
        public IActionResult VerifyToken(string access_token)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(access_token);
                if (string.IsNullOrWhiteSpace(access_token))
                    return Error(logId, "token not empty！");
                int timestamp = TimeHelper.GetUnixTimestamp();
                var result = AccessTokenService.VerifyAccessTokenPro(access_token, timestamp);
                if (result)
                    return Success(logId, "ok");
                else
                    return Error(logId, "fail");
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }

        #region 私有化方法
        private bool IsLoginParams(JObject formValue)
        {
            try
            {
                if (formValue == null)
                    throw new Exception("params not empty！");
                if (formValue.Property("account") == null)
                    throw new Exception("lack account params！");
                if (formValue.Property("password") == null)
                    throw new Exception("lack password params！");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private bool IsRegisterParams(JObject formValue)
        {
            try
            {
                if (formValue == null)
                    throw new Exception("params not empty！");
                if (formValue.Property("phone") == null)
                    throw new Exception("lack phone params！");
                if (formValue.Property("password") == null)
                    throw new Exception("lack password params！");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}