using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Service
{
    using EF.DbModels;
    using EF.DbEntitiesContext;
    using IDAL.Service;
    /// <summary>
    /// 服务板块 留言板
    /// </summary>
    public partial class MessageBoardDAL:BaseDAL<Ld_Service_MessageBoard>,IMessageBoardDAL
    {
        public MessageBoardDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
