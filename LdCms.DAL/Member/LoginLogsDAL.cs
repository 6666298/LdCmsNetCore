using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Member
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Member;
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginLogsDAL:BaseDAL<Ld_Member_LoginLogs>,ILoginLogsDAL
    {
        public LoginLogsDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
