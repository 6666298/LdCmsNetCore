using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web
{
    using LdCms.Web.Models;
    using LdCms.Common.Utility;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Common.Web;
    using LdCms.Common.Security;
    using LdCms.Web.Services;

    /// <summary>
    /// MVC 基础控制器
    /// </summary>
    public class BaseController : Controller
    {
        private readonly IBaseManager BaseManager;
        public BaseController(IBaseManager BaseManager)
        {
            this.BaseManager = BaseManager;
            this.SystemID = BaseSystemConfig.SystemID;
            this.SessionName = BaseSystemConfig.SessionName;
            InitLoginData();
        }

        protected string SessionName;
        protected int SystemID;
        protected string CompanyID;
        protected string StaffID;
        protected string StaffName;
        public void InitLoginData()
        {
            string sessionJson = WebHelper.GetCookie(SessionName);
            AccountModel loginStaffModel = DESEncryptHelper.DecryptDES(sessionJson).ToObject<AccountModel>();
            if (loginStaffModel != null)
            {
                CompanyID = loginStaffModel.CompanyID;
                StaffID = loginStaffModel.StaffID;
                StaffName = loginStaffModel.StaffName;
            }
        }

        [HttpGet]
        public virtual IActionResult Index()
        {
            return View();
        }
        protected virtual JsonResult Success(string message)
        {
            var result = new { state = "success", message };
            return Json(result);
        }
        protected virtual JsonResult Success(string message, object data)
        {
            var result = new { state = "success",  message,  data };
            return Json(result);
        }
        protected virtual JsonResult Error(string message)
        {
            var result = new { state = "error", message };
            return Json(result);
        }
        protected virtual IActionResult ToError(string message)
        {
            return RedirectToAction("Show", "Error", new { errcode = -1, errmsg = message });
        }
        protected virtual IActionResult ToPermission(string funcId)
        {
            return RedirectToAction("Permission", "Error", new { funcId });
        }

        public bool IsPermission(string functionId)
        {
            try
            {
                return BaseManager.IsPermission(CompanyID, StaffID, functionId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 私有化共用方法
        protected string GetQueryString(string name)
        {
            try
            {
                return BaseManager.GetQueryString(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected string GetFormValue(string name)
        {
            try
            {
                return BaseManager.GetFormValue(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected string GetFormValueArr(string name)
        {
            try
            {
                return BaseManager.GetFormValueArr(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected string GetFormValue(FormCollection formValue, string name)
        {
            try
            {
                return BaseManager.GetFormValue(formValue, name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}