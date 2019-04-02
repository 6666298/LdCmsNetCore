using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface INoticeService:IBaseService<Ld_Info_Notice>
    {
        bool SaveNotice(Ld_Info_Notice entity);
        bool UpdateNotice(Ld_Info_Notice entity);
        bool UpdateNoticeState(int systemId, string companyId, string noticeId, bool state);
        bool DeleteNotice(int systemId, string companyId, string noticeId);
        Ld_Info_Notice GetNotice(int systemId, string companyId, string noticeId);
        List<Ld_Info_Notice> GetNoticeTop(int systemId, string companyId, int count);
        List<Ld_Info_Notice> GetNoticePaging(int systemId, string companyId, int pageId, int pageSize);
        List<Ld_Info_Notice> SearchNotice(int systemId, string companyId, string startTime, string endTime, string classId, string state, string keyword);

        int CountNotice(int systemId, string companyId);

    }
}
