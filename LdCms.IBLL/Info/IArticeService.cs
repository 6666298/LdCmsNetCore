using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IArticeService:IBaseService<Ld_Info_Artice>
    {

        bool SaveArtice(Ld_Info_Artice entity);
        bool UpdateArtice(Ld_Info_Artice entity);
        bool UpdateArticeState(int systemId, string companyId, string articeId, bool state);
        bool UpdateArticeDelete(int systemId, string companyId, string articeId, bool delete);
        bool DeleteArtice(int systemId, string companyId, string articeId);


        Ld_Info_Artice GetArtice(int systemId, string companyId, string articeId);
        List<Ld_Info_Artice> GetArticeTop(int systemId, string companyId, bool delete, int count);
        List<Ld_Info_Artice> GetArticeTop(int systemId, string companyId, string classId, bool delete, int count);
        List<Ld_Info_Artice> GetArticePaging(int systemId, string companyId, bool delete, int pageId, int pageSize);
        List<Ld_Info_Artice> GetArticePaging(int systemId, string companyId, string classId, bool delete, int pageId, int pageSize);
        List<Ld_Info_Artice> SearchArtice(int systemId, string companyId, string startTime, string endTime, string classId, string state, string keyword, bool delete);
        int CountArtice(int systemId, string companyId, bool delete);
        int CountArtice(int systemId, string companyId, string classId, bool delete);

    }
}
