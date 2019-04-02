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
    public partial class TableService:BaseService<Ld_Log_Table>,ITableService
    {
        private readonly ITableDAL TableDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public TableService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ITableDAL TableDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.TableDAL = TableDAL;
            this.Dal = TableDAL;
        }
        public override void SetDal()
        {
            Dal = TableDAL;
        }

        private bool IsTableByName(string tableName)
        {
            return IsExists(m => m.TableName == tableName);
        }
        public bool SaveTable(Ld_Log_Table entity)
        {
            try
            {
                string tableName = entity.TableName;
                if (IsTableByName(tableName))
                    throw new Exception("表名已存在！");
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SaveTable(Ld_Log_Table entity, List<Ld_Log_TableDetails> list)
        {
            try
            {
                try
                {
                    string tableName = entity.TableName;
                    if (IsTableByName(tableName))
                        throw new Exception("表名已存在！");
                    int intnum = 0;
                    var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                    using (var db = dbContext.DbEntities())
                    {
                        using (var trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                entity.CreateDate = DateTime.Now;
                                db.Add(entity);
                                foreach (var m in list)
                                {
                                    m.CreateDate = DateTime.Now;
                                    db.Add(m);
                                }
                                intnum = db.SaveChanges();
                                trans.Commit();
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                            }
                            return intnum;
                        }
                    }
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
        public bool UpdateTableBusinessName(string tableId, string businessName, string remark)
        {
            try
            {
                var entity = GetTable(tableId);
                if (entity == null)
                    throw new Exception("table id invalid！");
                entity.BusinessName = businessName;
                entity.Remark = remark;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTable()
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_Table>();
                expression = expression.And(m => true);
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTable(string tableId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_Table>();
                expression = expression.And(m => m.TableID == tableId);
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTable(Ld_Log_Table entity)
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
        public Ld_Log_Table GetTable(string tableId)
        {
            try
            {
                return Find(m => m.TableID == tableId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Log_Table GetTableByName(string tableName)
        {
            try
            {
                return Find(m => m.TableName == tableName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_Table> GetTableTop(int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_Table>();
                expression = expression.And(m => true);
                var lists = FindListTop(expression, m => m.TableID, false, count);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_Table> GetTablePaging(int pageId, int pageSize, out int totalRows)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_Table>();
                expression = expression.And(m => true);
                totalRows = Count(expression);
                var lists = FindListPaging(expression, m => m.TableName, true, pageId, pageSize);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_Table> SearchTable(string keyword)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_Table>();
                expression = expression.And(m => m.TableName.Contains(keyword));
                var lists = FindList(expression, m => m.TableName, true);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
