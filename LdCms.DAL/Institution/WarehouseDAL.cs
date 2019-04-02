using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IDAL.Institution;
    public partial class WarehouseDAL:BaseDAL<Ld_Institution_Warehouse>,IWarehouseDAL
    {
        public WarehouseDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
