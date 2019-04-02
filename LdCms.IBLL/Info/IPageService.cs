using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IPageService:IBaseService<Ld_Info_Page>
    {
        bool SavePage(Ld_Info_Page entity);
        bool UpdatePage(Ld_Info_Page entity);
        bool UpdatePageState(int systemId, string companyId, string pageId, bool state);
        bool UpdatePageSort(int systemId, string companyId, string pageId, int sort);
        bool DeletePage(int systemId, string companyId, string pageId);
        Ld_Info_Page GetPage(int systemId, string companyId, string pageId);
        Ld_Info_Page GetPageByClassId(int systemId, string companyId, string classId);
        List<Ld_Info_Page> GetPage(int systemId, string companyId, string classId, bool? state);
        List<Ld_Info_Page> GetPageTop(int systemId, string companyId, int count);
        List<Ld_Info_Page> GetPagePaging(int systemId, string companyId, int pageId, int pageSize);
        List<Ld_Info_Page> SearchPage(int systemId, string companyId, string startTime, string endTime, string classId, string state, string keyword);
        int CountPage(int systemId, string companyId);

    }
}
