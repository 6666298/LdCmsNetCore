using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Member
{
    using EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IRankService :IBaseService<Ld_Member_Rank>
    {
        bool SaveRankPro(int systemId, string companyId, string rankName, int maxPoints, int discount, int showPrice, string remark, bool state);
        bool UpdateRankPro(int systemId, string companyId, string rankId, string rankName, int maxPoints, int discount, int showPrice, string remark, bool state);
        bool UpdateRankStatePro(int systemId, string companyId, string rankId, bool state);
        bool DeleteRankPro(int systemId, string companyId, string rankId);
        Ld_Member_Rank GetRankPro(int systemId, string companyId, string rankId);
        List<Ld_Member_Rank> GetRankStatePro(int systemId, string companyId, string state);
        List<Ld_Member_Rank> GetRankPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount);
        List<Ld_Member_Rank> SearchRankPro(int systemId, string companyId, string startTime, string endTime, string keyword);


    }
}
