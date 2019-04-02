using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class VersionDAL : BaseDAL<Ld_Sys_Version>, IVersionDAL
    {
        public VersionDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
