using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LdCms.Web.Controllers.MVC.Admin
{
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Common.Net;
    using LdCms.Common.Security;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Institution;
    using LdCms.IBLL.Log;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 后台管理主页控制器
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly SiteConfig SiteConfig;
        private readonly IBaseManager BaseManager;
        private readonly IStaffService StaffService;
        private readonly ILoginRecordService LoginRecordService;
        public AdminController(IOptions<SiteConfig> SiteConfig, IBaseManager BaseManager, IStaffService StaffService, ILoginRecordService LoginRecordService) : base(BaseManager)
        {
            this.SiteConfig = SiteConfig.Value;
            this.BaseManager = BaseManager;
            this.StaffService = StaffService;
            this.LoginRecordService = LoginRecordService;
        }
        /// <summary>
        /// 管理后台首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AdminAuthorize(Roles = "Admins")]
        public override IActionResult Index()
        {
            try
            {
                ViewBag.CompanyID = CompanyID;
                ViewBag.StaffID = StaffID;
                ViewBag.StaffName = StaffName;
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 后台欢迎主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AdminAuthorize(Roles = "Admins")]
        public IActionResult Welcome()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        /// <summary>
        /// 退出登录处理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AdminAuthorize(Roles = "Admins")]
        public IActionResult Logout()
        {
            try
            {
                SaveLoginRecord(CompanyID, StaffID, StaffName, true, 2);
                WebHelper.RemoveCookie(SessionName);
                return RedirectToAction("login", new { companyid = CompanyID });
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        #region 登录部分
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AdminAuthorize(Validate = false)]
        public IActionResult Login()
        {
            string companyId = GetQueryString("CompanyID");
            if (string.IsNullOrWhiteSpace(companyId))
                companyId = "sys";
            string sessionJson = WebHelper.GetCookie(SessionName);
            if (!string.IsNullOrEmpty(sessionJson))
            {
                AccountModel loginStaffModel = DESEncryptHelper.DecryptDES(sessionJson).ToObject<AccountModel>();
                if (loginStaffModel.Online)
                    return RedirectToAction("index", "admin");
            }
            return View(SiteConfig);
        }
        /// <summary>
        /// 处理登录请深圳市
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminAuthorize(Validate = false)]
        public JsonResult Login(string companyId)
        {
            try
            {
                string username = GetFormValue("username");
                string password = GetFormValue("password");
                string verifyCode = GetFormValue("verifycode");
                bool online = GetFormValue("online").ToBool();
                string cookieVerifyCode = WebHelper.GetCookie("VerifyCode");
                string decryptCookieVerifyCode = DESEncryptHelper.DecryptDES(cookieVerifyCode);

                string[] arrUserName = username.Split('@');
                if (arrUserName.Length == 2)
                {
                    companyId = arrUserName[0].ToString();
                    username = arrUserName[1].ToString();
                }
                else
                {
                    companyId = Utility.IIF(companyId, "sys");
                }

                if (string.IsNullOrEmpty(username))
                    return Error("用户名不能为空！");
                if (string.IsNullOrEmpty(password))
                    return Error("密码不能为空！");
                if (string.IsNullOrEmpty(verifyCode))
                    return Error("验证码不能为空！");
                if (decryptCookieVerifyCode.ToUpper() != verifyCode.ToUpper())
                    return Error("验证码不正确！");

                var LoginResult = StaffService.VerifyStaffLoginPro(SystemID, companyId, username, AlgorithmHelper.MD5(password));
                if (LoginResult)
                {
                    var entityStaff = StaffService.GetVStaffPro(SystemID, companyId, username);
                    string staffId = entityStaff.StaffID;
                    string staffName = entityStaff.StaffName;
                    string CompanyId = entityStaff.CompanyID;
                    SaveLoginRecord(companyId, username, staffName, LoginResult, 1);
                    AccountModel entity = new AccountModel() { SessionID = "", CompanyID = CompanyId, StaffID = username, StaffName = staffName, Online = online, Roles = "Admins" };
                    string userJson = DESEncryptHelper.EncryptDES(entity.ToJson());
                    WebHelper.WriteCookie(SessionName, userJson);
                    return Success("成功");
                }
                else
                {
                    SaveLoginRecord(companyId, username, "-", LoginResult, 2);
                    return Error("login fail");
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region 私有方法
        public void SaveLoginRecord(string companyId, string account, string nickName, bool isResult, int loginType)
        {
            try
            {
                byte typeId = loginType.ToByte();
                string typeName = loginType == 1 ? "登录" : "退出";
                string description = loginType == 1 ? "登录管理后台" : "退出管理后台";
                var result = LoginRecordService.SaveLoginRecord(new Ld_Log_LoginRecord()
                {
                    SystemID = SystemID,
                    CompanyID = companyId,
                    TypeID = typeId,
                    TypeName = typeName,
                    Account = account,
                    NickName = nickName,
                    ClientID = 1,
                    ClientName = "Web",
                    IpAddress = Net.Ip,
                    Description = description,
                    IsResult = isResult
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion



    }
}