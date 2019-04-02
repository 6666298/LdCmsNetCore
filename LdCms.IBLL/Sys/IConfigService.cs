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
    public partial interface IConfigService:IBaseService<Ld_Sys_Config>
    {
        bool UpdateConfig(Ld_Sys_Config entity);
        Ld_Sys_Config GetConfig(int systemId, string companyId);

        bool UpdateConfigPro(Ld_Sys_Config entity);
        bool UpdateConfigShieldingPro(int systemId, string companyId,string shielding);
        Ld_Sys_Config GetConfigPro(int systemId, string companyId);

    }
}
