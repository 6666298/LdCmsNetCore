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
        public ArrayList SP_Get_Member_AccessToken(string token, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Member_AccessToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@token", SqlDbType.NVarChar,64),
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
        public int SP_Add_Member_AccessToken(string token,string refreshToken, int systemId, string companyId,string memberId, string uuid, int expiresIn, int refreshTokenExpiresIn, string ipAddress, int createTimestamp, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Member_AccessToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@token",SqlDbType.VarChar,64),
                    new SqlParameter("@refreshToken",SqlDbType.VarChar,64),
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@memberId", SqlDbType.VarChar,20),
                    new SqlParameter("@uuid", SqlDbType.VarChar,32),
                    new SqlParameter("@expiresIn", SqlDbType.Int,4),
                    new SqlParameter("@refreshTokenExpiresIn", SqlDbType.Int,4),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@createTimestamp", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = token;
                param[1].Value = refreshToken;
                param[2].Value = systemId;
                param[3].Value = companyId;
                param[4].Value = memberId;
                param[5].Value = uuid;
                param[6].Value = expiresIn;
                param[7].Value = refreshTokenExpiresIn;
                param[8].Value = ipAddress;
                param[9].Value = createTimestamp;
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
        public int SP_Verify_Member_AccessToken(string token, int timestamp, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Member_AccessToken";
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
