using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Logs
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Log;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 系统操作记录管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class LogTableOperationController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly ITableOperationService TableOperationService;
        public LogTableOperationController(IBaseManager BaseManager, ITableOperationService TableOperationService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.TableOperationService = TableOperationService;
        }

        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.操作记录.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string clientId = GetQueryString("clientId");
                string classId = GetQueryString("classId");
                string keyword = GetQueryString("keyword");
                ViewBag.DateMin = startTime;
                ViewBag.DateMax = endTime;
                ViewBag.ClientID = clientId;
                ViewBag.ClassID = classId;
                ViewBag.Keyword = keyword;

                int totalNum = 0;
                List<Ld_Log_TableOperation> lists = new List<Ld_Log_TableOperation>();
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(startTime))
                    lists = TableOperationService.GetTableOperationPaging(1, 100, out totalNum);
                else
                {
                    lists = TableOperationService.SearchTableOperation(startTime, endTime, clientId, classId, keyword);
                    totalNum = lists.Count();
                }
                ViewBag.Count = totalNum;
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.操作记录.查看);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = TableOperationService.GetTableOperation(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 操作方法
        [HttpPost]
        public IActionResult Delete(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.操作记录.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                var entity = TableOperationService.GetTableOperation(id);
                if (entity == null)
                    return Error("id not exists！");
                bool result = TableOperationService.DeleteTableOperation(id);
                if (result)
                    return Success("成功！");
                else
                    return Error("失败！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult DeleteBatch(string[] arrId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.操作记录.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                foreach (var item in arrId)
                {
                    long id = Convert.ToInt64(item);
                    bool result = TableOperationService.DeleteTableOperation(id);
                }
                return Success("成功！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

    }
}