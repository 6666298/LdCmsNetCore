using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 系统接口访问token存储过程 SysInterfaceAccessTokenStoredProcedure
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Sys_InterfaceAccessToken(string token, int systemId, string appId, int expiresIn, string ipAddress, int createTimestamp, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_InterfaceAccessToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@token",SqlDbType.VarChar,128),
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@appId", SqlDbType.VarChar,16),
                    new SqlParameter("@expiresIn", SqlDbType.Int,4),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@createTimestamp", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = token;
                param[1].Value = systemId;
                param[2].Value = appId;
                param[3].Value = expiresIn;
                param[4].Value = ipAddress;
                param[5].Value = createTimestamp;
                param[6].Direction = ParameterDirection.Output;
                param[7].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[6].Value;
                errorMsg = (string)param[7].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Add_Sys_InterfaceAccessTokenAuto(string token, int systemId, string appId,string appSecret, int expiresIn, string ipAddress, int createTimestamp, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_InterfaceAccessTokenAuto";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@token",SqlDbType.VarChar,128),
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@appId", SqlDbType.VarChar,16),
                    new SqlParameter("@appSecret", SqlDbType.VarChar,32),
                    new SqlParameter("@expiresIn", SqlDbType.Int,4),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@createTimestamp", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = token;
                param[1].Value = systemId;
                param[2].Value = appId;
                param[3].Value = appSecret;
                param[4].Value = expiresIn;
                param[5].Value = ipAddress;
                param[6].Value = createTimestamp;
                param[7].Direction = ParameterDirection.Output;
                param[8].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[7].Value;
                errorMsg = (string)param[8].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Get_Sys_InterfaceAccessToken(string token, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_InterfaceAccessToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@token",SqlDbType.VarChar,128),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = token;
                param[1].Direction = ParameterDirection.Output;
                param[2].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[1].Value;
                errorMsg = (string)param[2].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Get_Sys_InterfaceAccessTokenTotalNumber(int systemId, string appId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_InterfaceAccessTokenTotalNumber";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@appId",SqlDbType.VarChar,16),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = appId;
                param[2].Direction = ParameterDirection.Output;
                param[3].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[2].Value;
                errorMsg = (string)param[3].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Verify_Sys_InterfaceAccessToken(string token,int timestamp, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Sys_InterfaceAccessToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@token",SqlDbType.VarChar,128),
                    new SqlParameter("@timestamp", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = token;
                param[1].Value = timestamp;
                param[2].Direction = ParameterDirection.Output;
                param[3].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[2].Value;
                errorMsg = (string)param[3].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
