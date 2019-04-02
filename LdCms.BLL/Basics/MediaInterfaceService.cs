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
    public partial class MediaInterfaceService:BaseService<Ld_Basics_MediaInterface>,IMediaInterfaceService
    {
        private readonly IMediaInterfaceDAL MediaInterfaceDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public MediaInterfaceService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IMediaInterfaceDAL MediaInterfaceDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.MediaInterfaceDAL = MediaInterfaceDAL;
            this.Dal = MediaInterfaceDAL;
        }
        public override void SetDal()
        {
            Dal = MediaInterfaceDAL;
        }



    }
}
