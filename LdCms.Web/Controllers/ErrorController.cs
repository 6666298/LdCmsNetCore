using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers
{
    using LdCms.Web.Services;
    /// <summary>
    /// 
    /// </summary>
    public class ErrorController : BaseController
    {
        private readonly IBaseManager BaseManager;
        public ErrorController(IBaseManager BaseManager) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
        }

        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult Show(int errCode, string errMsg)
        {
            try
            {
                ViewData["ErrCode"] = errCode;
                ViewData["ErrMsg"] = errMsg;

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Permission(string funcId)
        {
            try
            {
                ViewData["FuncID"] = string.Format("系统功能编号：{0}", funcId);
                ViewData["Message"] = "您没有操作权限，请联系系统管理员！";
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Developing()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}