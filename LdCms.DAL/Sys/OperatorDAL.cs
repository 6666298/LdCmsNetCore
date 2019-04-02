using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class OperatorDAL: BaseDAL<Ld_Sys_Operator>, IOperatorDAL
    {
        public OperatorDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
