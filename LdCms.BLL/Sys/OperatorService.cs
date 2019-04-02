using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LdCms.BLL.Sys
{
    using LdCms.Common.Json;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.EF.DbViews;
    using LdCms.IBLL.Sys;
    using LdCms.IDAL.Sys;

    /// <summary>
    /// 系统操作员业务逻辑服务类
    /// </summary>
    public partial class OperatorService : BaseService<Ld_Sys_Operator>, IOperatorService
    {
        private readonly IOperatorDAL OperatorDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public OperatorService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IOperatorDAL OperatorDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.OperatorDAL = OperatorDAL;
            this.Dal = OperatorDAL;
        }
        public override void SetDal()
        {
            Dal = OperatorDAL;
        }

        public bool SaveOperatorPro(int systemId, string companyId, string staffId, string roleId, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Sys_Operator(systemId, companyId, staffId, roleId, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteOperatorPro(int systemId, string companyId, string staffId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Sys_Operator(systemId, companyId, staffId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateOperatorPro(int systemId, string companyId, string staffId, string roleId, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_Operator(systemId, companyId, staffId, roleId, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateOperatorPasswordPro(int systemId, string companyId, string staffId, string password)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_OperatorPassword(systemId, companyId, staffId, password, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateOperatorRolePro(int systemId, string companyId, string staffId, string roleId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_OperatorRole(systemId, companyId, staffId, roleId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateOperatorStatePro(int systemId, string companyId, string staffId, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Sys_OperatorState(systemId, companyId, staffId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public V_Sys_Operator GetOperatorPro(int systemId, string companyId, string staffId)
        {
            try
            {
                try
                {
                    int errCode = -1;
                    string errMsg = "fail";
                    var result = LdCmsDbEntitiesContext.SP_Get_Sys_Operator(systemId, companyId, staffId, out errCode, out errMsg);
                    if (errCode != 0)
                        throw new Exception(errMsg);
                    if (result == null)
                        return null;
                    else
                        return result.ToObject<List<V_Sys_Operator>>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<V_Sys_Operator> GetOperatorPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                try
                {
                    int errCode = -1;
                    string errMsg = "fail";
                    var result = LdCmsDbEntitiesContext.SP_Get_Sys_OperatorPaging(systemId, companyId, pageId, pageSize, out errCode, out errMsg, out rowCount);
                    if (errCode != 0)
                        throw new Exception(errMsg);
                    if (result == null)
                        return null;
                    else
                        return result.ToObject<List<V_Sys_Operator>>();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<V_Sys_Operator> SearchOperatorPro(int systemId, string companyId, string startTime, string endTime, string keyword)
        {
            try
            {
                try
                {
                    int errCode = -1;
                    string errMsg = "fail";
                    var result = LdCmsDbEntitiesContext.SP_Search_Sys_Operator(systemId, companyId, startTime, endTime, keyword, out errCode, out errMsg);
                    if (errCode != 0)
                        throw new Exception(errMsg);
                    if (result == null)
                        return null;
                    else
                        return result.ToObject<List<V_Sys_Operator>>();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyOperatorPermission(int systemId, string companyId, string staffId, string functionId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Sys_OperatorPermission(systemId, companyId, staffId, functionId, out errCode, out errMsg);
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
