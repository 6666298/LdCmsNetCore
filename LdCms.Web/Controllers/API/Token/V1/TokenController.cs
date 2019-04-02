using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LdCms.Web.Controllers.API.Token.V1
{
    using LdCms.Common.Net;
    using LdCms.Common.Time;
    using LdCms.IBLL.Log;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;
    /// <summary>
    /// 全局Token
    /// 功能：
    ///     1、获取Token URL：cgi-bin/v1/token/get?appid=appid&secret=AppSecret
    ///     2、验证Token URL：cgi-bin/v1/token/verify?token=token
    ///     
    /// 
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class TokenController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IInterfaceAccountService InterfaceAccountService;
        private readonly IInterfaceAccessTokenService InterfaceAccessTokenService;
        private readonly IInterfaceAccessWhiteListService InterfaceAccessWhiteListService;
        public TokenController(IBaseApiManager BaseApiManager, IInterfaceAccountService InterfaceAccountService, IInterfaceAccessTokenService InterfaceAccessTokenService, IInterfaceAccessWhiteListService InterfaceAccessWhiteListService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.InterfaceAccountService = InterfaceAccountService;
            this.InterfaceAccessTokenService = InterfaceAccessTokenService;
            this.InterfaceAccessWhiteListService = InterfaceAccessWhiteListService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("index")]
        public IActionResult Index()
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs("");

                return Result(logId, "index");
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="name"></param>
        /// <returns>
        /// cgi-bin/v1/token/get?appid=Ld_5RFvLWnk57XAQ&secret=WdVIfzsl6yBAHksuxStbrs3m7xDitWzf
        /// </returns>
        [HttpGet]
        [ActionName("get")]
        public IActionResult GetToken(string appId, string secret)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(appId);
                if (string.IsNullOrWhiteSpace(appId))
                    return Error(logId, "appid not empty！");
                if (string.IsNullOrWhiteSpace(secret))
                    return Error(logId, "secret not empty！");

                int tokenTotalNumber = InterfaceAccessTokenService.GetInterfaceAccessTokenTotalNumberPro(SystemID, appId);
                string token = new Common.Token.TokenHelper(appId, secret).GetToken(tokenTotalNumber);
                int expiresIn = AccessTokenExpiresIn;
                string ipAddress = Net.Ip;
                int createTimestamp = TimeHelper.GetUnixTimestamp();

                bool IsIpAddress = InterfaceAccessWhiteListService.VerifyInterfaceAccessWhiteListByAppIdPro(SystemID, appId, ipAddress);
                if (!IsIpAddress)
                    return Error(logId, "ip verify fail！");

                bool createResult = InterfaceAccessTokenService.SaveInterfaceAccessTokenAutoPro(token, SystemID, appId, secret, expiresIn, ipAddress, createTimestamp);
                if (createResult)
                    return Result(logId, new { token, expiresin = expiresIn });
                else
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
        /// cgi-bin/v1/token/verify?token=token
        /// </returns>
        [HttpGet]
        [ActionName("verify")]
        public IActionResult VerifyToken(string token)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(token);
                if (string.IsNullOrWhiteSpace(token))
                    return Error(logId, "token not empty！");
                int timestamp = TimeHelper.GetUnixTimestamp();
                var result = InterfaceAccessTokenService.VerifyInterfaceAccessTokenPro(token, timestamp);
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

    }
}