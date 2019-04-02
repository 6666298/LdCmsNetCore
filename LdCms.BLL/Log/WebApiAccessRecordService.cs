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
    public partial class WebApiAccessRecordService:BaseService<Ld_Log_WebApiAccessRecord>,IWebApiAccessRecordService
    {
        private readonly IWebApiAccessRecordDAL SysWebApiAccessRecordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public WebApiAccessRecordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IWebApiAccessRecordDAL SysWebApiAccessRecordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.SysWebApiAccessRecordDAL = SysWebApiAccessRecordDAL;
            this.Dal = SysWebApiAccessRecordDAL;
        }
        public override void SetDal()
        {
            Dal = SysWebApiAccessRecordDAL;
        }

        public bool IsExists(long id)
        {
            try
            {
                return IsExists(m => m.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool SaveSysWebApiAccessRecord(Ld_Log_WebApiAccessRecord entity)
        {
            try
            {
                entity.State = false;
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = null;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateSysWebApiAccessRecordState(long id, string result, bool state)
        {
            try
            {
                var entity = GetSysWebApiAccessRecord(id);
                entity.Result = result;
                entity.State = state;
                entity.UpdateDate = DateTime.Now;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Log_WebApiAccessRecord GetSysWebApiAccessRecord(long id)
        {
            try
            {
                if (!IsExists(id))
                    throw new Exception("id invalid！");
                return Find(m => m.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
