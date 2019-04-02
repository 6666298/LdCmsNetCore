using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Extend
{
    using LdCms.EF.DbModels;
    public partial interface ILinkService:IBaseService<Ld_Extend_Link>
    {
        bool SaveLink(Ld_Extend_Link entity);
        bool UpdateLink(Ld_Extend_Link entity);
        bool UpdateLinkState(int systemId, string companyId, string linkId, bool state);
        bool UpdateLinkSort(int systemId, string companyId, string linkId, int sort);
        bool DeleteLink(int systemId, string companyId, string linkId);
        Ld_Extend_Link GetLink(int systemId, string companyId, string linkId);
        List<Ld_Extend_Link> GetLinkTop(int systemId, string companyId, int count);
        List<Ld_Extend_Link> GetLinkTop(int systemId, string companyId, string typeId, string state, int count);
        List<Ld_Extend_Link> SearchLink(int systemId, string companyId, string startTime, string endTime, string state, string keyword, int count);

        int CountLink(int systemId, string companyId);
    }
}
