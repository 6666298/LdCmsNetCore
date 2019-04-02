using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    using LdCms.Common.Extension;
    /// <summary>
    /// 
    /// </summary>
    public partial class LdCmsDbEntitiesContext
    {
        public int SP_Add_Institution_Staff(int systemId, string companyId, string staffId, string staffName, string username, string password, string nickname, string headImgUrl, string name, int sex, DateTime birthDate, string birthPlace, string Identification, string education, string phone, string qq, string weixin, string email, string address, decimal wages, int probation, DateTime startWorkDate, DateTime endWorkDate, DateTime signContractDate, DateTime expirationContractDate, string departmentId, string positionId, string storeId, string warehouseId, string Description, bool State, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Add_Institution_Staff";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@staffName", SqlDbType.NVarChar,20),
                    new SqlParameter("@username", SqlDbType.VarChar,20),
                    new SqlParameter("@password", SqlDbType.VarChar,32),
                    new SqlParameter("@nickname", SqlDbType.NVarChar,20),
                    new SqlParameter("@headImgUrl", SqlDbType.VarChar,250),
                    new SqlParameter("@name", SqlDbType.NVarChar,20),
                    new SqlParameter("@sex",SqlDbType.Int,4),
                    new SqlParameter("@birthDate",SqlDbType.Date,8),
                    new SqlParameter("@birthPlace",SqlDbType.NVarChar,250),
                    new SqlParameter("@Identification", SqlDbType.VarChar,20),
                    new SqlParameter("@education", SqlDbType.NVarChar,20),
                    new SqlParameter("@phone", SqlDbType.VarChar,20),
                    new SqlParameter("@qq", SqlDbType.VarChar,20),
                    new SqlParameter("@weixin", SqlDbType.VarChar,20),
                    new SqlParameter("@email", SqlDbType.VarChar,20),
                    new SqlParameter("@address", SqlDbType.NVarChar,250),
                    new SqlParameter("@wages",SqlDbType.Decimal,18),
                    new SqlParameter("@probation",SqlDbType.Int,4),
                    new SqlParameter("@startWorkDate",SqlDbType.DateTime,8),
                    new SqlParameter("@endWorkDate",SqlDbType.DateTime,8),
                    new SqlParameter("@signContractDate",SqlDbType.DateTime,8),
                    new SqlParameter("@expirationContractDate",SqlDbType.DateTime,8),
                    new SqlParameter("@departmentId", SqlDbType.VarChar,20),
                    new SqlParameter("@positionId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId", SqlDbType.VarChar,20),
                    new SqlParameter("@warehouseId", SqlDbType.VarChar,20),
                    new SqlParameter("@Description", SqlDbType.NVarChar,400),
                    new SqlParameter("@State", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = staffName;
                param[4].Value = username;
                param[5].Value = password;
                param[6].Value = nickname;
                param[7].Value = headImgUrl.ToDBNull();
                param[8].Value = name;
                param[9].Value = sex;
                param[10].Value = birthDate.ToDBNull();
                param[11].Value = birthPlace.ToDBNull();
                param[12].Value = Identification.ToDBNull();
                param[13].Value = education.ToDBNull();
                param[14].Value = phone;
                param[15].Value = qq.ToDBNull();
                param[16].Value = weixin.ToDBNull();
                param[17].Value = email;
                param[18].Value = address;
                param[19].Value = wages;
                param[20].Value = probation;
                param[21].Value = startWorkDate.ToDBNull();
                param[22].Value = endWorkDate.ToDBNull();
                param[23].Value = signContractDate.ToDBNull();
                param[24].Value = expirationContractDate.ToDBNull();
                param[25].Value = departmentId;
                param[26].Value = positionId;
                param[27].Value = storeId;
                param[28].Value = warehouseId;
                param[29].Value = Description;
                param[30].Value = State;
                param[31].Direction = ParameterDirection.Output;
                param[32].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[31].Value;
                errorMsg = (string)param[32].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Institution_Staff(int systemId, string companyId, string staffId, string staffName, string username, string password, string nickname, string headImgUrl, string name, int sex, DateTime birthDate, string birthPlace, string Identification, string education, string phone, string qq, string weixin, string email, string address, decimal wages, int probation, DateTime startWorkDate, DateTime endWorkDate, DateTime signContractDate, DateTime expirationContractDate, string departmentId, string positionId, string storeId, string warehouseId, string Description, bool State, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Institution_Staff";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@staffId", SqlDbType.VarChar,20),
                    new SqlParameter("@staffName", SqlDbType.NVarChar,20),
                    new SqlParameter("@username", SqlDbType.VarChar,20),
                    new SqlParameter("@password", SqlDbType.VarChar,32),
                    new SqlParameter("@nickname", SqlDbType.NVarChar,20),
                    new SqlParameter("@headImgUrl", SqlDbType.VarChar,250),
                    new SqlParameter("@name", SqlDbType.NVarChar,20),
                    new SqlParameter("@sex",SqlDbType.Int,4),
                    new SqlParameter("@birthDate",SqlDbType.Date,8),
                    new SqlParameter("@birthPlace",SqlDbType.NVarChar,250),
                    new SqlParameter("@Identification", SqlDbType.VarChar,20),
                    new SqlParameter("@education", SqlDbType.NVarChar,20),
                    new SqlParameter("@phone", SqlDbType.VarChar,20),
                    new SqlParameter("@qq", SqlDbType.VarChar,20),
                    new SqlParameter("@weixin", SqlDbType.VarChar,20),
                    new SqlParameter("@email", SqlDbType.VarChar,20),
                    new SqlParameter("@address", SqlDbType.NVarChar,250),
                    new SqlParameter("@wages",SqlDbType.Decimal,18),
                    new SqlParameter("@probation",SqlDbType.Int,4),
                    new SqlParameter("@startWorkDate",SqlDbType.DateTime,8),
                    new SqlParameter("@endWorkDate",SqlDbType.DateTime,8),
                    new SqlParameter("@signContractDate",SqlDbType.DateTime,8),
                    new SqlParameter("@expirationContractDate",SqlDbType.DateTime,8),
                    new SqlParameter("@departmentId", SqlDbType.VarChar,20),
                    new SqlParameter("@positionId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId", SqlDbType.VarChar,20),
                    new SqlParameter("@warehouseId", SqlDbType.VarChar,20),
                    new SqlParameter("@Description", SqlDbType.NVarChar,400),
                    new SqlParameter("@State", SqlDbType.Bit,1),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Value = staffName;
                param[4].Value = username;
                param[5].Value = password;
                param[6].Value = nickname;
                param[7].Value = headImgUrl.ToDBNull();
                param[8].Value = name;
                param[9].Value = sex;
                param[10].Value = birthDate.ToDBNull();
                param[11].Value = birthPlace.ToDBNull();
                param[12].Value = Identification.ToDBNull();
                param[13].Value = education.ToDBNull();
                param[14].Value = phone;
                param[15].Value = qq.ToDBNull();
                param[16].Value = weixin.ToDBNull();
                param[17].Value = email;
                param[18].Value = address;
                param[19].Value = wages;
                param[20].Value = probation;
                param[21].Value = startWorkDate.ToDBNull();
                param[22].Value = endWorkDate.ToDBNull();
                param[23].Value = signContractDate.ToDBNull();
                param[24].Value = expirationContractDate.ToDBNull();
                param[25].Value = departmentId;
                param[26].Value = positionId;
                param[27].Value = storeId;
                param[28].Value = warehouseId;
                param[29].Value = Description;
                param[30].Value = State;
                param[31].Direction = ParameterDirection.Output;
                param[32].Direction = ParameterDirection.Output;
                var result = this.ExecuteNonQueryPro(cmdText, param);
                errorCode = (int)param[31].Value;
                errorMsg = (string)param[32].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SP_Update_Institution_StaffState(int systemId, string companyId, string staffId, bool state, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Institution_StaffState";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
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
        public int SP_Update_Institution_StaffPassword(int systemId, string companyId, string staffId, string password, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Update_Institution_StaffPassword";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
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
        public int SP_Delete_Institution_Staff(int systemId, string companyId, string staffId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Delete_Institution_Staff";
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
        public ArrayList SP_Get_Institution_Staff(int systemId, string companyId, string staffId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_Institution_Staff";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
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
        public ArrayList SP_Get_Institution_StaffPaging(int systemId, string companyId, int pageId, int pageSize, out int errorCode, out string errorMsg, out int rowCount)
        {
            try
            {
                string cmdText = "SP_Get_Institution_StaffPaging";
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
        public ArrayList SP_Search_Institution_Staff(int systemId, string companyId, string startTime, string endTime,string departmentId,string positionId,string storeId,string warehouseId, string keyword, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Search_Institution_Staff";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.VarChar,20),
                    new SqlParameter("@startTime", SqlDbType.VarChar,20),
                    new SqlParameter("@endTime", SqlDbType.VarChar,20),
                    new SqlParameter("@departmentId", SqlDbType.VarChar,20),
                    new SqlParameter("@positionId", SqlDbType.VarChar,20),
                    new SqlParameter("@storeId", SqlDbType.VarChar,20),
                    new SqlParameter("@warehouseId", SqlDbType.VarChar,20),
                    new SqlParameter("@keyword", SqlDbType.NVarChar,20),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = startTime;
                param[3].Value = endTime;
                param[4].Value = departmentId;
                param[5].Value = positionId;
                param[6].Value = storeId;
                param[7].Value = warehouseId;
                param[8].Value = keyword;
                param[9].Direction = ParameterDirection.Output;
                param[10].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errorCode = (int)param[9].Value;
                errorMsg = (string)param[10].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int SP_Verify_Institution_StaffLogin(int systemId, string companyId,string username, string password, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Verify_Institution_StaffLogin";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@username", SqlDbType.NVarChar,20),
                    new SqlParameter("@password", SqlDbType.VarChar,32),
                    new SqlParameter("@errorCode", SqlDbType.Int,4),
                    new SqlParameter("@errorMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = username;
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
        public ArrayList SP_Get_VInstitution_Staff(int systemId, string companyId,string staffId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_VInstitution_Staff";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@staffId", SqlDbType.NVarChar,20),
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
        public ArrayList SP_Get_VInstitution_StaffAll(int systemId, string companyId, out int errorCode, out string errorMsg)
        {
            try
            {
                string cmdText = "SP_Get_VInstitution_StaffAll";
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

    }
}
