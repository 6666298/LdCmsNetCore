using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Sys
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Sys;

    public partial class InterfaceAccessTokenDAL:BaseDAL<Ld_Sys_InterfaceAccessToken>,IInterfaceAccessTokenDAL
    {
        public InterfaceAccessTokenDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
