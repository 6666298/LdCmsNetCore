using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Log
{
    using LdCms.Common.Json;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Log;
    using LdCms.IDAL.Log;
    /// <summary>
    /// 
    /// </summary>
    public partial class VisitorRecordService:BaseService<Ld_Log_VisitorRecord>,IVisitorRecordService
    {
        private readonly IVisitorRecordDAL VisitorRecordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public VisitorRecordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IVisitorRecordDAL VisitorRecordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.VisitorRecordDAL = VisitorRecordDAL;
            this.Dal = VisitorRecordDAL;
        }
        public override void SetDal()
        {
            Dal = VisitorRecordDAL;
        }

        public bool SaveVisitorRecord(Ld_Log_VisitorRecord entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
