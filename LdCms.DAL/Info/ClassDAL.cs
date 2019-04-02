using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Info;
    public partial class ClassDAL:BaseDAL<Ld_Info_Class>,IClassDAL
    {
        public ClassDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
