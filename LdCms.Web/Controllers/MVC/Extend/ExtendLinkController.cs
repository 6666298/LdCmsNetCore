using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Extend;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class ExtendLinkController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly ILinkService LinkService;
        private readonly ILinkGroupService LinkGroupService;
        public ExtendLinkController(IBaseManager BaseManager, ILinkService LinkService, ILinkGroupService LinkGroupService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.LinkService = LinkService;
            this.LinkGroupService = LinkGroupService;
        }
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["State"] = state;
                ViewData["Keyword"] = keyword;

                int total = 100;
                List<Ld_Extend_Link> lists = new List<Ld_Extend_Link>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = LinkService.GetLinkTop(SystemID, CompanyID, total);
                else
                    lists = LinkService.SearchLink(SystemID, CompanyID, startTime, endTime, state, keyword, total);
                ViewData["Count"] = LinkService.CountLink(SystemID, CompanyID);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string linkId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsAddPermission(linkId, out funcId)) { return ToPermission(funcId); }

                if (string.IsNullOrWhiteSpace(linkId))
                    return View(new Ld_Extend_Link());
                var entity = LinkService.GetLink(SystemID, CompanyID, linkId);
                if (entity == null)
                    return View(new Ld_Extend_Link());
                return View(entity);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        public IActionResult ListGroup()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.链接类别.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                return View();
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public JsonResult SaveLink(string linkId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsSavePermission(linkId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                string fGroupId = GetFormValue("fGroupId");
                string fGroupName = GetFormValue("fGroupName");
                string fName = GetFormValue("fName");
                string fLogo = GetFormValue("fLogo");
                string fUrl = GetFormValue("fUrl");
                string fSort = GetFormValue("fSort");
                string fState = GetFormValue("fState");
                if (fUrl.Left(4).ToLower() != "http") { return Error("访问网址格式有误！"); }
                var entity = new Ld_Extend_Link()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    LinkID = linkId,
                    GroupID = fGroupId,
                    GroupName = fGroupName,
                    Name = fName,
                    Logo = fLogo,
                    Url = fUrl,
                    Sort = fSort.ToInt(),
                    State = fState.ToBool()
                };

                var result = false;
                if (string.IsNullOrEmpty(linkId))
                    result = LinkService.SaveLink(entity);
                else
                    result = LinkService.UpdateLink(entity);
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
        [ActionName("UpdateState")]
        public JsonResult UpdateState(string linkId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.审核);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                bool state = GetFormValue("state").ToBool();
                var result = LinkService.UpdateLinkState(SystemID, CompanyID, linkId, state);
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
        [ActionName("Delete")]
        public JsonResult DeleteLink(string linkId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                var result = LinkService.DeleteLink(SystemID, CompanyID, linkId);
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
        [ActionName("DeleteBatch")]
        public JsonResult DeleteLinkBatch(string[] arrId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string linkId = item.ToString();
                    try
                    {
                        bool result = LinkService.DeleteLink(SystemID, CompanyID, linkId);
                        lists.Add(new { link_id= linkId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { link_id = linkId, result = false, message = ex.Message });
                    }
                }
                return Success("成功！", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }


        [HttpGet]
        [ActionName("linkgroup-list-get")]
        public JsonResult GetLinkGroup()
        {
            try
            {
                var lists = LinkGroupService.GetLinkGroup(SystemID, CompanyID);
                if (lists == null) { return Error("not date！"); }
                var data = from m in lists
                           select new
                           {
                               id = m.GroupID,
                               name = m.Name,
                               remark = m.Remark,
                               sort = m.Sort,
                               external = m.IsExternal.ToBool(),
                               state = m.State.ToBool(),
                               date = m.CreateDate.ToDate().ToString("yyyy-MM-dd HH:mm")
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("SaveGroup")]
        public JsonResult SaveLinkGroup(string groupId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsSavePermission(groupId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                string fName = GetFormValue("fName");
                string fIsExternal = GetFormValue("fIsExternal");
                string fRemark = GetFormValue("fRemark");
                string fState = GetFormValue("fState");

                var entity = new Ld_Extend_LinkGroup()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    GroupID = groupId,
                    Name = fName,
                    IsExternal = fIsExternal.ToBool(),
                    State = fState.ToBool(),
                    Remark = fRemark
                };

                var result = false;
                if (string.IsNullOrEmpty(groupId))
                    result = LinkGroupService.SaveLinkGroup(entity);
                else
                    result = LinkGroupService.UpdateLinkGroup(entity);
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
        [ActionName("DeleteGroup")]
        public JsonResult DeleteLinkGroup()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.链接类别.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                string groupId = GetFormValue("GroupID");
                var result = LinkGroupService.DeleteLinkGroup(SystemID, CompanyID, groupId);
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
        [ActionName("GetGroup")]
        public JsonResult GetLinkGroup(string groupId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.链接类别.查看);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                var entity = LinkGroupService.GetLinkGroup(SystemID, CompanyID, groupId);
                if (entity == null)
                    return Error("主建ID无效！");
                var data = new
                {
                    id = entity.GroupID,
                    name = entity.Name,
                    remark = entity.Remark,
                    external = entity.IsExternal.ToBool() ? 1 : 0,
                    state = entity.State.ToBool()
                };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }


        #region 私有化方法
        public bool IsSavePermission(string linkId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(linkId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string linkId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(linkId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.友情链接.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsSaveGroupPermission(string groupId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(groupId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.链接类别.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.扩展管理.链接类别.编辑);
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