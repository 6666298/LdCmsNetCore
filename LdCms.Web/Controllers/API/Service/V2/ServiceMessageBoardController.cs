using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LdCms.Web.Controllers.API.Service.V2
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Log;
    using LdCms.IBLL.Sys;
    using LdCms.IBLL.Service;
    using LdCms.Common.Net;
    using LdCms.Common.Utility;
    using LdCms.Common.Extension;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("service/messageboard")]
    public class ServiceMessageBoardController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IConfigService ConfigService;
        private readonly IMessageBoardService MessageBoardService;
        public ServiceMessageBoardController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IConfigService ConfigService, IMessageBoardService MessageBoardService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.ConfigService = ConfigService;
            this.MessageBoardService = MessageBoardService;
        }

        [HttpPost]
        [ActionName("save")]
        public IActionResult Save(string uuid, [FromBody]JObject fromValue)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid, fromValue);
                if (!IsUuid(uuid))
                    return Error(logId, "verify uuid fail！");
                bool isParams = IsSaveParams(fromValue);
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                string ipAddress = Net.Ip;
                string name = GetJObjectValue(fromValue, "name");
                string phone = GetJObjectValue(fromValue, "phone");
                string email = GetJObjectValue(fromValue, "email");
                string address = GetJObjectValue(fromValue, "address");
                string content = GetJObjectValue(fromValue, "content");

                if (string.IsNullOrWhiteSpace(name))
                    return Error("昵称不能为空！");
                if (string.IsNullOrWhiteSpace(content))
                    return Error("内容不能为空！");

                var entityConfig = ConfigService.GetConfig(SystemID, companyId);
                if (entityConfig != null)
                {
                    string Shielding = entityConfig.Shielding;
                    if (!string.IsNullOrWhiteSpace(Shielding))
                    {
                        string[] keyword = Shielding.Split("|");
                        if (!IsKeyword(keyword, content))
                            return Error("内容有非法虑字！");
                    }
                }
                var entity = new Ld_Service_MessageBoard()
                {
                    SystemID = SystemID,
                    CompanyID = companyId,
                    Name = name,
                    Phone = phone,
                    Email = email,
                    Address = address,
                    Content = content,
                    IpAddress = ipAddress
                };
                var result = MessageBoardService.SaveMessageBoard(entity);
                if (result)
                    return Success(logId, "ok");
                else
                    return Error(logId, "fail");
            }
            catch (Exception ex)
            {
                return Error(logId, ex.Message);
            }
        }
        [HttpGet("paging")]
        [ActionName("list")]
        public IActionResult GetListPaging(string uuid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid)) { return Error(logId, "verify uuid fail！"); }
                    
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                int pageIndex = Utility.ToPageIndex(Accessor.HttpContext.Request.GetQueryString("page").ToInt());
                int pageCount = Utility.ToPageCount(Accessor.HttpContext.Request.GetQueryString("count").ToInt());

                bool state = true;
                int total = MessageBoardService.CountMessageBoard(SystemID, companyId, state);
                var lists = MessageBoardService.GetMessageBoardPaging(SystemID, companyId, state, pageIndex, pageCount);
                if (lists == null) { return Error(logId, "not data！"); }
                var data = from m in lists
                           select new
                           {
                               id = m.MessageID,
                               company_name = m.CompanyName.IIF(""),
                               name = m.Name.IIF(""),
                               phone = m.Phone.IIF(""),
                               email = m.Email.IIF(""),
                               address = m.Address.IIF(""),
                               content = m.Content.IIF(""),
                               reply = m.Reply.IIF(""),
                               staff_id = m.ReplyStaffId.IIF(""),
                               staff_name = m.ReplyStaffName.IIF(""),
                               reply_time = m.ReplyTime.ToDate().ToString().IIF(""),
                               ip = m.IpAddress.IIF(""),
                               date = m.CreateDate
                           };
                return Success(logId, "ok", new { page = pageIndex, total, rows = data });
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #region 私有方法
        private bool IsKeyword(string[] keyword,string content)
        {
            foreach (var item in keyword)
            {
                if (content.ToLower().Contains(item.ToLower()))
                    return false;
            }
            return true;
        }
        private bool IsSaveParams(JObject formValue)
        {
            try
            {
                if (formValue == null)
                    throw new Exception("params not empty！");
                if (formValue.Property("name") == null)
                    throw new Exception("lack name params！");
                if (formValue.Property("content") == null)
                    throw new Exception("lack content params！");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}