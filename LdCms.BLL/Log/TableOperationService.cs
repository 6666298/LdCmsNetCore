using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Log
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Log;
    using LdCms.IDAL.Log;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    /// <summary>
    /// 
    /// </summary>
    public partial class TableOperationService:BaseService<Ld_Log_TableOperation>,ITableOperationService
    {
        private readonly ITableOperationDAL TableOperationDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public TableOperationService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ITableOperationDAL TableOperationDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.TableOperationDAL = TableOperationDAL;
            this.Dal = TableOperationDAL;
        }
        public override void SetDal()
        {
            Dal = TableOperationDAL;
        }

        private bool IsTableOperation(long id)
        {
            return IsExists(m => m.ID == id);
        }
        public bool SaveTableOperation(Ld_Log_TableOperation entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveTableOperation(Ld_Log_TableOperation entity, out long tableOperationID)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                bool result = Add(entity);
                tableOperationID = result ? entity.ID : 0;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateTableOperationState(long id, bool state)
        {
            try
            {
                if (!IsTableOperation(id))
                    throw new Exception("id invalid！");
                var entity = GetTableOperation(id);
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTableOperation(long id)
        {
            try
            {
                return Delete(m => m.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Log_TableOperation GetTableOperation(long id)
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
        public List<Ld_Log_TableOperation> GetTableOperationTop(int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_TableOperation>();
                expression = expression.And(m => true);
                var lists = FindListTop(expression, m => m.ID, false, count);
                if (lists == null)
                    return null;
                else
                    return lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_TableOperation> GetTableOperationPaging(int pageId, int pageSize, out int totalRows)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_TableOperation>();
                expression = expression.And(m => true);
                totalRows = Count(expression);
                var lists = FindListPaging(expression, m => m.ID, false, pageId, pageSize);
                if (lists == null)
                    return null;
                else
                    return lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_TableOperation> SearchTableOperation(string startTime, string endTime, string clientId, string classId, string keyword)
        {
            try
            {
                DateTime dateStartTime;
                DateTime dateEndTime;
                if (string.IsNullOrWhiteSpace(startTime))
                {
                    var entity = Find(m => m.CreateDate != null);
                    if (entity == null)
                        return null;
                    else
                        dateStartTime = entity.CreateDate.Value;
                }
                else
                {
                    dateStartTime = startTime.ToDate();
                }
                if (string.IsNullOrWhiteSpace(endTime))
                    dateEndTime = DateTime.Now;
                else
                    dateEndTime = endTime.ToDate();

                byte btClientId = 0;
                byte btClassId = 0;
                if (!string.IsNullOrWhiteSpace(clientId))
                    btClientId = clientId.ToByte();
                if (!string.IsNullOrWhiteSpace(classId))
                    btClassId = classId.ToByte();

                var expression = ExtLinq.True<Ld_Log_TableOperation>();
                expression = expression.And(m => m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                m.ClientID == (string.IsNullOrWhiteSpace(clientId) ? m.ClientID : btClientId) &&
                m.ClassID == (string.IsNullOrWhiteSpace(classId) ? m.ClassID : btClassId) &&
                (m.TableName.Contains(keyword) || m.Account.Contains(keyword))
                );
                var lists = FindListTop(expression, m => m.ID, false, 1000);
                if (lists == null)
                    return null;
                else
                    return lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
