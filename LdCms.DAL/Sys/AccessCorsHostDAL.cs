using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class AccessCorsHostDAL : BaseDAL<Ld_Sys_AccessCorsHost>, IAccessCorsHostDAL
    {
        public AccessCorsHostDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
