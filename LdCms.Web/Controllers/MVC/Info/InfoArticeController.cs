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
    using LdCms.Common.Json;
    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InfoArticeController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IClassService ClassService;
        private readonly IArticeService ArticeService;
        public InfoArticeController(IBaseManager BaseManager, IClassService ClassService, IArticeService ArticeService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.ClassService = ClassService;
            this.ArticeService = ArticeService;
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
                    
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["State"] = state;
                ViewData["Keyword"] = keyword;

                string classId = "";
                bool delete = false;
                int total = 100;
                List<Ld_Info_Artice> lists = new List<Ld_Info_Artice>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = ArticeService.GetArticeTop(SystemID, CompanyID, delete, total);
                else
                    lists = ArticeService.SearchArtice(SystemID, CompanyID, startTime, endTime, classId, state, keyword, delete);
                ViewData["Count"] = ArticeService.CountArtice(SystemID, CompanyID, delete);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.回收站.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["State"] = state;
                ViewData["Keyword"] = keyword;

                string classId = "";
                bool delete = true;
                int total = 100;
                List<Ld_Info_Artice> lists = new List<Ld_Info_Artice>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = ArticeService.GetArticeTop(SystemID, CompanyID, delete, total);
                else
                    lists = ArticeService.SearchArtice(SystemID, CompanyID, startTime, endTime, classId, state, keyword, delete);
                ViewData["Count"] = ArticeService.CountArtice(SystemID, CompanyID, delete);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult ListClass(string classId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewData["DateMin"] = startTime;
                ViewData["DateMax"] = endTime;
                ViewData["ClassID"] = classId;
                ViewData["State"] = state;
                ViewData["Keyword"] = keyword;

                bool delete = false;
                int total = 100;
                List<Ld_Info_Artice> lists = new List<Ld_Info_Artice>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = ArticeService.GetArticeTop(SystemID, CompanyID, classId, delete, total);
                else
                    lists = ArticeService.SearchArtice(SystemID, CompanyID, startTime, endTime, classId, state, keyword, delete);
                ViewData["Count"] = ArticeService.CountArtice(SystemID, CompanyID, classId, delete);
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string classId, string articeId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsAddPermission(articeId, out funcId)) { return ToPermission(funcId); }
                    
                var entityClass = ClassService.GetClass(SystemID, CompanyID, classId);
                if (entityClass == null)
                    return ToError("class id invalid！");
                ViewData["ClassID"] = entityClass.ClassID;
                ViewData["ClassName"] = entityClass.ClassName;

                if (string.IsNullOrWhiteSpace(articeId))
                    return View(new Ld_Info_Artice() { CreateDate = DateTime.Now });
                var entity = ArticeService.GetArtice(SystemID, CompanyID, articeId);
                if (entity == null)
                    return View(new Ld_Info_Artice() { CreateDate = DateTime.Now });
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }


        [HttpPost]
        public JsonResult Save(string articeId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsSavePermission(articeId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                string fTitle = GetFormValue("fTitle");
                string fClassId = GetFormValue("fClassId");
                string fClassName = GetFormValue("fClassName");
                string fImgSrc = GetFormValue("fImgSrc");
                string fImgArray = GetFormValueArr("fImgArray[]");
                string fAuthor = GetFormValue("fAuthor");
                string fSource = GetFormValue("fSource");
                string fUrl = GetFormValue("fUrl");
                string fHits = GetFormValue("fHits");
                string fIsTop = GetFormValue("fIsTop");
                string fIsPush = GetFormValue("fIsPush");
                string fAllowComment = GetFormValue("fAllowComment");
                string fCommentStartTime = GetFormValue("fCommentStartTime");
                string fCommentEndTime = GetFormValue("fCommentEndTime");
                string fTags = GetFormValue("fTags");
                string fCreateDate = GetFormValue("fCreateDate");
                string fTitleBrief = GetFormValue("fTitleBrief");
                string fKeyword = GetFormValue("fKeyword");
                string fDescription = GetFormValue("fDescription");
                string fContent = GetFormValue("fContent");
                string fState = GetFormValue("fState");

                string fIsDraft = GetFormValue("fIsDraft");

                if (string.IsNullOrWhiteSpace(fTitle))
                    return Error("标题不能为空！");
                if (string.IsNullOrWhiteSpace(fContent))
                    return Error("内容不能为空！");
                if (string.IsNullOrWhiteSpace(fImgSrc) && !string.IsNullOrWhiteSpace(fImgArray))
                    fImgSrc = fImgArray.Split(",")[0];
                string imgArray = string.IsNullOrWhiteSpace(fImgArray) ? "" : fImgArray.Split(",").ToJson();

                string author = string.IsNullOrWhiteSpace(fAuthor) ? StaffName : fAuthor;
                string titleBrief = string.IsNullOrWhiteSpace(fTitleBrief) ? WebHelper.NoHtml(Utility.ContentDecode(fTitle)).Left(80) : fKeyword;
                string keyword = string.IsNullOrWhiteSpace(fKeyword) ? WebHelper.NoHtml(Utility.ContentDecode(fContent)).Left(100) : fKeyword;
                string description = string.IsNullOrWhiteSpace(fDescription) ? WebHelper.NoHtml(Utility.ContentDecode(fContent)).Left(200) : fDescription;

                bool state = fState.ToBool();
                Ld_Info_Artice entity = new Ld_Info_Artice()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    ArticeID = articeId,
                    Title = fTitle,
                    TitleBrief = titleBrief,
                    ClassID = fClassId,
                    ClassName = fClassName,
                    ImgSrc = fImgSrc,
                    ImgArray = imgArray,
                    Author = author,
                    Source = fSource,
                    Tags = fTags,
                    Keyword = keyword,
                    Description = description,
                    Content = fContent,
                    Url = fUrl,
                    Hits = fHits.ToInt(),
                    Sort = 0,
                    UpNum = 0,
                    DownNum = 0,
                    AllowComment = fAllowComment.ToBool(),
                    CommentStartTime = fCommentStartTime.ToDateOrNull(),
                    CommentEndTime = fCommentEndTime.ToDateOrNull(),
                    IsTop = fIsTop.ToBool(),
                    IsPush = fIsPush.ToBool(),
                    IsVip = false,
                    IsDraft = fIsDraft.ToBool(),
                    IsDel=false,
                    Account = StaffID,
                    NickName = StaffName,
                    State = state,
                    CreateDate = DateTime.Now
                };
                bool result = false;
                if (string.IsNullOrEmpty(articeId))
                    result = ArticeService.SaveArtice(entity);
                else
                    result = ArticeService.UpdateArtice(entity);
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
        public JsonResult UpdateState(string articeId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.审核);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                var result = ArticeService.UpdateArticeState(SystemID, CompanyID, articeId, state);
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
        public JsonResult UpdateDelete(string articeId, bool delete)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                var result = ArticeService.UpdateArticeDelete(SystemID, CompanyID, articeId, delete);
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
        public JsonResult UpdateDeleteBatch(string[] arrId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string articeId = item;
                    try
                    {
                        bool result = ArticeService.UpdateArticeDelete(SystemID, CompanyID, articeId, true);
                        lists.Add(new { artice_id = articeId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { artice_id = articeId, result = false, message = ex.Message });
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
        public JsonResult Restore(string articeId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.回收站.还原);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                var result = ArticeService.UpdateArticeDelete(SystemID, CompanyID, articeId, false);
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
        public JsonResult Delete(string articeId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.回收站.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                var result = ArticeService.DeleteArtice(SystemID, CompanyID, articeId);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.回收站.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string articeId = item;
                    try
                    {
                        bool result = ArticeService.DeleteArtice(SystemID, CompanyID, articeId);
                        lists.Add(new { artice_id = articeId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { artice_id = articeId, result = false, message = ex.Message });
                    }
                }
                return Success("成功！", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }


        #region 私有化方法
        public bool IsSavePermission(string articeId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(articeId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string articeId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articeId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.资讯管理.编辑);
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