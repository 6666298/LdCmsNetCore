using System;
using System.Collections.Generic;
using System.Reflection;

namespace LdCms.Web
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Log;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Net;
    using LdCms.Common.Web;
    using LdCms.Web.Models;
    

    /// <summary>
    /// 系统操作日志
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class TableOperationManager<T>: ITableOperationManager<T> where T : new()
    {
        private readonly ITableService TableService;
        private readonly ITableDetailsService TableDetailsService;
        private readonly ITableOperationService TableOperationService;
        public TableOperationManager(ITableService TableService, ITableDetailsService TableDetailsService, ITableOperationService TableOperationService)
        {
            this.TableService = TableService;
            this.TableDetailsService = TableDetailsService;
            this.TableOperationService = TableOperationService;
        }
        private string TableId { get; set; }
        private string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        private Ld_Log_Table Table
        {
            get
            {
                return TableService.GetTableByName(TableName);
            }
        }
        private List<Ld_Log_TableDetails> TableDetails
        {
            get
            {
                return TableDetailsService.GetTableDetailsByTableID(TableId);
            }
        }
        private string CreateTableID()
        {
            try
            {
                return PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.Table, PrimaryKeyHelper.PrimaryKeyLen.V1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void InitializeTabele()
        {
            TableName = typeof(T).Name;
            var entity = Table;
            if (entity != null)
            {
                TableId = entity.TableID;
                TableName = entity.TableName;
                PrimaryKey = entity.PrimaryKey;
            }
            else
            {
                var m = SaveTable(TableName);
                TableId = m.TableID;
                TableName = m.TableName;
                PrimaryKey = m.PrimaryKey;
            }
            if (string.IsNullOrEmpty(Account))
            {
                string SessionName = BaseSystemConfig.SessionName;
                string sessionJson = WebHelper.GetCookie(SessionName);
                AccountModel loginStaffModel = DESEncryptHelper.DecryptDES(sessionJson).ToObject<AccountModel>();
                if (loginStaffModel != null)
                {
                    Account = loginStaffModel.StaffID;
                    NickName = loginStaffModel.StaffName;
                }
            }

        }
        private Ld_Log_Table SaveTable(string tableName)
        {
            try
            {
                string tableID = CreateTableID();
                Ld_Log_Table entity = new Ld_Log_Table();
                entity.TableID = tableID;
                entity.TableName = tableName;
                entity.PrimaryKey = PrimaryKey;
                entity.Account = Account;
                entity.NickName = NickName;

                List<Ld_Log_TableDetails> lists = new List<Ld_Log_TableDetails>();
                PropertyInfo[] pi = typeof(T).GetProperties();
                foreach (PropertyInfo p in pi)
                {
                    string columnName = p.Name.ToString();  //得到属性的名称
                    Type columnType = p.PropertyType;       //得到属性的类型
                    if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    { 
                        columnType = p.PropertyType.GetGenericArguments()[0];
                    }
                    lists.Add(new Ld_Log_TableDetails()
                    {
                        TableID = tableID,
                        TableName = tableName,
                        ColumnName = columnName,
                        ColumnDataType = columnType.Name,
                        Account = Account,
                        NickName = NickName,
                    });
                }
                TableService.SaveTable(entity);
                TableDetailsService.SaveTableDetails(lists);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private bool Add(T t, string newContent, OperationClass operationClass, out long tableOperationID)
        {
            try
            {
                InitializeTabele();

                Type objTye = typeof(T);
                string oldContent = t.ToJson();
                var entity = new Ld_Log_TableOperation();
                entity.TableID = TableId;
                entity.TableName = objTye.Name;
                entity.ClientID = (int)OperationClient.Web;
                entity.ClientName = OperationClient.Web.ToString();
                entity.ClassID = ((int)operationClass).ToByte();
                entity.ClassName = operationClass.ToString();
                entity.OldContent = oldContent;
                entity.NewContent = newContent;
                entity.Account = Account;
                entity.NickName = NickName;
                entity.IpAdress = Net.Ip;
                entity.State = false;
                if (!string.IsNullOrEmpty(PrimaryKey))
                {
                    PropertyInfo p = objTye.GetProperty(PrimaryKey);
                    object primaryKeyValue = p.GetValue(t, null);
                    if (primaryKeyValue != null)
                    { 
                        entity.PrimaryKeyValue = primaryKeyValue.ToString();
                    }
                }
                return TableOperationService.SaveTableOperation(entity, out tableOperationID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Select(T t, out long operationId)
        {
            return Add(t, null, OperationClass.Select, out operationId);
        }
        public bool Add(T t, out long operationId)
        {
            return Add(t, null, OperationClass.Insert, out operationId);
        }
        public bool Update(T t, string newContent, out long operationId)
        {
            return Add(t, newContent, OperationClass.Update, out operationId);
        }
        public bool Delete(T t,out long operationId)
        {
            return Add(t, null, OperationClass.Delete, out operationId);
        }
        public bool SetState(long id, bool state)
        {
            try
            {
                return TableOperationService.UpdateTableOperationState(id, state);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public enum OperationClass
        {
            Select = 1,
            Insert = 2,
            Update = 3,
            Delete = 4
        }
        public enum OperationClient
        {
            Web = 1,
            M = 2,
            WX = 3,
            App = 4
        }


    }
}