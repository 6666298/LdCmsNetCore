using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Extend
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Extend;
    using LdCms.IDAL.Extend;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Extension;
    using LdCms.Model.Extend;

    /// <summary>
    /// 
    /// </summary>
    public partial class SearchKeywordService:BaseService<Ld_Extend_SearchKeyword>,ISearchKeywordService
    {
        private readonly ISearchKeywordDAL SearchKeywordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public SearchKeywordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ISearchKeywordDAL SearchKeywordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.SearchKeywordDAL = SearchKeywordDAL;
            this.Dal = SearchKeywordDAL;
        }
        public override void SetDal()
        {
            Dal = SearchKeywordDAL;
        }


        public bool SaveSearchKeyword(Ld_Extend_SearchKeyword entity)
        {
            try
            {
                entity.Hits = entity.Hits.ToInt();
                entity.IsTop = entity.IsTop.ToBool();
                entity.State = entity.State.ToBool();
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateSearchKeywordState(long id, bool state)
        {
            try
            {
                var entity = Find(m => m.ID == id);
                if (entity == null)
                    throw new Exception("id invalid！");
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateSearchKeywordTop(long id, bool top)
        {
            try
            {
                var entity = Find(m => m.ID == id);
                if (entity == null)
                    throw new Exception("id invalid！");
                entity.IsTop = top;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteSearchKeyword(long id)
        {
            try
            {
                var entity = Find(m => m.ID == id);
                return Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Extend_SearchKeyword GetSearchKeyword(long id)
        {
            try
            {
                return Find(m => m.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_SearchKeyword> GetSearchKeyword(int systemId, string companyId, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_SearchKeyword>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);

                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                var db = dbContext.DbEntities();
                return db.Ld_Extend_SearchKeyword.Where(expression)
                    .OrderByDescending(m => m.IsTop)
                    .ThenByDescending(m => m.ID)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_SearchKeyword> GetSearchKeywordByKeyword(int systemId, string companyId,string keyword, int count)
        {
            try
            {
                int total = GetTopTotal(count);
                var expression = ExtLinq.True<Ld_Extend_SearchKeyword>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.Keyword == keyword);
                return FindListTop(expression, m => m.ID, false, total).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_SearchKeyword> SearchSearchKeyword(int systemId, string companyId, string startTime, string endTime, string keyword, int count)
        {
            try
            {
                DateTime dateStartTime = ToStartTime(startTime);
                DateTime dateEndTime = ToEndTime(endTime);
                if (dateStartTime == DateTime.MinValue) { return null; }
                int total = GetTopTotal(count);
                //条件
                var expression = ExtLinq.True<Ld_Extend_SearchKeyword>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date
                && (m.Keyword.Contains(keyword)));
                //执行
                return FindListTop(expression, m => m.ID, false, total).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CountSearchKeywordByKeywordResult> CountSearchKeywordByKeyword(int systemId, string companyId, int count)
        {
            try
            {
                int total = GetTopTotal(count);
                var expression = ExtLinq.True<Ld_Extend_SearchKeyword>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                var db = dbContext.DbEntities();
                var lists = db.Ld_Extend_SearchKeyword.Where(expression)
                    .GroupBy(m => m.Keyword).Select(m => new
                    {
                        ID = m.Max(x => x.ID),
                        Keyword = m.Key,
                        Total = m.Count(),
                        MinDate = m.Min(x => x.CreateDate),
                        MaxDate = m.Max(x => x.CreateDate)
                    }).Take(total).ToList();
                return lists.ToJson().ToObject<List<CountSearchKeywordByKeywordResult>>();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CountSearchKeywordByKeywordResult> CountSearchKeywordByKeyword(int systemId, string companyId, string startTime, string endTime, string keyword, int count)
        {
            try
            {
                DateTime dateStartTime = ToStartTime(startTime);
                DateTime dateEndTime = ToEndTime(endTime);
                if (dateStartTime == DateTime.MinValue) { return null; }
                int total = GetTopTotal(count);
                //条件
                var expression = ExtLinq.True<Ld_Extend_SearchKeyword>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date
                && (m.Keyword.Contains(keyword)));
                //执行
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                var db = dbContext.DbEntities();
                var lists = db.Ld_Extend_SearchKeyword.Where(expression)
                    .GroupBy(m => m.Keyword).Select(m => new
                    {
                        ID = m.Max(x => x.ID),
                        Keyword = m.Key,
                        Total = m.Count(),
                        MinDate = m.Min(x => x.CreateDate),
                        MaxDate = m.Max(x => x.CreateDate)
                    }).Take(count).ToList();
                return lists.ToJson().ToObject<List<CountSearchKeywordByKeywordResult>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountSearchKeyword(int systemId, string companyId)
        {
            try
            {
                return Count(m => m.SystemID == systemId && m.CompanyID == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        #region 私有化方法
        private DateTime ToStartTime(string startTime)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(startTime))
                {
                    var entity = Find(m => m.CreateDate != null);
                    if (entity == null)
                        return DateTime.MinValue;
                    else
                        return entity.CreateDate.Value;
                }
                else
                {
                    return startTime.ToDate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DateTime ToEndTime(string endTime)
        {
            try
            {
                return string.IsNullOrWhiteSpace(endTime) ? DateTime.Now.ToDate() : endTime.ToDate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private int GetTopTotal(int count)
        {
            try
            {
                int total = count <= 0 ? 10 : count;
                return total > 1000 ? 1000 : total;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private int GetPageIndex(int pageId)
        {
            try
            {
                int pageIndex = pageId <= 0 ? 1 : pageId;
                return pageIndex > 1000 ? 1000 : pageIndex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private int GetPageCount(int pageSize)
        {
            try
            {
                int pageCount = pageSize <= 0 ? 10 : pageSize;
                return pageCount > 100 ? 100 : pageCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
