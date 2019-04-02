using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LdCms.Web
{
    using LdCms.Web.Models;
    using LdCms.Common.Web;
    using LdCms.Common.Json;
    using LdCms.Common.Extension;
    using LdCms.Common.Security;
    using System.Collections.Generic;

    /// <summary>
    /// 权限验证
    /// </summary>
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 是否验证
        /// </summary>
        private bool validate = true;
        public bool Validate
        {
            get
            {
                return validate;
            }
            set
            {
                validate = value;
            }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Roles { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 验证权限（action执行前会先执行这里）
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {                                  
            if (validate)
            {
                var SiteConfig = ConfigurationHelper.GetAppSettings<SiteConfig>("SiteConfig");
                string loginUrl = SiteConfig.LoginUrl;
                var loginJosn = WebHelper.GetCookie(BaseSystemConfig.SessionName);
                var model = DESEncryptHelper.DecryptDES(loginJosn).ToObject<AccountModel>();
                if (model == null) //如果不存在身份信息
                {
                    filterContext.Result = new RedirectResult(loginUrl);
                }
                else
                {
                    string[] Role = model.Roles.Split(',');//获取所有角色
                    if (!Role.Contains(Roles))//验证权限
                    {
                        filterContext.Result = new RedirectResult(loginUrl);
                    }
                }
            }
        }


    }
}