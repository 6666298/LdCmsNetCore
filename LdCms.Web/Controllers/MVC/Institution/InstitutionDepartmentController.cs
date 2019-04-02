using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Institution;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 公司部门管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InstitutionDepartmentController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IDepartmentService DepartmentService;
        private readonly ITableOperationManager<Ld_Institution_Department> TableOperationManager;
        public InstitutionDepartmentController(IBaseManager BaseManager, IDepartmentService DepartmentService, ITableOperationManager<Ld_Institution_Department> TableOperationManager) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.DepartmentService = DepartmentService;
            this.TableOperationManager = TableOperationManager;
            TableOperationManager.Account = StaffID;
            TableOperationManager.NickName = StaffName;
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string keyword = GetQueryString("keyword");
                ViewBag.datemin = startTime;
                ViewBag.datemax = endTime;
                ViewBag.keyword = keyword;

                string departmentId = "0";
                string state = "";
                List<Ld_Institution_Department> lists = new List<Ld_Institution_Department>();
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(startTime))
                    lists = DepartmentService.GetDepartmentByNodePathPro(SystemID, CompanyID, departmentId, state);
                else
                    lists = DepartmentService.SearchDepartmentPro(SystemID, CompanyID, startTime, endTime, keyword);
                int totalNum = lists == null ? 0 : lists.Count();
                ViewBag.Count = totalNum;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add(string departmentId = "")
        {
            try
            {
                string funcId = "";
                if (!IsAddPermission(departmentId, out funcId))
                    return ToPermission(funcId);
                var entity = DepartmentService.GetDepartmentPro(SystemID, CompanyID, departmentId);
                if (entity == null)
                    return View(new Ld_Institution_Department());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 操作方法
        [HttpPost]
        public JsonResult Save(string departmentId)
        {
            try
            {
                if (!IsSavePermission(departmentId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string fDepartmentId = GetFormValue("fDepartmentId");
                string fDepartmentName = GetFormValue("fDepartmentName");
                string fParentId = GetFormValue("fParentId");
                string fDescription = GetFormValue("fDescription");
                string fState = GetFormValue("fState");
                bool state = fState.ToBool();

                if (string.IsNullOrWhiteSpace(departmentId))
                {
                    if (string.IsNullOrWhiteSpace(fDepartmentId))
                        return Error("部门编号不能为空！");
                    else
                    {
                        if (fDepartmentId.Length > 6)
                            return Error("部门编号必是6位数字！");
                    }
                    fDepartmentId = fDepartmentId.PadLeft(6, '0');
                }
                
                bool result = false;
                if (string.IsNullOrEmpty(departmentId))
                    result = DepartmentService.SaveDepartmentPro(SystemID, CompanyID, fDepartmentId, fDepartmentName, fParentId, fDescription, state);
                else
                    result = DepartmentService.UpdateDepartmentPro(SystemID, CompanyID, departmentId, fDepartmentName, fDescription, state);
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
        public JsonResult UpdateState(string departmentId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.审核);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (string.IsNullOrEmpty(departmentId))
                    return Error("department id not empty");
                var result = DepartmentService.UpdateDepartmentStatePro(SystemID, CompanyID, departmentId, state);
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
        public JsonResult Delete(string departmentId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                if (string.IsNullOrEmpty(departmentId))
                    return Error("department id not empty");
                var result = DepartmentService.DeleteDepartmentPro(SystemID, CompanyID, departmentId);
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
        #endregion

        #region 辅助方法
        [HttpGet]
        [ActionName("list-byid-get")]
        public JsonResult GetListById(string id)
        {
            try
            {
                string departmentId = id;
                string state = "1";
                var result = DepartmentService.GetDepartmentByNodePathPro(SystemID, CompanyID, departmentId, state);
                if (result == null)
                    return Error("not data!");
                var data = from m in result
                           select new
                           {
                               system_id = m.SystemID,
                               company_id = m.CompanyID,
                               department_id = m.DepartmentID,
                               department_name = m.DepartmentName,
                               rank_id = m.RankID
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("list-byparentid-get")]
        public JsonResult GetListByParentId(string id)
        {
            try
            {
                string departmentId = id;
                string state = "1";
                var result = DepartmentService.GetDepartmentByParentIdPro(SystemID, CompanyID, departmentId, state);
                if (result == null)
                    return Error("not data!");
                var data = from m in result
                           select new
                           {
                               system_id = m.SystemID,
                               company_id = m.CompanyID,
                               department_id = m.DepartmentID,
                               department_name = m.DepartmentName,
                               rank_id = m.RankID
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region 私有化方法
        public bool IsSavePermission(string departmentId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(departmentId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string departmentId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(departmentId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.公司管理.部门管理.编辑);
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