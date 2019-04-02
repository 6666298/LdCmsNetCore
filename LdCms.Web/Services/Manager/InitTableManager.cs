using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace LdCms.Web.Services
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Log;
    using LdCms.Common.Security;
    /// <summary>
    /// 
    /// </summary>
    public class InitTableManager: IInitTableManager
    {
        private readonly ITableService TableService;
        private readonly ITableDetailsService TableDetailsService;
        public InitTableManager(ITableService TableService, ITableDetailsService TableDetailsService)
        {
            this.TableService = TableService;
            this.TableDetailsService = TableDetailsService;
        }

        public void CreateTable<T>(T t) where T : class
        {
            try
            {
                string tableID = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.Table, PrimaryKeyHelper.PrimaryKeyLen.V1);
                string tableName = typeof(T).Name;
                string primaryKey = "SystemID";
                string Account = "sys";
                string NickName = "系统生成";

                Ld_Log_Table entity = new Ld_Log_Table();
                entity.TableID = tableID;
                entity.TableName = tableName;
                entity.PrimaryKey = primaryKey;
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteTableAll()
        {
            try
            {
                TableService.DeleteTable();
                TableDetailsService.DeleteTableDetails();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Init()
        {
            try
            {
                DeleteTableAll();

                CreateTable(new Ld_Sys_AccessCorsHost());
                CreateTable(new Ld_Sys_Code());
                CreateTable(new Ld_Sys_Config());
                CreateTable(new Ld_Sys_Function());
                CreateTable(new Ld_Sys_InterfaceAccessToken());
                CreateTable(new Ld_Sys_InterfaceAccessWhiteList());
                CreateTable(new Ld_Sys_InterfaceAccount());
                CreateTable(new Ld_Sys_Operator());
                CreateTable(new Ld_Sys_OperatorRole());
                CreateTable(new Ld_Sys_Role());
                CreateTable(new Ld_Sys_RoleFunction());
                CreateTable(new Ld_Sys_Version());

                CreateTable(new Ld_Log_ErrorRecord());
                CreateTable(new Ld_Log_LoginRecord());
                CreateTable(new Ld_Log_Table());
                CreateTable(new Ld_Log_TableDetails());
                CreateTable(new Ld_Log_TableOperation());
                CreateTable(new Ld_Log_VisitorRecord());
                CreateTable(new Ld_Log_WebApiAccessRecord());

                CreateTable(new Ld_Institution_Company());
                CreateTable(new Ld_Institution_Dealer());
                CreateTable(new Ld_Institution_Department());
                CreateTable(new Ld_Institution_Position());
                CreateTable(new Ld_Institution_Staff());
                CreateTable(new Ld_Institution_Store());
                CreateTable(new Ld_Institution_Supplier());
                CreateTable(new Ld_Institution_Warehouse());

                CreateTable(new Ld_Member_AccessToken());
                CreateTable(new Ld_Member_Account());
                CreateTable(new Ld_Member_Address());
                CreateTable(new Ld_Member_Invoice());
                CreateTable(new Ld_Member_LoginLogs());
                CreateTable(new Ld_Member_AmountRecord());
                CreateTable(new Ld_Member_PointRecord());
                CreateTable(new Ld_Member_Rank());
                CreateTable(new Ld_Member_RefreshToken());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
