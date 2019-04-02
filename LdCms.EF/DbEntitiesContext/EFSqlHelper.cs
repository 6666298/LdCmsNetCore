using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LdCms.EF.DbEntitiesContext
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF Core SQL操作类
    /// 
    /// 
    /// 
    /// </summary>
    public static class EFSqlHelper
    {

        #region 执行新增、修改、删除动作返回 int
        /// <summary>
        /// 执行SQL语句、存储过程返回影响的行数 注：主要用于新增、修改、删除操作返回影响行数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this LdCmsDbEntitiesContext db, CommandType cmdType, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                int numint;
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    db.Database.OpenConnection();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                    }
                    numint = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                return numint;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行SQL语句不带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return db.ExecuteNonQuery(CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行SQL语句带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return db.ExecuteNonQuery(CommandType.Text, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程不带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public static int ExecuteNonQueryPro(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return db.ExecuteNonQuery(CommandType.StoredProcedure, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static int ExecuteNonQueryPro(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return db.ExecuteNonQuery(CommandType.StoredProcedure, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 执行查询第一行第一列数据返回 string
        /// <summary>
        /// 执行SQL语句、存储过程返回第一行第一列数据 主要用于统计、计算
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static string ExecuteScalar(this LdCmsDbEntitiesContext db, CommandType cmdType, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                string result = string.Empty;
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    db.Database.OpenConnection();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                    }
                    var scalarResult = cmd.ExecuteScalar();
                    if (scalarResult != null)
                        result = scalarResult.ToString();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行SQL语句不带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public static string ExecuteScalar(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return db.ExecuteScalar(CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行SQL语句带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static string ExecuteScalar(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return db.ExecuteScalar(CommandType.Text, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程不带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public static string ExecuteScalarPro(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return db.ExecuteScalar(CommandType.StoredProcedure, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行存储过程带参数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static string ExecuteScalarPro(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return db.ExecuteScalar(CommandType.StoredProcedure, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 执行查询数据集返回 DbDataReader、ArrayList
        /// <summary>
        /// 执行SQL语句、存储过程查询数据集合返回 ArrayList 主要用于数据集合查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static ArrayList ExecuteReader(this LdCmsDbEntitiesContext db, CommandType cmdType, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    db.Database.OpenConnection();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                    }
                    var dr = cmd.ExecuteReader();
                    var columnSchema = dr.GetColumnSchema();
                    var data = new ArrayList();
                    while (dr.Read())
                    {
                        var item = new Dictionary<string, object>();
                        foreach (var kv in columnSchema)
                        {
                            if (kv.ColumnOrdinal.HasValue)
                            {
                                var itemVal = dr.GetValue(kv.ColumnOrdinal.Value);
                                item.Add(kv.ColumnName, itemVal.GetType() != typeof(DBNull) ? itemVal : "");
                            }
                        }
                        data.Add(item);
                    }
                    dr.Close();
                    dr.Dispose();
                    cmd.Parameters.Clear();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public static ArrayList ExecuteReader(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return db.ExecuteReader(CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static ArrayList ExecuteReader(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return db.ExecuteReader(CommandType.Text, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public static ArrayList ExecuteReaderPro(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return db.ExecuteReader(CommandType.StoredProcedure, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public static ArrayList ExecuteReaderPro(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return db.ExecuteReader(CommandType.StoredProcedure, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 创建事务执行SQL语句
        /// <summary>
        /// 创建事务执行SQL语句 主要用于多表新增、修改、删除操作
        /// 调用说明：
        ///     ArrayList cmdTextList = new ArrayList();
        ///     cmdTextList.Add("insert into Ld_Sys_Test (Name,CreateDate) values ('李白',getdate())");
        ///     cmdTextList.Add("insert into Ld_Sys_Staff (id,StaffId,StaffName) values (15,'1234567890','李白')");
        ///     cmdTextList.Add("declare @errCode int,@errMsg nvarchar(400) exec SP_Add_Sys_Test '张长',@errCode output,@errMsg output");
        ///     db.ExecuteCommandTrans(cmdTextList);
        /// 
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdTextList">SQL语句数组列表</param>
        public static void ExecuteCommandTrans(this LdCmsDbEntitiesContext db, ArrayList cmdTextList)
        {
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    db.Database.OpenConnection();
                    var varTrans = connection.BeginTransaction();
                    cmd.Connection = connection;
                    cmd.Transaction = varTrans;
                    try
                    {
                        foreach (string varCmdText in cmdTextList)
                        {
                            cmd.CommandText = varCmdText;
                            cmd.ExecuteNonQuery();
                        }
                        varTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        varTrans.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 异步执行新增、修改、删除动作返回 Task<int>
        /// <summary>
        /// 异步执行SQL语句、存储过程返回影响的行数 注：主要用于新增、修改、删除操作返回影响行数
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<int> ExecuteNonQueryAsync(this LdCmsDbEntitiesContext db, CommandType cmdType, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                int numint;
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    await db.Database.OpenConnectionAsync();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                    }
                    numint = await cmd.ExecuteNonQueryAsync();
                    //var a = sqlParams[1].Value;//测试回调参数值
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                return numint;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public async static Task<int> ExecuteNonQueryAsync(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return await db.ExecuteNonQueryAsync(CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<int> ExecuteNonQueryAsync(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return await db.ExecuteNonQueryAsync(CommandType.Text, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public async static Task<int> ExecuteNonQueryProAsync(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return await db.ExecuteNonQueryAsync(CommandType.StoredProcedure, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<int> ExecuteNonQueryProAsync(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return await db.ExecuteNonQueryAsync(CommandType.StoredProcedure, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 异步执行查询第一行第一列数据返回 Task<string>
        /// <summary>
        /// 异步执行SQL语句、存储过程返回第一行第一列数据 主要用于统计、计算
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<string> ExecuteScalarAsync(this LdCmsDbEntitiesContext db, CommandType cmdType, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                string result = string.Empty;
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    await db.Database.OpenConnectionAsync();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                    }
                    var scalarResult = await cmd.ExecuteScalarAsync();
                    if (scalarResult != null)
                        result = scalarResult.ToString();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public async static Task<string> ExecuteScalarAsync(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return await db.ExecuteScalarAsync(CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<string> ExecuteScalarAsync(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return await db.ExecuteScalarAsync(CommandType.Text, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public async static Task<string> ExecuteScalarProAsync(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return await db.ExecuteScalarAsync(CommandType.StoredProcedure, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<string> ExecuteScalarProAsync(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return await db.ExecuteScalarAsync(CommandType.StoredProcedure, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 异步执行查询数据集返回 DbDataReader、Task<ArrayList>
        /// <summary>
        /// 异步执行SQL语句、存储过程查询数据集合及空值处理返回ArrayList。主要用于数据集合查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<ArrayList> ExecuteReaderAsync(this LdCmsDbEntitiesContext db, CommandType cmdType, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                var connection = db.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    await db.Database.OpenConnectionAsync();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (sqlParams != null)
                    {
                        cmd.Parameters.AddRange(sqlParams);
                    }
                    var dr = await cmd.ExecuteReaderAsync();
                    var columnSchema = dr.GetColumnSchema();
                    var data = new ArrayList();
                    while (await dr.ReadAsync())
                    {
                        var item = new Dictionary<string, object>();
                        foreach (var kv in columnSchema)
                        {
                            if (kv.ColumnOrdinal.HasValue)
                            {
                                var itemVal = dr.GetValue(kv.ColumnOrdinal.Value);
                                item.Add(kv.ColumnName, itemVal.GetType() != typeof(DBNull) ? itemVal : "");
                            }
                        }
                        data.Add(item);
                    }
                    dr.Close();
                    dr.Dispose();
                    //var a = sqlParams[1].Value;//测试回调参数值
                    cmd.Parameters.Clear();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public async static Task<ArrayList> ExecuteReaderAsync(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return await db.ExecuteReaderAsync(CommandType.Text, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<ArrayList> ExecuteReaderAsync(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return await db.ExecuteReaderAsync(CommandType.Text, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <returns></returns>
        public async static Task<ArrayList> ExecuteReaderProAsync(this LdCmsDbEntitiesContext db, string cmdText)
        {
            try
            {
                return await db.ExecuteReaderAsync(CommandType.StoredProcedure, cmdText, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="db">链接数据库EF DbContext对象</param>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="sqlParams">参数 SqlParameter集合</param>
        /// <returns></returns>
        public async static Task<ArrayList> ExecuteReaderProAsync(this LdCmsDbEntitiesContext db, string cmdText, SqlParameter[] sqlParams)
        {
            try
            {
                return await db.ExecuteReaderAsync(CommandType.StoredProcedure, cmdText, sqlParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #endregion

    }
}
