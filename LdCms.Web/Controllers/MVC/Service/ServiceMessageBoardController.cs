using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Service
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Service;
    using LdCms.Common.Extension;
    using LdCms.Web.Models;
    using LdCms.Web.Services;

    /// <summary>
    /// 
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class ServiceMessageBoardController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IMessageBoardService MessageBoardService;
        public ServiceMessageBoardController(IBaseManager BaseManager, IMessageBoardService MessageBoardService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.MessageBoardService = MessageBoardService;
        }

        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.列表);
                if (!IsPermission(funcId))
                    return ToPermission(funcId);
                string startTime = GetQueryString("datemin");
                string endTime = GetQueryString("datemax");
                string state = GetQueryString("state");
                string keyword = GetQueryString("keyword");
                ViewBag.DateMin = startTime;
                ViewBag.DateMax = endTime;
                ViewBag.State = state;
                ViewBag.Keyword = keyword;

                int pageId = 1;
                int pageSize = 100;
                int rowCount = 0;
                List<Ld_Service_MessageBoard> lists = new List<Ld_Service_MessageBoard>();
                string strKeyword = string.Format("{0}{1}", startTime, keyword);
                if (string.IsNullOrWhiteSpace(strKeyword))
                    lists = MessageBoardService.GetMessageBoardPaging(SystemID, CompanyID, pageId, pageSize, out rowCount);
                else
                    lists = MessageBoardService.SearchMessageBoard(SystemID, CompanyID, startTime, endTime, state, keyword);
                int totalNum = rowCount > 0 ? rowCount : lists == null ? 0 : lists.Count();
                ViewBag.Count = totalNum;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Show(string messageId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.查看);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }
                var entity = MessageBoardService.GetMessageBoard(SystemID, CompanyID, messageId);
                if (entity == null)
                    return ToError("message id invalid！");
                return View(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Reply(string messageId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.回复);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }
                var entity = MessageBoardService.GetMessageBoard(SystemID, CompanyID, messageId);
                if (entity == null)
                    return ToError("message id invalid！");
                return View(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult UpdateState(string messageId, bool state)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.审阅);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                var result = MessageBoardService.UpdateMessageBoardState(SystemID, CompanyID, messageId, state);
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
        public JsonResult UpdateReply(string messageId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.回复);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                string fReply = GetFormValue("fReply");
                string fState = GetFormValue("fState");
                bool state = fState.ToBool();
                var result = MessageBoardService.UpdateMessageBoardReply(SystemID, CompanyID, messageId, fReply, StaffID, StaffName, state);
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
        public JsonResult Delete(string messageId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }
                    
                var result = MessageBoardService.DeleteMessageBoard(SystemID, CompanyID, messageId);
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
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.客服管理.留言管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                if (arrId.Length == 0)
                    return Error("请选择删除ID!");
                List<object> lists = new List<object>();
                foreach (var item in arrId)
                {
                    string messageId = item;
                    try
                    {
                        bool result = MessageBoardService.DeleteMessageBoard(SystemID, CompanyID, messageId);
                        lists.Add(new { message_id = messageId, result, message = "ok" });
                    }
                    catch (Exception ex)
                    {
                        lists.Add(new { message_id = messageId, result = false, message = ex.Message });
                    }
                }
                return Success("成功！", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

    }
}