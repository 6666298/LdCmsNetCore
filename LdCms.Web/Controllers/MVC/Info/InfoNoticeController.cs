using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Info
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Info;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.Common.Json;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InfoNoticeController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly INoticeService NoticeService;
        private readonly INoticeCategoryService NoticeCategoryService;
        public InfoNoticeController(IBaseManager BaseManager, INoticeService NoticeService, INoticeCategoryService NoticeCategoryService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.NoticeService = NoticeService;
            this.NoticeCategoryService = NoticeCategoryService;
        }
        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string classId = GetQueryString("classId");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["ClassId"] = classId;
                ViewData["State"] = state;
                ViewData["Keyword"] = keyword;

                int total = 100;
                List<Ld_Info_Notice> lists = new List<Ld_Info_Notice>();
                string strKeyword = string.Format("{0}{1}{2}", startTime, classId, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = NoticeService.GetNoticeTop(SystemID, CompanyID, total);
                else
                    lists = NoticeService.SearchNotice(SystemID, CompanyID, startTime, endTime, classId, state, keyword);
                ViewData["Count"] = NoticeService.CountNotice(SystemID, CompanyID);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string noticeId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsAddPermission(noticeId, out funcId)) { return ToPermission(funcId); }
                    
                if (string.IsNullOrWhiteSpace(noticeId))
                    return View(new Ld_Info_Notice());
                var entity = NoticeService.GetNotice(SystemID, CompanyID, noticeId);
                if (entity == null)
                    return View(new Ld_Info_Notice());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 操作处理方法
        [HttpPost]
        public JsonResult Save(string noticeId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsSavePermission(noticeId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                string fTitle = GetFormValue("fTitle");
                string fClassId = GetFormValue("fClassId");
                string fClassName = GetFormValue("fClassName");
                string fImgSrc = GetFormValue("fImgSrc");
                string fImgArray = GetFormValueArr("fImgArray[]");
                string fAuthor = GetFormValue("fAuthor");
                string fKeyword = GetFormValue("fKeyword");
                string fDescription = GetFormValue("fDescription");
                string fContent = GetFormValue("fContent");
                string fState = GetFormValue("fState");

                if (string.IsNullOrWhiteSpace(fTitle))
                    return Error("标题不能为空！");
                if (string.IsNullOrWhiteSpace(fContent))
                    return Error("内容不能为空！");
                if (string.IsNullOrWhiteSpace(fImgSrc) && !string.IsNullOrWhiteSpace(fImgArray))
                    fImgSrc = fImgArray.Split(",")[0];

                string imgArray = string.IsNullOrWhiteSpace(fImgArray) ? "" : fImgArray.Split(",").ToJson();
                string author = string.IsNullOrWhiteSpace(fAuthor) ? StaffName : fAuthor;
                string keyword = string.IsNullOrWhiteSpace(fKeyword) ? WebHelper.NoHtml(Utility.ContentDecode(fContent)).Left(100) : fKeyword;
                string description = string.IsNullOrWhiteSpace(fDescription) ? WebHelper.NoHtml(Utility.ContentDecode(fContent)).Left(100) : fDescription;


                bool state = fState.ToBool();
                Ld_Info_Notice entity = new Ld_Info_Notice()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    NoticeID = noticeId,
                    Title = fTitle,
                    ClassID = fClassId,
                    ClassName = fClassName,
                    ImgSrc = fImgSrc,
                    ImgArray= imgArray,
                    Author = author,
                    Keyword = keyword,
                    Description = description,
                    Content = fContent,
                    StaffId = StaffID,
                    StaffName = StaffName,
                    State = state,
                    CreateDate = DateTime.Now
                };
                bool result = false;
                if (string.IsNullOrEmpty(noticeId))
                    result = NoticeService.SaveNotice(entity);
                else
                    result = NoticeService.UpdateNotice(entity);
                if (result)
                    return Success("ok");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult UpdateState(string noticeId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.审核);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                var result = NoticeService.UpdateNoticeState(SystemID, CompanyID, noticeId, state);
                if (result)
                    return Success("ok");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult Delete(string noticeId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                var result = NoticeService.DeleteNotice(SystemID, CompanyID, noticeId);
                if (result)
                    return Success("ok");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult DeleteBatch(string[] arrId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string noticeId = item;
                    try
                    {
                        bool result = NoticeService.DeleteNotice(SystemID, CompanyID, noticeId);
                        lists.Add(new { category_id = noticeId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { category_id = noticeId, result = false, message = ex.Message });
                    }
                }
                return Success("成功！", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region 辅助方法
        [HttpGet]
        [ActionName("category-list-get")]
        public JsonResult GetNoticeCategory()
        {
            try
            {
                bool state = true;
                var lists = NoticeCategoryService.GetNoticeCategoryByState(SystemID, CompanyID, state.ToString());
                if (lists == null)
                    return Error("not data！");
                var data = from m in lists
                           select new
                           {
                               id = m.CategoryID,
                               name = m.CategoryName
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 私有化方法
        public bool IsSavePermission(string noticeId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(noticeId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string noticeId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(noticeId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.公告信息.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}