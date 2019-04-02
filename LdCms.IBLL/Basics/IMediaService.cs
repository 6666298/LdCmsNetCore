using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Basics
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IMediaService:IBaseService<Ld_Basics_Media>
    {
        bool SaveMedia(Ld_Basics_Media entity);
        int MediaInterface(string appId, Ld_Basics_Media entity);
        int MediaInterface(string appId, List<Ld_Basics_Media> lists);
        int SaveMediaMember(string memberId, Ld_Basics_Media entity);
        int SaveMediaMember(string memberId, List<Ld_Basics_Media> lists);

    }
}
