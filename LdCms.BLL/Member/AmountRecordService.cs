using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Member
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Member;
    using LdCms.IDAL.Member;
    /// <summary>
    /// 
    /// </summary>
    public partial class AmountRecordService:BaseService<Ld_Member_AmountRecord>,IAmountRecordService
    {
        private readonly IAmountRecordDAL AmountRecordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AmountRecordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAmountRecordDAL AmountRecordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AmountRecordDAL = AmountRecordDAL;
            this.Dal = AmountRecordDAL;
        }
        public override void SetDal()
        {
            Dal = AmountRecordDAL;
        }

    }
}
