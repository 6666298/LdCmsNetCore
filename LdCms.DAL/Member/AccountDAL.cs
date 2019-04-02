using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;

    public partial class AccountDAL:BaseDAL<Ld_Member_Account>,IAccountDAL
    {
        public AccountDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
        


    }
}
