using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.Model.Extend;

    public partial interface ISearchKeywordService:IBaseService<Ld_Extend_SearchKeyword>
    {
        bool SaveSearchKeyword(Ld_Extend_SearchKeyword entity);
        bool UpdateSearchKeywordState(long id, bool state);
        bool UpdateSearchKeywordTop(long id, bool top);
        bool DeleteSearchKeyword(long id);
        Ld_Extend_SearchKeyword GetSearchKeyword(long id);
        List<Ld_Extend_SearchKeyword> GetSearchKeyword(int systemId, string companyId, int count);
        List<Ld_Extend_SearchKeyword> GetSearchKeywordByKeyword(int systemId, string companyId, string keyword, int count);
        List<Ld_Extend_SearchKeyword> SearchSearchKeyword(int systemId, string companyId, string startTime, string endTime, string keyword, int count);
        List<CountSearchKeywordByKeywordResult> CountSearchKeywordByKeyword(int systemId, string companyId, int count);
        List<CountSearchKeywordByKeywordResult> CountSearchKeywordByKeyword(int systemId, string companyId, string startTime, string endTime, string keyword, int count);
        int CountSearchKeyword(int systemId, string companyId);

    }
}
