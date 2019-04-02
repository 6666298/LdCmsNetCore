using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Member_RefreshToken(string verifyRefreshToken,string token, string refreshToken, int expiresIn, int refreshTokenExpiresIn, string ipAddress, int createTimestamp, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Member_RefreshToken";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@verifyRefreshToken",SqlDbType.VarChar,64),
                    new SqlParameter("@token",SqlDbType.VarChar,64),
                    new SqlParameter("@refreshToken",SqlDbType.VarChar,64),
                    new SqlParameter("@expiresIn", SqlDbType.Int,4),
                    new SqlParameter("@refreshTokenExpiresIn", SqlDbType.Int,4),
                    new SqlParameter("@ipAddress", SqlDbType.VarChar,20),
                    new SqlParameter("@createTimestamp", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = verifyRefreshToken;
                param[1].Value = token;
                param[2].Value = refreshToken;
                param[3].Value = expiresIn;
                param[4].Value = refreshTokenExpiresIn;
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
    }
}
