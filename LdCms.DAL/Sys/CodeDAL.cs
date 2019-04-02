using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class CodeDAL:BaseDAL<Ld_Sys_Code>, ICodeDAL
    {
        public CodeDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }




    }
}
