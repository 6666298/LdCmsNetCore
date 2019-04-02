using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;
    public partial class RefreshTokenDAL:BaseDAL<Ld_Member_RefreshToken>
    {
        public RefreshTokenDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
