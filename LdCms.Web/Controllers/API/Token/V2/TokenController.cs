using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace LdCms.Web.Controllers.API.Token.V2
{
    using LdCms.Common;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    [EnableCors("AllowSameDomain")]
    public class TokenController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        public TokenController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
        }

        [HttpGet]
        [ActionName("index")]
        public IActionResult Index()
        {
            try
            {

                var oo = Accessor.HttpContext.Request.GetHttpHeaderValue("Origin");
                var pp = Accessor.HttpContext.Request.GetHttpHeaderValue("Referer");
                var ss = Accessor.HttpContext.Response.Headers["access-control-allow-origin"];
                return Success("ok", new { oo, pp, ss, v = "v2" });
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }


    }
}