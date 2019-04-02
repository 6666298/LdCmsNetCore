using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LdCms.BLL.Institution
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Institution;
    using LdCms.IDAL.Institution;
    using LdCms.Common.Json;
    using LdCms.Common.Extension;
    /// <summary>
    /// 
    /// </summary>
    public partial class StaffService:BaseService<Ld_Institution_Staff>,IStaffService
    {
        private readonly IStaffDAL StaffDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public StaffService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IStaffDAL StaffDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.StaffDAL = StaffDAL;
            this.Dal = StaffDAL;
        }
        public override void SetDal()
        {
            Dal = StaffDAL;
        }

        public bool SaveStaffPro(Ld_Institution_Staff entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string staffId = entity.StaffID;
                string staffName = entity.StaffName;
                string username = entity.UserName;
                string password = entity.Password;
                string nickname = entity.NickName;
                string headImgUrl = entity.HeadImgUrl;
                string name = entity.Name;
                int sex = entity.Sex.ToInt();
                DateTime birthDate = entity.BirthDate.ToSqlDate();
                string birthPlace = entity.BirthPlace;
                string Identification = entity.Identification;
                string education = entity.Education;
                string phone = entity.Phone;
                string qq = entity.QQ;
                string weixin = entity.Weixin;
                string email = entity.Email;
                string address = entity.Address;
                decimal wages = entity.Wages.ToDecimal();
                int probation = entity.Probation.ToInt();
                DateTime startWorkDate = entity.StartWorkDate.ToSqlDate();
                DateTime endWorkDate = entity.EndWorkDate.ToSqlDate();
                DateTime signContractDate = entity.SignContractDate.ToSqlDate();
                DateTime expirationContractDate = entity.ExpirationContractDate.ToSqlDate();
                string departmentId = entity.DepartmentID;
                string positionId = entity.PositionID;
                string storeId = entity.StoreID;
                string warehouseId = entity.WarehouseID;
                string Description = entity.Description;
                bool State = entity.State.ToBool();

                return SaveStaffPro(systemId, companyId, staffId, staffName, username, password, nickname, headImgUrl, name, sex, birthDate, birthPlace, Identification, education, phone, qq, weixin, email, address, wages, probation, startWorkDate, endWorkDate, signContractDate, expirationContractDate, departmentId, positionId, storeId, warehouseId, Description, State);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateStaffPro(Ld_Institution_Staff entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string staffId = entity.StaffID;
                string staffName = entity.StaffName;
                string username = entity.UserName;
                string password = entity.Password;
                string nickname = entity.NickName;
                string headImgUrl = entity.HeadImgUrl;
                string name = entity.Name;
                int sex = entity.Sex.ToInt();
                DateTime birthDate = entity.BirthDate.ToDate();
                string birthPlace = entity.BirthPlace;
                string Identification = entity.Identification;
                string education = entity.Education;
                string phone = entity.Phone;
                string qq = entity.QQ;
                string weixin = entity.Weixin;
                string email = entity.Email;
                string address = entity.Address;
                decimal wages = entity.Wages.ToDecimal();
                int probation = entity.Probation.ToInt();
                DateTime startWorkDate = entity.StartWorkDate.ToDate();
                DateTime endWorkDate = entity.EndWorkDate.ToDate();
                DateTime signContractDate = entity.SignContractDate.ToDate();
                DateTime expirationContractDate = entity.ExpirationContractDate.ToDate();
                string departmentId = entity.DepartmentID;
                string positionId = entity.PositionID;
                string storeId = entity.StoreID;
                string warehouseId = entity.WarehouseID;
                string Description = entity.Description;
                bool State = entity.State.ToBool();

                return UpdateStaffPro(systemId, companyId, staffId, staffName, username, password, nickname, headImgUrl, name, sex, birthDate, birthPlace, Identification, education, phone, qq, weixin, email, address, wages, probation, startWorkDate, endWorkDate, signContractDate, expirationContractDate, departmentId, positionId, storeId, warehouseId, Description, State);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveStaffPro(int systemId, string companyId, string staffId, string staffName, string username, string password, string nickname, string headImgUrl, string name, int sex, DateTime birthDate, string birthPlace, string Identification, string education, string phone, string qq, string weixin, string email, string address, decimal wages, int probation, DateTime startWorkDate, DateTime endWorkDate, DateTime signContractDate, DateTime expirationContractDate, string departmentId, string positionId, string storeId, string warehouseId, string Description, bool State)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Institution_Staff(systemId, companyId, staffId, staffName, username, password, nickname, headImgUrl, name, sex, birthDate, birthPlace, Identification, education, phone, qq, weixin, email, address, wages, probation, startWorkDate, endWorkDate, signContractDate, expirationContractDate, departmentId, positionId, storeId, warehouseId, Description, State, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateStaffPro(int systemId, string companyId, string staffId, string staffName, string username, string password, string nickname, string headImgUrl, string name, int sex, DateTime birthDate, string birthPlace, string Identification, string education, string phone, string qq, string weixin, string email, string address, decimal wages, int probation, DateTime startWorkDate, DateTime endWorkDate, DateTime signContractDate, DateTime expirationContractDate, string departmentId, string positionId, string storeId, string warehouseId, string Description, bool State)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Institution_Staff(systemId, companyId, staffId, staffName, username, password, nickname, headImgUrl, name, sex, birthDate, birthPlace, Identification, education, phone, qq, weixin, email, address, wages, probation, startWorkDate, endWorkDate, signContractDate, expirationContractDate, departmentId, positionId, storeId, warehouseId, Description, State, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateStaffStatePro(int systemId, string companyId, string staffId, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Institution_StaffState(systemId, companyId, staffId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateStaffPasswordPro(int systemId, string companyId, string staffId, string password)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Institution_StaffPassword(systemId, companyId, staffId, password, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteStaffPro(int systemId, string companyId, string staffId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Institution_Staff(systemId, companyId, staffId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Institution_Staff GetStaffPro(int systemId, string companyId, string staffId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrWhiteSpace(companyId))
                    throw new Exception("公司编号不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_Staff(systemId, companyId, staffId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Staff>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Staff> GetStaffPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_StaffPaging(systemId, companyId, pageId, pageSize, out errCode, out errMsg, out rowCount);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Staff>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Staff> SearchStaffPro(int systemId, string companyId, string startTime, string endTime, string departmentId, string positionId, string storeId, string warehouseId, string keyword)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Search_Institution_Staff(systemId, companyId, startTime, endTime, departmentId, positionId, storeId, warehouseId, keyword, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Staff>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyStaffLoginPro(int systemId, string companyId, string username, string password)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Institution_StaffLogin(systemId, companyId, username, password, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public V_Institution_Staff GetVStaffPro(int systemId, string companyId, string staffId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrWhiteSpace(companyId))
                    throw new Exception("公司编号不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_VInstitution_Staff(systemId, companyId, staffId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<V_Institution_Staff>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<V_Institution_Staff> GetVStaffAllPro(int systemId, string companyId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrWhiteSpace(companyId))
                    throw new Exception("公司编号不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_VInstitution_StaffAll(systemId, companyId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<V_Institution_Staff>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
