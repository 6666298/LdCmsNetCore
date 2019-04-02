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
    /// <summary>
    /// 
    /// </summary>
    public partial class LinkService:BaseService<Ld_Extend_Link>,ILinkService
    {
        private readonly ILinkDAL LinkDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public LinkService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ILinkDAL LinkDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.LinkDAL = LinkDAL;
            this.Dal = LinkDAL;
        }
        public override void SetDal()
        {
            Dal = LinkDAL;
        }

        public bool SaveLink(Ld_Extend_Link entity)
        {
            try
            {
                var link = PrimaryKeyHelper.PrimaryKeyType.ExtendLink;
                var version = PrimaryKeyHelper.PrimaryKeyLen.V1;
                string linkId = PrimaryKeyHelper.MakePrimaryKey(link, version);
                string logo = entity.Logo;
                int typeId = string.IsNullOrEmpty(logo) ? 1 : 2;
                string typeName = string.IsNullOrEmpty(logo) ? "文字" : "图片";

                entity.LinkID = linkId;
                entity.TypeID = typeId.ToByte();
                entity.TypeName = typeName;
                entity.Sort = entity.Sort.ToInt();
                entity.State = entity.State.ToBool();
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateLink(Ld_Extend_Link entity)
        {
            try
            {
                string logo = entity.Logo;
                int typeId = string.IsNullOrEmpty(logo) ? 1 : 2;
                string typeName = string.IsNullOrEmpty(logo) ? "文字" : "Logo";

                entity.TypeID = typeId.ToByte();
                entity.TypeName = typeName;
                entity.Sort = entity.Sort.ToInt();
                entity.State = entity.State.ToBool();
                entity.CreateDate = DateTime.Now;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateLinkState(int systemId, string companyId, string linkId, bool state)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.LinkID == linkId);
                var entity = Find(expression);
                if (entity==null)
                    throw new Exception("主建ID无效！");
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateLinkSort(int systemId, string companyId, string linkId, int sort)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.LinkID == linkId);
                var entity = Find(expression);
                if (entity == null)
                    throw new Exception("主建ID无效！");
                entity.Sort = Math.Abs(sort);
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLink(int systemId, string companyId, string linkId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.LinkID == linkId);
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Extend_Link GetLink(int systemId, string companyId, string linkId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.LinkID == linkId);
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_Link> GetLinkTop(int systemId, string companyId, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                int total = GetTopTotal(count);
                return FindListTop(expression, m => m.Sort, true, total).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_Link> GetLinkTop(int systemId, string companyId, string typeId, string state, int count)
        {
            try
            {
                byte byteTypeId = typeId.ToByte();
                bool blnState = state.ToBool();
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && m.TypeID.Equals(string.IsNullOrWhiteSpace(typeId) ? m.TypeID : byteTypeId)
                && m.State.Equals(string.IsNullOrWhiteSpace(typeId) ? m.State : blnState));
                int total = GetTopTotal(count);
                return FindListTop(expression, m => m.Sort, true, total).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_Link> SearchLink(int systemId, string companyId, string startTime, string endTime, string state, string keyword, int count)
        {
            try
            {
                DateTime dateStartTime = ToStartTime(startTime);
                DateTime dateEndTime = ToEndTime(endTime);
                if (dateStartTime == DateTime.MinValue) { return null; }
                bool blnState = state.ToBool();
                int total = GetTopTotal(count);
                //条件
                var expression = ExtLinq.True<Ld_Extend_Link>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId &&
                m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                (string.IsNullOrWhiteSpace(state) ? m.State.Equals(m.State) : m.State.Value == blnState) &&
                (m.Name.Contains(keyword) || m.Url.Contains(keyword)) );
                //执行
                var lists = FindListTop(expression, m => m.CreateDate, false, total);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountLink(int systemId, string companyId)
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
