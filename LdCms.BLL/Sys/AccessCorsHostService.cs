using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LdCms.BLL.Sys
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Sys;
    using LdCms.IDAL.Sys;
    using LdCms.Common.Json;
    /// <summary>
    /// 
    /// </summary>
    public partial class AccessCorsHostService:BaseService<Ld_Sys_AccessCorsHost>,IAccessCorsHostService
    {
        private readonly IAccessCorsHostDAL AccessCorsHostDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AccessCorsHostService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAccessCorsHostDAL AccessCorsHostDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AccessCorsHostDAL = AccessCorsHostDAL;
            this.Dal = AccessCorsHostDAL;
        }
        public override void SetDal()
        {
            Dal = AccessCorsHostDAL;
        }

        public bool SaveAccessCorsHostPro(int systemId, string webHost, string remark, string account, string nickname, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Sys_AccessCorsHost(systemId, webHost, remark, account, nickname, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccessCorsHostPro(int systemId, string webHost, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_AccessCorsHost(systemId, webHost, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAccessCorsHostPro(int systemId, string webHost)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Sys_AccessCorsHost(systemId, webHost, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_AccessCorsHost GetAccessCorsHostPro(int systemId, string webHost)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_AccessCorsHost(systemId, webHost, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result == null ? null : result.ToObject<List<Ld_Sys_AccessCorsHost>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Sys_AccessCorsHost> GetAccessCorsHostAllPro(int systemId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_AccessCorsHostAll(systemId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result == null ? null : result.ToObject<List<Ld_Sys_AccessCorsHost>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
