using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Log
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IErrorRecordService:IBaseService<Ld_Log_ErrorRecord>
    {
    }
}
