using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    /// <summary>
    /// 公司表存储过程
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Institution_CompanyRegister(int systemId, string dealerId, int classId, string companyName, string password, string nickName, string phone, string email, string registerIpAddress, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Institution_CompanyRegister";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@dealerId", SqlDbType.VarChar,20),
                    new SqlParameter("@classId",SqlDbType.Int,4),
                    new SqlParameter("@companyName", SqlDbType.NVarChar,200),
                    new SqlParameter("@password", SqlDbType.VarChar,32),
                    new SqlParameter("@nickName", SqlDbType.NVarChar,20),
                    new SqlParameter("@phone", SqlDbType.NVarChar,20),
                    new SqlParameter("@email", SqlDbType.NVarChar,50),
                    new SqlParameter("@registerIpAddress", SqlDbType.NVarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = dealerId;
                param[2].Value = classId;
                param[3].Value = companyName;
                param[4].Value = password;
                param[5].Value = nickName;
                param[6].Value = phone;
                param[7].Value = email;
                param[8].Value = registerIpAddress;
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
        public int SP_Update_Institution_Company(int systemId, string companyId, string companyName, string nickName, string tel, string fax, string phone, string email, string address, string description, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Institution_Company";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@companyName",SqlDbType.NVarChar,50),
                    new SqlParameter("@nickName", SqlDbType.NVarChar,20),
                    new SqlParameter("@tel", SqlDbType.VarChar,20),
                    new SqlParameter("@fax", SqlDbType.VarChar,20),
                    new SqlParameter("@phone", SqlDbType.VarChar,20),
                    new SqlParameter("@email", SqlDbType.VarChar,50),
                    new SqlParameter("@address", SqlDbType.NVarChar,250),
                    new SqlParameter("@description", SqlDbType.NVarChar,200),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = companyName;
                param[3].Value = nickName;
                param[4].Value = tel;
                param[5].Value = fax;
                param[6].Value = phone;
                param[7].Value = email;
                param[8].Value = address;
                param[9].Value = description;
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
        public ArrayList SP_Get_Institution_Company(int systemId, string companyId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Institution_Company";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
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
        public ArrayList SP_Get_Institution_CompanyPaging(int systemId, int pageId, int pageSize, out int errorCode, out string errorMsg, out int rowCount)
        {
            try
            {
                string cmdText = "SP_Get_Institution_CompanyPaging";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@pageId", SqlDbType.Int,4),
                    new SqlParameter("@pageSize", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400),
                    new SqlParameter("@rowCount", SqlDbType.Int,4)
                };
                param[0].Value = systemId;
                param[1].Value = pageId;
                param[2].Value = pageSize;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[3].Value;
                errorMsg = (string)param[4].Value;
                rowCount= (int)param[5].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList SP_Get_Institution_CompanyTop(int systemId, int count, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Institution_CompanyTop";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@count", SqlDbType.Int,4),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = count;
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
        public ArrayList SP_Search_Institution_Company(int systemId, string companyId, string startTime, string endTime, string keyword, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Search_Institution_Company";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@startTime", SqlDbType.VarChar,20),
                    new SqlParameter("@endTime", SqlDbType.VarChar,20),
                    new SqlParameter("@keyword", SqlDbType.NVarChar,20),
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
