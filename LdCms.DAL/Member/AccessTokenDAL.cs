using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;

    public partial class AccessTokenDAL : BaseDAL<Ld_Member_AccessToken>, IAccessTokenDAL
    {
        public AccessTokenDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
