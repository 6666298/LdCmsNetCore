using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Info;
    public partial class BlockDAL:BaseDAL<Ld_Info_Block>,IBlockDAL
    {
        public BlockDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
