using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Member
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Member;
    using LdCms.IDAL.Member;
    /// <summary>
    /// 
    /// </summary>
    public partial class InvoiceService:BaseService<Ld_Member_Invoice>,IInvoiceService
    {
        private readonly IInvoiceDAL InvoiceDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public InvoiceService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IInvoiceDAL InvoiceDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.InvoiceDAL = InvoiceDAL;
            this.Dal = InvoiceDAL;
        }
        public override void SetDal()
        {
            Dal = InvoiceDAL;
        }

    }
}
