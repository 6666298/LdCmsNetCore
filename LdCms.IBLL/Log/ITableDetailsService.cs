using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Log
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    /// <summary>
    /// 
    /// </summary>
    public partial interface ITableDetailsService:IBaseService<Ld_Log_TableDetails>
    {
        bool SaveTableDetails(Ld_Log_TableDetails entity);
        int SaveTableDetails(List<Ld_Log_TableDetails> list);
        bool UpdateTableDetailsPrimaryKey(long id, bool isPrimaryKey);
        bool UpdateTableDetailsColumnText(long id, string columnText, string remark);
        bool DeleteTableDetails();
        bool DeleteTableDetails(long id);
        bool DeleteTableDetails(Ld_Log_TableDetails entity);
        Ld_Log_TableDetails GetTableDetails(long id);
        List<Ld_Log_TableDetails> GetTableDetailsByTableID(string tableId);
    }
}
