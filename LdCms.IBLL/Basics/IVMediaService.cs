using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Basics
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IVMediaService: IBaseService<V_Basics_Media>
    {
        V_Basics_Media GetVMedia(int systemId, string companyId, string memberId, string mediaId);

    }
}
