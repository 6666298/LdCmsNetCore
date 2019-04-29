using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    [ControllerName("info/notice")]
    public class InfoNoticeController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly INoticeCategoryService NoticeCategoryService;
        private readonly INoticeService NoticeService;
        public InfoNoticeController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, INoticeCategoryService NoticeCategoryService, INoticeService NoticeService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.NoticeCategoryService = NoticeCategoryService;
            this.NoticeService = NoticeService;
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
        [HttpGet("list")]
        [ActionName("category")]
        public IActionResult GetNoticeCategoryAll(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string state = "true";
                var lists = NoticeCategoryService.GetNoticeCategoryByState(SystemID, companyId, state);
                var data = from m in lists
                           select new
                           {
                               id = m.CategoryID,
                               name = m.CategoryName
                           };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet("top")]
        [ActionName("list")]
        public IActionResult GetNoticeTop(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string classId = Accessor.HttpContext.Request.GetQueryString("classid");
                int count = Utility.ToPageIndex(Accessor.HttpContext.Request.GetQueryString("count").ToInt());
                string state = "true";
                var lists = NoticeService.GetNoticeTop(SystemID, companyId, classId, state, count);
                var data = from m in lists
                           select new
                           {
                               id = m.NoticeID,
                               title = m.Title,
                               class_id = m.ClassID,
                               class_name = m.ClassName,
                               author = m.Author,
                               image_src = m.ImgSrc,
                               keyword = m.Keyword.IIF(string.Empty),
                               description = m.Description.IIF(string.Empty),
                               date = m.CreateDate
                           };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet("paging")]
        [ActionName("list")]
        public IActionResult GetNoticePaging(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string classId = Accessor.HttpContext.Request.GetQueryString("classid");
                int pageIndex = Utility.ToPageIndex(Accessor.HttpContext.Request.GetQueryString("page").ToInt());
                int pageCount = Utility.ToPageCount(Accessor.HttpContext.Request.GetQueryString("count").ToInt());
                string state = "true";
                int total = NoticeService.CountNotice(SystemID, companyId, classId, state);
                var lists = NoticeService.GetNoticePaging(SystemID, companyId, classId, state, pageIndex, pageCount);
                var data = from m in lists
                           select new
                           {
                               id = m.NoticeID,
                               title = m.Title,
                               class_id = m.ClassID,
                               class_name = m.ClassName,
                               author = m.Author,
                               image_src = m.ImgSrc,
                               keyword = m.Keyword.IIF(string.Empty),
                               description = m.Description.IIF(string.Empty),
                               date = m.CreateDate
                           };
                return Success(logId, "ok", new { page = pageIndex, total, rows = data });
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet]
        [ActionName("get")]
        public IActionResult GetNotice(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string id = Accessor.HttpContext.Request.GetQueryString("id");
                var entity = NoticeService.GetNotice(SystemID, companyId, id);
                var data = new
                {
                    id = entity.NoticeID,
                    title = entity.Title,
                    class_id = entity.ClassID,
                    class_name = entity.ClassName,
                    author = entity.Author,
                    image_src = entity.ImgSrc,
                    image_src_array = ToImgArray(entity.ImgArray),
                    keyword = entity.Keyword.IIF(string.Empty),
                    description = entity.Description.IIF(string.Empty),
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