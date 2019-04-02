using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LdCms.BLL
{
    using IDAL;

    /// <summary>
    /// DAL业务操作服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class BaseService<T> where T : class, new()
    {
        public BaseService()
        {
            SetDal();
        }
        public IBaseDAL<T> Dal;
        public abstract void SetDal();
        public bool Add(T t)
        {
            Dal.Add(t);
            return Dal.SaveChanges();
        }
        public bool Add(List<T> entitys)
        {
            Dal.Add(entitys);
            return Dal.SaveChanges();
        }
        public bool Update(T t)
        {
            Dal.Update(t);
            return Dal.SaveChanges();
        }
        public bool Update(List<T> entitys)
        {
            Dal.Update(entitys);
            return Dal.SaveChanges();
        }
        public bool Delete(T t)
        {
            Dal.Delete(t);
            return Dal.SaveChanges();
        }
        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            Dal.Delete(whereLambda);
            return Dal.SaveChanges();
        }
        public bool IsExists(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.IsExists(whereLambda);
        }
        public int Count(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.Count(whereLambda);
        }
        public T Find(params object[] keyValues)
        {
            return Dal.Find(keyValues);
        }
        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.Find(whereLambda);
        }
        public List<T> FindList(string cmdText)
        {
            return Dal.FindList(cmdText);
        }
        public List<T> FindList(string cmdText, DbParameter[] sqlParameter)
        {
            return Dal.FindList(cmdText, sqlParameter);
        }
        public IQueryable<T> FindList()
        {
            return Dal.FindList();
        }
        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLambda)
        {
            return Dal.FindList(whereLambda);
        }
        public IQueryable<T> FindList<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc)
        {
            return Dal.FindList(whereLambda, orderLambda, isAsc);
        }
        public IQueryable<T> FindListTop(Expression<Func<T, bool>> whereLambda, int count)
        {
            return Dal.FindListTop(whereLambda, count);
        }
        public IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int count)
        {
            return Dal.FindListTop(whereLambda, orderLambda, isAsc, count);
        }
        public IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, int count)
        {
            return Dal.FindListTop<type>(whereLambda, scalarLambda, count);
        }
        public IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int count)
        {
            return Dal.FindListTop<type>(whereLambda, scalarLambda, orderLambda, isAsc, count);
        }

        public IQueryable<T> FindListPaging<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int pageIndex, int pageSize)
        {
            return Dal.FindListPaging(whereLambda, orderLambda, isAsc, pageIndex, pageSize);
        }
        public IQueryable<T> FindListPaging<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int pageIndex, int pageSize)
        {
            return Dal.FindListPaging(whereLambda, scalarLambda, orderLambda, isAsc, pageIndex, pageSize);
        }
    }

}
