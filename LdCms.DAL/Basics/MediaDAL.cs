using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Basics
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Basics;
    public partial class MediaDAL:BaseDAL<Ld_Basics_Media>,IMediaDAL
    {
        public MediaDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
