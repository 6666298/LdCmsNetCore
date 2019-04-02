using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web.Services
{
    using LdCms.EF.DbModels;
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBaseApiManager
    {
        long SaveLogs(string appId);
        long SaveLogs(string appId, string parameter);
        long SaveLogs(string token, object formValue);
        bool UpdateLogs(long id, string result, bool state);
        bool IsUuid(string uuid);
        bool IsToken(string token);
        bool IsAccessToken(string accessToken);
        Ld_Sys_InterfaceAccount GetInterfaceAccount(string companyId, string account);
        Ld_Sys_InterfaceAccount GetInterfaceAccountByToken(string token);
        Ld_Sys_InterfaceAccount GetInterfaceAccountByUuid(string uuid);
        Ld_Sys_InterfaceAccount GetInterfaceAccountByAppID(string appid);
        Ld_Member_Account GetMemberAccount(string companyId, string memberId);
        Ld_Member_Account GetMemberAccountByAccessToken(string accessToken);
        Ld_Member_Account GetMemberAccountByRefreshToken(string refreshToken);


    }
}
