using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.Model.Extend;
    using LdCms.IBLL.Extend;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.Common.Net;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class ExtendSearchKeywordController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly ISearchKeywordService SearchKeywordService;
        public ExtendSearchKeywordController(IBaseManager BaseManager, ISearchKeywordService SearchKeywordService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.SearchKeywordService = SearchKeywordService;
        }
        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.搜索管理.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["Keyword"] = keyword;
                int total = 100;
                List<Ld_Extend_SearchKeyword> lists = new List<Ld_Extend_SearchKeyword>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = SearchKeywordService.GetSearchKeyword(SystemID, CompanyID, total);
                else
                    lists = SearchKeywordService.SearchSearchKeyword(SystemID, CompanyID, startTime, endTime, keyword, total);
                ViewData["Count"] = SearchKeywordService.CountSearchKeyword(SystemID, CompanyID);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult ListCount()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.搜索管理.统计);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["Keyword"] = keyword;
                int total = 100;
                List<CountSearchKeywordByKeywordResult> lists = new List<CountSearchKeywordByKeywordResult>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = SearchKeywordService.CountSearchKeywordByKeyword(SystemID, CompanyID, total);
                else
                    lists = SearchKeywordService.CountSearchKeywordByKeyword(SystemID, CompanyID, startTime, endTime, keyword, total);
                ViewData["Count"] = SearchKeywordService.CountSearchKeyword(SystemID, CompanyID);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Show(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.搜索管理.查看);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                var entity = SearchKeywordService.GetSearchKeyword(id);
                if (entity == null)
                    return ToError("ID无效！");
                string keyword = entity.Keyword;
                int total = 10;
                var lists = SearchKeywordService.GetSearchKeywordByKeyword(SystemID, CompanyID, keyword, total);
                ViewData["List"] = lists;
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 操作方法
        [HttpPost]
        public JsonResult UpdateTop(long id, bool top)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.搜索管理.置顶);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                var result = SearchKeywordService.UpdateSearchKeywordTop(id, top);
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
        public JsonResult Delete(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.搜索管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                var result = SearchKeywordService.DeleteSearchKeyword(id);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.搜索管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    long id = item.ToInt();
                    try
                    {
                        bool result = SearchKeywordService.DeleteSearchKeyword(id);
                        lists.Add(new {id, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { id, result = false, message = ex.Message });
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







    }
}