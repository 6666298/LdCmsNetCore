using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace LdCms.Web.Controllers.API.Basics.V2
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Basics;
    using LdCms.IBLL.Member;
    using LdCms.IBLL.Sys;
    using LdCms.Common.Extension;
    using LdCms.Common.Image;
    using LdCms.Common.Security;
    using LdCms.Common.Utility;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    [ControllerName("basics/media")]
    public class BasicsMediaController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IAccountService AccountService;
        private readonly IMediaService MediaService;
        private readonly IVMediaService VMediaService;
        public BasicsMediaController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IHostingEnvironment HostingEnvironment, IAccountService AccountService, IMediaService MediaService, IVMediaService VMediaService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.HostingEnvironment = HostingEnvironment;
            this.AccountService = AccountService;
            this.MediaService = MediaService;
            this.VMediaService = VMediaService;
        }
        [ActionName("index")]
        public IActionResult Index(string uuid)
        {
            try
            {
                string index = "index";
                var data = new { index, uuid };
                return Success("ok", data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token">531b34ef675f4bd781b1b6c384628ff6a7c7c089596b1bde3757862000301182</param>
        /// <param name="mediaid">3019031053994126</param>
        /// <returns>
        /// cgi-bin/v2/basics/media/get?access_token=531b34ef675f4bd781b1b6c384628ff6a7c7c089596b1bde3757862000301182&mediaid=3019031053994126
        /// </returns>
        [HttpGet]
        [ActionName("get")]
        public IActionResult GetMedia(string access_token, string mediaid)
        {
            long logId = 0;
            try
            {
                logId = BaseApiManager.SaveLogs(access_token);
                if (!IsAccessToken(access_token)) { return Error(logId, "验证token失败！"); }
                var entityMember = AccountService.GetAccountByAccessTokenPro(SystemID, access_token);
                if (entityMember == null) { return Error(logId, "验证会员资料失败！"); }
                if (string.IsNullOrEmpty(entityMember.CompanyID)) { return Error(logId, "公司编号不能为空！"); }
                string companyId = entityMember.CompanyID;
                string memberId = entityMember.MemberID;

                var entity = VMediaService.GetVMedia(SystemID, companyId, memberId, mediaid);
                if (entity == null) { return Error(logId, "mediaid invalid！"); }
                var data = new
                {
                    mediaid = entity.MediaID,
                    name = entity.FileName,
                    size = entity.FileSize,
                    url = entity.Url,
                    src = entity.Src
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