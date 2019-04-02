using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Basics
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Info;
    using LdCms.Common.Extension;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    using LdCms.Common.Security;
    using System.IO;
    using LdCms.IBLL.Basics;
    using LdCms.Common.Utility;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using LdCms.IBLL.Sys;

    [AdminAuthorize(Roles = "Admins")]
    public class UploadController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IConfigService ConfigService;
        private readonly IMediaService MediaService;
        public UploadController(IBaseManager BaseManager, IHttpContextAccessor Accessor, IHostingEnvironment HostingEnvironment, IConfigService ConfigService, IMediaService MediaService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.Accessor = Accessor;
            this.HostingEnvironment = HostingEnvironment;
            this.ConfigService = ConfigService;
            this.MediaService = MediaService;
        }

        public override IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("file")]
        public JsonResult SaveFileSingle()
        {
            try
            {

                var files = Request.Form.Files;
                int count = files.Count;
                long size = files.Sum(f => f.Length);
                if (count == 0)
                    return Error("上传文件不能为空！");
                if (size == 0)
                    return Error("上传文件大小不能为0字节！");
                var formFile = files.FirstOrDefault();
                var name = formFile.Name;
                var fileSize = formFile.Length;
                var fileName = formFile.FileName;
                string uploadPath = CreateUploadPath(SystemID, CompanyID, fileName);
                string newFileName = CreateFileName(fileName);
                var filePath = string.Format("{0}\\{1}", uploadPath, newFileName);
                string src = ToRelativePath(filePath);
                string url = ToAbsoluteUri(filePath);
                string mediaId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.BasicsMedia);
                var media = new Ld_Basics_Media()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    MediaID = mediaId,
                    FileName = fileName,
                    FileExtension = Path.GetExtension(fileName).ToLower(),
                    FileSize = fileSize,
                    Url = url,
                    Src = src,
                };
                var data = new { mediaid = mediaId, name = fileName, size = fileSize, url, src };
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                        stream.Flush();
                    }
                }
                bool result = MediaService.SaveMedia(media);
                if (result)
                    return Success("ok", new { count, size, file = data });
                else
                    return Error("upload fail！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #region 私有化方法
        private string CreateUploadPath(int systemId, string companyId, string fileName)
        {
            try
            {
                string webRootPath = HostingEnvironment.WebRootPath;
                string UploadRootDirectory = string.Format("{0}\\{1}", webRootPath, GetUploadRootDirectory(systemId, companyId));
                if (!Directory.Exists(UploadRootDirectory))
                    Directory.CreateDirectory(UploadRootDirectory);
                string companyFolder = string.Format("{0}\\{1}", UploadRootDirectory, companyId);
                if (!Directory.Exists(companyFolder))
                    Directory.CreateDirectory(companyFolder);

                string fileCategoryFolder = "file";
                string fileExtension = Path.GetExtension(fileName);
                if (Utility.IsPic(fileName))
                    fileCategoryFolder = "image";
                if (Utility.IsOfficeFile(fileName))
                    fileCategoryFolder = "office";
                if (Utility.IsVideoFile(fileName))
                    fileCategoryFolder = "video";

                string extensionFolder = string.Format("{0}\\{1}", companyFolder, fileCategoryFolder);
                if (!Directory.Exists(extensionFolder))
                    Directory.CreateDirectory(extensionFolder);
                string dateFolder = string.Format("{0}\\{1}", extensionFolder, DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(dateFolder))
                    Directory.CreateDirectory(dateFolder);
                return dateFolder;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string CreateFileName(string fileName)
        {
            try
            {
                string timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
                string guidInt = GeneralCodeHelper.GuidTo16String();
                string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
                return string.Format("{0}_{1}{2}", timeStr, guidInt, fileExtension);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string GetUploadRootDirectory(int systemId, string companyId)
        {
            try
            {
                string rootDirectory = "uploads";
                var entity = ConfigService.GetConfigPro(systemId, companyId);
                if (entity == null)
                    return rootDirectory;
                return string.IsNullOrEmpty(entity.UploadRoot) ? rootDirectory : entity.UploadRoot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string ToRelativePath(string filePath)
        {
            try
            {
                string webRootPath = HostingEnvironment.WebRootPath;
                return filePath.Replace(webRootPath, "").Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string ToAbsoluteUri(string filePath)
        {
            try
            {
                string httpWebRoot = Accessor.HttpContext.Request.GetHttpWebRoot();
                string relativePath = ToRelativePath(filePath);
                return string.Format("{0}{1}", httpWebRoot, relativePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}