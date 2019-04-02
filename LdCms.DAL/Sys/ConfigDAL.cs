using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class ConfigDAL:BaseDAL<Ld_Sys_Config>,IConfigDAL
    {
        public ConfigDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }


    }
}
