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
        public int SP_Add_Institution_Store(int systemId, string  companyId, string storeId, string storeName, string logo, string contacts, string tel, string fax, string phone, string email, int provinceId, int cityId, int areaId, string address, string keyword, string description, DateTime startTime, DateTime endTime, bool push, int sort, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Institution_Store";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeName", SqlDbType.VarChar,100),
                    new SqlParameter("@logo", SqlDbType.NVarChar,250),
                    new SqlParameter("@contacts", SqlDbType.VarChar,20),
                    new SqlParameter("@tel", SqlDbType.VarChar,20),
                    new SqlParameter("@fax", SqlDbType.VarChar,20),
                    new SqlParameter("@phone", SqlDbType.VarChar,20),
                    new SqlParameter("@email", SqlDbType.VarChar,100),
                    new SqlParameter("@provinceId",SqlDbType.Int,4),
                    new SqlParameter("@cityId",SqlDbType.Int,4),
                    new SqlParameter("@areaId",SqlDbType.Int,4),
                    new SqlParameter("@address", SqlDbType.NVarChar,250),
                    new SqlParameter("@keyword", SqlDbType.NVarChar,200),
                    new SqlParameter("@description", SqlDbType.NVarChar,400),
                    new SqlParameter("@startTime",SqlDbType.DateTime,8),
                    new SqlParameter("@endTime",SqlDbType.DateTime,8),
                    new SqlParameter("@push", SqlDbType.Bit,1),
                    new SqlParameter("@sort", SqlDbType.Int,4),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = storeId;
                param[3].Value = storeName;
                param[4].Value = logo;
                param[5].Value = contacts;
                param[6].Value = tel;
                param[7].Value = fax;
                param[8].Value = phone;
                param[9].Value = email;
                param[10].Value = provinceId;
                param[11].Value = cityId;
                param[12].Value = areaId;
                param[13].Value = address;
                param[14].Value = keyword;
                param[15].Value = description;
                param[16].Value = startTime;
                param[17].Value = endTime;
                param[18].Value = push;
                param[19].Value = sort;
                param[20].Value = state;
                param[21].Direction = ParameterDirection.Output;
                param[22].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[21].Value;
                errorMsg = (string)param[22].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Institution_Store(int systemId, string companyId, string storeId, string storeName, string logo, string contacts, string tel, string fax, string phone, string email, int provinceId, int cityId, int areaId, string address, string keyword, string description, DateTime startTime, DateTime endTime, bool push, int sort, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Institution_Store";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeName", SqlDbType.VarChar,100),
                    new SqlParameter("@logo", SqlDbType.NVarChar,250),
                    new SqlParameter("@contacts", SqlDbType.VarChar,20),
                    new SqlParameter("@tel", SqlDbType.VarChar,20),
                    new SqlParameter("@fax", SqlDbType.VarChar,20),
                    new SqlParameter("@phone", SqlDbType.VarChar,20),
                    new SqlParameter("@email", SqlDbType.VarChar,100),
                    new SqlParameter("@provinceId",SqlDbType.Int,4),
                    new SqlParameter("@cityId",SqlDbType.Int,4),
                    new SqlParameter("@areaId",SqlDbType.Int,4),
                    new SqlParameter("@address", SqlDbType.NVarChar,250),
                    new SqlParameter("@keyword", SqlDbType.NVarChar,200),
                    new SqlParameter("@description", SqlDbType.NVarChar,400),
                    new SqlParameter("@startTime",SqlDbType.DateTime,8),
                    new SqlParameter("@endTime",SqlDbType.DateTime,8),
                    new SqlParameter("@push", SqlDbType.Bit,1),
                    new SqlParameter("@sort", SqlDbType.Int,4),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = storeId;
                param[3].Value = storeName;
                param[4].Value = logo;
                param[5].Value = contacts;
                param[6].Value = tel;
                param[7].Value = fax;
                param[8].Value = phone;
                param[9].Value = email;
                param[10].Value = provinceId;
                param[11].Value = cityId;
                param[12].Value = areaId;
                param[13].Value = address;
                param[14].Value = keyword;
                param[15].Value = description;
                param[16].Value = startTime;
                param[17].Value = endTime;
                param[18].Value = push;
                param[19].Value = sort;
                param[20].Value = state;
                param[21].Direction = ParameterDirection.Output;
                param[22].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[21].Value;
                errorMsg = (string)param[22].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Institution_StoreState(int systemId, string companyId, string storeId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Institution_StoreState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId",SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = storeId;
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
        public int SP_Delete_Institution_Store(int systemId, string companyId, string storeId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Institution_Store";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId",SqlDbType.VarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = storeId;
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
        public ArrayList SP_Get_Institution_Store(int systemId, string companyId, string storeId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Institution_Store";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@storeId", SqlDbType.NVarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = storeId;
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
        public ArrayList SP_Get_Institution_StoreByState(int systemId, string companyId, string state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Institution_StoreByState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@state", SqlDbType.VarChar,5),
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
        public ArrayList SP_Get_Institution_StorePaging(int systemId, string companyId, int pageId, int pageSize, out int errorCode, out string errorMsg, out int rowCount)
        {
            try
            {
                string cmdText = "SP_Get_Institution_StorePaging";
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
        public ArrayList SP_Search_Institution_Store(int systemId, string companyId, string startTime, string endTime, string keyword, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Search_Institution_Store";
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
