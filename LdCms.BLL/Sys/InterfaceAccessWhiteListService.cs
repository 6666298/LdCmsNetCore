using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Sys
{
    using LdCms.Common.Json;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Sys;
    using LdCms.IDAL.Sys;

    /// <summary>
    /// 系统接口帐号访问白名单业务逻辑服务类
    /// </summary>
    public partial class InterfaceAccessWhiteListService : BaseService<Ld_Sys_InterfaceAccessWhiteList>, IInterfaceAccessWhiteListService
    {
        private readonly IInterfaceAccessWhiteListDAL InterfaceAccessWhiteListDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public InterfaceAccessWhiteListService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IInterfaceAccessWhiteListDAL InterfaceAccessWhiteListDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.InterfaceAccessWhiteListDAL = InterfaceAccessWhiteListDAL;
            this.Dal = InterfaceAccessWhiteListDAL;
        }
        public override void SetDal()
        {
            Dal = InterfaceAccessWhiteListDAL;
        }

        public bool SaveInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress, int classId, string className, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Sys_InterfaceAccessWhiteList(systemId, companyId, account, ipAddress, classId, className, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress, int classId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Sys_InterfaceAccessWhiteList(systemId, companyId, account, ipAddress, classId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress, int classId, string className, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_InterfaceAccessWhiteList(systemId, companyId, account, ipAddress, classId, className, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SP_Get_Sys_InterfaceAccessWhiteList> GetInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_InterfaceAccessWhiteList(systemId, companyId, account, ipAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                return result.ToObject<List<SP_Get_Sys_InterfaceAccessWhiteList>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SP_Get_Sys_InterfaceAccessWhiteListByAccount> GetInterfaceAccessWhiteListByAccountPro(int systemId, string companyId, string account)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_InterfaceAccessWhiteListByAccount(systemId, companyId, account, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                return result.ToObject<List<SP_Get_Sys_InterfaceAccessWhiteListByAccount>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyInterfaceAccessWhiteListPro(int systemId, string companyId, string account, string ipAddress)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Sys_InterfaceAccessWhiteList(systemId, companyId, account, ipAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyInterfaceAccessWhiteListByAccessTokenPro(int systemId, string accessToken, string ipAddress)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Sys_InterfaceAccessWhiteListByAccessToken(systemId, accessToken, ipAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyInterfaceAccessWhiteListByAppIdPro(int systemId, string appId, string ipAddress)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Sys_InterfaceAccessWhiteListByAppId(systemId, appId, ipAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
