using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

using Bytes2you.Validation;
using SofiaDayAndNight.Data.Contracts;

namespace SofiaDayAndNight.Data.EfDbSetWrappers
{
    public class EfDbSetWrapper<T> : IEfDbSetWrapper<T>
         where T : class
    {
        private readonly DbContext efDbContext;
        private readonly IDbSet<T> dbSet;

        public EfDbSetWrapper(DbContext efDbContext)
        {
            Guard.WhenArgument(efDbContext, "efDbContext").IsNull().Throw();

            this.efDbContext = efDbContext;
            this.dbSet = efDbContext.Set<T>();
        }

        public IQueryable<T> All
        {
            get
            {
                return this.dbSet;
            }
        }

        public IQueryable<T> AllWithInclude<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            return this.All.Include(includeExpression);
        }

        public T GetById(Guid id)
        {
            return this.dbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.efDbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.efDbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.efDbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.dbSet.Attach(entity);
                this.dbSet.Remove(entity);
            }
        }
    }
}
