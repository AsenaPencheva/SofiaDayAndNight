using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Bytes2you.Validation;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Contratcs;
using SofiaDayAndNight.Data.Models.Contracts;

namespace SofiaDayAndNight.Data.EfDbSetWrappers
{
    public class EfDbSetWrapper<T> : IEfDbSetWrapper<T>
         where T : class, IDeletable
    {
        private readonly ISofiaDayAndNightDbContext efDbContext;
        private readonly IDbSet<T> dbSet;

        public EfDbSetWrapper(ISofiaDayAndNightDbContext efDbContext)
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
            var state = this.efDbContext.GetState<T>(entity);
            if (state != EntityState.Detached)
            {
                this.efDbContext.SetEntryState(entity, EntityState.Added);
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            var state = this.efDbContext.GetState<T>(entity);
            if (state != EntityState.Detached)
            {
                this.efDbContext.Set<T>().Attach(entity);
            }

            this.efDbContext.SetEntryState(entity, EntityState.Added);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

           this.efDbContext.SetEntryState(entity, EntityState.Modified);
        }
    }
}
