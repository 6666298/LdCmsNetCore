using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;

    public partial interface IInterfaceAccessWhiteListService:IBaseService<Ld_Sys_InterfaceAccessWhiteList>
    {
        bool SaveInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress, int classId, string className, string remark, bool state);
        bool DeleteInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress, int classId);
        bool UpdateInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress, int classId, string className, string remark, bool state);
        List<SP_Get_Sys_InterfaceAccessWhiteList> GetInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress);
        List<SP_Get_Sys_InterfaceAccessWhiteListByAccount> GetInterfaceAccessWhiteListByAccountPro(int systemId, string companyId, string account);
        bool VerifyInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress);
        bool VerifyInterfaceAccessWhiteListByAccessTokenPro(int systemId, string accessToken, string ipAddress);
        bool VerifyInterfaceAccessWhiteListByAppIdPro(int systemId, string appId, string ipAddress);
    }
}
