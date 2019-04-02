using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LdCms.IDAL
{
    using EF.DbEntitiesContext;
    using LdCms.EF.DbModels;

    /// <summary>
    /// DAL接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseDAL 
    {
        LdCmsDbEntitiesContext DbEntities();
        void Add<T>(T t) where T : class;
        void Add<T>(List<T> entitys) where T : class;
        void Update<T>(T t) where T : class;
        void Update<T>(List<T> entitys) where T : class;
        void Delete<T>(T t) where T : class;
        void Delete<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        bool IsExists<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        int Count<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        int ExecuteCommand(string cmdText);
        int ExecuteCommand(string cmdText, DbParameter[] sqlParameter);
        T Find<T>(params object[] keyValues) where T : class;
        T Find<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        List<T> FindList<T>(string cmdText) where T : class;
        List<T> FindList<T>(string cmdText, DbParameter[] sqlParameter) where T : class;
        IQueryable<T> FindList<T>() where T : class;
        IQueryable<T> FindList<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        IQueryable<T> FindList<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> orderLambda, bool isAsc) where T : class;
        IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, int count) where T : class;
        IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int count) where T : class;
        IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, int count) where T : class;
        IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int count) where T : class;

        IQueryable<T> FindListPaging<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int pageIndex, int pageSize) where T : class;
        IQueryable<T> FindListPaging<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int pageIndex, int pageSize) where T : class;
        /// <summary>
        /// 一个业务中有可能涉及到对多张表的操作,那么可以将操作的数据,打上相应的标记,最后调用该方法,将数据一次性提交到数据库中,避免了多次链接数据库。
        /// </summary>
        bool SaveChanges();
    }
}
