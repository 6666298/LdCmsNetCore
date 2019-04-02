using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LdCms.IBLL
{
    /// <summary>
    /// DAL业务操作服务接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseService<T> where T:class ,new()
    {
        bool Add(T t);
        bool Add(List<T> entitys);
        bool Update(T t);
        bool Update(List<T> entitys);
        bool Delete(T t);
        bool Delete(Expression<Func<T, bool>> whereLambda);
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
    }
}
