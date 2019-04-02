using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;

    public partial class AddressDAL:BaseDAL<Ld_Member_Address>,IAddressDAL
    {
        public AddressDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }


    }
}
