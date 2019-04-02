using System;
using System.Linq;
using System.Linq.Expressions;
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
    /// <summary>
    /// 
    /// </summary>
    public partial class AdvertisementService:BaseService<Ld_Extend_Advertisement>,IAdvertisementService
    {
        private readonly IAdvertisementDAL AdvertisementDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AdvertisementService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAdvertisementDAL AdvertisementDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AdvertisementDAL = AdvertisementDAL;
            this.Dal = AdvertisementDAL;
        }
        public override void SetDal()
        {
            Dal = AdvertisementDAL;
        }


        public bool SaveAdvertisement(Ld_Extend_Advertisement entity,  List<Ld_Extend_AdvertisementDetails> lists)
        {
            try
            {
                var advertisement = PrimaryKeyHelper.PrimaryKeyType.ExtendAdvertisement;
                var advertisementDetails = PrimaryKeyHelper.PrimaryKeyType.ExtendAdvertisementDetails;
                var version = PrimaryKeyHelper.PrimaryKeyLen.V1;
                string advertisementId = PrimaryKeyHelper.MakePrimaryKey(advertisement, version);

                entity.AdvertisementID = advertisementId;
                entity.Sort = entity.Sort.ToInt();
                entity.State = entity.State.ToBool();
                entity.CreateDate = DateTime.Now;

                if (lists == null)
                    throw new Exception("广告列表不能为空！");
                List<Ld_Extend_AdvertisementDetails> details = new List<Ld_Extend_AdvertisementDetails>();
                foreach (var m in lists)
                {
                    string advertisementDetailsId = PrimaryKeyHelper.MakePrimaryKey(advertisementDetails, version);
                    m.SystemID = entity.SystemID;
                    m.CompanyID = entity.CompanyID;
                    m.DetailsID = advertisementDetailsId;
                    m.AdvertisementID = advertisementId;
                    m.Sort = m.Sort.ToInt();
                    m.State = m.State.ToBool();
                    m.CreateDate = DateTime.Now;
                    details.Add(m);
                }

                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            dbContext.Add(entity);
                            dbContext.Add(details);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateAdvertisementState(int systemId, string companyId, string advertisementId, bool state)
        {
            try
            {
                var entity = Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.AdvertisementID == advertisementId);
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
        public bool UpdateAdvertisementSort(int systemId, string companyId, string advertisementId, int sort)
        {
            try
            {
                var entity = Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.AdvertisementID == advertisementId);
                if (entity == null)
                    throw new Exception("id invalid！");
                entity.Sort = sort;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAdvertisement(int systemId, string companyId, string advertisementId)
        {
            try
            {
                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            dbContext.Delete<Ld_Extend_Advertisement>(m => m.SystemID == systemId && m.CompanyID == companyId && m.AdvertisementID == advertisementId);
                            dbContext.Delete<Ld_Extend_AdvertisementDetails>(m => m.SystemID == systemId && m.CompanyID == companyId && m.AdvertisementID == advertisementId);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Extend_Advertisement GetAdvertisement(int systemId, string companyId, string advertisementId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Advertisement>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.AdvertisementID == advertisementId);
                if (!IsExists(expression))
                    throw new Exception("主建ID无效！");
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_Advertisement> GetAdvertisementTop(int systemId, string companyId, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Advertisement>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                return FindListTop(expression, m => m.CreateDate, false, count).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_Advertisement> GetAdvertisementPaging(int systemId, string companyId, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Advertisement>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                int pageIndex = GetPageIndex(pageId);
                int pageCount = GetPageCount(pageSize);
                return FindListPaging(expression, m => m.CreateDate, false, pageIndex, pageCount).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_Advertisement> SearchAdvertisement(int systemId, string companyId, string startTime, string endTime, bool? state, string keyword, int count)
        {
            try
            {
                DateTime dateStartTime = ToStartTime(startTime);
                DateTime dateEndTime = ToEndTime(endTime);
                if (dateStartTime == DateTime.MinValue) { return null; }
                int total = GetTopTotal(count);
                //条件
                var expression = ExtLinq.True<Ld_Extend_Advertisement>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId &&
                m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                (state.HasValue ? m.State.Value == state: m.State.Equals(m.State)) &&
                (m.Name.Contains(keyword) || m.Remark.Contains(keyword)));
                //执行
                return FindListTop(expression, m => m.CreateDate, false, total).ToList();
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
