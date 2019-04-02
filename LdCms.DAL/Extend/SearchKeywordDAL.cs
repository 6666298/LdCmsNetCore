using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.DAL.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IDAL.Extend;
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchKeywordDAL:BaseDAL<Ld_Extend_SearchKeyword>,ISearchKeywordDAL
    {
        public SearchKeywordDAL(LdCmsDbEntitiesContext dbContext) : base(dbContext)
        {
        }
    }
}
