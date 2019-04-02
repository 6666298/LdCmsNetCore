using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class InterfaceAccessWhiteListDAL:BaseDAL<Ld_Sys_InterfaceAccessWhiteList>,IInterfaceAccessWhiteListDAL
    {
        public InterfaceAccessWhiteListDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
