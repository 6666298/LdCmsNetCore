using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class InterfaceAccountDAL:BaseDAL<Ld_Sys_InterfaceAccount>,IInterfaceAccountDAL
    {
        public InterfaceAccountDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
