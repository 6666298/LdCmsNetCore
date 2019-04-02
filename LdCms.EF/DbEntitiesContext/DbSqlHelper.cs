using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace LdCms.EF.DbEntitiesContext
{
    using LdCms.EF.DbConfig;
    using LdCms.Common.Extension;
    
    /// <summary>
    /// SQL操作类
    /// </summary>
    public static class DbSqlHelper
    {
        #region 数据库链接
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private static string connectionString = ConfigurationHelper.GetAppSettings<ConnectionStrings>("ConnectionStrings").SqlServerConnection;
        /// <summary>
        /// 数据库链接
        /// </summary>
        /// <returns></returns>
        private static SqlConnection OpenCn()
        {
            string cmdText = connectionString;
            SqlConnection conn = new SqlConnection();
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
            return new SqlConnection(cmdText);
        }
        #endregion

        #region 操作新增、修改、删除动作返回int
        /// <summary>
        /// 执行SQL语句返回影响的行数 注：主要用于新增、修改、删除操作返回影响行数
        /// </summary>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, SqlParameter[] spr)
        {
            try
            {
                int i = 0;
                using (SqlConnection conn = OpenCn())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.Clear();
                    cmd.CommandType = cmdType;
                    if (spr != null)
                    {
                        cmd.Parameters.AddRange(spr);
                    }
                    i = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQueryPro(string cmdText, SqlParameter[] spr)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQueryPro(string cmdText)
        {
            return ExecuteNonQueryPro(cmdText, null);
        }
        /// <summary>
        /// 带参数的操作语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] spr)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的操作语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>返回影响行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(cmdText, null);
        }
        #endregion

        #region 查询数据集返回DataSet
        /// <summary>
        /// 获取数据集返回DataSet
        /// </summary>
        /// <param name="cmdType">链接类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static DataSet GetDataSet(CommandType cmdType, string cmdText, SqlParameter[] spr)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = OpenCn())
                {
                    conn.Open();
                    SqlDataAdapter oda = new SqlDataAdapter(cmdText, conn);
                    oda.SelectCommand.CommandType = cmdType;
                    if (spr != null)
                    {
                        oda.SelectCommand.Parameters.AddRange(spr);
                    }
                    oda.Fill(ds);
                    oda.SelectCommand.Parameters.Clear();
                    oda.Dispose();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static DataSet GetDataSetPro(string cmdText, SqlParameter[] spr)
        {
            return GetDataSet(CommandType.StoredProcedure, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns></returns>
        public static DataSet GetDataSetPro(string cmdText)
        {
            return GetDataSetPro(cmdText, null);
        }
        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText, SqlParameter[] spr)
        {
            return GetDataSet(CommandType.Text, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText)
        {
            return GetDataSet(CommandType.Text, cmdText, null);
        }
        #endregion

        #region 查询数据集返回SqlDataReader
        /// <summary>
        /// 数据阅读器
        /// </summary>
        /// <param name="cmdType">链接类型 如：SQL语句、存储过程</param>
        /// <param name="cmdText">SQL文本</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, SqlParameter[] spr)
        {
            
            try
            {
                SqlDataReader dr = null;
                SqlConnection conn = OpenCn();
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdType;
                cmd.Parameters.Clear();
                if (spr != null)
                {
                    cmd.Parameters.AddRange(spr);
                }
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                cmd.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReaderPro(string cmdText, SqlParameter[] spr)
        {
            return ExecuteReader(CommandType.StoredProcedure, cmdText, spr);
        }
        /// <summary>
        /// 不带带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReaderPro(string cmdText)
        {
            return ExecuteReaderPro(cmdText, null);
        }
        /// <summary>
        /// 带参数的文本
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] spr)
        {
            return ExecuteReader(CommandType.Text, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的文本
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(cmdText, null);
        }
        #endregion

        #region 查询第一行第一列数据返回string
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmdType">链接类型 如：SQL语句、存储过程</param>
        /// <param name="cmdText">SQL文本</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static string ExecuteScalar(CommandType cmdType, string cmdText, SqlParameter[] spr)
        {
            try
            {
                string str = string.Empty;
                using (SqlConnection conn = OpenCn())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.CommandType = cmdType;
                    cmd.Parameters.Clear();
                    if (spr != null)
                    {
                        cmd.Parameters.AddRange(spr);
                    }
                    if (cmd.ExecuteScalar() != null)
                    {
                        str = cmd.ExecuteScalar().ToString();
                    }
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static string ExecuteScalarPro(string cmdText, SqlParameter[] spr)
        {
            return ExecuteScalar(CommandType.StoredProcedure, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <returns></returns>
        public static string ExecuteScalarPro(string cmdText)
        {
            return ExecuteScalarPro(cmdText, null);
        }
        /// <summary>
        /// 带参数的文本
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="spr">SqlParameter集合</param>
        /// <returns></returns>
        public static string ExecuteScalar(string cmdText, SqlParameter[] spr)
        {
            return ExecuteScalar(CommandType.Text, cmdText, spr);
        }
        /// <summary>
        /// 不带参数的文本
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        public static string ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(cmdText, null);
        }
        #endregion

        #region 数据库事务执行
        /// <summary>
        /// 执行创建事务执行SQL语句
        /// </summary>
        /// <param name="varSqlList">SQL语句数组</param>
        public static void ExecuteCommandTrans(ArrayList cmdTextList)
        {
            try
            {
                using (SqlConnection conn = OpenCn())
                {
                    conn.Open();
                    SqlTransaction varTrans = conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
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

        #region SQL脚本文件操作
        /// <summary>
        /// SQL脚本文件转换SQL语句数组
        /// </summary>
        /// <param name="sqlFile"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(string sqlFile)
        {
            try
            {
                if (!File.Exists(sqlFile))
                    return null;
                StreamReader sr = File.OpenText(sqlFile);
                ArrayList alSql = new ArrayList();
                string commandText = "";
                string varLine = "";
                while (sr.Peek() > -1)
                {
                    varLine = sr.ReadLine();
                    if (varLine == "")
                        continue;
                    if (varLine != "GO")
                    {
                        commandText += varLine;
                        commandText += "\r\n";
                    }
                    else
                    {
                        alSql.Add(commandText);
                        commandText = "";
                    }
                }
                sr.Close();
                return alSql;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }

}