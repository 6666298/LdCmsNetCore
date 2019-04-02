using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Info;
    public partial class NoticeDAL:BaseDAL<Ld_Info_Notice>,INoticeDAL
    {
        public NoticeDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
