using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Extend
{
    using LdCms.EF.DbModels;
    public partial interface ILinkGroupService:IBaseService<Ld_Extend_LinkGroup>
    {
        bool SaveLinkGroup(Ld_Extend_LinkGroup entity);
        bool UpdateLinkGroup(Ld_Extend_LinkGroup entity);
        bool UpdateLinkGroupState(int systemId, string companyId, string groupId, bool state);
        bool UpdateLinkGroupSort(int systemId, string companyId, string groupId, int sort);
        bool DeleteLinkGroup(int systemId, string companyId, string groupId);
        Ld_Extend_LinkGroup GetLinkGroup(int systemId, string companyId, string groupId);
        List<Ld_Extend_LinkGroup> GetLinkGroup(int systemId, string companyId);
        List<Ld_Extend_LinkGroup> GetLinkGroupByState(int systemId, string companyId, string state);

    }
}
