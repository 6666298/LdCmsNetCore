using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers
{
    using LdCms.Common.Security;
    using LdCms.Common.Web;
    using LdCms.Common.VerifyCode;
    /// <summary>
    /// 
    /// </summary>
    public class VerifyCodeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                string randomCode = GeneralCodeHelper.GetRandomString(5);
                string encryptCode = DESEncryptHelper.EncryptDES(randomCode);
                WebHelper.WriteCookie("VerifyCode", encryptCode, 30);
                var imageByte = VerifyCodeHelper.Create(randomCode);
                return File(imageByte, @"image/png");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("get")]
        public string GetVerifyCode(string cdoe)
        {
            try
            {
                string decodeCode = HttpUtility.UrlDecode(cdoe);
                string verifyCode = WebHelper.GetCookie("VerifyCode");
                return DESEncryptHelper.DecryptDES(verifyCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}