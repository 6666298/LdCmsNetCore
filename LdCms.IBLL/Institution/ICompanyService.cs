using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    public partial interface ICompanyService:IBaseService<Ld_Institution_Company>
    {
        bool SaveCompanyRegisterPro(int systemId, string dealerId, int classId, string companyName, string password, string nickName, string phone, string email, string registerIpAddress);
        bool UpdateCompanyPro(int systemId, string companyId, string companyName, string nickName, string tel, string fax, string phone, string email, string address, string description);
        Ld_Institution_Company GetCompanyPro(int systemId, string companyId);
        List<Ld_Institution_Company> GetCompanyPagingPro(int systemId, int pageId, int pageSize, out int rowCount);
        List<Ld_Institution_Company> GetCompanyTop(int systemId, int count);
        List<Ld_Institution_Company> SearchCompany(int systemId, string companyId, string startTime, string endTime, string keyword);

    }
}
