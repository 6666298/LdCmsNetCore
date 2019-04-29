using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace LdCms.Web.Controllers.API.Info.V2
{
    using LdCms.Common.Extension;
    using LdCms.IBLL.Info;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("info/block")]
    public class InfoBlockController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IBlockService BlockService;
        public InfoBlockController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IBlockService BlockService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.BlockService = BlockService;
        }

        public IActionResult Index(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }


                return Success(logId, "ok", uuid);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet]
        [ActionName("get")]
        public IActionResult GetBlock(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string state = "true";
                string tags = Accessor.HttpContext.Request.GetQueryString("tags");
                var entity = BlockService.GetBlock(SystemID, companyId, tags, state);
                if (entity == null) { return Error(logId, "tags invalid！"); }
                var data = new
                {
                    id = entity.BlockID,
                    title = entity.Title,
                    tags = entity.Tags,
                    content = entity.Content,
                    date = entity.CreateDate
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