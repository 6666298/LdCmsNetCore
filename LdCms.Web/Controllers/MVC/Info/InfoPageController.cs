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
    using LdCms.Common.Json;
    using LdCms.Common.Web;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InfoPageController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IClassService ClassService;
        private readonly IPageService PageService;
        public InfoPageController(IBaseManager BaseManager, IClassService ClassService, IPageService PageService) : base(BaseManager)
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.单页管理.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string classId = GetQueryString("classId");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["ClassId"] = classId;
                ViewData["State"] = state;
                ViewData["Keyword"] = keyword;

                int total = 100;
                List<Ld_Info_Page> lists = new List<Ld_Info_Page>();
                string strKeyword = string.Format("{0}{1}{2}", startTime, classId, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = PageService.GetPageTop(SystemID, CompanyID, total);
                else
                    lists = PageService.SearchPage(SystemID, CompanyID, startTime, endTime, classId, state, keyword);
                int rowCount = PageService.CountPage(SystemID, CompanyID);
                ViewData["Count"] = string.IsNullOrWhiteSpace(strKeyword) ? rowCount : lists.Count();
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Edit(string pageId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.单页管理.查看);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }
                var entity = PageService.GetPage(SystemID, CompanyID, pageId);
                if (entity == null)
                    return ToError("id invalid！");
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("update")]
        public JsonResult Update(string pageId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.单页管理.编辑);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                string fImgSrc = GetFormValue("fImgSrc");
                string fImgArray = GetFormValueArr("fImgArray[]");
                string fContent = GetFormValue("fContent");
                string fState = GetFormValue("fState");

                if (string.IsNullOrWhiteSpace(fContent))
                    return Error("内容不能为空！");
                if (string.IsNullOrWhiteSpace(fImgSrc) && !string.IsNullOrWhiteSpace(fImgArray))
                    fImgSrc = fImgArray.Split(",")[0];
                string imgArray = string.IsNullOrWhiteSpace(fImgArray) ? "" : fImgArray.Split(",").ToJson();
                string keyword = WebHelper.NoHtml(Utility.ContentDecode(fContent)).Left(200);
                string description = WebHelper.NoHtml(Utility.ContentDecode(fContent)).Left(400);

                var entity = PageService.GetPage(SystemID, CompanyID, pageId);
                if (entity == null)
                    return Error("ID无效！");
                entity.ImgSrc = fImgSrc;
                entity.ImgArray = imgArray;
                entity.Keyword = keyword;
                entity.Description = description;
                entity.Content = fContent;
                entity.State = fState.ToBool();
                entity.CreateDate = DateTime.Now;
                var result = PageService.UpdatePage(entity);
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

        #region 辅助方法
        [ActionName("class-list-get")]
        public JsonResult GetNoticeCategory()
        {
            try
            {
                bool state = true;
                var lists = ClassService.GetClassByParentPath(SystemID, CompanyID, "0", state);
                if (lists == null)
                    return Error("not data！");
                var data = from m in lists
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
                throw new Exception(ex.Message);
            }
        }
        #endregion




    }
}