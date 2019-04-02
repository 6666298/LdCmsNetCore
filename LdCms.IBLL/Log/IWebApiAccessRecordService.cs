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
    public partial interface IWebApiAccessRecordService:IBaseService<Ld_Log_WebApiAccessRecord>
    {
        bool SaveSysWebApiAccessRecord(Ld_Log_WebApiAccessRecord entity);
        bool UpdateSysWebApiAccessRecordState(long id, string result, bool state);
        Ld_Log_WebApiAccessRecord GetSysWebApiAccessRecord(long id);
    }
}
