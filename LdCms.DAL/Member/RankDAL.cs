using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;

    public partial class RankDAL:BaseDAL<Ld_Member_Rank>,IRankDAL
    {
        public RankDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
