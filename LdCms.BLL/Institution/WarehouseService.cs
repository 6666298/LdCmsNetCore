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
    public partial class WarehouseService:BaseService<Ld_Institution_Warehouse>,IWarehouseService
    {
        private readonly IWarehouseDAL WarehouseDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public WarehouseService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IWarehouseDAL WarehouseDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.WarehouseDAL = WarehouseDAL;
            this.Dal = WarehouseDAL;
        }
        public override void SetDal()
        {
            Dal = WarehouseDAL;
        }

    }
}
