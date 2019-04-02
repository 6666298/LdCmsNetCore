using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IDAL.Institution;
    /// <summary>
    /// 
    /// </summary>
    public partial class StaffDAL:BaseDAL<Ld_Institution_Staff>,IStaffDAL
    {
        public StaffDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
