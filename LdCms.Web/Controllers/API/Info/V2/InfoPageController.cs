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
    using LdCms.Common.Utility;
    using LdCms.Common.Json;
    using LdCms.IBLL.Info;
    using LdCms.Web.Services;
    

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("info/page")]
    public class InfoPageController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IPageService PageService;
        public InfoPageController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IPageService PageService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.PageService = PageService;
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
        public IActionResult GetPage(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string classId = Accessor.HttpContext.Request.GetQueryString("classid");
                var entity = PageService.GetPageByClassId(SystemID, companyId, classId);
                if (entity == null) { return Error(logId, "classid invalid！"); }
                var data = new
                {
                    id = entity.PageID,
                    class_id = entity.ClassID,
                    title = entity.Title.IIF(string.Empty),
                    image_src = entity.ImgSrc.IIF(string.Empty),
                    image_src_array = ToImgArray(entity.ImgArray),
                    keyword = entity.Keyword.IIF(string.Empty),
                    description = entity.Description.IIF(string.Empty),
                    content = entity.Content.IIF(string.Empty),
                    date = entity.CreateDate
                };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }

        #region 私有化方法
        public List<string> ToImgArray(string imageArray)
        {
            try
            {
                if (string.IsNullOrEmpty(imageArray))
                    return new List<string>();
                return imageArray.ToObject<List<string>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}