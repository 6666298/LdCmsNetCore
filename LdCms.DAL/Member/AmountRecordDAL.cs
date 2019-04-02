using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;

    public partial class AmountRecordDAL:BaseDAL<Ld_Member_AmountRecord>, IAmountRecordDAL
    {
        public AmountRecordDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }


    }
}
