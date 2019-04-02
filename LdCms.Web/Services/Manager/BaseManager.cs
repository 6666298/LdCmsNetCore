using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LdCms.Web.Services
{
    using LdCms.IBLL.Sys;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    /// <summary>
    /// 
    /// </summary>
    public partial class BaseManager: IBaseManager
    {
        private readonly IHttpContextAccessor Accessor;
        private readonly IOperatorService OperatorService;
        public BaseManager(IHttpContextAccessor Accessor, IOperatorService OperatorService)
        {
            this.Accessor = Accessor;
            this.OperatorService = OperatorService;
        }
        public int SystemID = BaseSystemConfig.SystemID;

        public bool IsPermission(string companyId, string staffId, string functionId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(functionId))
                    return false;
                bool result = OperatorService.VerifyOperatorPermission(SystemID, companyId, staffId, functionId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 私有化共用方法
        public string GetQueryString(string name)
        {
            try
            {
                var result = Accessor.HttpContext.Request.GetQueryString(name);
                if (string.IsNullOrEmpty(result))
                    return "";
                else
                    return Utility.FilterText(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetFormValue(string name)
        {
            try
            {
                var result = Accessor.HttpContext.Request.GetFormValue(name);
                if (string.IsNullOrEmpty(result))
                    return "";
                else
                    return Utility.FilterText(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetFormValueArr(string name)
        {
            try
            {
                var result = Accessor.HttpContext.Request.GetFormValueArr(name);
                if (string.IsNullOrEmpty(result))
                    return "";
                else
                    return Utility.FilterText(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetFormValue(FormCollection formValue, string name)
        {
            try
            {
                var result = formValue[name];
                if (string.IsNullOrEmpty(result))
                    return "";
                else
                    return Utility.FilterText(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}
