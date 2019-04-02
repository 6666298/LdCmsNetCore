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
    public partial class TableOperationDAL:BaseDAL<Ld_Log_TableOperation>,ITableOperationDAL
    {
        public TableOperationDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
