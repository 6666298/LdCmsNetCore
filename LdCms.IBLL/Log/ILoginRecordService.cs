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
    public partial interface ILoginRecordService:IBaseService<Ld_Log_LoginRecord>
    {
        bool SaveLoginRecord(Ld_Log_LoginRecord entity);
        bool DeleteLoginRecord(int systemId, string companyId, long id);
        bool DeleteLoginRecordAll(int systemId, string companyId);
        Ld_Log_LoginRecord GetLoginRecord(long id);
        Ld_Log_LoginRecord GetLoginRecord(int systemId, string companyId, long id);
        List<Ld_Log_LoginRecord> GetLoginRecordTop(int systemId, string companyId, int count);
        List<Ld_Log_LoginRecord> GetLoginRecordPaging(int systemId, string companyId, int pageId, int pageSize, out int totalRows);
        List<Ld_Log_LoginRecord> SearchLoginRecord(int systemId, string companyId, string startTime, string endTime, string clientId, string keyword);


    }
}
