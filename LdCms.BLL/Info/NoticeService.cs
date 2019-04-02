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
    using LdCms.Common.Extension;
    using LdCms.Common.Security;
    using System.Linq.Expressions;

    public partial class NoticeService:BaseService<Ld_Info_Notice>,INoticeService
    {
        private readonly INoticeDAL NoticeDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public NoticeService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, INoticeDAL NoticeDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.NoticeDAL = NoticeDAL;
            this.Dal = NoticeDAL;
        }
        public override void SetDal()
        {
            Dal = NoticeDAL;
        }

        public bool IsExists(int systemId, string companyId, string noticeId)
        {
            try
            {
                return IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.NoticeID == noticeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveNotice(Ld_Info_Notice entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string noticeId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.InfoNotice);
                if (IsExists(systemId, companyId, noticeId))
                    throw new Exception("主建ID重复！");
                entity.NoticeID = noticeId;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateNotice(Ld_Info_Notice entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string noticeId = entity.NoticeID;
                if (!IsExists(systemId, companyId, noticeId))
                    throw new Exception("主建ID无效！");
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateNoticeState(int systemId, string companyId, string noticeId, bool state)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.NoticeID == noticeId);
                if (!IsExists(expression))
                    throw new Exception("类别ID无效！");
                var entity = Find(expression);
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteNotice(int systemId, string companyId, string noticeId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.NoticeID == noticeId);
                if (!IsExists(expression))
                    throw new Exception("主建ID无效！");
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Info_Notice GetNotice(int systemId, string companyId, string noticeId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.NoticeID == noticeId);
                if (!IsExists(expression))
                    throw new Exception("主建ID无效！");
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Notice> GetNoticeTop(int systemId, string companyId, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                var expressionScalarLambda = GetExpressionScalarLambda();
                return FindListTop(expression, expressionScalarLambda, m => m.CreateDate, false, count).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Notice> GetNoticePaging(int systemId, string companyId, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                var expressionScalarLambda = GetExpressionScalarLambda();
                int pageIndex = pageId <= 0 ? 1 : pageId;
                int pageCount = pageSize <= 1 ? 1 : pageSize;
                return FindListPaging(expression, expressionScalarLambda, m => m.CreateDate, false, pageIndex, pageCount).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Notice> SearchNotice(int systemId, string companyId, string startTime, string endTime, string classId, string state, string keyword)
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
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId &&
                m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                (string.IsNullOrWhiteSpace(classId) ? m.ClassID.Contains(m.ClassID) : m.ClassID == classId) &&
                (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == blnState) &&
                (m.Title.Contains(keyword) || m.StaffId.Contains(keyword) || m.StaffName.Contains(keyword)));
                //字段
                var expressionScalarLambda = GetExpressionScalarLambda();
                //执行
                var lists = FindListTop(expression, expressionScalarLambda, m => m.CreateDate, false, total);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CountNotice(int systemId, string companyId)
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
        public int CountNotice(int systemId, string companyId, string classId, string state)
        {
            try
            {
                bool blnState = state.ToBool();
                var expression = ExtLinq.True<Ld_Info_Notice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && (string.IsNullOrWhiteSpace(classId) ? m.ClassID.Contains(m.ClassID) : m.ClassID == classId)
                && (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == blnState));
                return Count(expression);
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
        private Expression<Func<Ld_Info_Notice, Ld_Info_Notice>> GetExpressionScalarLambda()
        {
            try
            {
                Expression<Func<Ld_Info_Notice, Ld_Info_Notice>> scalarLambda = m => new Ld_Info_Notice
                {
                    SystemID = m.SystemID,
                    CompanyID = m.CompanyID,
                    NoticeID = m.NoticeID,
                    Title = m.Title,
                    ClassID = m.ClassID,
                    ClassName = m.ClassName,
                    Author = m.Author,
                    ImgSrc = m.ImgSrc,
                    StaffId = m.StaffId,
                    StaffName = m.StaffName,
                    State = m.State,
                    CreateDate = m.CreateDate
                };
                return scalarLambda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
