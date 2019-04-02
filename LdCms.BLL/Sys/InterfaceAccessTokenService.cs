using System;
using System.Linq;
using System.Collections;
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
    /// 系统接口访问Token业务逻辑服务类
    /// </summary>
    public partial class InterfaceAccessTokenService:BaseService<Ld_Sys_InterfaceAccessToken>,IInterfaceAccessTokenService
    {
        private readonly IInterfaceAccessTokenDAL InterfaceAccessTokenDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public InterfaceAccessTokenService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IInterfaceAccessTokenDAL InterfaceAccessTokenDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.InterfaceAccessTokenDAL = InterfaceAccessTokenDAL;
            this.Dal = InterfaceAccessTokenDAL;
        }
        public override void SetDal()
        {
            Dal = InterfaceAccessTokenDAL;
        }

        public bool SaveInterfaceAccessTokenPro(string token, int systemId, string appId, int expiresIn, string ipAddress, int createTimestamp)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Sys_InterfaceAccessToken(token, systemId, appId, expiresIn, ipAddress, createTimestamp, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveInterfaceAccessTokenAutoPro(string token, int systemId, string appId, string appSecret, int expiresIn, string ipAddress, int createTimestamp)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Sys_InterfaceAccessTokenAuto(token, systemId, appId, appSecret, expiresIn, ipAddress, createTimestamp, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SP_Get_Sys_InterfaceAccessToken GetInterfaceAccessTokenPro(string token)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_InterfaceAccessToken(token, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                return result.ToObject<List<SP_Get_Sys_InterfaceAccessToken>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetInterfaceAccessTokenTotalNumberPro(int systemId, string appId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_InterfaceAccessTokenTotalNumber(systemId, appId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return 0;
                var lists = result.ToObject<List<SP_Get_Sys_InterfaceAccessTokenTotalNumber>>();
                return lists.FirstOrDefault().TotalNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyInterfaceAccessTokenPro(string token, int timestamp)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Sys_InterfaceAccessToken(token, timestamp, out errCode, out errMsg);
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
