using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 系统功能存储过程
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Sys_Function(string functionId, string functionName, string parentId, int rankId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_Function";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@functionId",SqlDbType.VarChar,20),
                    new SqlParameter("@functionName", SqlDbType.NVarChar,20),
                    new SqlParameter("@parentId", SqlDbType.NVarChar,20),
                    new SqlParameter("@rankId", SqlDbType.NVarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = functionId;
                param[1].Value = functionName;
                param[2].Value = parentId;
                param[3].Value = rankId;
                param[4].Value = state;
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
        public int SP_Update_Sys_Function(string functionId, string functionName, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_Function";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@functionId",SqlDbType.VarChar,20),
                    new SqlParameter("@functionName", SqlDbType.NVarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = functionId;
                param[1].Value = functionName;
                param[2].Value = state;
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
        public int SP_Update_Sys_FunctionState(string functionId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_FunctionState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@functionId",SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = functionId;
                param[1].Value = state;
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
        public int SP_Delete_Sys_Function(string functionId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Sys_Function";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@functionId",SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = functionId;
                param[1].Direction = ParameterDirection.Output;
                param[2].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[1].Value;
                errorMsg = (string)param[2].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Get_Sys_Function(string functionId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_Function";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@functionId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = functionId;
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
        public ArrayList SP_Get_Sys_FunctionByParentId(string parentId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_FunctionByParentId";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@parentId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = parentId;
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

    }
}
