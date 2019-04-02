using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Sys
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    /// <summary>
    /// 
    /// </summary>
    public partial interface IOperatorService:IBaseService<Ld_Sys_Operator>
    {
        
        bool SaveOperatorPro(int systemId, string companyId, string staffId, string roleId, string remark, bool state);
        bool DeleteOperatorPro(int systemId, string companyId, string staffId);
        bool UpdateOperatorPro(int systemId, string companyId, string staffId, string roleId, string remark, bool state);
        bool UpdateOperatorPasswordPro(int systemId, string companyId, string staffId, string password);
        bool UpdateOperatorRolePro(int systemId, string companyId, string staffId, string roleId);
        bool UpdateOperatorStatePro(int systemId, string companyId, string staffId, bool state);
        V_Sys_Operator GetOperatorPro(int systemId, string companyId, string staffId);
        List<V_Sys_Operator> GetOperatorPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount);
        List<V_Sys_Operator> SearchOperatorPro(int systemId, string companyId, string startTime, string endTime, string keyword);
        bool VerifyOperatorPermission(int systemId, string companyId, string staffId, string functionId);
    }
}
