using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 会员等级存储过程
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Member_Rank(int systemId,string companyId,string rankName, int maxPoints,int discount,int showPrice,string remark,bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Member_Rank";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@rankName", SqlDbType.VarChar,20),
                    new SqlParameter("@maxPoints",SqlDbType.Int,11),
                    new SqlParameter("@discount",SqlDbType.Int,4),
                    new SqlParameter("@showPrice",SqlDbType.Int,4),
                    new SqlParameter("@remark", SqlDbType.NVarChar,400),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = rankName;
                param[3].Value = maxPoints;
                param[4].Value = discount;
                param[5].Value = showPrice;
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
        public int SP_Update_Member_Rank(int systemId, string companyId, string rankId, string rankName, int maxPoints, int discount, int showPrice, string remark, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Member_Rank";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@rankId", SqlDbType.VarChar,20),
                    new SqlParameter("@rankName", SqlDbType.VarChar,20),
                    new SqlParameter("@maxPoints",SqlDbType.Int,11),
                    new SqlParameter("@discount",SqlDbType.Int,4),
                    new SqlParameter("@showPrice",SqlDbType.Int,4),
                    new SqlParameter("@remark", SqlDbType.NVarChar,400),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = rankId;
                param[3].Value = rankName;
                param[4].Value = maxPoints;
                param[5].Value = discount;
                param[6].Value = showPrice;
                param[7].Value = remark;
                param[8].Value = state;
                param[9].Direction = ParameterDirection.Output;
                param[10].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[9].Value;
                errorMsg = (string)param[10].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Member_RankState(int systemId, string companyId, string rankId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Member_RankState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@rankId", SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = rankId;
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
        public int SP_Delete_Member_Rank(int systemId, string companyId, string rankId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Member_Rank";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@rankId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = rankId;
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
        public ArrayList SP_Get_Member_Rank(int systemId, string companyId, string rankId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Member_Rank";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@rankId", SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = rankId;
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
        public ArrayList SP_Get_Member_RankState(int systemId, string companyId, string state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Member_RankState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.VarChar,8),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = state;
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
        public ArrayList SP_Get_Member_RankPaging(int systemId, string companyId, int pageId, int pageSize, out int errorCode, out string errorMsg, out int rowCount)
        {
            try
            {
                string cmdText = "SP_Get_Member_RankPaging";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@pageId", SqlDbType.Int,4),
                    new SqlParameter("@pageSize", SqlDbType.Int,4),
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
        public ArrayList SP_Search_Member_Rank(int systemId, string companyId, string startTime, string endTime, string keyword, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Search_Member_Rank";
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



    }
}
