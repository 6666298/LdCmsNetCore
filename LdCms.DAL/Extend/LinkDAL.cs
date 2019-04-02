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
    public partial class LinkDAL:BaseDAL<Ld_Extend_Link>,ILinkDAL
    {
        public LinkDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
