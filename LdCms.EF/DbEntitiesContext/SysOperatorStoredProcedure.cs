using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 系统操作员存储过程
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Sys_Operator(int systemId, string companyId,string staffId,string roleId,string remark,bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_Operator";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@roleId", SqlDbType.VarChar,20),
                    new SqlParameter("@remark", SqlDbType.NVarChar,400),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = roleId;
                param[4].Value = remark;
                param[5].Value = state;
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
        public int SP_Delete_Sys_Operator(int systemId, string companyId, string staffId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Sys_Operator";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
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
        public int SP_Update_Sys_Operator(int systemId, string companyId, string staffId, string roleId, string remark, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_Operator";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@roleId", SqlDbType.VarChar,20),
                    new SqlParameter("@remark", SqlDbType.NVarChar,400),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = roleId;
                param[4].Value = remark;
                param[5].Value = state;
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
        public int SP_Update_Sys_OperatorPassword(int systemId, string companyId, string staffId, string password, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_OperatorPassword";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@password", SqlDbType.VarChar,32),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = password;
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
        public int SP_Update_Sys_OperatorRole(int systemId, string companyId, string staffId, string roleId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_OperatorRole";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@roleId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = roleId;
                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[5].Value;
                errorMsg = (string)param[5].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Sys_OperatorState(int systemId, string companyId, string staffId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_OperatorState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
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
        public ArrayList SP_Get_Sys_Operator(int systemId, string companyId, string staffId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_Operator";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
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
        public ArrayList SP_Get_Sys_OperatorPaging(int systemId, string companyId,int pageId,int pageSize, out int errorCode, out string errorMsg,out int rowCount)
        {
            try
            {
                string cmdText = "SP_Get_Sys_OperatorPaging";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@pageId",SqlDbType.Int,4),
                    new SqlParameter("@pageSize",SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400),
                    new SqlParameter("@rowCount", SqlDbType.Int,4)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = pageId;
                param[3].Value = pageSize;
                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;
                param[6].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[4].Value;
                errorMsg = (string)param[5].Value;
                rowCount = (int)param[6].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Search_Sys_Operator(int systemId, string companyId, string startTime, string endTime, string keyword, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Search_Sys_Operator";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@startTime", SqlDbType.VarChar,20),
                    new SqlParameter("@endTime", SqlDbType.VarChar,20),
                    new SqlParameter("@keyword", SqlDbType.NVarChar,100),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = startTime;
                param[3].Value = endTime;
                param[4].Value = keyword;
                param[5].Direction = ParameterDirection.Output;
                param[6].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[5].Value;
                errorMsg = (string)param[6].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Verify_Sys_OperatorPermission(int systemId, string companyId, string staffId, string functionId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Sys_OperatorPermission";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@functionId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = functionId;
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
    }
}
