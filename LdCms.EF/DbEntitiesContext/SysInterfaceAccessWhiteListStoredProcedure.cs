using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 系统接口帐号访问IP白名单存储过程
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Sys_InterfaceAccessWhiteList(int systemId, string companyId, string account, string ipAddress,int classId,string className, string remark, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_InterfaceAccessWhiteList";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@account", SqlDbType.VarChar,20),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@classId", SqlDbType.Int,4),
                    new SqlParameter("@className", SqlDbType.NVarChar,20),
                    new SqlParameter("@remark", SqlDbType.NVarChar,400),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = account;
                param[3].Value = ipAddress;
                param[4].Value = classId;
                param[5].Value = className;
                param[6].Value = remark;
                param[7].Value = state;
                param[8].Direction = ParameterDirection.Output;
                param[9].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[8].Value;
                errorMsg = (string)param[9].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Delete_Sys_InterfaceAccessWhiteList(int systemId, string companyId, string account, string ipAddress, int classId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Sys_InterfaceAccessWhiteList";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@account", SqlDbType.VarChar,20),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@classId",SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = account;
                param[3].Value = ipAddress;
                param[4].Value = classId;
                param[5].Direction = ParameterDirection.Output;
                param[6].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[5].Value;
                errorMsg = (string)param[6].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Sys_InterfaceAccessWhiteList(int systemId, string companyId, string account, string ipAddress, int classId, string className, string remark, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_InterfaceAccessWhiteList";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@account", SqlDbType.VarChar,20),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@classId", SqlDbType.Int,4),
                    new SqlParameter("@className", SqlDbType.NVarChar,20),
                    new SqlParameter("@remark", SqlDbType.NVarChar,400),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = account;
                param[3].Value = ipAddress;
                param[4].Value = classId;
                param[5].Value = className;
                param[6].Value = remark;
                param[7].Value = state;
                param[8].Direction = ParameterDirection.Output;
                param[9].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[8].Value;
                errorMsg = (string)param[9].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Get_Sys_InterfaceAccessWhiteList(int systemId, string companyId, string account, string ipAddress, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_InterfaceAccessWhiteList";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@account", SqlDbType.VarChar,20),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = account;
                param[3].Value = ipAddress;
                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[4].Value;
                errorMsg = (string)param[5].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Get_Sys_InterfaceAccessWhiteListByAccount(int systemId, string companyId, string account, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_InterfaceAccessWhiteListByAccount";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@account", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = account;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[3].Value;
                errorMsg = (string)param[4].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Verify_Sys_InterfaceAccessWhiteList(int systemId, string companyId, string account, string ipAddress, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Sys_InterfaceAccessWhiteList";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@account", SqlDbType.VarChar,20),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = account;
                param[3].Value = ipAddress;
                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[4].Value;
                errorMsg = (string)param[5].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Verify_Sys_InterfaceAccessWhiteListByAccessToken(int systemId, string accessToken, string ipAddress, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Sys_InterfaceAccessWhiteListByAccessToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@accessToken",SqlDbType.VarChar,128),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = accessToken;
                param[2].Value = ipAddress;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[3].Value;
                errorMsg = (string)param[4].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Verify_Sys_InterfaceAccessWhiteListByAppId(int systemId, string appId, string ipAddress, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Sys_InterfaceAccessWhiteListByAppId";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@appId",SqlDbType.VarChar,16),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = appId;
                param[2].Value = ipAddress;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[3].Value;
                errorMsg = (string)param[4].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
