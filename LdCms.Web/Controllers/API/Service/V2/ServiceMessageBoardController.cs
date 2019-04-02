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
        private readonly IConfigService ConfigService;
        private readonly IMessageBoardService MessageBoardService;
        public ServiceMessageBoardController(IBaseApiManager BaseApiManager, IConfigService ConfigService, IMessageBoardService MessageBoardService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
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
        [ActionName("list")]
        public IActionResult GetListPaging(string uuid, int page, int size)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(uuid);
                if (!IsUuid(uuid))
                    return Error(logId, "verify uuid fail！");
                var entityInterfaceAccount = GetInterfaceAccountByUuid(uuid);
                string companyId = entityInterfaceAccount.CompanyID;
                int pageId = page.ToInt() <= 0 ? 1 : page;
                int pageSize = size.ToInt() > 100 ? 100 : size;

                bool state = true;
                int totalNum = 0;
                var lists = MessageBoardService.GetMessageBoardPaging(SystemID, companyId, state, pageId, pageSize, out totalNum);
                if (lists == null)
                    return Error(logId, "not data！");
                var data = from m in lists
                           select new
                           {
                               messageid = m.MessageID,
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
                return Success(logId, "ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

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


    }
}