using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Sys_Code(int systemId, string systemName, string description, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_Code";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@systemName", SqlDbType.NVarChar,50),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = systemName;
                param[2].Value = description;
                param[3].Value = state;
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
        public int SP_Delete_Sys_Code(int systemId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Sys_Code";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
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
        public int SP_Update_Sys_Code(int systemId, string systemName, string description, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_Code";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@systemName", SqlDbType.NVarChar,50),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = systemName;
                param[2].Value = description;
                param[3].Value = state;
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
        public int SP_Update_Sys_CodeState(int systemId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_CodeState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
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
        public ArrayList SP_Get_Sys_Code(string systemId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_Code";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.VarChar,10),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
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
