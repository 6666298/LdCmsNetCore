using System;
using System.Linq;
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
    /// 
    /// </summary>
    public partial class CodeService : BaseService<Ld_Sys_Code>, ICodeService
    {
        private readonly ICodeDAL CodeDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public CodeService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ICodeDAL CodeDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.CodeDAL = CodeDAL;
            this.Dal = CodeDAL;
        }
        public override void SetDal()
        {
            Dal = CodeDAL;
        }

        public bool SaveCodePro(int systemId, string systemName, string description, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Sys_Code(systemId, systemName, description, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCodePro(int systemId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Sys_Code(systemId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateCodePro(int systemId, string systemName, string description, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_Code(systemId, systemName, description, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateCodeStatePro(int systemId, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_CodeState(systemId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Sys_Code GetCodePro(int systemId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_Code(systemId.ToString(), out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Sys_Code>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Sys_Code> GetCodeAllPro()
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Sys_Code("", out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Sys_Code>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
