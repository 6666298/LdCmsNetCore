using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Info;
    public partial class PageDAL:BaseDAL<Ld_Info_Page>,IPageDAL
    {
        public PageDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
