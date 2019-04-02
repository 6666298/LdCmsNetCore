using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.API.Token.V2
{
    using LdCms.Common;
    using LdCms.Common.Json;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Services;

    [ApiVersion("2.0")]
    public class TokenController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        public TokenController(IBaseApiManager BaseApiManager) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
        }

    }
}