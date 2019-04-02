using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Log
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Log;
    /// <summary>
    /// 
    /// </summary>
    public partial class WebApiAccessRecordDAL:BaseDAL<Ld_Log_WebApiAccessRecord>,IWebApiAccessRecordDAL
    {
        public WebApiAccessRecordDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }

    }
}
