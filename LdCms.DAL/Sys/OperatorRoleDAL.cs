using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class OperatorRoleDAL : BaseDAL<Ld_Sys_OperatorRole>, IOperatorRoleDAL
    {
        public OperatorRoleDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
