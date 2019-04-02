using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LdCms.IDAL
{
    /// <summary>
    /// DAL接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseDAL<T> where T : class, new()
    {
        void Add(T t);
        void Add(List<T> entitys);
        void Update(T t);
        void Update(List<T> entitys);
        void Delete(T t);
        void Delete(Expression<Func<T, bool>> whereLambda);
        bool IsExists(Expression<Func<T, bool>> whereLambda);
        int Count(Expression<Func<T, bool>> whereLambda);
        T Find(params object[] keyValues);
        T Find(Expression<Func<T, bool>> whereLambda);
        List<T> FindList(string cmdText);
        List<T> FindList(string cmdText, DbParameter[] sqlParameter);
        IQueryable<T> FindList();
        IQueryable<T> FindList(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> FindList<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc);
        IQueryable<T> FindListTop(Expression<Func<T, bool>> whereLambda, int count);
        IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int count);
        IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, int count);
        IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int count);
        IQueryable<T> FindListPaging<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int pageIndex, int pageSize);
        IQueryable<T> FindListPaging<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int pageIndex, int pageSize);
        /// <summary>
        /// 一个业务中有可能涉及到对多张表的操作,那么可以将操作的数据,打上相应的标记,最后调用该方法,将数据一次性提交到数据库中,避免了多次链接数据库。
        /// </summary>
        bool SaveChanges();
    }
}
