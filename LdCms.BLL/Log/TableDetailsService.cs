using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LdCms.BLL.Log
{
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Log;
    using LdCms.IDAL.Log;
    /// <summary>
    /// 
    /// </summary>
    public partial class TableDetailsService:BaseService<Ld_Log_TableDetails>,ITableDetailsService
    {
        private readonly ITableDetailsDAL TableDetailsDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public TableDetailsService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ITableDetailsDAL TableDetailsDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.TableDetailsDAL = TableDetailsDAL;
            this.Dal = TableDetailsDAL;
        }
        public override void SetDal()
        {
            Dal = TableDetailsDAL;
        }

        public bool IsTableDetails(string tableId, string columnName)
        {
            return IsExists(m => m.TableID == tableId && m.ColumnName == columnName);
        }
        public bool SaveTableDetails(Ld_Log_TableDetails entity)
        {
            try
            {
                string tableId = entity.TableID;
                string columnName = entity.ColumnName;
                if (IsTableDetails(tableId, columnName))
                    throw new Exception("字段名已存在！");
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SaveTableDetails(List<Ld_Log_TableDetails> list)
        {
            try
            {
                int result = 0;
                foreach (var m in list)
                {
                    string tableId = m.TableID;
                    string columnName = m.ColumnName;
                    if (!IsTableDetails(tableId, columnName))
                    {
                        m.CreateDate = DateTime.Now;
                        result = Add(m) ? result + 1 : result;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateTableDetailsColumnText(long id, string columnText, string remark)
        {
            try
            {
                var entity = GetTableDetails(id);
                if (entity == null)
                    throw new Exception("table details id invalid！");
                entity.ColumnText = columnText;
                entity.Remark = remark;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateTableDetailsPrimaryKey(long id, bool isPrimaryKey)
        {
            try
            {
                var entity = GetTableDetails(id);
                if (entity == null)
                    throw new Exception("table details id invalid！");
                entity.IsPrimaryKey = isPrimaryKey;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteTableDetails()
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_TableDetails>();
                expression = expression.And(m => true);
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTableDetails(long id)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_TableDetails>();
                expression = expression.And(m => m.ID == id);
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTableDetails(Ld_Log_TableDetails entity)
        {
            try
            {
                return Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Log_TableDetails GetTableDetails(long id)
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
        public List<Ld_Log_TableDetails> GetTableDetailsByTableID(string tableId)
        {
            try
            {
                return FindList(m => m.TableID == tableId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
