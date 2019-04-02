using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Sys;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 系统设置控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class SysConfigController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IConfigService ConfigService;
        private readonly ITableOperationManager<Ld_Sys_Config> TableOperationManager;
        public SysConfigController(IBaseManager BaseManager, IConfigService ConfigService, ITableOperationManager<Ld_Sys_Config> TableOperationManager) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.ConfigService = ConfigService;
            this.TableOperationManager = TableOperationManager;
            TableOperationManager.Account = StaffID;
            TableOperationManager.NickName = StaffName;
        }

        public override IActionResult Index()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.系统设置.查看);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                long operationId = 0;
                var entity = ConfigService.GetConfigPro(SystemID, CompanyID);
                TableOperationManager.Select(entity, out operationId);
                TableOperationManager.SetState(operationId, true);
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Shielding()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.屏蔽词.查看);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                long operationId = 0;
                var entity = ConfigService.GetConfigPro(SystemID, CompanyID);
                TableOperationManager.Select(entity, out operationId);
                ViewBag.Shielding = entity.Shielding;
                TableOperationManager.SetState(operationId, true);
                return View();
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Update()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.系统设置.编辑);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string title = GetFormValue("title");
                string keyword = GetFormValue("keyword");
                string description = GetFormValue("description");
                string homeUrl = GetFormValue("homeUrl");
                string styleSrc = GetFormValue("styleSrc");
                string uploadRoot = GetFormValue("uploadRoot");
                string copyright = GetFormValue("copyright");
                string icpNumber = GetFormValue("icpNumber");
                string statisticsCode = GetFormValue("statisticsCode");
                string loginIpAddressWhiteList = GetFormValue("loginIpAddressWhiteList");
                string maxLoginFail = GetFormValue("maxLoginFail");
                string isLoginIpAddress = GetFormValue("isLoginIpAddress");
                string emailSendPattern = GetFormValue("emailSendPattern");
                string emailHost = GetFormValue("emailHost");
                string emailPort = GetFormValue("emailPort");
                string emailName = GetFormValue("emailName");
                string emailPassword = GetFormValue("emailPassword");
                string emailAddress = GetFormValue("emailAddress");
                bool isLogin = isLoginIpAddress.Split(",")[0].ToBool();

                Ld_Sys_Config entity = new Ld_Sys_Config();
                entity.SystemID = SystemID;
                entity.CompanyID = CompanyID;
                entity.Title = title;
                entity.Keyword = keyword;
                entity.Description = description;
                entity.HomeUrl = homeUrl;
                entity.StyleSrc = styleSrc;
                entity.UploadRoot = uploadRoot;
                entity.Copyright = copyright;
                entity.IcpNumber = icpNumber;
                entity.StatisticsCode = statisticsCode;
                entity.LoginIpAddressWhiteList = loginIpAddressWhiteList;
                entity.MaxLoginFail = maxLoginFail.ToInt();
                entity.IsLoginIpAddress = isLogin;
                entity.EmailSendPattern = emailSendPattern;
                entity.EmailHost = emailHost;
                entity.EmailPort = emailPort.ToInt();
                entity.EmailName = emailName;
                entity.EmailPassword = emailPassword;
                entity.EmailAddress = emailAddress;
                entity.CreateDate = DateTime.Now;

                var m = ConfigService.GetConfigPro(SystemID, CompanyID);
                if (m == null)
                    return Error("公司编号不存在！");
                long operationId = 0;
                TableOperationManager.Update(m, entity.ToJson(),out operationId);
                bool result = ConfigService.UpdateConfigPro(entity);
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
        public JsonResult UpdateShielding()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.系统管理.屏蔽词.编辑);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string shielding = GetFormValue("Shielding");
                var m = ConfigService.GetConfigPro(SystemID, CompanyID);
                if (m == null)
                    return Error("公司编号不存在！");
                long operationId = 0;
                var entity = m;
                entity.Shielding = shielding;
                TableOperationManager.Update(m, entity.ToJson(), out operationId);
                var result = ConfigService.UpdateConfigShieldingPro(SystemID, CompanyID, shielding);
                TableOperationManager.SetState(operationId, result);

                return Success("ok");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

    }
}