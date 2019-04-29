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

    public partial class BlockService:BaseService<Ld_Info_Block>,IBlockService
    {
        private readonly IBlockDAL BlockDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public BlockService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IBlockDAL BlockDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.BlockDAL = BlockDAL;
            this.Dal = BlockDAL;
        }
        public override void SetDal()
        {
            Dal = BlockDAL;
        }

        public bool SaveBlock(Ld_Info_Block entity)
        {
            try
            {
                var infoBlock = PrimaryKeyHelper.PrimaryKeyType.InfoBlock;
                var primaryKeyLen = PrimaryKeyHelper.PrimaryKeyLen.V1;
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string blockId = PrimaryKeyHelper.MakePrimaryKey(infoBlock, primaryKeyLen);
                string tags = entity.Tags;
                bool state = entity.State.ToBool();
                if (string.IsNullOrEmpty(tags))
                    throw new Exception("块标签不能为空！");
                bool verifyTags = IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.Tags == tags);
                if (verifyTags)
                    throw new Exception("块标签不能重复！");
                entity.BlockID = blockId;
                entity.State = state;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateBlock(Ld_Info_Block entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string blockId = entity.BlockID;
                string tags = entity.Tags;
                bool state = entity.State.ToBool();
                if (string.IsNullOrEmpty(tags))
                    throw new Exception("块标签不能为空！");
                bool verifyTags = IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.BlockID != blockId && m.Tags == tags);
                if (verifyTags)
                    throw new Exception("块标签不能重复！");
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateBlockState(int systemId, string companyId, string blockId, bool state)
        {
            try
            {
                try
                {
                    var entity = Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.BlockID == blockId);
                    entity.State = state;
                    return Update(entity);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteBlock(int systemId, string companyId, string blockId)
        {
            try
            {
                return Delete(m => m.SystemID == systemId && m.CompanyID == companyId && m.BlockID == blockId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Info_Block GetBlock(int systemId, string companyId, string blockId)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.BlockID == blockId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Info_Block GetBlock(int systemId, string companyId, string tags, string state)
        {
            try
            {
                bool noticeState = state.ToBool();
                var expression = ExtLinq.True<Ld_Info_Block>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.Tags == tags
                && (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == noticeState));
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Ld_Info_Block> GetBlockAll(int systemId, string companyId, string state)
        {
            try
            {
                bool verifyState = state.ToBool();
                var expression = ExtLinq.True<Ld_Info_Block>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId
                && (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == verifyState));
                return FindList(expression, m => m.CreateDate, false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Info_Block> GetBlock(int systemId, string companyId, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Info_Block>();
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
        public List<Ld_Info_Block> SearchBlock(int systemId, string companyId, string startTime, string endTime, string state, string keyword)
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
                var expression = ExtLinq.True<Ld_Info_Block>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId &&
                m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                (string.IsNullOrWhiteSpace(state) ? m.State.Value.Equals(m.State) : m.State.Value == blnState) &&
                (m.Title.Contains(keyword) || m.Tags.Contains(keyword)));
                //执行
                var lists = FindListTop(expression, m => m.CreateDate, false, total);
                return lists == null ? null : lists.ToList();
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
