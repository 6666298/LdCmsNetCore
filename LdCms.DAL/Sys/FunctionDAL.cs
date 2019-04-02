using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class FunctionDAL:BaseDAL<Ld_Sys_Function>,IFunctionDAL
    {
        public FunctionDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
