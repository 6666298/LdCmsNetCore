using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Sys
{
    using LdCms.IBLL.Sys;
    using LdCms.Common.Extension;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 系统角色管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class SysRoleController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IFunctionService FunctionService;
        private readonly IRoleService RoleService;
        public SysRoleController(IBaseManager BaseManager, IFunctionService FunctionService, IRoleService RoleService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.FunctionService = FunctionService;
            this.RoleService = RoleService;
        }

        public override IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string keyword = GetQueryString("keyword");
                ViewBag.datemin = startTime;
                ViewBag.datemax = endTime;
                ViewBag.keyword = keyword;

                int pageId = 1;
                int pageSize = 100;
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(startTime))
                {
                    int totalNum = 0;
                    var list = RoleService.GetRolePagingPro(SystemID, CompanyID, pageId, pageSize, out totalNum);
                    ViewBag.Count = totalNum;
                    return View(list);
                }
                else
                {
                    var list = RoleService.SearchRolePro(SystemID, CompanyID, startTime, endTime, keyword);
                    ViewBag.Count = list.Count();
                    return View(list);
                }
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.新增);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);

                string parentId = "";
                var lists = FunctionService.GetFunctionByParentIdPro(parentId);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Edit(string roleId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.编辑);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);

                var entity = RoleService.GetRolePro(SystemID, CompanyID, roleId);
                if (entity == null)
                    return ToError("角色ID错误！");

                var lists = RoleService.GetRoleFunctionSelectPro(SystemID, CompanyID, roleId);
                ViewData["RoleID"] = entity.RoleID;
                ViewData["RoleName"] = entity.RoleName;
                ViewData["Remark"] = entity.Remark;
                ViewData["State"] = entity.State;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 操作方法
        [HttpPost]
        public JsonResult Save()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.新增);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string roleId = GetFormValue("fRoleId");
                string roleName = GetFormValue("fRoleName");
                string remark = GetFormValue("fRemark");
                string funcid = GetFormValueArr("fFuncId");
                string fState = GetFormValue("fState");

                if (string.IsNullOrWhiteSpace(roleId))
                    return Error("角色ID不能为空！");
                if (string.IsNullOrWhiteSpace(roleName))
                    return Error("角色名称不能为空！");
                if (string.IsNullOrWhiteSpace(funcid))
                    return Error("请选择角色功能列表！");

                bool state = fState.ToBool();

                var result = RoleService.SaveRolePro(SystemID, CompanyID, roleId, roleName, funcid, remark, state);
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
        public JsonResult Update(string roleId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.编辑);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string roleName = GetFormValue("fRoleName");
                string remark = GetFormValue("fRemark");
                string funcid = GetFormValueArr("fFuncId");
                string fState = GetFormValue("fState");

                if (roleId=="2001")
                    return Error("初始化角色不可编辑！");
                if (string.IsNullOrWhiteSpace(roleId))
                    return Error("角色ID不能为空！");
                if (string.IsNullOrWhiteSpace(roleName))
                    return Error("角色名称不能为空！");
                if (string.IsNullOrWhiteSpace(funcid))
                    return Error("请选择角色功能列表！");

                bool state = fState.ToBool();
                var result = RoleService.UpdateRolePro(SystemID, CompanyID, roleId, roleName, funcid, remark, state);
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
        public JsonResult UpdateState(string roleId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.审核);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                if (roleId == "2001")
                    return Error("初始化角色不可变更状态！");
                string fState = GetFormValue("State");
                bool state = fState.ToBool();
                var result = RoleService.UpdateRoleStatePro(SystemID, CompanyID, roleId, state);
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
        public JsonResult Delete(string roleId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                if (string.IsNullOrEmpty(roleId))
                    return Error("角色ID不能为空！");
                var result = RoleService.DeleteRolePro(SystemID, CompanyID, roleId);
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
        public JsonResult DeleteBatch(string[] arrRoleId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.角色管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                List<object> lists = new List<object>();
                foreach (var roleId in arrRoleId)
                {
                    try
                    {
                        bool result = RoleService.DeleteRolePro(SystemID, CompanyID, roleId);
                        lists.Add(new { role_id = roleId, result, message = "成功" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { role_id = roleId, result = false, message = ex.Message });
                    }
                }
                return Success("ok", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #endregion
    }
}