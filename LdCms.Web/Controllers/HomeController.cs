using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LdCms.Web.Controllers
{
    using LdCms.Common.Extension;
    using LdCms.IBLL.Info;
    using LdCms.IBLL.Sys;
    using LdCms.Web.Models;
    using Microsoft.AspNetCore.Hosting;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;


    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly SiteConfig SiteConfig;
        private readonly IHttpContextAccessor Accessor;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IConfigService ConfigService;
        private readonly IArticeService ArticeService;

        public HomeController(IOptions<SiteConfig> SiteConfig, IHostingEnvironment HostingEnvironment,IHttpContextAccessor Accessor, IConfigService ConfigService, IArticeService ArticeService)
        {
            this.SiteConfig = SiteConfig.Value;
            this.Accessor = Accessor;
            this.HostingEnvironment = HostingEnvironment;
            this.ConfigService = ConfigService;
            this.ArticeService = ArticeService;
        }
        public IActionResult Index()
        {
            try
            {
                int systemId = BaseSystemConfig.SystemID;
                string companyId = SiteConfig.CompanyID;
                var entity = ConfigService.GetConfig(systemId, companyId);
                string homeUrl =string.IsNullOrEmpty(entity.HomeUrl)? SiteConfig.HomeUrl : entity.HomeUrl;
                return Redirect(homeUrl);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Error()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                @ViewBag.Error = ex.Message;
                return View();
            }
        }
        public JsonResult Show()
        {
            List<object> result = new List<object>();
            for (var i = 0; i < 10; i++)
            {
                result.Add(ArticeService.SaveArtice(new EF.DbModels.Ld_Info_Artice()
                {
                    SystemID = 100101,
                    CompanyID = "300001",
                    ClassID = "4312059490",
                    ClassName = "公司新闻",
                    Title = Common.Security.GeneralCodeHelper.GetRandomString(32),
                    Content = "公司新闻"
                }));
            }
            return Json(result);
        }

        public IActionResult Content()
        {
            string WebRootPath = HostingEnvironment.WebRootPath;

            string rootdir = AppContext.BaseDirectory;
            DirectoryInfo Dir = Directory.GetParent(rootdir);
            string root = Dir.Parent.Parent.FullName;


            var s = Accessor.HttpContext.Request.Host;
            string s1 = s.Value;

            string s2 = Accessor.HttpContext.Request.GetHttpWebRoot();

            return Content(s2);
        }

        [HttpPost]
        public JsonResult UploadFiles()
        {
            try
            {
                string uploads = "uploads";
                string webRootPath = HostingEnvironment.WebRootPath;
                var files = Request.Form.Files;
                int count = files.Count;
                long size = files.Sum(f => f.Length);
                List<object> data = new List<object>();
                foreach (var formFile in files)
                {
                    var name = formFile.Name;
                    var fileSize = formFile.Length;
                    var fileName = formFile.FileName;
                    var filePath = string.Format("{0}\\{1}\\{2}", webRootPath, uploads, fileName);
                    data.Add(new { name, fileSize, fileName, filePath });
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                            stream.Flush();
                        }
                    }
                }
                return Json(new { count, size, data });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

 



}
