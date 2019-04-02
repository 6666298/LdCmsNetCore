using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface INoticeCategoryService:IBaseService<Ld_Info_NoticeCategory>
    {
        bool SaveNoticeCategory(Ld_Info_NoticeCategory entity);
        bool UpdateNoticeCategory(Ld_Info_NoticeCategory entity);
        bool UpdateNoticeCategoryState(int systemId, string companyId, string categoryId, bool state);
        bool DeleteNoticeCategory(int systemId, string companyId, string categoryId);
        Ld_Info_NoticeCategory GetNoticeCategory(int systemId, string companyId, string categoryId);
        List<Ld_Info_NoticeCategory> GetNoticeCategory(int systemId, string companyId);
        List<Ld_Info_NoticeCategory> GetNoticeCategoryByState(int systemId, string companyId, string state);


    }
}
