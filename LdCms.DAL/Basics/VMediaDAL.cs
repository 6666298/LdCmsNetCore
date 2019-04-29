using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Basics
{
    using EF.DbViews;
    using EF.DbEntitiesContext;
    using IDAL.Basics;
    public partial class VMediaDAL: BaseDAL<V_Basics_Media>, IVMediaDAL
    {
        public VMediaDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
