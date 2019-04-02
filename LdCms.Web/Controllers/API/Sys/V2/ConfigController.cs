using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.API.Sys.V2
{
    using LdCms.IBLL.Log;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
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
        /// <param name="uuid"></param>
        /// <returns>
        /// cgi-bin/v2/config/get?uuid=460e64203493444ba27d4fc7ad7efae8
        /// </returns>
        [HttpGet]
        [ActionName("get")]
        public IActionResult GetConfig(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid))
                    return Error(logId, "uuid invalid！");
                var entity = GetInterfaceAccountByUuid(uuid);
                string companyId = entity.CompanyID;
                var result = ConfigService.GetConfig(SystemID, companyId);
                var data = new
                {
                    title = result.Title,
                    keywords = result.Keyword,
                    description = result.Description
                };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }



    }
}