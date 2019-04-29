using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace LdCms.Web.Controllers.API.Info.V2
{
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Json;
    using LdCms.IBLL.Info;
    using LdCms.Web.Services;
    

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("info/class")]
    public class InfoClassController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IClassService ClassService;
        public InfoClassController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IClassService ClassService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.ClassService = ClassService;
        }
        public IActionResult Index(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }


                return Success(logId, "ok", uuid);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }

        [HttpGet("list")]
        [ActionName("all")]
        public IActionResult GetClassAll(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                bool state = true;
                var lists = ClassService.GetClassState(SystemID, companyId, state);
                if (lists == null) { return Error(logId, "not data！"); }
                var data = from m in lists
                           orderby m.OrderID
                           select new
                           {
                               id = m.ClassID,
                               pid = m.ParentID,
                               name = m.ClassName,
                               type = m.ClassType.ToInt(),
                               rank = m.ClassRank.ToInt(),
                               sort = m.OrderID,
                               keyword = m.Keyword,
                               description = m.Description
                           };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet("list")]
        [ActionName("parent")]
        public IActionResult GetClassParentId(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string parentId = Accessor.HttpContext.Request.GetQueryString("parentid");
                var lists = ClassService.GetClassByParentId(SystemID, companyId, parentId, "true");
                if (lists == null) { return Error(logId, "not data！"); }
                var data = from m in lists
                           orderby m.OrderID
                           select new
                           {
                               id = m.ClassID,
                               pid = m.ParentID,
                               name = m.ClassName,
                               type = m.ClassType.ToInt(),
                               rank = m.ClassRank.ToInt(),
                               sort = m.OrderID,
                               keyword = m.Keyword,
                               description = m.Description
                           };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet("list")]
        [ActionName("path")]
        public IActionResult GetClassParentPath(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string classId = Accessor.HttpContext.Request.GetQueryString("classid");
                string state = "true";
                var lists = ClassService.GetClassByParentPath(SystemID, companyId, classId, state);
                if (lists == null) { return Error(logId, "not data！"); }
                var data = from m in lists
                           orderby m.OrderID
                           select new
                           {
                               id = m.ClassID,
                               pid = m.ParentID,
                               name = m.ClassName,
                               type = m.ClassType.ToInt(),
                               rank = m.ClassRank.ToInt(),
                               sort = m.OrderID,
                               keyword = m.Keyword,
                               description = m.Description
                           };
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }



    }
}