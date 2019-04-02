using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;

    public partial interface IFunctionService:IBaseService<Ld_Sys_Function>
    {
        bool SaveFunctionPro(string functionId, string functionName, string parentId, int rankId, bool state);
        bool UpdateFunctionPro(string functionId, string functionName, bool state);
        bool UpdateFunctionStatePro(string functionId, bool state);
        bool DeleteFunctionPro(string functionId);
        Ld_Sys_Function GetFunctionPro(string functionId);
        List<Ld_Sys_Function> GetFunctionByParentIdPro(string parentId);
    }
}
