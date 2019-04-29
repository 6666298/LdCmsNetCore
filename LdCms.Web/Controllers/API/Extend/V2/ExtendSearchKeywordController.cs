using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.API.Extend.V2
{
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.IBLL.Extend;
    using LdCms.Web.Services;

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("extend/search")]
    public class ExtendSearchKeywordController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly ISearchKeywordService SearchKeywordService;
        public ExtendSearchKeywordController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, ISearchKeywordService SearchKeywordService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.SearchKeywordService = SearchKeywordService;
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

        [HttpGet("top")]
        [ActionName("keyword")]
        public IActionResult GetArticeTop(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string memberId = Accessor.HttpContext.Request.GetQueryString("memberid");
                int count = Utility.ToPageIndex(Accessor.HttpContext.Request.GetQueryString("count").ToInt());
                var lists = SearchKeywordService.GetSearchKeywordByTop(SystemID, companyId, memberId, count);
                if (lists == null)
                    return Error("not data!");
                var data = from m in lists
                           select new
                           {
                               keyword = m.Keyword.IIF(string.Empty),
                               hits = m.Hits.ToInt()
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