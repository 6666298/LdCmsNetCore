using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Extend;
    /// <summary>
    /// 
    /// </summary>
    public partial class AdvertisementDAL:BaseDAL<Ld_Extend_Advertisement>,IAdvertisementDAL
    {
        public AdvertisementDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
