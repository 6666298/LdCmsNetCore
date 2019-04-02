using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LdCms.DAL
{
    using IDAL;
    using EF.DbEntitiesContext;

    /// <summary>
    /// DAL操作实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        private readonly LdCmsDbEntitiesContext dbContext;
        public BaseDAL(LdCmsDbEntitiesContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(T t)
        {
            dbContext.Set<T>().Add(t);
        }
        public void Add(List<T> entitys)
        {
            foreach (var entity in entitys)
            {
                dbContext.Entry<T>(entity).State = EntityState.Added;
            }
        }
        public void Update(T t)
        {
            dbContext.Set<T>().Update(t);
        }
        public void Update(List<T> entitys)
        {
            foreach (var entity in entitys)
            {
                dbContext.Entry<T>(entity).State = EntityState.Modified;
            }
        }
        public void Delete(T t)
        {
            dbContext.Set<T>().Remove(t);
        }
        public void Delete(Expression<Func<T, bool>> whereLambda)
        {
            var entitys = dbContext.Set<T>().Where(whereLambda).ToList();
            entitys.ForEach(m => dbContext.Entry<T>(m).State = EntityState.Deleted);
        }
        public bool IsExists(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Any(whereLambda);
        }
        public int Count(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Count(whereLambda);
        }
        public T Find(params object[] keyValues)
        {
            return dbContext.Set<T>().Find(keyValues);
        }
        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().FirstOrDefault<T>(whereLambda);
        }
        public List<T> FindList(string cmdText)
        {
            return dbContext.Set<T>().FromSql(cmdText).ToList<T>();
        }
        public List<T> FindList(string cmdText, DbParameter[] sqlParameter)
        {
            return dbContext.Set<T>().FromSql(cmdText, sqlParameter).ToList<T>();
        }
        public IQueryable<T> FindList()
        {
            return dbContext.Set<T>();
        }
        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where(whereLambda);
        }
        public IQueryable<T> FindList<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc)
        {
            if(isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda);
        }
        public IQueryable<T> FindListTop(Expression<Func<T, bool>> whereLambda, int count)
        {
            return dbContext.Set<T>().Where(whereLambda).Take(count);
        }
        public IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int count)
        {
            if (isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda).Take(count);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Take(count);
        }
        public IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, int count)
        {
            return dbContext.Set<T>().AsNoTracking().Where(whereLambda).Select(scalarLambda).Take(count);
        }
        public IQueryable<T> FindListTop<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int count)
        {
            if (isAsc)
                return dbContext.Set<T>().AsNoTracking().Where(whereLambda).OrderBy(orderLambda).Select(scalarLambda).Take(count);
            else
                return dbContext.Set<T>().AsNoTracking().Where(whereLambda).OrderByDescending(orderLambda).Select(scalarLambda).Take(count);
        }


        public IQueryable<T> FindListPaging<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int pageIndex, int pageSize)
        {
            if (isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<T> FindListPaging<type>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, type>> orderLambda, bool isAsc, int pageIndex, int pageSize)
        {
            if (isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda).Select(scalarLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Select(scalarLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }





        public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0;
        }
    }
}
