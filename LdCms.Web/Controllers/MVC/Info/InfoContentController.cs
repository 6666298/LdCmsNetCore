using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Info
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Info;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InfoContentController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IClassService ClassService;
        private readonly IPageService PageService;
        public InfoContentController(IBaseManager BaseManager, IClassService ClassService, IPageService PageService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.ClassService = ClassService;
            this.PageService = PageService;
        }
        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                return View();
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [ActionName("class-all-get")]
        public JsonResult GetClassAll()
        {
            try
            {
                var lists = ClassService.GetClassAll(SystemID, CompanyID);
                if (lists == null)
                    return Error("not date！");
                var data = from m in lists orderby m.OrderID
                           select new
                           {
                               id = m.ClassID,
                               pid = m.ParentID,
                               name = m.ClassName,
                               file = GetFileUrl(m.ClassID, m.ClassType.ToInt()),
                               open = m.ParentID == "0" ? true : false
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        private string GetFileUrl(string classId, int classType)
        {
            try
            {
                if (classType == 1)
                {
                    var entity = PageService.GetPageByClassId(SystemID, CompanyID, classId);
                    if (entity == null)
                        throw new Exception("not date！");
                    return Url.Action("Edit", "InfoPage", new { pageid = entity.PageID });
                }
                else
                {
                    return Url.Action("ListClass", "InfoArtice", new { classid = classId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}