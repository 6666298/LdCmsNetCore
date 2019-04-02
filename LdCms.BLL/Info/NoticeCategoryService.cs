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

    public partial class NoticeCategoryService:BaseService<Ld_Info_NoticeCategory>,INoticeCategoryService
    {
        private readonly INoticeCategoryDAL NoticeCategoryDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public NoticeCategoryService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, INoticeCategoryDAL NoticeCategoryDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.NoticeCategoryDAL = NoticeCategoryDAL;
            this.Dal = NoticeCategoryDAL;
        }
        public override void SetDal()
        {
            Dal = NoticeCategoryDAL;
        }

        public bool SaveNoticeCategory(Ld_Info_NoticeCategory entity)
        {
            try
            {
                var infoNoticeCategory = PrimaryKeyHelper.PrimaryKeyType.InfoNoticeCategory;
                var version = PrimaryKeyHelper.PrimaryKeyLen.V1;
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string categoryId = PrimaryKeyHelper.MakePrimaryKey(infoNoticeCategory, version);
                string categoryName = entity.CategoryName;
                if (IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryID == categoryId))
                    throw new Exception("主建ID重复！");
                if (IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryName == categoryName))
                    throw new Exception("类别名称已存在！");
                entity.CategoryID = categoryId;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateNoticeCategory(Ld_Info_NoticeCategory entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string categoryId = entity.CategoryID;
                string categoryName = entity.CategoryName;
                if (!IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryID == categoryId))
                    throw new Exception("类别ID无效！");
                if (IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryID != categoryId&&m.CategoryName==categoryName))
                    throw new Exception("类别名称已存在！");
                entity.CreateDate = DateTime.Now;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateNoticeCategoryState(int systemId, string companyId, string categoryId, bool state)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_NoticeCategory>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryID == categoryId);
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
        public bool DeleteNoticeCategory(int systemId, string companyId, string categoryId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_NoticeCategory>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryID == categoryId);
                if (!IsExists(expression))
                    throw new Exception("类别ID无效！");
                var expressionNotice = ExtLinq.True<Ld_Info_Notice>();
                expressionNotice = expressionNotice.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.ClassID == categoryId);
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                var isUse = dbContext.IsExists(expressionNotice);
                if (isUse)
                    throw new Exception("类别ID已被使用，不能操作删除！");
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Info_NoticeCategory GetNoticeCategory(int systemId, string companyId, string categoryId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_NoticeCategory>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.CategoryID == categoryId);
                if (!IsExists(expression))
                    throw new Exception("类别ID无效！");
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_NoticeCategory> GetNoticeCategory(int systemId, string companyId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_NoticeCategory>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                return FindList(expression, m => m.Sort, false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_NoticeCategory> GetNoticeCategoryByState(int systemId, string companyId, string state)
        {
            try
            {
                bool blnState = state.ToBool();
                var expression = ExtLinq.True<Ld_Info_NoticeCategory>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId&& m.State == (string.IsNullOrWhiteSpace(state) ? m.State : blnState));
                return FindList(expression, m => m.Sort, false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
