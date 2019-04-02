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
        public int SP_Update_Sys_Config(int systemId, string companyId, string title, string keyword, string description, string homeUrl, string styleSrc, string uploadRoot, string copyright, string icpNumber, string statisticsCode, bool isLoginIpAddress, string loginIpAddressWhiteList, int maxLoginFail, string emailSendPattern, string emailHost, int emailPort, string emailName, string emailPassword, string emailAddress, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_Config";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@title", SqlDbType.NVarChar,200),
                    new SqlParameter("@keyword", SqlDbType.NVarChar,200),
                    new SqlParameter("@description", SqlDbType.NVarChar,400),
                    new SqlParameter("@homeUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@styleSrc", SqlDbType.NVarChar,200),
                    new SqlParameter("@uploadRoot", SqlDbType.NVarChar,50),
                    new SqlParameter("@copyright", SqlDbType.NVarChar,250),
                    new SqlParameter("@icpNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@statisticsCode", SqlDbType.NVarChar,800),
                    new SqlParameter("@isLoginIpAddress", SqlDbType.Bit,1),
                    new SqlParameter("@loginIpAddressWhiteList", SqlDbType.NVarChar,1000),
                    new SqlParameter("@maxLoginFail", SqlDbType.Int,4),
                    new SqlParameter("@emailSendPattern", SqlDbType.NVarChar,50),
                    new SqlParameter("@emailHost", SqlDbType.NVarChar,50),
                    new SqlParameter("@emailPort", SqlDbType.Int,4),
                    new SqlParameter("@emailName", SqlDbType.NVarChar,100),
                    new SqlParameter("@emailPassword", SqlDbType.NVarChar,30),
                    new SqlParameter("@emailAddress", SqlDbType.NVarChar,100),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = title;
                param[3].Value = keyword;
                param[4].Value = description;
                param[5].Value = homeUrl;
                param[6].Value = styleSrc;
                param[7].Value = uploadRoot;
                param[8].Value = copyright;
                param[9].Value = icpNumber;
                param[10].Value = statisticsCode;
                param[11].Value = isLoginIpAddress;
                param[12].Value = loginIpAddressWhiteList;
                param[13].Value = maxLoginFail;
                param[14].Value = emailSendPattern;
                param[15].Value = emailHost;
                param[16].Value = emailPort;
                param[17].Value = emailName;
                param[18].Value = emailPassword;
                param[19].Value = emailAddress;
                param[20].Direction = ParameterDirection.Output;
                param[21].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[20].Value;
                errorMsg = (string)param[21].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Sys_ConfigShielding(int systemId, string companyId, string shielding, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Sys_ConfigShielding";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@shielding", SqlDbType.NVarChar,1000),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = shielding;
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
        public ArrayList SP_Get_Sys_Config(int systemId, string companyId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Sys_Config";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId",SqlDbType.VarChar,20),
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


    }
}
