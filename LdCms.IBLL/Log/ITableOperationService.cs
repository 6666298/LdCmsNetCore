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
    public partial interface ITableOperationService:IBaseService<Ld_Log_TableOperation>
    {
        bool SaveTableOperation(Ld_Log_TableOperation entity);
        bool SaveTableOperation(Ld_Log_TableOperation entity, out long tableOperationID);
        bool UpdateTableOperationState(long id, bool state);
        bool DeleteTableOperation(long id);
        Ld_Log_TableOperation GetTableOperation(long id);
        List<Ld_Log_TableOperation> GetTableOperationTop(int count);
        List<Ld_Log_TableOperation> GetTableOperationPaging(int pageId, int pageSize, out int totalRows);
        List<Ld_Log_TableOperation> SearchTableOperation(string startTime, string endTime, string clientId, string classId,string keyword);


    }
}
