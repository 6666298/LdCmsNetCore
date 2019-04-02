using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IDAL.Institution;
    public partial class DepartmentDAL:BaseDAL<Ld_Institution_Department>,IDepartmentDAL
    {
        public DepartmentDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
