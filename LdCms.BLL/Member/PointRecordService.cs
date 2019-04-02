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
    public partial class PointRecordService:BaseService<Ld_Member_PointRecord>,IPointRecordService
    {
        private readonly IPointRecordDAL PointRecordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public PointRecordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IPointRecordDAL PointRecordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.PointRecordDAL = PointRecordDAL;
            this.Dal = PointRecordDAL;
        }
        public override void SetDal()
        {
            Dal = PointRecordDAL;
        }

    }
}
