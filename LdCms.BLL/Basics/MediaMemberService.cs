using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Basics
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Basics;
    using LdCms.IDAL.Basics;
    using LdCms.Common.Json;
    /// <summary>
    /// 
    /// </summary>
    public partial class MediaMemberService:BaseService<Ld_Basics_MediaMember>,IMediaMemberService
    {
        private readonly IMediaMemberDAL MediaMemberDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public MediaMemberService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IMediaMemberDAL MediaMemberDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.MediaMemberDAL = MediaMemberDAL;
            this.Dal = MediaMemberDAL;
        }
        public override void SetDal()
        {
            Dal = MediaMemberDAL;
        }


    }
}
