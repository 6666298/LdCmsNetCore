using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    [ControllerName("basics/upload")]
    public class BasicsUploadController : BaseApiController
    {
        private readonly IBaseApiManager BaseApiManager;
        private readonly IHttpContextAccessor Accessor;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IConfigService ConfigService;
        private readonly IAccountService AccountService;
        private readonly IMediaService MediaService;
        public BasicsUploadController(IBaseApiManager BaseApiManager, IHttpContextAccessor Accessor, IHostingEnvironment HostingEnvironment, IConfigService ConfigService, IAccountService AccountService, IMediaService MediaService) : base(BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
            this.Accessor = Accessor;
            this.HostingEnvironment = HostingEnvironment;
            this.ConfigService = ConfigService;
            this.AccountService = AccountService;
            this.MediaService = MediaService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns>
        /// /cgi-bin/v2/basics/upload/index?uuid=460e64203493444ba27d4fc7ad7efae8
        /// </returns>
        [HttpGet]
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
        /// 单个上传文件
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpPost("single")]
        [ActionName("file")]
        public IActionResult SaveFileSingle(string access_token)
        {
            try
            {
                int systemId = SystemID;
                if (!IsAccessToken(access_token))
                    return Error("验证token失败！");
                var entity = AccountService.GetAccountByAccessTokenPro(systemId, access_token);
                if (entity == null)
                    return Error("验证会员资料失败！");
                if (string.IsNullOrEmpty(entity.CompanyID))
                    return Error("验证会员资料失败！");
                string companyId = entity.CompanyID;
                string memberId = entity.MemberID;

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
                string uploadPath = CreateUploadPath(systemId, companyId, fileName);
                string newFileName = CreateFileName(fileName);
                var filePath = string.Format("{0}\\{1}", uploadPath, newFileName);
                string src = ToRelativePath(filePath);
                string url = ToAbsoluteUri(filePath);
                string mediaId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.BasicsMedia);
                var media = new Ld_Basics_Media()
                {
                    SystemID = systemId,
                    CompanyID = companyId,
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
                int result = MediaService.SaveMediaMember(memberId, media);
                if (result > 0)
                    return Success("ok", new { count, size, file = data });
                else
                    return Error("upload fail！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpPost("batch")]
        [ActionName("file")]
        public IActionResult SaveFileBatch(string access_token)
        {
            try
            {
                int systemId = SystemID;
                if (!IsAccessToken(access_token))
                    return Error("验证token失败！");
                var entity = AccountService.GetAccountByAccessTokenPro(systemId, access_token);
                if (entity == null)
                    return Error("验证会员资料失败！");
                if (string.IsNullOrEmpty(entity.CompanyID))
                    return Error("验证会员资料失败！");
                string companyId = entity.CompanyID;
                string memberId = entity.MemberID;

                var files = Request.Form.Files;
                int count = files.Count;
                long size = files.Sum(f => f.Length);
                if (count == 0)
                    return Error("上传文件不能为空！");
                if (size == 0)
                    return Error("上传文件大小不能为0字节！");

                List<object> lists = new List<object>();
                List<Ld_Basics_Media> media = new List<Ld_Basics_Media>();
                foreach (var formFile in files)
                {
                    var name = formFile.Name;
                    var fileSize = formFile.Length;
                    var fileName = formFile.FileName;
                    string uploadPath= CreateUploadPath(systemId, companyId, fileName);
                    string newFileName = CreateFileName(fileName);
                    var filePath = string.Format("{0}\\{1}", uploadPath, newFileName);
                    string src = ToRelativePath(filePath);
                    string url = ToAbsoluteUri(filePath);
                    string mediaId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.BasicsMedia);
                    media.Add(new Ld_Basics_Media()
                    {
                        SystemID = systemId,
                        CompanyID = companyId,
                        MediaID = mediaId,
                        FileName = fileName,
                        FileExtension = Path.GetExtension(fileName).ToLower(),
                        FileSize = fileSize,
                        Url = url,
                        Src = src,
                    });
                    lists.Add(new { mediaid = mediaId, name = fileName, size = fileSize, url, src });
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                            stream.Flush();
                        }
                    }
                }
                int result = MediaService.SaveMediaMember(memberId, media);
                if (result > 0)
                    return Success("ok", new { count, size, file = lists });
                else
                    return Error("upload fail！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        ///  将echarts返回的base64 转成图片
        /// </summary>
        /// <param name="image">图片的base64形式</param>
        /// <param name="proname">项目区分</param>
        [HttpPost("code")]
        [ActionName("image")]
        public IActionResult SaveImageCode(string access_token)
        {
            try
            {
                int systemId = SystemID;
                if (!IsAccessToken(access_token))
                    return Error("验证token失败！");
                var entity = AccountService.GetAccountByAccessTokenPro(systemId, access_token);
                if (entity == null)
                    return Error("验证会员资料失败！");
                if (string.IsNullOrEmpty(entity.CompanyID))
                    return Error("验证会员资料失败！");
                string companyId = entity.CompanyID;
                string memberId = entity.MemberID;

                string base64String = Accessor.HttpContext.Request.GetInputStream();
                string imageCode = ImageHelper.GetImageCode(base64String);
                long fileSize = imageCode.Length;
                string fileName = string.Format("{0}.{1}", GeneralCodeHelper.GetRandomInt(8), ImageHelper.GetImageExtension(base64String));
                string uploadPath = CreateUploadPath(systemId, companyId, fileName);
                string newFileName = CreateFileName(fileName);
                var filePath = string.Format("{0}\\{1}", uploadPath, newFileName);
                string src = ToRelativePath(filePath);
                string url = ToAbsoluteUri(filePath);
                string mediaId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.BasicsMedia);
                var media = new Ld_Basics_Media()
                {
                    SystemID = systemId,
                    CompanyID = companyId,
                    MediaID = mediaId,
                    FileName = fileName,
                    FileExtension = Path.GetExtension(fileName).ToLower(),
                    FileSize = fileSize,
                    Url = url,
                    Src = src,
                };
                var data = new { mediaid = mediaId, name = fileName, size = fileSize, url, src };
                if (fileSize > 0)
                {
                    string savePath = ImageHelper.Base64StringToImage(filePath, base64String);
                }
                int result = MediaService.SaveMediaMember(memberId, media);
                if (result > 0)
                    return Success("ok", new { count = 1, size = fileSize, file = data });
                else
                    return Error("upload fail！");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #region 私有化方法
        private string CreateUploadPath(int systemId, string companyId,string fileName)
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
        private string GetUploadRootDirectory(int systemId,string companyId)
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