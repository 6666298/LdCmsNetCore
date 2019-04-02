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
    public partial class TableDetailsDAL:BaseDAL<Ld_Log_TableDetails>,ITableDetailsDAL
    {
        public TableDetailsDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
