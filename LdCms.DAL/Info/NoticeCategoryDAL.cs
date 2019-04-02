using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Info;
    public partial class NoticeCategoryDAL:BaseDAL<Ld_Info_NoticeCategory>,INoticeCategoryDAL
    {
        public NoticeCategoryDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
