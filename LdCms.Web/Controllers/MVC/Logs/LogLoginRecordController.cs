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
    /// 系统日志管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class LogLoginRecordController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly ILoginRecordService LoginRecordService;
        private readonly ITableOperationManager<Ld_Log_LoginRecord> TableOperationManager;
        public LogLoginRecordController(IBaseManager BaseManager, ILoginRecordService LoginRecordService, ITableOperationManager<Ld_Log_LoginRecord> TableOperationManager) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.LoginRecordService = LoginRecordService;
            this.TableOperationManager = TableOperationManager;
            TableOperationManager.Account = StaffID;
            TableOperationManager.NickName = StaffName;
        }
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.系统日志.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string clientId = GetQueryString("clientId");
                string keyword = GetQueryString("keyword");
                ViewBag.DateMin = startTime;
                ViewBag.DateMax = endTime;
                ViewBag.ClientID = clientId;
                ViewBag.Keyword = keyword;

                int totalNum = 0;
                List<Ld_Log_LoginRecord> lists = new List<Ld_Log_LoginRecord>();
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(startTime))
                    lists = LoginRecordService.GetLoginRecordPaging(SystemID, CompanyID, 1, 100, out totalNum);
                else
                {
                    lists = LoginRecordService.SearchLoginRecord(SystemID, CompanyID, startTime, endTime, clientId, keyword);
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

        [HttpPost]
        public IActionResult Delete(long id)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.系统日志.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                long operationId = 0;
                var entity = LoginRecordService.GetLoginRecord(id);
                if (entity == null)
                    return Error("id not exists！");
                TableOperationManager.Delete(entity, out operationId);
                bool result = LoginRecordService.DeleteLoginRecord(SystemID, CompanyID, id);
                TableOperationManager.SetState(operationId, result);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.系统日志.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                foreach (var item in arrId)
                {
                    long id = Convert.ToInt64(item);
                    long operationId = 0;
                    var entity = LoginRecordService.GetLoginRecord(id);
                    if (entity == null)
                        return Error("id not exists！");
                    TableOperationManager.Delete(entity, out operationId);
                    bool result = LoginRecordService.DeleteLoginRecord(SystemID, CompanyID, id);
                    TableOperationManager.SetState(operationId, result);
                }
                return Success("成功！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult DeleteAll()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.系统日志.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                bool result = LoginRecordService.DeleteLoginRecordAll(SystemID, CompanyID);
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

    }
}