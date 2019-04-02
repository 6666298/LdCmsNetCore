using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Member
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Member;
    using LdCms.Common.Extension;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    using LdCms.Common.Security;
    using LdCms.Common.Net;
    using LdCms.Common.Utility;

    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class MemberAccountController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IRankService RankService;
        private readonly IAccountService AccountService;
        public MemberAccountController(IBaseManager BaseManager, IRankService RankService,IAccountService AccountService) :base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.RankService = RankService;
            this.AccountService = AccountService;
        }

        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string rankId = GetQueryString("rankId");
                string keyword = GetQueryString("keyword");
                ViewBag.DateMin = startTime;
                ViewBag.DateMax = endTime;
                ViewBag.RankID = rankId;
                ViewBag.Keyword = keyword;

                int pageId = 1;
                int pageSize = 100;
                int rowCount = 0;
                string delete = "false";
                List<Ld_Member_Account> lists = new List<Ld_Member_Account>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = AccountService.GetAccountPagingPro(SystemID, CompanyID, delete, pageId, pageSize, out rowCount);
                else
                    lists = AccountService.SearchAccountPro(SystemID, CompanyID, startTime, endTime, rankId, delete, keyword);
                int totalNum = rowCount > 0 ? rowCount : lists == null ? 0 : lists.Count();
                ViewBag.Count = totalNum;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult ListDelete()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.删除的会员.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string rankId = GetQueryString("rankId");
                string keyword = GetQueryString("keyword");
                ViewBag.DateMin = startTime;
                ViewBag.DateMax = endTime;
                ViewBag.RankID = rankId;
                ViewBag.Keyword = keyword;

                int pageId = 1;
                int pageSize = 100;
                int rowCount = 0;
                string delete = "true";
                List<Ld_Member_Account> lists = new List<Ld_Member_Account>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = AccountService.GetAccountPagingPro(SystemID, CompanyID, delete, pageId, pageSize, out rowCount);
                else
                    lists = AccountService.SearchAccountPro(SystemID, CompanyID, startTime, endTime, rankId, delete, keyword);
                int totalNum = rowCount > 0 ? rowCount : lists == null ? 0 : lists.Count();
                ViewBag.Count = totalNum;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string memberId)
        {
            try
            {
                string funcId = "";
                if (!IsAddPermission(memberId, out funcId))
                    return ToPermission(funcId);

                if (string.IsNullOrEmpty(memberId))
                    return View(new Ld_Member_Account());
                var entity = AccountService.GetAccountPro(SystemID, CompanyID, memberId);
                if (entity == null)
                    return View(new Ld_Member_Account());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult UpdatePassword(string memberId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.改密);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }
                    
                if (string.IsNullOrEmpty(memberId))
                    return View(new Ld_Member_Account());
                var entity = AccountService.GetAccountPro(SystemID, CompanyID, memberId);
                if (entity == null)
                    return View(new Ld_Member_Account());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }


        #region 操作方法
        [HttpPost]
        public JsonResult Save(string memberId)
        {
            try
            {
                if (!IsSavePermission(memberId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string fRankId = GetFormValue("fRankId");
                string fRankName = GetFormValue("fRankName");
                string fCompanyName = GetFormValue("CompanyName");
                string fName = GetFormValue("fName");
                string fSex = GetFormValue("fSex");
                string fPhone = GetFormValue("fPhone");
                string fEmail = GetFormValue("fEmail");
                string fAddress = GetFormValue("fAddress");
                string fRemark = GetFormValue("fRemark");
                string fState = GetFormValue("fState");
                string fMemberId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.MemberAccount);

                if (!Utility.IsMobilePhone(fPhone))
                    return Error("手机号码格式错误！");

                string password = AlgorithmHelper.MD5(fPhone.Right(8));

                Ld_Member_Account entity = new Ld_Member_Account()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    MemberID = fMemberId,
                    RankID = fRankId,
                    RankName = fRankName,
                    UserName = fPhone,
                    Password= password,
                    CompanyName = fCompanyName,
                    Name = fName,
                    NickName = fName,
                    Sex = fSex.ToByte(),
                    Phone = fPhone,
                    Email = fEmail,
                    Address = fAddress,
                    Remark = fRemark,
                    State = fState.ToBool(),
                    TotalPoints = 0,
                    TotalAmount = 0.00.ToDecimal(),
                    TotalConsumption= 0.00.ToDecimal(),
                    RegisterIpAddress = Net.Ip,
                    RegisterTime=DateTime.Now,
                    IsDel=false,
                    CreateDate=DateTime.Now
                };

                bool result = false;
                if (string.IsNullOrEmpty(memberId))
                {
                    result = AccountService.SaveAccount(entity);
                }
                else
                {
                    var m = AccountService.GetAccount(SystemID, CompanyID, memberId);
                    m.RankID = fRankId;
                    m.RankName = fRankName;
                    m.UserName = fPhone;
                    m.CompanyName = fCompanyName;
                    m.Name = fName;
                    m.NickName = fName;
                    m.Sex = fSex.ToByte();
                    m.Phone = fPhone;
                    m.Email = fEmail;
                    m.Address = fAddress;
                    m.Remark = fRemark;
                    m.State = fState.ToBool();
                    result = AccountService.UpdateAccount(m);
                }
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
        public JsonResult UpdateState(string memberId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.审核);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                var result = AccountService.UpdateAccountStatePro(SystemID, CompanyID, memberId, state);
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
        public JsonResult UpdateDelete(string memberId, bool delete)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                var result = AccountService.UpdateAccountDeletePro(SystemID, CompanyID, memberId, delete);
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
        public JsonResult UpdatePassword()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.改密);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string memberId = GetQueryString("memberId");
                string newPassword = GetFormValue("fNewPassword");
                string confirmPassword = GetFormValue("fConfirmPassword");
                if (string.IsNullOrEmpty(memberId))
                    return Error("会员ID不能为空！");
                if (confirmPassword.Length < 6)
                    return Error("密码长度不能少于6位字符！");
                if (newPassword != confirmPassword)
                    return Error("输入的二次密码不相同！");
                string password = AlgorithmHelper.MD5(confirmPassword).ToLower();

                var result = AccountService.UpdateAccountPasswordPro(SystemID, CompanyID, memberId, password);
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
        public JsonResult Delete(string memberId, string delete)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.删除的会员.删除);
                if (!IsPermission(funcId))
                    return Error("您没有操作权限，请联系系统管理员！");
                bool isDel = true;
                bool result = false;
                if (delete.ToBool())
                    result = AccountService.DeleteAccountPro(SystemID, CompanyID, memberId);
                else
                    result = AccountService.UpdateAccountDeletePro(SystemID, CompanyID, memberId, isDel);
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
        public JsonResult DeleteBatch(string[] arrId, string delete)
        {
            try
            {
                string funcId = "";
                if (!IsDeleteBatchPermission(delete, out funcId))
                    return Error("您没有操作权限，请联系系统管理员！");

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string memberId = item;
                    try
                    {
                        bool isDel = true;
                        bool result = false;
                        if (delete.ToBool())
                            result = AccountService.DeleteAccountPro(SystemID, CompanyID, memberId);
                        else
                            result = AccountService.UpdateAccountDeletePro(SystemID, CompanyID, memberId, isDel);
                        lists.Add(new { member_id = memberId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { member_id = memberId, result = false, message = ex.Message });
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

        #region 辅助方法
        [ActionName("rank-list-get")]
        public JsonResult GetMemberRank(string state)
        {
            try
            {
                var lists = RankService.GetRankStatePro(SystemID, CompanyID, state);
                if (lists == null)
                    return Error("not date！");
                var data = from m in lists
                           select new
                           {
                               id = m.RankID,
                               name = m.RankName
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
        public bool IsSavePermission(string memberId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(memberId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string memberId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(memberId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsDeleteBatchPermission(string delete, out string funcId)
        {
            try
            {
                if (delete.ToBool())
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.删除的会员.删除);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.会员管理.会员资料.删除);
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