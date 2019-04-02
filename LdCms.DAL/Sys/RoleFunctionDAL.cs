using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class RoleFunctionDAL : BaseDAL<Ld_Sys_RoleFunction>, IRoleFunctionDAL
    {
        public RoleFunctionDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
