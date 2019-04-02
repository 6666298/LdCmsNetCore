using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Info
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Info;
    using LdCms.IDAL.Info;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Extension;

    public partial class PageService:BaseService<Ld_Info_Page>,IPageService
    {
        private readonly IPageDAL PageDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public PageService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IPageDAL PageDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.PageDAL = PageDAL;
            this.Dal = PageDAL;
        }
        public override void SetDal()
        {
            Dal = PageDAL;
        }

        public bool SavePage(Ld_Info_Page entity)
        {
            try
            {
                var infoPage = PrimaryKeyHelper.PrimaryKeyType.InfoPage;
                var primaryKeyLen = PrimaryKeyHelper.PrimaryKeyLen.V1;
                string pageId = PrimaryKeyHelper.MakePrimaryKey(infoPage, primaryKeyLen);
                int sort = entity.Sort.ToInt();
                entity.PageID = pageId;
                entity.Sort = sort;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdatePage(Ld_Info_Page entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string pageId = entity.PageID;
                int sort = entity.Sort.ToInt();
                if (!IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.PageID == pageId))
                    throw new Exception("id invalid！");
                entity.Sort = sort;
                entity.CreateDate = DateTime.Now;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdatePageState(int systemId, string companyId, string pageId, bool state)
        {
            try
            {
                var entity = Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.PageID == pageId);
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdatePageSort(int systemId, string companyId, string pageId, int sort)
        {
            try
            {
                var entity = Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.PageID == pageId);
                entity.Sort = sort;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeletePage(int systemId, string companyId, string pageId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.PageID == pageId);
                if (!IsExists(expression))
                    throw new Exception("delete id invalid！");
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Info_Page GetPage(int systemId, string companyId, string pageId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.PageID == pageId);
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Info_Page GetPageByClassId(int systemId, string companyId, string classId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.ClassID == classId);
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Ld_Info_Page> GetPage(int systemId, string companyId, string classId, bool? state)
        {
            try
            {
                bool verifyState = state.HasValue;
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && (string.IsNullOrWhiteSpace(classId) ? m.ClassID.Contains(m.ClassID) : m.ClassID == classId)
                && verifyState ? m.State.Value == state.Value : m.State.Equals(m.State));
                return FindList(expression, m => m.CreateDate, false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Page> GetPageTop(int systemId, string companyId, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                return FindListTop(expression, m => m.CreateDate, false, count).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Page> GetPagePaging(int systemId, string companyId, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                int pageIndex = pageId <= 0 ? 1 : pageId;
                int pageCount = pageSize <= 1 ? 1 : pageSize;
                return FindListPaging(expression, m => m.CreateDate, false, pageIndex, pageCount).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Page> SearchPage(int systemId, string companyId, string startTime, string endTime, string classId, string state, string keyword)
        {
            try
            {
                DateTime dateStartTime = ToStartTime(startTime);
                DateTime dateEndTime = ToEndTime(endTime);
                if (dateStartTime == DateTime.MinValue)
                    return null;
                bool blnState = state.ToBool();
                int total = 200;
                //条件
                var expression = ExtLinq.True<Ld_Info_Page>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId &&
                m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                (string.IsNullOrWhiteSpace(classId) ? m.ClassID.Contains(m.ClassID) : m.ClassID == classId) &&
                (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == blnState) &&
                (m.Title.Contains(keyword)));
                //执行
                var lists = FindListTop(expression, m => m.CreateDate, false, total);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CountPage(int systemId, string companyId)
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
        #endregion
    }
}
