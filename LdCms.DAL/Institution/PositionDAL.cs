using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IDAL.Institution;
    public partial class PositionDAL:BaseDAL<Ld_Institution_Position>,IPositionDAL
    {
        public PositionDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
