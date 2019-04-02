using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IDAL.Institution;
    public partial class StoreDAL:BaseDAL<Ld_Institution_Store>,IStoreDAL
    {
        public StoreDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
