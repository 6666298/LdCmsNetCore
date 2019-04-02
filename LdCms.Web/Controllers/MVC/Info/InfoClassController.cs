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

    [AdminAuthorize(Roles = "Admins")]
    public class InfoClassController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IClassService ClassService;
        public InfoClassController(IBaseManager BaseManager, IClassService ClassService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.ClassService = ClassService;
        }
        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }
                    
                List<Ld_Info_Class> result = ClassService.GetClassAll(SystemID, CompanyID);
                var lists = from m in result orderby m.OrderPath ascending, m.OrderID ascending select m;
                ViewBag.Count = lists.Count();
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string classId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsAddPermission(classId, out funcId)) { return ToPermission(funcId); }

                if (string.IsNullOrWhiteSpace(classId))
                    return View(new Ld_Info_Class());
                var entity = ClassService.GetClass(SystemID, CompanyID, classId);
                if (entity == null)
                    return View(new Ld_Info_Class());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Save(string classId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsSavePermission(classId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                string fClassName = GetFormValue("fClassName");
                string fTypeId = GetFormValue("fTypeId");
                string fTypeName = GetFormValue("fTypeName");
                string fImgSrc= GetFormValue("fImgSrc");
                string fParentId = GetFormValue("fParentId");
                string fKeyword = GetFormValue("fKeyword");
                string fDescription = GetFormValue("fDescription");
                string fState = GetFormValue("fState");
                bool state = fState.ToBool();

                bool result = false;
                if (string.IsNullOrEmpty(classId))
                    result = ClassService.SaveClass(new Ld_Info_Class()
                    {
                        SystemID = SystemID,
                        CompanyID = CompanyID,
                        ClassName = fClassName,
                        ClassType = fTypeId.ToByte(),
                        ClassTypeName = fTypeName,
                        ParentID = fParentId,
                        ImgSrc = "",
                        Keyword = fKeyword,
                        Description = fDescription,
                        State = state
                    });
                else
                    result = ClassService.UpdateClass(SystemID, CompanyID, classId, fClassName, fImgSrc, fKeyword, fDescription, state);
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
        public JsonResult UpdateState(string classId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.审核);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                var result = ClassService.UpdateClassState(SystemID, CompanyID, classId, state);
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
        public JsonResult Delete(string classId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                var result = ClassService.DeleteClass(SystemID, CompanyID, classId);
                if (result > 0)
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
        [ActionName("list-get")]
        public JsonResult GetClassList()
        {
            try
            {
                bool state = true;
                var result = ClassService.GetClassState(SystemID, CompanyID, state);
                if (result == null)
                    return Error("not data!");
                var data = from m in result
                           select new
                           {
                               id = m.ClassID,
                               name = m.ClassName,
                               rank = m.ClassRank
                           };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public bool IsSavePermission(string classId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(classId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string classId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(classId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.栏目管理.分类栏目.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}