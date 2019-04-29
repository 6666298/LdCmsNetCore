using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;

namespace LdCms.Web.Controllers.API.Info.V2
{
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Json;
    using LdCms.Common.Net;
    using LdCms.Common.Enum;
    using LdCms.IBLL.Info;
    using LdCms.Web.Services;
    using LdCms.IBLL.Extend;
    using LdCms.EF.DbModels;

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("info/artice")]
    public class InfoArticeController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IArticeService ArticeService;
        private readonly ISearchKeywordService SearchKeywordService;
        public InfoArticeController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IArticeService ArticeService, ISearchKeywordService SearchKeywordService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.ArticeService = ArticeService;
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
        [ActionName("list")]
        public IActionResult GetArticeTop(string uuid)
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
                bool delete = false;
                var lists = ArticeService.GetArticeTop(SystemID, companyId, classId, state, delete, count);
                var data = from m in lists
                           select new
                           {
                               id = m.ArticeID,
                               title = m.Title.IIF(string.Empty),
                               title_brief=m.TitleBrief.IIF(string.Empty),
                               class_id = m.ClassID,
                               class_name = m.ClassName,
                               author = m.Author.IIF(string.Empty),
                               source=m.Source.IIF(string.Empty),
                               image_src = m.ImgSrc.IIF(string.Empty),
                               hits = m.Hits.ToInt(),
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
        public IActionResult GetArticePaging(string uuid)
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
                bool delete = false;
                int total = ArticeService.CountArtice(SystemID, companyId, classId, state, delete);
                var lists = ArticeService.GetArticePaging(SystemID, companyId, classId, state, delete, pageIndex, pageCount);
                if (lists == null) { return Error("not data!"); }
                var data = from m in lists
                           select new
                           {
                               id = m.ArticeID,
                               title = m.Title.IIF(string.Empty),
                               title_brief = m.TitleBrief.IIF(string.Empty),
                               class_id = m.ClassID,
                               class_name = m.ClassName,
                               author = m.Author.IIF(string.Empty),
                               source = m.Source.IIF(string.Empty),
                               image_src = m.ImgSrc.IIF(string.Empty),
                               hits = m.Hits.ToInt(),
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
        public IActionResult GetArtice(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string id = Accessor.HttpContext.Request.GetQueryString("id");
                var entity = ArticeService.GetArtice(SystemID, companyId, id);
                if (entity == null) { return Error(logId, "id invalid！"); }
                var data = new
                {
                    id = entity.ArticeID,
                    title = entity.Title.IIF(string.Empty),
                    title_brief = entity.TitleBrief.IIF(string.Empty),
                    class_id = entity.ClassID,
                    class_name = entity.ClassName,
                    author = entity.Author.IIF(string.Empty),
                    source = entity.Source.IIF(string.Empty),
                    image_src = entity.ImgSrc.IIF(string.Empty),
                    image_src_array = ToImgArray(entity.ImgArray),
                    hits = entity.Hits.ToInt(),
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
        [HttpPost]
        [ActionName("search")]
        public IActionResult SearchArtice(string uuid, [FromBody]JObject fromValue)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                bool verifyParams = IsSearchArticeParams(fromValue);
                string keyword = GetJObjectValue(fromValue, "keyword");
                string memberId = GetJObjectValue(fromValue, "memberid");
                string clientId = GetJObjectValue(fromValue, "clientid");
                if (string.IsNullOrWhiteSpace(keyword))
                    return Error("关建字不能为空！");

                bool saveResult = SaveSearchKeyword(companyId, memberId, clientId.ToInt(), keyword);
                var lists = ArticeService.SearchArtice(SystemID, companyId, keyword);
                if (lists == null) { return Error("not data!"); }
                var data = from m in lists
                           select new
                           {
                               id = m.ArticeID,
                               title = m.Title.IIF(string.Empty),
                               title_brief = m.TitleBrief.IIF(string.Empty),
                               class_id = m.ClassID,
                               class_name = m.ClassName,
                               author = m.Author.IIF(string.Empty),
                               source = m.Source.IIF(string.Empty),
                               image_src = m.ImgSrc.IIF(string.Empty),
                               hits = m.Hits.ToInt(),
                               keyword = m.Keyword.IIF(string.Empty),
                               description = m.Description.IIF(string.Empty),
                               date = m.CreateDate
                           };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
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
        private bool IsSearchArticeParams(JObject formValue)
        {
            try
            {
                if (formValue == null)
                    throw new Exception("params not empty！");
                if (formValue.Property("keyword") == null)
                    throw new Exception("lack keyword params！");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private bool SaveSearchKeyword(string companyId, string memberId, int clientId, string keyword)
        {
            try
            {
                return SearchKeywordService.SaveSearchKeyword(new Ld_Extend_SearchKeyword()
                {
                    SystemID = SystemID,
                    CompanyID = companyId,
                    MemberID = memberId,
                    ClientID = clientId.ToByte(),
                    ClientName = EnumHelper.GetName<ParamEnum.Client>(clientId),
                    Keyword = keyword,
                    IpAddress = Net.Ip,
                    Hits = 1,
                    State = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}