using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IStaffService:IBaseService<Ld_Institution_Staff>
    {
        bool SaveStaffPro(Ld_Institution_Staff entity);
        bool UpdateStaffPro(Ld_Institution_Staff entity);
        bool SaveStaffPro(int systemId, string companyId, string staffId, string staffName, string username, string password, string nickname, string headImgUrl, string name, int sex, DateTime birthDate, string birthPlace, string Identification, string education, string phone, string qq, string weixin, string email, string address, decimal wages, int probation, DateTime startWorkDate, DateTime endWorkDate, DateTime signContractDate, DateTime expirationContractDate, string departmentId, string positionId, string storeId, string warehouseId, string Description, bool State);
        bool UpdateStaffPro(int systemId, string companyId, string staffId, string staffName, string username, string password, string nickname, string headImgUrl, string name, int sex, DateTime birthDate, string birthPlace, string Identification, string education, string phone, string qq, string weixin, string email, string address, decimal wages, int probation, DateTime startWorkDate, DateTime endWorkDate, DateTime signContractDate, DateTime expirationContractDate, string departmentId, string positionId, string storeId, string warehouseId, string Description, bool State);
        bool UpdateStaffStatePro(int systemId, string companyId, string staffId, bool state);
        bool UpdateStaffPasswordPro(int systemId, string companyId, string staffId, string password);
        bool DeleteStaffPro(int systemId, string companyId, string staffId);
        Ld_Institution_Staff GetStaffPro(int systemId, string companyId, string staffId);
        List<Ld_Institution_Staff> GetStaffPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount);
        List<Ld_Institution_Staff> SearchStaffPro(int systemId, string companyId, string startTime, string endTime, string departmentId, string positionId, string storeId, string warehouseId, string keyword);


        bool VerifyStaffLoginPro(int systemId, string companyId, string username, string password);
        V_Institution_Staff GetVStaffPro(int systemId, string companyId, string staffId);
        List<V_Institution_Staff> GetVStaffAllPro(int systemId, string companyId);
    }
}
