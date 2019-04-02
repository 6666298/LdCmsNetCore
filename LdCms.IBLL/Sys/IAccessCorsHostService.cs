using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IAccessCorsHostService:IBaseService<Ld_Sys_AccessCorsHost>
    {
        bool SaveAccessCorsHostPro(int systemId, string webHost, string remark, string account, string nickname, bool state);
        bool UpdateAccessCorsHostPro(int systemId, string webHost, string remark, bool state);
        bool DeleteAccessCorsHostPro(int systemId, string webHost);
        Ld_Sys_AccessCorsHost GetAccessCorsHostPro(int systemId, string webHost);
        List<Ld_Sys_AccessCorsHost> GetAccessCorsHostAllPro(int systemId);

    }
}
