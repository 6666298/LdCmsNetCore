using System;
using System.Linq;
using System.Collections.Generic;

namespace LdCms.BLL.Sys
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Sys;
    using LdCms.IDAL.Sys;
    using LdCms.Common.Json;
    using LdCms.Common.Extension;
    /// <summary>
    /// 系统配置业务逻辑服务类
    /// </summary>
    public partial class ConfigService:BaseService<Ld_Sys_Config>,IConfigService
    {
        private readonly IConfigDAL ConfigDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public ConfigService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IConfigDAL ConfigDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.ConfigDAL = ConfigDAL;
            this.Dal = ConfigDAL;
        }
        public override void SetDal()
        {
            Dal = ConfigDAL;
        }

        public bool UpdateConfig(Ld_Sys_Config entity)
        {
            try
            {
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_Config GetConfig(int systemId, string companyId)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateConfigPro(Ld_Sys_Config entity)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_Config(entity.SystemID, entity.CompanyID, entity.Title, entity.Keyword, entity.Description, entity.HomeUrl, entity.StyleSrc, entity.UploadRoot, entity.Copyright, entity.IcpNumber, entity.StatisticsCode, entity.IsLoginIpAddress.ToBool(), entity.LoginIpAddressWhiteList, entity.MaxLoginFail.ToInt(), entity.EmailSendPattern, entity.EmailHost, entity.EmailPort.ToInt(), entity.EmailName, entity.EmailPassword, entity.EmailAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateConfigShieldingPro(int systemId, string companyId, string shielding)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_ConfigShielding(systemId, companyId, shielding, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_Config GetConfigPro(int systemId, string companyId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_Config(systemId, companyId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Sys_Config>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
