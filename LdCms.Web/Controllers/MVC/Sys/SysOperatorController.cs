using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Sys
{
    using LdCms.Common.Extension;
    using LdCms.Common.Security;
    using LdCms.Common.Utility;
    using LdCms.EF.DbViews;
    using LdCms.IBLL.Institution;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 系统操作员管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class SysOperatorController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IRoleService RoleService;
        private readonly IStaffService StaffService;
        private readonly IOperatorService OperatorService;
        public SysOperatorController(IBaseManager BaseManager, IRoleService RoleService, IStaffService StaffService, IOperatorService OperatorService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.RoleService = RoleService;
            this.StaffService = StaffService;
            this.OperatorService = OperatorService;
        }
        public override IActionResult Index()
        {
            return View();
        }
        
        #region 视图
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string keyword = GetQueryString("keyword");
                ViewBag.DateMin = startTime;
                ViewBag.DateMax = endTime;
                ViewBag.Keyword = keyword;

                int pageId = 1;
                int pageSize = 100;
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(startTime))
                {
                    int totalNum = 0;
                    var lists = OperatorService.GetOperatorPagingPro(SystemID, CompanyID, pageId, pageSize, out totalNum);
                    ViewBag.Count = totalNum;
                    return View(lists);
                }
                else
                {
                    var lists = OperatorService.SearchOperatorPro(SystemID, CompanyID, startTime, endTime, keyword);
                    ViewBag.Count = lists.Count();
                    return View(lists);
                }
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add(string staffId = "")
        {
            try
            {
                string funcId = "";
                if (!IsAddPermission(staffId, out funcId))
                    return ToPermission(funcId);
                var entity = OperatorService.GetOperatorPro(SystemID, CompanyID, staffId);
                if (entity == null)
                    return View(new V_Sys_Operator());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult UpdatePassword(string staffId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.改密);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = OperatorService.GetOperatorPro(SystemID, CompanyID, staffId);
                if (entity == null)
                    return ToError("staff id invalid！");
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        #endregion

        #region 操作方法
        [HttpPost]
        public JsonResult Save(string staffId)
        {
            try
            {
                if (!IsSavePermission(staffId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string fStaffId = GetFormValue("fStaffId");
                string fRoleId = GetFormValue("fRoleId");
                string fRemark = GetFormValue("fRemark");
                string fState = GetFormValue("fState");

                if (string.IsNullOrWhiteSpace(fStaffId))
                    return Error("员工工号不能为空！");
                if (string.IsNullOrWhiteSpace(fRoleId))
                    return Error("角色编号不能为空！");

                string remark = Utility.Left(fRemark, 200);
                bool state = fState.ToBool();

                if (!string.IsNullOrEmpty(staffId))
                {
                    var entity = OperatorService.GetOperatorPro(SystemID, CompanyID, staffId);
                    if (entity != null)
                    {
                        string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.变更角色);
                        if (!IsPermission(funcId))
                            fRoleId = entity.RoleID;
                    }
                }
                var result = false;
                if (string.IsNullOrEmpty(staffId))
                    result = OperatorService.SaveOperatorPro(SystemID, CompanyID, fStaffId, fRoleId, remark, state);
                else
                    result = OperatorService.UpdateOperatorPro(SystemID, CompanyID, staffId, fRoleId, remark, state);
                if (result)
                    return Success("成功");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult UpdatePassword()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.改密);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                string staffId = GetQueryString("staffId");
                string fNewPassword = GetFormValue("fNewPassword");
                string fConfirmPassword = GetFormValue("fConfirmPassword");
                if (string.IsNullOrEmpty(staffId))
                    return Error("员工工号不能为空！");
                if (fConfirmPassword.Length < 6)
                    return Error("密码长度不能少于6位字符！");
                if (fNewPassword != fConfirmPassword)
                    return Error("输入的二次密码不相同！");
                string password = AlgorithmHelper.MD5(fConfirmPassword).ToLower();

                var result = OperatorService.UpdateOperatorPasswordPro(SystemID, CompanyID, staffId, password);
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
        public JsonResult UpdateState(string staffId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.审核);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (staffId == StaffID)
                    return Error("不能操作自已的状态！");
                var result = OperatorService.UpdateOperatorStatePro(SystemID, CompanyID, staffId, state);
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
        public JsonResult Delete(string staffId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (staffId == StaffID)
                    return Error("员工正在使用中，不能删除！");
                var result = OperatorService.DeleteOperatorPro(SystemID, CompanyID, staffId);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string staffId = item;
                    if (staffId != StaffID)
                    {
                        bool result = OperatorService.DeleteOperatorPro(SystemID, CompanyID, staffId);
                        lists.Add(new { staff_id = staffId, result });
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

        #region 辅助接口
        [HttpGet]
        [ActionName("role-list-get")]
        public JsonResult GetRoleList()
        {
            try
            {
                var list = RoleService.GetRoleAllPro(SystemID, CompanyID);
                var data = from m in list
                           where m.RoleID != "2001"
                           orderby m.RoleID
                           select new
                           {
                               role_id = m.RoleID,
                               role_name = m.RoleName
                           };
                var total = data.Count();
                var result = new { total, list = data };
                return Success("成功", result);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("staff-list-get")]
        public JsonResult GetStaffList()
        {
            try
            {
                var list = StaffService.GetVStaffAllPro(SystemID, CompanyID);
                var data = from m in list
                           orderby m.StaffID
                           select new
                           {
                               role_id = m.StaffID,
                               role_name = m.StaffName
                           };
                List<string> arrStaff = new List<string>();
                foreach (var m in data)
                    arrStaff.Add(m.role_id);
                var total = data.Count();
                var result = new { total, list = arrStaff };
                return Success("成功", result);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region 私有化方法
        public bool IsSavePermission(string staffId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(staffId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string staffId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(staffId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.管理员管理.操作员管理.编辑);
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