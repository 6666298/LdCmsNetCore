using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LdCms.BLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IBLL.Institution;
    using LdCms.IDAL.Institution;
    using LdCms.Common.Json;
    public partial class CompanyService: BaseService<Ld_Institution_Company>, ICompanyService
    {
        private readonly ICompanyDAL CompanyDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public CompanyService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ICompanyDAL CompanyDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.CompanyDAL = CompanyDAL;
            this.Dal = CompanyDAL;
        }
        public override void SetDal()
        {
            Dal = CompanyDAL;
        }

        public bool SaveCompanyRegisterPro(int systemId, string dealerId, int classId, string companyName, string password, string nickName, string phone, string email, string registerIpAddress)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Institution_CompanyRegister(systemId, dealerId, classId, companyName, password, nickName, phone, email, registerIpAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateCompanyPro(int systemId, string companyId, string companyName, string nickName, string tel, string fax, string phone, string email, string address, string description)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Institution_Company(systemId, companyId, companyName, nickName, tel, fax, phone, email, address, description, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Institution_Company GetCompanyPro(int systemId, string companyId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrWhiteSpace(companyId))
                    throw new Exception("公司编号不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_Company(systemId, companyId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Company>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Company> GetCompanyPagingPro(int systemId, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_CompanyPaging(systemId, pageId, pageSize, out errCode, out errMsg, out rowCount);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Company>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Company> GetCompanyTop(int systemId, int count)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (count == 0)
                    count = 1;
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_CompanyTop(systemId, count, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Company>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Company> SearchCompany(int systemId, string companyId, string startTime, string endTime, string keyword)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Search_Institution_Company(systemId, companyId, startTime, endTime, keyword, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Company>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
