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
    public partial interface ITableService:IBaseService<Ld_Log_Table>
    {
        bool SaveTable(Ld_Log_Table entity);
        int SaveTable(Ld_Log_Table entity, List<Ld_Log_TableDetails> list);
        bool UpdateTableBusinessName(string tableId, string businessName, string remark);
        bool DeleteTable();
        bool DeleteTable(string tableId);
        bool DeleteTable(Ld_Log_Table entity);
        Ld_Log_Table GetTable(string tableId);
        Ld_Log_Table GetTableByName(string tableName);
        List<Ld_Log_Table> GetTableTop(int count);
        List<Ld_Log_Table> GetTablePaging(int pageId, int pageSize, out int totalRows);
        List<Ld_Log_Table> SearchTable(string keyword);

    }
}
