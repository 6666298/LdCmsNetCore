using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LdCms.Common.Extension
{
    /// <summary>
    /// 扩展HttpContext
    /// </summary>
    public static class HttpContext
    {
        private static IHttpContextAccessor Accessors;
        public static Microsoft.AspNetCore.Http.HttpContext Current => Accessors.HttpContext;
        internal static void Configure(IHttpContextAccessor accessors)
        {
            Accessors = accessors;
        }
    }
    public static class StaticHttpContextExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }




}
