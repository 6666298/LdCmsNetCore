using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LdCms.Web.Services
{
    using LdCms.Common.Json;
    using LdCms.Common.Extension;
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Log;
    using LdCms.IBLL.Sys;
    using LdCms.Common.Net;
    using Newtonsoft.Json.Linq;
    using LdCms.IBLL.Member;

    /// <summary>
    /// WebApi访问日志操作服务类
    /// 
    /// 
    /// 
    /// </summary>
    public class ApiRecordManager: IApiRecordManager
    {
        private readonly IHttpContextAccessor Accessor;
        private readonly IInterfaceAccountService InterfaceAccountService;
        private readonly IInterfaceAccessTokenService InterfaceAccessTokenService;
        private readonly IAccessTokenService AccessTokenService;
        private readonly IWebApiAccessRecordService SysWebApiAccessRecordService;
        public ApiRecordManager(IHttpContextAccessor Accessor, IInterfaceAccountService InterfaceAccountService, IInterfaceAccessTokenService InterfaceAccessTokenService, IAccessTokenService AccessTokenService,IWebApiAccessRecordService SysWebApiAccessRecordService)
        {
            this.Accessor = Accessor;
            this.InterfaceAccountService = InterfaceAccountService;
            this.InterfaceAccessTokenService = InterfaceAccessTokenService;
            this.AccessTokenService = AccessTokenService;
            this.SysWebApiAccessRecordService = SysWebApiAccessRecordService;
        }
        private int SystemID = BaseSystemConfig.SystemID;

        public long Save(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    str = string.Empty;
                int[] arrSys = new int[] { 16, 128 };
                int[] arrUI = new int[] { 32, 64 };
                RecordType recordType = arrSys.Contains(str.Length) ? RecordType.全局 : RecordType.前端;
                string identityId = GetIdentityID(str);
                return Save(identityId, "", recordType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public long Save(string str, string parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    str = string.Empty;
                int[] arrSys = new int[] { 16, 128 };
                int[] arrUI = new int[] { 32, 64 };
                RecordType recordType = arrSys.Contains(str.Length) ? RecordType.全局 : RecordType.前端;
                string identityId = GetIdentityID(str);
                return Save(identityId, parameter, recordType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public long Save(string token, object formValue)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    token = string.Empty;
                int[] arrSys = new int[] { 16, 128 };
                int[] arrUI = new int[] { 32, 64 };
                RecordType recordType = arrSys.Contains(token.Length) ? RecordType.全局 : RecordType.前端;
                string appId = string.Empty;
                if ((int)recordType == (int)RecordType.全局)
                {
                    var entity = GetSysInterfaceAccessToken(token);
                    appId = entity == null ? "" : entity.AppID;
                }
                else
                {
                    if (token.Length == 64)
                    {
                        var entity = GetMemberAccessToken(token);
                        if (entity != null)
                        {
                            int systemId = entity.SystemID.ToInt();
                            string uuid = entity.Uuid;
                            var entityAccount = GetSysInterfaceAccountByUuID(systemId, uuid);
                            appId = entityAccount == null ? "" : entityAccount.AppID;
                        }
                    }
                    else
                    {
                        var entityAccount = GetSysInterfaceAccountByUuID(SystemID, token);
                        appId = entityAccount == null ? "" : entityAccount.AppID;
                    }
                }
                string parameter = formValue.ToJson();
                return Save(appId, parameter, recordType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public long Save(string identityId, string parameter, RecordType recordType)
        {
            try
            {
                long result = SaveWebApiAccessRecord(identityId, parameter, recordType);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Update(long id, string result, bool state)
        {
            try
            {
                if (id <= 0)
                    return false;
                return SysWebApiAccessRecordService.UpdateSysWebApiAccessRecordState(id, result, state);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetIdentityID(string str)
        {
            try
            {
                string appId = string.Empty;
                if (string.IsNullOrEmpty(str))
                    str = string.Empty;
                if (str.Length == 16)
                    appId = str;
                else if (str.Length == 128)
                {
                    var entityAccessToken = GetSysInterfaceAccessToken(str);
                    appId = entityAccessToken == null ? "" : entityAccessToken.AppID;
                }
                else if (str.Length == 32)
                {
                    var entityAccount = GetSysInterfaceAccountByUuID(SystemID, str);
                    appId = entityAccount == null ? "" : entityAccount.AppID;
                }
                else
                {
                    var entity = GetMemberAccessToken(str);
                    if (entity != null)
                    {
                        int systemId = entity.SystemID.ToInt();
                        string uuid = entity.Uuid;
                        var entityAccount = GetSysInterfaceAccountByUuID(systemId, uuid);
                        appId = entityAccount == null ? "" : entityAccount.AppID;
                    }
                }
                return appId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Ld_Sys_InterfaceAccount GetSysInterfaceAccount(int systemId, string appId)
        {
            try
            {
                var entity = InterfaceAccountService.GetInterfaceAccountByAppIDPro(systemId, appId);
                if (entity == null)
                    return null;
                return entity.ToObject<Ld_Sys_InterfaceAccount>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Ld_Sys_InterfaceAccount GetSysInterfaceAccountByUuID(int systemId, string uuid)
        {
            try
            {
                var entity = InterfaceAccountService.GetInterfaceAccountByUuidPro(systemId, uuid);
                if (entity == null)
                    return null;
                return entity.ToObject<Ld_Sys_InterfaceAccount>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Ld_Sys_InterfaceAccessToken GetSysInterfaceAccessToken(string token)
        {
            try
            {
                var entity = InterfaceAccessTokenService.GetInterfaceAccessTokenPro(token);
                if (entity == null)
                    return null;
                return entity.ToObject<Ld_Sys_InterfaceAccessToken>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Ld_Member_AccessToken GetMemberAccessToken(string token)
        {
            try
            {
                var entity = AccessTokenService.GetAccessToken(token);
                if (entity == null)
                    return null;
                return entity.ToObject<Ld_Member_AccessToken>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private long SaveWebApiAccessRecord(string appId, string parameter, RecordType recordType)
        {
            try
            {
                string url = Accessor.HttpContext.Request.GetAbsoluteUri();
                string method = Accessor.HttpContext.Request.GetHttpMethod();
                string version = string.Empty;
                string controllerName = string.Empty;
                string actionName = "index";
                string parameterName = "";
                string[] arrUrl = url.Split('?')[0].Split('/');
                if (arrUrl.Length >= 5)
                    version = arrUrl[4].ToLower();
                if (arrUrl.Length >= 6)
                    controllerName = arrUrl[5].ToLower();
                if (arrUrl.Length >= 7)
                    actionName = arrUrl[6].ToLower();
                if (arrUrl.Length >= 8)
                    parameterName = arrUrl[7].ToLower();

                string companyId = string.Empty;
                byte totkenType = (byte)recordType;
                string tokenTypeName = recordType.ToString();
                var entityInterfaceAccount = GetSysInterfaceAccount(SystemID, appId);
                if (entityInterfaceAccount != null)
                {
                    companyId = entityInterfaceAccount.CompanyID;
                }
                var entity = new Ld_Log_WebApiAccessRecord()
                {
                    SystemID = SystemID,
                    CompanyID = companyId,
                    AppID = appId,
                    ClassID = (byte)totkenType,
                    ClassName= tokenTypeName,
                    Method = method,
                    Url = url,
                    Parameter = parameter,
                    Version = version,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    ParameterName = parameterName,
                    Result = null,
                    IpAddress = Net.Ip,
                    State = false,
                    CreateDate = DateTime.Now
                };
                var result = SysWebApiAccessRecordService.SaveSysWebApiAccessRecord(entity);
                return result ? entity.ID : 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public enum RecordType
        {
            /// <summary>
            /// 
            /// </summary>
            全局 = 1,
            /// <summary>
            /// 
            /// </summary>
            前端 = 2
        }


    }
}
