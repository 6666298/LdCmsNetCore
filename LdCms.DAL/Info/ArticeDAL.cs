using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Info;
    /// <summary>
    /// 
    /// </summary>
    public partial class ArticeDAL:BaseDAL<Ld_Info_Artice>,IArticeDAL
    {
        public ArticeDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
