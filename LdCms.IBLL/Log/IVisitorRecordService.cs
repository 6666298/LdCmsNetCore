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
    public partial interface IVisitorRecordService:IBaseService<Ld_Log_VisitorRecord>
    {
        bool SaveVisitorRecord(Ld_Log_VisitorRecord entity);


    }
}
