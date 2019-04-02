using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    /// API访问跨域管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class SysAccessCorsHostController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IAccessCorsHostService AccessCorsHostService;
        public SysAccessCorsHostController(IBaseManager BaseManager, IAccessCorsHostService AccessCorsHostService) :base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.AccessCorsHostService = AccessCorsHostService;
        }
        public override IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Show()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.接口设置.查看);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);

                return View();
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("AccessCorsHost-List-Get")]
        public JsonResult GetAccessCorsHost(string account)
        {
            try
            {
                var lists = AccessCorsHostService.GetAccessCorsHostAllPro(SystemID);
                if (lists == null)
                    return Error("not data！");
                var data = from m in lists
                           select new
                           {
                               web_host = m.WebHost,
                               state = m.State.ToBool() ? "启用" : "停用",
                               create_date = m.CreateDate.Value.ToString("yyyy-MM-dd HH:mm")
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Save()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.接口设置.新增);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string fWebHost = GetFormValue("fWebHost");
                string fRemark = GetFormValue("fRemark");
                string fState = GetFormValue("fState");
                bool state = fState.ToBool();
                var result = AccessCorsHostService.SaveAccessCorsHostPro(SystemID, fWebHost, fRemark, StaffID, StaffName, state);
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
        public JsonResult Delete(string webHost)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.接口设置.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                var result = AccessCorsHostService.DeleteAccessCorsHostPro(SystemID, webHost);
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


    }

}