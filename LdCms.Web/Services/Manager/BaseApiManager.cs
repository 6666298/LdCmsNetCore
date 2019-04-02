using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web.Services
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Member;
    using LdCms.IBLL.Sys;
    using LdCms.Common.Json;
    using LdCms.Common.Time;
    /// <summary>
    /// 
    /// </summary>
    public partial class BaseApiManager:IBaseApiManager
    {
        private readonly IApiRecordManager ApiRecordManager;
        private readonly IInterfaceAccountService InterfaceAccountService;
        private readonly IInterfaceAccessTokenService InterfaceAccessTokenService;
        private readonly IAccountService AccountService;
        private readonly IAccessTokenService AccessTokenService;
        public BaseApiManager(IApiRecordManager ApiRecordManager, IInterfaceAccountService InterfaceAccountService, IInterfaceAccessTokenService InterfaceAccessTokenService, IAccountService AccountService, IAccessTokenService AccessTokenService)
        {
            this.ApiRecordManager = ApiRecordManager;
            this.InterfaceAccountService = InterfaceAccountService;
            this.InterfaceAccessTokenService = InterfaceAccessTokenService;
            this.AccountService = AccountService;
            this.AccessTokenService = AccessTokenService;
        }

        public int SystemID = BaseSystemConfig.SystemID;

        public long SaveLogs(string appId)
        {
            return ApiRecordManager.Save(appId);
        }
        public long SaveLogs(string appId, string parameter)
        {
            return ApiRecordManager.Save(appId, parameter);
        }
        public long SaveLogs(string token, object formValue)
        {
            return ApiRecordManager.Save(token, formValue);
        }
        public bool UpdateLogs(long id, string result, bool state)
        {
            return ApiRecordManager.Update(id, result, state);
        }


        public bool IsUuid(string uuid)
        {
            try
            {
                if (string.IsNullOrEmpty(uuid))
                    return false;
                if (uuid.Length != 32)
                    return false;
                return InterfaceAccountService.VerifyInterfaceAccountByUuIDPro(SystemID, uuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return false;
                if (token.Length != 128)
                    return false;
                int timestamp = TimeHelper.GetUnixTimestamp();
                return InterfaceAccessTokenService.VerifyInterfaceAccessTokenPro(token, timestamp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsAccessToken(string accessToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accessToken))
                    return false;
                if (accessToken.Length != 64)
                    return false;
                int timestamp = TimeHelper.GetUnixTimestamp();
                return AccessTokenService.VerifyAccessTokenPro(accessToken, timestamp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Sys_InterfaceAccount GetInterfaceAccount(string companyId, string account)
        {
            try
            {
                var entity = InterfaceAccountService.GetInterfaceAccountPro(SystemID, companyId, account);
                return entity.ToObject<Ld_Sys_InterfaceAccount>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_InterfaceAccount GetInterfaceAccountByToken(string token)
        {
            try
            {
                return InterfaceAccountService.GetInterfaceAccountByTokenPro(SystemID, token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_InterfaceAccount GetInterfaceAccountByUuid(string uuid)
        {
            try
            {
                return InterfaceAccountService.GetInterfaceAccountByUuidPro(SystemID, uuid);
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
                return InterfaceAccountService.GetInterfaceAccountByAppIDPro(SystemID, appid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetMemberAccount(string companyId,string memberId)
        {
            try
            {
                return AccountService.GetAccountPro(SystemID, companyId, memberId);
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
                return AccountService.GetAccountByAccessTokenPro(SystemID, accessToken);
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
                return AccountService.GetAccountByRefreshTokenPro(SystemID, refreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
