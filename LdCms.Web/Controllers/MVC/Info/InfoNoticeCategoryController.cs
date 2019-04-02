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
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InfoNoticeCategoryController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly INoticeCategoryService NoticeCategoryService;
        public InfoNoticeCategoryController(IBaseManager BaseManager, INoticeCategoryService NoticeCategoryService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }
                    
                List<Ld_Info_NoticeCategory> lists = NoticeCategoryService.GetNoticeCategory(SystemID, CompanyID);
                ViewBag.Count = lists.Count();
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string categoryId = "")
        {
            try
            {
                string funcId = string.Empty;
                if (!IsAddPermission(categoryId, out funcId)) { return ToPermission(funcId); }
                    
                if (string.IsNullOrWhiteSpace(categoryId))
                    return View(new Ld_Info_NoticeCategory());
                var entity = NoticeCategoryService.GetNoticeCategory(SystemID, CompanyID, categoryId);
                if (entity == null)
                    return View(new Ld_Info_NoticeCategory());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult Save(string categoryId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsSavePermission(categoryId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                string fCategoryName = GetFormValue("fCategoryName");
                string fSort = GetFormValue("fSort");
                string fRemark = GetFormValue("fRemark");
                string fState = GetFormValue("fState");
                if (string.IsNullOrWhiteSpace(fCategoryName))
                    return Error("名称不能为空！");
                int sort = fSort.ToInt();
                bool state = fState.ToBool();
                Ld_Info_NoticeCategory entity = new Ld_Info_NoticeCategory()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    CategoryID = categoryId,
                    CategoryName = fCategoryName,
                    Sort = sort,
                    Remark = fRemark,
                    State = state
                };
                bool result = false;
                if (string.IsNullOrEmpty(categoryId))
                    result = NoticeCategoryService.SaveNoticeCategory(entity);
                else
                    result = NoticeCategoryService.UpdateNoticeCategory(entity);
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
        public JsonResult UpdateState(string categoryId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.审核);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                var result = NoticeCategoryService.UpdateNoticeCategoryState(SystemID, CompanyID, categoryId, state);
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
        public JsonResult Delete(string categoryId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                var result = NoticeCategoryService.DeleteNoticeCategory(SystemID, CompanyID, categoryId);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string categoryId = item;
                    try
                    {
                        bool result = NoticeCategoryService.DeleteNoticeCategory(SystemID, CompanyID, categoryId);
                        lists.Add(new { category_id = categoryId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { category_id = categoryId, result = false, message = ex.Message });
                    }
                }
                return Success("成功！", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #region 私有化方法
        public bool IsSavePermission(string categoryId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(categoryId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string categoryId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公告管理.类别管理.编辑);
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