using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Info
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Info;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.Web.Models;
    using LdCms.Web.Services;
    /// <summary>
    /// 区块管理控制器
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class InfoBlockController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IBlockService BlockService;
        public InfoBlockController(IBaseManager BaseManager, IBlockService BlockService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.BlockService = BlockService;
        }
        public override IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.区块管理.列表);
                if (!IsPermission(funcId)) { return ToPermission(funcId); }

                List<Ld_Info_Block> lists = BlockService.GetBlockAll(SystemID, CompanyID, "");
                ViewBag.Count = lists.Count();
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        public IActionResult Add(string blockId)
        {
            try
            {
                string funcId = string.Empty;
                if (!IsAddPermission(blockId, out funcId)) { return ToPermission(funcId); }

                if (string.IsNullOrWhiteSpace(blockId))
                    return View(new Ld_Info_Block());
                var entity = BlockService.GetBlock(SystemID, CompanyID, blockId);
                if (entity == null)
                    return View(new Ld_Info_Block());
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Save(string blockId)
        {
            try
            {
                if (!IsSavePermission(blockId))
                    return Error("您没有操作权限，请联系系统管理员！");

                string fTitle = GetFormValue("fTitle");
                string fTags = GetFormValue("fTags");
                string fContent = GetFormValue("fContent");

                if (string.IsNullOrWhiteSpace(fTitle))
                    return Error("区块说明不能为空！");
                if (string.IsNullOrWhiteSpace(fTags))
                    return Error("标签不能为空！");
                if (string.IsNullOrWhiteSpace(fContent))
                    return Error("内容不能为空！");

                var entity = new Ld_Info_Block()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    BlockID = blockId,
                    Title = fTitle,
                    Tags = fTags,
                    Content = fContent,
                    State = true,
                    CreateDate = DateTime.Now
                };
                bool result = false;
                if (string.IsNullOrEmpty(blockId))
                    result = BlockService.SaveBlock(entity);
                else
                    result = BlockService.UpdateBlock(entity);
                if (result)
                    return Success("ok");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult Delete(string blockId)
        {
            try
            {
                string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.区块管理.删除);
                if (!IsPermission(funcId)) { return Error("您没有操作权限，请联系系统管理员！"); }

                var result = BlockService.DeleteBlock(SystemID, CompanyID, blockId);
                if (result)
                    return Success("ok");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #region 私有化方法
        public bool IsSavePermission(string blockId)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(blockId))
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.区块管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    string funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.区块管理.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAddPermission(string blockId, out string funcId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(blockId))
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.区块管理.新增);
                    return IsPermission(funcId) ? true : false;
                }
                else
                {
                    funcId = PermissionEnum.CodeFormat((int)PermissionEnum.内容管理.区块管理.编辑);
                    return IsPermission(funcId) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}