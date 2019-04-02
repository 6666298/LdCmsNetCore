using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;

    /// <summary>
    /// 
    /// </summary>
    public partial interface IInterfaceAccountService:IBaseService<Ld_Sys_InterfaceAccount>
    {
        bool SaveInterfaceAccountPro(int systemId, string companyId, string account, string password, bool isWcf, bool isWeb, bool isApi, bool isCors, string description, bool state);
        bool DeleteInterfaceAccountPro(int systemId, string companyId, string account);
        bool UpdateInterfaceAccountPro(int systemId, string companyId, string account, string password, bool isWcf, bool isWeb, bool isApi, bool isCors, string description, bool state);
        bool UpdateInterfaceAccountAppKeyPro(int systemId, string companyId, string account);
        bool UpdateInterfaceAccountAppSecretPro(int systemId, string companyId, string account);
        bool UpdateInterfaceAccountDelPro(int systemId, string companyId, string account, bool isDel);
        bool UpdateInterfaceAccountPasswordPro(int systemId, string companyId, string account, string password);
        bool UpdateInterfaceAccountStatePro(int systemId, string companyId, string account, bool state);
        Ld_Sys_InterfaceAccount GetInterfaceAccountPro(int systemId, string companyId, string account);
        Ld_Sys_InterfaceAccount GetInterfaceAccountByAppIDPro(int systemId, string appId);
        Ld_Sys_InterfaceAccount GetInterfaceAccountByUuidPro(int systemId, string uuid);
        Ld_Sys_InterfaceAccount GetInterfaceAccountByTokenPro(int systemId, string token);
        List<Ld_Sys_InterfaceAccount> GetInterfaceAccountAllPro(int systemId, string companyId);
        List<Ld_Sys_InterfaceAccount> GetInterfaceAccountPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount);
        List<Ld_Sys_InterfaceAccount> SearchInterfaceAccountPro(int systemId, string companyId, string startTime, string endTime, string keyword);

        bool VerifyInterfaceAccountPro(int systemId, string companyId, string account, string password, int classId);
        bool VerifyInterfaceAccountByAppIDPro(int systemId, string appId, string appSecret);
        bool VerifyInterfaceAccountByUuIDPro(int systemId, string uuid);
    }
}
