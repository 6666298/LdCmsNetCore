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
        public int SP_Add_Sys_Version(string versionId, string versionName, decimal marketPrice,decimal dealerPrice,int departmentTotalQuantity, int staffTotalQuantity,int storeTotalQuantity,int warehouseTotalQuantity,string description,  bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Sys_Version";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@versionId",SqlDbType.VarChar,20),
                    new SqlParameter("@versionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@marketPrice", SqlDbType.Decimal,18),
                    new SqlParameter("@dealerPrice", SqlDbType.Decimal,18),
                    new SqlParameter("@departmentTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@staffTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@storeTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@warehouseTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = versionId;
                param[1].Value = versionName;
                param[2].Value = marketPrice;
                param[3].Value = dealerPrice;
                param[4].Value = departmentTotalQuantity;
                param[5].Value = staffTotalQuantity;
                param[6].Value = storeTotalQuantity;
                param[7].Value = warehouseTotalQuantity;
                param[8].Value = description;
                param[9].Value = state;
                param[10].Direction = ParameterDirection.Output;
                param[11].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[10].Value;
                errorMsg = (string)param[11].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Delete_Sys_Version(string versionId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Sys_Version";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@versionId",SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = versionId;
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
        public int SP_Update_Sys_Version(string versionId, string versionName, decimal marketPrice, decimal dealerPrice, int departmentTotalQuantity, int staffTotalQuantity, int storeTotalQuantity, int warehouseTotalQuantity, string description, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_Version";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@versionId",SqlDbType.VarChar,20),
                    new SqlParameter("@versionName", SqlDbType.NVarChar,50),
                    new SqlParameter("@marketPrice", SqlDbType.Decimal,18),
                    new SqlParameter("@dealerPrice", SqlDbType.Decimal,18),
                    new SqlParameter("@departmentTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@staffTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@storeTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@warehouseTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = versionId;
                param[1].Value = versionName;
                param[2].Value = marketPrice;
                param[3].Value = dealerPrice;
                param[4].Value = departmentTotalQuantity;
                param[5].Value = staffTotalQuantity;
                param[6].Value = storeTotalQuantity;
                param[7].Value = warehouseTotalQuantity;
                param[8].Value = description;
                param[9].Value = state;
                param[10].Direction = ParameterDirection.Output;
                param[11].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[10].Value;
                errorMsg = (string)param[11].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Sys_VersionState(string versionId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_CodeState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@versionId",SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = versionId;
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
        public ArrayList SP_Get_Sys_Version(string versionId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_Version";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = versionId;
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
