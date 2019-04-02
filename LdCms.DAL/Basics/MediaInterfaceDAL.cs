using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Basics
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Basics;
    public partial class MediaInterfaceDAL:BaseDAL<Ld_Basics_MediaInterface>,IMediaInterfaceDAL
    {
        public MediaInterfaceDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
