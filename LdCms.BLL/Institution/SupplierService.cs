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
    public partial class SupplierService:BaseService<Ld_Institution_Supplier>,ISupplierService
    {
        private readonly ISupplierDAL SupplierDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public SupplierService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ISupplierDAL SupplierDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.SupplierDAL = SupplierDAL;
            this.Dal = SupplierDAL;
        }
        public override void SetDal()
        {
            Dal = SupplierDAL;
        }

    }
}
