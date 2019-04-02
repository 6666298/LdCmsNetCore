using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IClassService :IBaseService<Ld_Info_Class>
    {
        bool SaveClass(Ld_Info_Class entity);
        bool UpdateClass(int systemId, string companyId, string classId, string className, string imgSrc, string keyword, string description, bool state);
        bool UpdateClassState(int systemId, string companyId, string classId, bool state);
        bool UpdateClassOrderId(int systemId, string companyId, string classId, int sort);
        int DeleteClass(int systemId, string companyId, string classId);
        Ld_Info_Class GetClass(int systemId, string companyId, string classId);
        List<Ld_Info_Class> GetClassAll(int systemId, string companyId);
        List<Ld_Info_Class> GetClassState(int systemId, string companyId, bool state);
        List<Ld_Info_Class> GetClassByParentPath(int systemId, string companyId, string classId, bool? state);
        List<Ld_Info_Class> GetClassByParentPath(int systemId, string companyId, string classId, int? typeId, bool? state);
        List<Ld_Info_Class> GetClassByParentId(int systemId, string companyId, string parentId, bool? state);


    }
}
