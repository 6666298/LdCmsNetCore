using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IAdvertisementService:IBaseService<Ld_Extend_Advertisement>
    {
        bool SaveAdvertisement(Ld_Extend_Advertisement entity, List<Ld_Extend_AdvertisementDetails> lists);

    }
}
