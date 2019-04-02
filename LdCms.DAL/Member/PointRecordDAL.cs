using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;

    public partial class PointRecordDAL:BaseDAL<Ld_Member_PointRecord>,IPointRecordDAL
    {
        public PointRecordDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
