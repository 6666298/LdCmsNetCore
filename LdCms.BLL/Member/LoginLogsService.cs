using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Member
{
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Time;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Member;
    using LdCms.IDAL.Member;
    
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginLogsService:BaseService<Ld_Member_LoginLogs>,ILoginLogsService
    {
        private readonly ILoginLogsDAL LoginLogsDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public LoginLogsService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ILoginLogsDAL LoginLogsDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.LoginLogsDAL = LoginLogsDAL;
            this.Dal = LoginLogsDAL;
        }
        public override void SetDal()
        {
            Dal = LoginLogsDAL;
        }



    }
}
