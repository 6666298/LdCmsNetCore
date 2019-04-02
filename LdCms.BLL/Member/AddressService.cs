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
    public partial class AddressService:BaseService<Ld_Member_Address>,IAddressService
    {
        private readonly IAddressDAL AddressDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AddressService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAddressDAL AddressDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AddressDAL = AddressDAL;
            this.Dal = AddressDAL;
        }
        public override void SetDal()
        {
            Dal = AddressDAL;
        }



    }
}
