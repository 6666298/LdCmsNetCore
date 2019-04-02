using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class RoleDAL : BaseDAL<Ld_Sys_Role>, IRoleDAL
    {
        public RoleDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
