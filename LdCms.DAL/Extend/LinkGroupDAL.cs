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
    public partial class LinkGroupDAL:BaseDAL<Ld_Extend_LinkGroup>,ILinkGroupDAL
    {
        public LinkGroupDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
