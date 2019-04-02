using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;

    /// <summary>
    /// 系统编号业务逻辑服务类
    /// </summary>
    public partial interface ICodeService:IBaseService<Ld_Sys_Code>
    {
        bool SaveCodePro(int systemId, string systemName, string description, bool state);
        bool DeleteCodePro(int systemId);
        bool UpdateCodePro(int systemId, string systemName, string description, bool state);
        bool UpdateCodeStatePro(int systemId, bool state);
        Ld_Sys_Code GetCodePro(int systemId);
        List<Ld_Sys_Code> GetCodeAllPro();

    }
}
