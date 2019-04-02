using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web
{
    using LdCms.Common.Json;
    using LdCms.Common.Time;
    using LdCms.Common.Utility;
    using LdCms.EF.DbModels;
    using LdCms.Web.Services;
    using Newtonsoft.Json.Linq;
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("cgi-bin/v{version:apiVersion}/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private readonly IBaseApiManager BaseApiManager;
        public BaseApiController(IBaseApiManager BaseApiManager)
        {
            this.BaseApiManager = BaseApiManager;
        }


        protected int SystemID = BaseSystemConfig.SystemID;
        protected int AccessTokenExpiresIn = BaseSystemConfig.AccessTokenExpiresIn;
        protected int RefreshTokenExpiresIn = BaseSystemConfig.RefreshTokenExpiresIn;

        private IActionResult ContentResult(string result)
        {
            var contentResult = new ContentResult
            {
                Content = result,
                ContentType = "application/json; charset=utf-8",
                StatusCode = 200
            };
            return contentResult;
        }
        protected virtual IActionResult Result(string data)
        {
            return ContentResult(data);
        }
        protected virtual IActionResult Result(object data)
        {
            string result = data.ToJson();
            return ContentResult(result);
        }
        protected virtual IActionResult Success(string message)
        {
            var entity = new { errcode = 0, errmsg = message };
            string result = entity.ToJson();
            return ContentResult(result);
        }
        protected virtual IActionResult Success(string message, object data)
        {
            var entity = new { errcode = 0, errmsg = message, data };
            string result = entity.ToJson();
            return ContentResult(result);
        }
        protected virtual IActionResult Error(string message)
        {
            var entity = new { errcode = 99, errmsg = message };
            string result = entity.ToJson();
            return ContentResult(result);
        }
        protected virtual IActionResult Error(int errCode, string message)
        {
            var entity = new { errcode = errCode, errmsg = message };
            string result = entity.ToJson();
            return ContentResult(result);
        }
        protected virtual IActionResult Result(long logId, string data)
        {
            BaseApiManager.UpdateLogs(logId, data, true);
            return ContentResult(data);
        }
        protected virtual IActionResult Result(long logId, object data)
        {
            string result = data.ToJson();
            BaseApiManager.UpdateLogs(logId, result, true);
            return ContentResult(result);
        }
        protected virtual IActionResult Success(long logId, string message)
        {
            var entity = new { errcode = 0, errmsg = message };
            string result = entity.ToJson();
            BaseApiManager.UpdateLogs(logId, result, true);
            return ContentResult(result);
        }
        protected virtual IActionResult Success(long logId, string message, object data)
        {
            var entity = new { errcode = 0, errmsg = message, data };
            string result = entity.ToJson();
            BaseApiManager.UpdateLogs(logId, result, true);
            return ContentResult(result);
        }
        protected virtual IActionResult Error(long logId, string message)
        {
            var entity = new { errcode = 99, errmsg = message };
            string result = entity.ToJson();
            BaseApiManager.UpdateLogs(logId, result, true);
            return ContentResult(result);
        }
        protected virtual IActionResult Error(long logId, int errCode, string message)
        {
            var entity = new { errcode = errCode, errmsg = message };
            string result = entity.ToJson();
            BaseApiManager.UpdateLogs(logId, result, true);
            return ContentResult(result);
        }

        protected virtual bool IsUuid(string uuid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uuid))
                    return false;
                if (uuid.Length != 32)
                    return false;
                return BaseApiManager.IsUuid(uuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected virtual bool IsToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return false;
                if (token.Length != 128)
                    return false;
                int timestamp = TimeHelper.GetUnixTimestamp();
                return BaseApiManager.IsToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected virtual bool IsAccessToken(string accessToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accessToken))
                    return false;
                if (accessToken.Length != 64)
                    return false;
                int timestamp = TimeHelper.GetUnixTimestamp();
                return BaseApiManager.IsAccessToken(accessToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected virtual Ld_Sys_InterfaceAccount GetInterfaceAccountByToken(string token)
        {
            try
            {
                return BaseApiManager.GetInterfaceAccountByToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected virtual Ld_Sys_InterfaceAccount GetInterfaceAccountByUuid(string uuid)
        {
            try
            {
                return BaseApiManager.GetInterfaceAccountByUuid(uuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_InterfaceAccount GetInterfaceAccountByAppID(string appid)
        {
            try
            {
                return BaseApiManager.GetInterfaceAccountByAppID(appid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetMemberAccountByAccessToken(string accessToken)
        {
            try
            {
                return BaseApiManager.GetMemberAccountByAccessToken(accessToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetMemberAccountByRefreshToken(string refreshToken)
        {
            try
            {
                return BaseApiManager.GetMemberAccountByRefreshToken(refreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected virtual string GetJObjectValue(JObject formValue, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return "";
                if (formValue.Property(name) == null)
                    return "";
                var result = formValue[name];
                if (result == null)
                    return "";
                else
                    return Utility.FilterText(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}