using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IDepartmentService:IBaseService<Ld_Institution_Department>
    {
        bool SaveDepartmentPro(int systemId, string companyId, string departmentId, string departmentName, string parentId, string description, bool state);
        bool UpdateDepartmentPro(int systemId, string companyId, string departmentId, string departmentName, string description, bool state);
        bool UpdateDepartmentStatePro(int systemId, string companyId, string departmentId, bool state);
        bool DeleteDepartmentPro(int systemId, string companyId, string departmentId);
        Ld_Institution_Department GetDepartmentPro(int systemId, string companyId, string departmentId);
        List<Ld_Institution_Department> GetDepartmentByNodePathPro(int systemId, string companyId, string departmentId, string state);
        List<Ld_Institution_Department> GetDepartmentByParentIdPro(int systemId, string companyId, string parentId, string state);
        List<Ld_Institution_Department> GetDepartmentPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount);
        List<Ld_Institution_Department> SearchDepartmentPro(int systemId, string companyId, string startTime, string endTime, string keyword);

    }
}
