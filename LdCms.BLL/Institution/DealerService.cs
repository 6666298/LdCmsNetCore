using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IBLL.Institution;
    using LdCms.IDAL.Institution;
    using LdCms.Common.Json;
    public partial class DealerService:BaseService<Ld_Institution_Dealer>,IDealerService
    {
        private readonly IDealerDAL DealerDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public DealerService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IDealerDAL DealerDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.DealerDAL = DealerDAL;
            this.Dal = DealerDAL;
        }
        public override void SetDal()
        {
            Dal = DealerDAL;
        }

    }
}
