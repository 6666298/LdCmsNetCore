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
    using LdCms.Common.Extension;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 系统数据字典管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class LogTableController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly ITableService TableService;
        private readonly ITableDetailsService TableDetailsService;
        private readonly ITableOperationManager<Ld_Log_LoginRecord> TableOperationManager;
        public LogTableController(IBaseManager BaseManager, ITableService TableService, ITableDetailsService TableDetailsService, ITableOperationManager<Ld_Log_LoginRecord> TableOperationManager) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.TableService = TableService;
            this.TableDetailsService = TableDetailsService;
            this.TableOperationManager = TableOperationManager;
            TableOperationManager.Account = StaffID;
            TableOperationManager.NickName = StaffName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string keyword = GetQueryString("keyword");
                ViewBag.Keyword = keyword;

                int totalNum = 0;
                List<Ld_Log_Table> lists = new List<Ld_Log_Table>();
                if (string.IsNullOrWhiteSpace(keyword))
                    lists = TableService.GetTablePaging(1, 500, out totalNum);
                else
                {
                    lists = TableService.SearchTable(keyword);
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
        public IActionResult Edit(string tableId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.编辑);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = TableService.GetTable(tableId);
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Details(string tableId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.查看);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = TableService.GetTable(tableId);
                var lists = TableDetailsService.GetTableDetailsByTableID(tableId);
                ViewBag.TableName = entity.TableName;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult EditDetails(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.编辑明细);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = TableDetailsService.GetTableDetails(id);
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 操作方法
        [HttpPost]
        public JsonResult Update(string tableId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.编辑);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string businessName = GetFormValue("fBusinessName");
                string remark = GetFormValue("fRemark");
                if (string.IsNullOrWhiteSpace(businessName))
                    return Error("中文注释不能为空！");
                bool result = TableService.UpdateTableBusinessName(tableId, businessName, remark);
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
        public JsonResult UpdateDetails(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.编辑明细);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string columnText = GetFormValue("fColumnText");
                string remark = GetFormValue("fRemark");
                if (string.IsNullOrWhiteSpace(columnText))
                    return Error("字段中文名称不能为空！");
                bool result = TableDetailsService.UpdateTableDetailsColumnText(id, columnText, remark);
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
        public JsonResult UpdatePrimaryKey(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.数据字典.设置主建);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string fState = GetFormValue("fState");
                bool isPrimaryKey = fState.ToBool();

                bool result = TableDetailsService.UpdateTableDetailsPrimaryKey(id, isPrimaryKey);
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
        public JsonResult InitTable()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.Admins.所有者);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                InitTableManager t = new InitTableManager(TableService, TableDetailsService);
                t.Init();
                return Success("成功");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion
    }
}