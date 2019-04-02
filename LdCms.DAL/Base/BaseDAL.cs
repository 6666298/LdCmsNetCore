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
    public partial class BaseDAL : IBaseDAL
    {
        private readonly LdCmsDbEntitiesContext dbContext;
        public BaseDAL(LdCmsDbEntitiesContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public LdCmsDbEntitiesContext DbEntities()
        {
            return dbContext;
        }
        public void Add<T>(T t) where T : class
        {
            dbContext.Set<T>().Add(t);
        }
        public void Add<T>(List<T> entitys) where T : class
        {
            foreach (var entity in entitys)
            {
                dbContext.Entry<T>(entity).State = EntityState.Added;
            }
        }
        public void Update<T>(T t) where T : class
        {
            dbContext.Set<T>().Update(t);
        }
        public void Update<T>(List<T> entitys) where T : class
        {
            foreach (var entity in entitys)
            {
                dbContext.Entry<T>(entity).State = EntityState.Modified;
            }
        }
        public void Delete<T>(T t) where T : class
        {
            dbContext.Set<T>().Remove(t);
        }
        public void Delete<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            var entitys = dbContext.Set<T>().Where(whereLambda).ToList();
            entitys.ForEach(m => dbContext.Entry<T>(m).State = EntityState.Deleted);
        }
        public bool IsExists<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return dbContext.Set<T>().Any(whereLambda);
        }
        public int Count<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return dbContext.Set<T>().Count(whereLambda);
        }
        public int ExecuteCommand(string cmdText)
        {
            return dbContext.Database.ExecuteSqlCommand(cmdText);
        }
        public int ExecuteCommand(string cmdText, DbParameter[] sqlParameter)
        {
            return dbContext.Database.ExecuteSqlCommand(cmdText, sqlParameter);
        }
        public T Find<T>(params object[] keyValues) where T : class
        {
            return dbContext.Set<T>().Find(keyValues);
        }
        public T Find<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return dbContext.Set<T>().FirstOrDefault<T>(whereLambda);
        }
        public List<T> FindList<T>(string cmdText) where T : class
        {
            return dbContext.Set<T>().FromSql(cmdText).ToList<T>();
        }
        public List<T> FindList<T>(string cmdText, DbParameter[] sqlParameter) where T : class
        {
            return dbContext.Set<T>().FromSql(cmdText, sqlParameter).ToList<T>();
        }
        public IQueryable<T> FindList<T>() where T : class
        {
            return dbContext.Set<T>();
        }
        public IQueryable<T> FindList<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return dbContext.Set<T>().Where(whereLambda);
        }
        public IQueryable<T> FindList<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> orderLambda, bool isAsc) where T : class
        {
            if(isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda);
        }
        public IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, int count) where T : class
        {
            return dbContext.Set<T>().Where(whereLambda).Take(count);
        }
        public IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int count) where T : class
        {
            if (isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda).Take(count);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Take(count);
        }
        public IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, int count) where T : class
        {
            return dbContext.Set<T>().AsNoTracking().Where(whereLambda).Select(scalarLambda).Take(count);
        }
        public IQueryable<T> FindListTop<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int count) where T : class
        {
            if (isAsc)
                return dbContext.Set<T>().AsNoTracking().Where(whereLambda).OrderBy(orderLambda).Select(scalarLambda).Take(count);
            else
                return dbContext.Set<T>().AsNoTracking().Where(whereLambda).OrderByDescending(orderLambda).Select(scalarLambda).Take(count);
        }

        public IQueryable<T> FindListPaging<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int pageIndex, int pageSize) where T : class
        {
            if (isAsc)
                return dbContext.Set<T>().Where(whereLambda).OrderBy(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return dbContext.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<T> FindListPaging<T>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> scalarLambda, Expression<Func<T, bool>> orderLambda, bool isAsc, int pageIndex, int pageSize) where T : class
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
