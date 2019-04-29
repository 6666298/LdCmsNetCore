using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Basics
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Basics;
    using LdCms.IDAL.Basics;
    using LdCms.Common.Json;
    public partial class VMediaService : BaseService<V_Basics_Media>, IVMediaService
    {
        private readonly IVMediaDAL VMediaDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public VMediaService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IVMediaDAL VMediaDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.VMediaDAL = VMediaDAL;
            this.Dal = VMediaDAL;
        }
        public override void SetDal()
        {
            Dal = VMediaDAL;
        }

        public V_Basics_Media GetVMedia(int systemId, string companyId, string memberId, string mediaId)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.MemberID == memberId && m.MediaID == mediaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
