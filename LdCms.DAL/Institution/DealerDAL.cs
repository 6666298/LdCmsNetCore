using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IDAL.Institution;
    public partial class DealerDAL:BaseDAL<Ld_Institution_Dealer>,IDealerDAL
    {
        public DealerDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
