using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Log
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Log;
    /// <summary>
    /// 
    /// </summary>
    public partial class ErrorRecordDAL:BaseDAL<Ld_Log_ErrorRecord>,IErrorRecordDAL
    {
        public ErrorRecordDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
