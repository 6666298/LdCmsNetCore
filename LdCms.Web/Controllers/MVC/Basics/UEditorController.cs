using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Basics
{
    using LdCms.Plugins.UEditor;
    /// <summary>
    /// 
    /// </summary>
    [Route("Basics/[controller]")]
    [AdminAuthorize(Roles = "Admins")]
    public class UEditorController : Controller
    {
        private UEditorService UEditorService;
        public UEditorController(UEditorService UEditorService)
        {
            this.UEditorService = UEditorService;
        }

        public void Do()
        {
            UEditorService.DoAction(HttpContext);
        }
    }
}