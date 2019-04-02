using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.API.Sys.V1
{
    using LdCms.Common.Json;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    public partial class ConfigController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IConfigService ConfigService;
        public ConfigController(IBaseApiManager BaseApiManager, IConfigService ConfigService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.ConfigService = ConfigService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("index")]
        public IActionResult Index()
        {
            try
            {
                return Result("index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// cgi-bin/v2/config/get?token=token
        /// </returns>
        [HttpGet]
        [ActionName("get")]
        public IActionResult GetConfig(string token)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(token);
                if (!IsToken(token))
                    return Error(logId, "token invalid！");
                var entity = GetInterfaceAccountByToken(token);
                string companyId = entity.CompanyID;
                var result = ConfigService.GetConfig(SystemID, companyId);
                var data = new
                {
                    title = result.Title
                };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId,ex.Message);
            }
        }


    }
}