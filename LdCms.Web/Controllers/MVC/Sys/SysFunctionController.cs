using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Sys
{
    using LdCms.IBLL.Sys;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Web.Services;
    using LdCms.Web.Models;

    /// <summary>
    /// 系统功能管理控制器 已完成
    /// </summary>
    [AdminAuthorize(Roles = "Admins")]
    public class SysFunctionController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IFunctionService FunctionService;
        private readonly string FunctionID;
        public SysFunctionController(IBaseManager BaseManager, IFunctionService FunctionService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.FunctionService = FunctionService;

            this.FunctionID = PermissionEnum.CodeFormat((int)PermissionEnum.Admins.所有者);
        }

        #region 视图操作器
        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            try
            {
                if (!IsPermission(FunctionID))
                    return ToPermission(FunctionID);

                string keyword = GetQueryString("keyword");
                string parentId = string.IsNullOrWhiteSpace(keyword) ? "" : keyword;
                var lists = FunctionService.GetFunctionByParentIdPro(parentId);
                int totalNum = lists.Count();
                ViewBag.keyword = keyword;
                ViewBag.Count = totalNum;
                return View(lists);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            if (!IsPermission(FunctionID) || CompanyID != "sys")
                return ToPermission(FunctionID);
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string functionId)
        {
            try
            {
                if (!IsPermission(FunctionID) || CompanyID != "sys")
                    return ToPermission(FunctionID);
                var entity = FunctionService.GetFunctionPro(functionId);
                return View(entity);
            }
            catch (Exception ex)
            {
                return ToError(ex.Message);
            }
        }
        #endregion

        #region 操作方法
        [HttpPost]
        public JsonResult Save()
        {
            try
            {
                if (!IsPermission(FunctionID) || CompanyID != "sys")
                    return Error("您没有操作权限，请联系系统管理员！");

                string fFirstClass = GetFormValue("fFirstClass");
                string fSecondClass = GetFormValue("fSecondClass");
                string fFunctionId = GetFormValue("fFunctionId");
                string fFunctionName = GetFormValue("fFunctionName");
                string fState = GetFormValue("fState");

                if (fFunctionId.Length != 2)
                    return Error("功能为二位数字！");

                string funcId = GetfunctionId(fFirstClass, fSecondClass, fFunctionId);
                string parentId = GetParentId(fFirstClass, fSecondClass, fFunctionId);
                int rankId = GetRankId(fFirstClass, fSecondClass, fFunctionId);
                bool state = fState.Split(",")[0].ToBool();
                bool result = FunctionService.SaveFunctionPro(funcId, fFunctionName, parentId, rankId, state);
                if (result)
                    return Success("成功");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult Update(string functionId)
        {
            try
            {
                if (!IsPermission(FunctionID) || CompanyID != "sys")
                    return Error("您没有操作权限，请联系系统管理员！");

                string fFunctionName = GetFormValue("fFunctionName");
                string fState = GetFormValue("fState");
                bool state = fState.Split(",")[0].ToBool();
                bool result = FunctionService.UpdateFunctionPro(functionId, fFunctionName, state);
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
        public JsonResult UpdateState(string functionId, bool state)
        {
            try
            {
                if (!IsPermission(FunctionID) || CompanyID != "sys")
                    return Error("您没有操作权限，请联系系统管理员！");

                bool result = FunctionService.UpdateFunctionStatePro(functionId, state);
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
        public JsonResult Delete(string functionId)
        {
            try
            {
                if (!IsPermission(FunctionID) || CompanyID != "sys")
                    return Error("您没有操作权限，请联系系统管理员！");

                bool result = FunctionService.DeleteFunctionPro(functionId);
                if (result)
                    return Success("成功");
                else
                    return Error("fail");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult DeleteBatch(string[] arrFunctionId)
        {
            try
            {
                if (!IsPermission(FunctionID) || CompanyID != "sys")
                    return Error("您没有操作权限，请联系系统管理员！");

                List<object> lists = new List<object>();
                foreach (var functionId in arrFunctionId)
                {
                    bool result = FunctionService.DeleteFunctionPro(functionId);
                    lists.Add(new { function_id = functionId, result });
                }
                return Success("ok", lists);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        public JsonResult GetFunctionByParentId(string parentId = "000000")
        {
            try
            {
                var list = FunctionService.GetFunctionByParentIdPro(parentId);
                var data = from m in list
                           where m.State == true && m.IsDel == false
                           select new
                           {
                               function_id = m.FunctionID,
                               function_name = m.FunctionName,
                               parent_id = m.ParentID,
                               rank_id = m.RankID
                           };
                int totalNum = list.Count();
                var result = new { total = totalNum, list = data };
                return Success("成功", result);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        #endregion

        #region 私有方法
        private string[] ArrParent(string firstClass, string secondClass, string functionId)
        {
            string funcId = string.Empty;
            string parentId = string.Empty;
            string rankId = string.Empty;
            if (string.IsNullOrWhiteSpace(firstClass))
            {
                funcId = string.Format("{0}0000", functionId);
                parentId = "000000";
                rankId = "1";
            }
            if (!string.IsNullOrWhiteSpace(firstClass) && string.IsNullOrWhiteSpace(secondClass))
            {
                funcId = string.Format("{0}{1}00", Utility.Left(firstClass, 2), functionId);
                parentId = firstClass;
                rankId = "2";
            }
            if (!string.IsNullOrWhiteSpace(firstClass) && !string.IsNullOrWhiteSpace(secondClass))
            {
                funcId = string.Format("{0}{1}", Utility.Left(secondClass, 4), functionId);
                parentId = secondClass;
                rankId = "3";
            }
            return new string[] { funcId, parentId, rankId };
        }
        private string GetfunctionId(string firstClass, string secondClass, string functionId)
        {
            return ArrParent(firstClass, secondClass, functionId)[0].ToString();
        }
        private string GetParentId(string firstClass, string secondClass, string functionId)
        {
            return ArrParent(firstClass, secondClass, functionId)[1].ToString();
        }
        private int GetRankId(string firstClass, string secondClass, string functionId)
        {
            return ArrParent(firstClass, secondClass, functionId)[2].ToInt();
        }
        #endregion

    }
}