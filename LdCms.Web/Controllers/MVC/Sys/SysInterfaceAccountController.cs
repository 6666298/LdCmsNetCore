using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Sys
{
    using LdCms.Common.Extension;
    using LdCms.Common.Security;
    using LdCms.Common.Utility;
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;
    using LdCms.Web.Models;
    /// <summary>
    /// 系统接口帐号管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class SysInterfaceAccountController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IInterfaceAccountService InterfaceAccountService;
        private readonly IInterfaceAccessWhiteListService InterfaceAccessWhiteListService;
        public SysInterfaceAccountController(IBaseManager BaseManager, IInterfaceAccountService InterfaceAccountService, IInterfaceAccessWhiteListService InterfaceAccessWhiteListService) :base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.InterfaceAccountService = InterfaceAccountService;
            this.InterfaceAccessWhiteListService = InterfaceAccessWhiteListService;
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.列表);
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
                    var lists = InterfaceAccountService.GetInterfaceAccountPagingPro(SystemID, CompanyID, pageId, pageSize, out totalNum);
                    ViewBag.Count = totalNum;
                    return View(lists);
                }
                else
                {
                    var lists = InterfaceAccountService.SearchInterfaceAccountPro(SystemID, CompanyID, startTime, endTime, keyword);
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
        public IActionResult Add(string account = "")
        {
            try
            {
                string funcId = "";
                if (!IsAddPermission(account, out funcId))
                    return ToPermission(funcId);
                var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, CompanyID, account);
                if (entity == null)
                    return View(new Ld_Sys_InterfaceAccount());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult UpdatePassword(string account)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.修改密码);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, CompanyID, account);
                if (entity == null)
                    return ToError("staff id invalid！");
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Show(string account)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.查看);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, CompanyID, account);
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region POST动作处理
        [HttpPost]
        public JsonResult Save(string account)
        {
            try
            {
                if (!IsSavePermission(account))
                    return Error("您没有操作权限，请联系系统管理员！");

                string fAccount = GetFormValue("fAccount");
                string fPassword = GetFormValue("fPassword");
                string fIsWcf = GetFormValue("fIsWcf");
                string fIsWeb = GetFormValue("fIsWeb");
                string fIsApi = GetFormValue("fIsApi");
                string fDescription = GetFormValue("fDescription");
                string fState = GetFormValue("fState");
                string password = string.Empty;

                if (string.IsNullOrWhiteSpace(fAccount))
                    return Error("帐号不能为空！");
                if (string.IsNullOrWhiteSpace(account))
                {
                    if (string.IsNullOrWhiteSpace(fPassword))
                        return Error("密码不能为空！");
                    password = AlgorithmHelper.MD5(fPassword);
                }
                string description = Utility.Left(fDescription, 200);
                bool isWcf = fIsWcf.ToBool();
                bool isWeb = fIsWeb.ToBool();
                bool isApi = fIsApi.ToBool();
                bool isCors = false;
                bool state = fState.ToBool();

                var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, CompanyID, fAccount);
                if (entity != null)
                {
                    if (string.IsNullOrWhiteSpace(fPassword))
                        password = entity.Password;
                    else
                        password = AlgorithmHelper.MD5(fPassword);
                }

                var result = false;
                if (string.IsNullOrEmpty(account))
                    result = InterfaceAccountService.SaveInterfaceAccountPro(SystemID, CompanyID, fAccount, password.ToLower(), isWcf, isWeb, isApi, isCors, description, state);
                else
                    result = InterfaceAccountService.UpdateInterfaceAccountPro(SystemID, CompanyID, fAccount, password.ToLower(), isWcf, isWeb, isApi, isCors, description, state);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.修改密码);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string account = GetQueryString("account");
                string fNewPassword = GetFormValue("fNewPassword");
                string fConfirmPassword = GetFormValue("fConfirmPassword");
                if (string.IsNullOrEmpty(account))
                    return Error("帐号不能为空！");
                if (fConfirmPassword.Length < 6)
                    return Error("密码长度不能少于6位字符！");
                if (fNewPassword != fConfirmPassword)
                    return Error("输入的二次密码不相同！");
                string password = AlgorithmHelper.MD5(fConfirmPassword).ToLower();

                var result = InterfaceAccountService.UpdateInterfaceAccountPasswordPro(SystemID, CompanyID, account, password);
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
        public JsonResult UpdateState(string account, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.审核);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                var result = InterfaceAccountService.UpdateInterfaceAccountStatePro(SystemID, CompanyID, account, state);
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
        public JsonResult UpdateAppSecret(string account)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.刷新AppSecret);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                var result = InterfaceAccountService.UpdateInterfaceAccountAppSecretPro(SystemID, CompanyID, account);
                if (result)
                {
                    var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, CompanyID, account);
                    var data = new { appsecret = entity.AppSecret };
                    return Success("ok", data);
                }
                else
                {
                    return Error("fail");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult UpdateAppKey(string account)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.刷新AppKey);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                var result = InterfaceAccountService.UpdateInterfaceAccountAppKeyPro(SystemID, CompanyID, account);
                if (result)
                {
                    var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, CompanyID, account);
                    var data = new { key = entity.AppKey };
                    return Success("ok", data);
                }
                else
                {
                    return Error("fail");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult Delete(string account)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                var result = InterfaceAccountService.DeleteInterfaceAccountPro(SystemID, CompanyID, account);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string account = item;
                    try
                    {
                        bool result = InterfaceAccountService.DeleteInterfaceAccountPro(SystemID, CompanyID, account);
                        lists.Add(new { account, result, message = result ? "success" : "fail" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { account, result = false, message = ex.Message });
                    }
                }
                return Success("成功！", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult DeleteWhiteList(string account,string ipAddress, int classId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.删除白名单);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                var result = InterfaceAccessWhiteListService.DeleteInterfaceAccessWhiteListPro(SystemID, CompanyID, account, ipAddress, classId);
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
        public JsonResult SaveWhiteList(string account)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.新增白名单);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string fClassID = GetFormValue("fClassID");
                string fClassName = GetFormValue("fClassName");
                string fIpAddress = GetFormValue("fIpAddress");
                string fState = GetFormValue("fState");
                int classId = fClassID.ToInt();
                string remark = "授权IP请求接口！";
                bool state = fState.ToBool();
                var result = InterfaceAccessWhiteListService.SaveInterfaceAccessWhiteListPro(SystemID, CompanyID, account, fIpAddress, classId, fClassName, remark, state);
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

        [HttpGet]
        [ActionName("white-list-get")]
        public JsonResult GetWhiteList(string account)
        {
            try
            {
                var lists = InterfaceAccessWhiteListService.GetInterfaceAccessWhiteListByAccountPro(SystemID, CompanyID, account);
                if (lists == null)
                    return Error("not data！");
                var data = from m in lists
                           select new
                           {
                               account = m.Account,
                               ip = m.IpAddress,
                               class_id = m.ClassID,
                               class_name = m.ClassName,
                               state = m.State,
                               create_date = m.CreateDate.Value.ToString("yyyy-MM-dd HH:mm")
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
        public bool IsSavePermission(string account)
        {
            try
            {
                string saveFuncId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.新增);
                string updateFuncId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.编辑);
                if (string.IsNullOrWhiteSpace(account))
                    return IsPermission(saveFuncId) ? true : false;
                else
                    return IsPermission(updateFuncId) ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string account, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(account))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.接口管理.帐号管理.编辑);
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