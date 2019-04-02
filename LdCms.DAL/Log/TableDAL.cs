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
    public partial class TableDAL:BaseDAL<Ld_Log_Table>,ITableDAL
    {
        public TableDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }


    }
}
