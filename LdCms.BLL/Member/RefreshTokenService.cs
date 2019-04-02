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
    public partial class RefreshTokenService:BaseService<Ld_Member_RefreshToken>,IRefreshTokenService
    {
        private readonly IRefreshTokenDAL RefreshTokenDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public RefreshTokenService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IRefreshTokenDAL RefreshTokenDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.RefreshTokenDAL = RefreshTokenDAL;
            this.Dal = RefreshTokenDAL;
        }
        public override void SetDal()
        {
            Dal = RefreshTokenDAL;
        }

    }
}
