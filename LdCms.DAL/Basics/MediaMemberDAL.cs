using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Basics
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Basics;
    public partial class MediaMemberDAL:BaseDAL<Ld_Basics_MediaMember>,IMediaMemberDAL
    {
        public MediaMemberDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
