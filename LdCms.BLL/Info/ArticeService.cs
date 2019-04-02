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
    using System.Linq.Expressions;

    /// <summary>
    /// 
    /// </summary>
    public partial class ArticeService:BaseService<Ld_Info_Artice>,IArticeService
    {
        private readonly IArticeDAL ArticeDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public ArticeService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IArticeDAL ArticeDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.ArticeDAL = ArticeDAL;
            this.Dal = ArticeDAL;
        }
        public override void SetDal()
        {
            Dal = ArticeDAL;
        }

        public bool IsExists(int systemId, string companyId, string articeId)
        {
            try
            {
                return IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.ArticeID == articeId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveArtice(Ld_Info_Artice entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string articeId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.InfoArtice);
                if (IsExists(systemId, companyId, articeId))
                    throw new Exception("主建ID重复！");
                entity.Hits = entity.Hits.ToInt();
                entity.Sort = entity.Sort.ToInt();
                entity.UpNum = entity.UpNum.ToInt();
                entity.DownNum = entity.DownNum.ToInt();
                entity.AllowComment = entity.AllowComment.ToBool();
                entity.IsTop = entity.IsTop.ToBool();
                entity.IsPush = entity.IsPush.ToBool();
                entity.IsVip = entity.IsVip.ToBool();
                entity.IsDraft = entity.IsDraft.ToBool();
                entity.IsDel = entity.IsDel.ToBool();
                entity.ArticeID = articeId;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateArtice(Ld_Info_Artice entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string articeId = entity.ArticeID;
                if (!IsExists(systemId, companyId, articeId))
                    throw new Exception("ID无效！");
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateArticeState(int systemId, string companyId, string articeId, bool state)
        {
            try
            {
                var entity = GetArtice(systemId, companyId, articeId);
                if (entity == null)
                    throw new Exception("artice id invalid！");
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateArticeDelete(int systemId, string companyId, string articeId, bool delete)
        {
            try
            {
                var entity = GetArtice(systemId, companyId, articeId);
                if (entity == null)
                    throw new Exception("artice id invalid！");
                entity.IsDel = delete;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteArtice(int systemId, string companyId, string articeId)
        {
            try
            {
                var entity = GetArtice(systemId, companyId, articeId);
                if (entity == null)
                    throw new Exception("artice id invalid！");
                if (!entity.IsDel.ToBool())
                    throw new Exception("资讯不在回收站不能操作删除！");
                return Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Ld_Info_Artice GetArtice(int systemId, string companyId, string articeId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Artice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.ArticeID == articeId);
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Artice> GetArticeTop(int systemId, string companyId, bool delete, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Artice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.IsDel == delete);
                var scalarLambda = GetExpressionScalarLambda();
                return FindListTop(expression, scalarLambda, m => m.CreateDate, false, count).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Artice> GetArticeTop(int systemId, string companyId, string classId, bool delete, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Artice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.ClassID == classId && m.IsDel == delete);
                var scalarLambda = GetExpressionScalarLambda();
                return FindListTop(expression, scalarLambda, m => m.CreateDate, false, count).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Artice> GetArticePaging(int systemId, string companyId, bool delete, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Artice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.IsDel == delete);
                var scalarLambda = GetExpressionScalarLambda();
                int pageIndex = pageId <= 0 ? 1 : pageId;
                int pageCount = pageSize <= 1 ? 1 : pageSize;
                return FindListPaging(expression, scalarLambda, m => m.CreateDate, false, pageIndex, pageCount).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Artice> GetArticePaging(int systemId, string companyId, string classId, bool delete, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Artice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.ClassID == classId && m.IsDel == delete);
                var scalarLambda = GetExpressionScalarLambda();
                int pageIndex = pageId <= 0 ? 1 : pageId;
                int pageCount = pageSize <= 1 ? 1 : pageSize;
                return FindListPaging(expression, scalarLambda, m => m.CreateDate, false, pageIndex, pageCount).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Artice> SearchArtice(int systemId, string companyId, string startTime, string endTime, string classId, string state, string keyword, bool delete)
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
                var expression = ExtLinq.True<Ld_Info_Artice>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date
                && (string.IsNullOrWhiteSpace(classId) ? m.ClassID.Contains(m.ClassID) : m.ClassID == classId)
                && (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == blnState)
                && (m.Title.Contains(keyword) || m.Account.Contains(keyword) || m.NickName.Contains(keyword))
                && m.IsDel == delete);
                //字段
                var scalarLambda = GetExpressionScalarLambda();
                //执行
                var lists = FindListTop(expression, scalarLambda, m => m.CreateDate, false, total);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CountArtice(int systemId, string companyId, bool delete)
        {
            try
            {
                return Count(m => m.SystemID == systemId && m.CompanyID == companyId && m.IsDel == delete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CountArtice(int systemId, string companyId,string classId,bool delete)
        {
            try
            {
                return Count(m => m.SystemID == systemId && m.CompanyID == companyId && m.ClassID == classId && m.IsDel == delete);
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
        private Expression<Func<Ld_Info_Artice, Ld_Info_Artice>> GetExpressionScalarLambda()
        {
            try
            {
                Expression<Func<Ld_Info_Artice, Ld_Info_Artice>> scalarLambda = m => new Ld_Info_Artice
                {
                    SystemID = m.SystemID,
                    CompanyID = m.CompanyID,
                    ArticeID = m.ArticeID,
                    ClassID = m.ClassID,
                    ClassName = m.ClassName,
                    Title = m.Title,
                    TitleBrief = m.TitleBrief,
                    Author = m.Author,
                    Hits = m.Hits,
                    AllowComment = m.AllowComment,
                    IsTop=m.IsTop,
                    IsPush=m.IsPush,
                    State = m.State,
                    IsDel=m.IsDel,
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
