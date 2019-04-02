using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Log
{
    using LdCms.Common.Json;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Log;
    using LdCms.IDAL.Log;
    /// <summary>
    /// 
    /// </summary>
    public partial class ErrorRecordService:BaseService<Ld_Log_ErrorRecord>,IErrorRecordService
    {
        private readonly IErrorRecordDAL ErrorRecordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public ErrorRecordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IErrorRecordDAL ErrorRecordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.ErrorRecordDAL = ErrorRecordDAL;
            this.Dal = ErrorRecordDAL;
        }
        public override void SetDal()
        {
            Dal = ErrorRecordDAL;
        }

    }
}
