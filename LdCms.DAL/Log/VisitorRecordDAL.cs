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
    public partial class VisitorRecordDAL:BaseDAL<Ld_Log_VisitorRecord>,IVisitorRecordDAL
    {
        public VisitorRecordDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
