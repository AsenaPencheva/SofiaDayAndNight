using SofiaDayAndNight.Data.Contracts;
using System.Data.Entity;

namespace SofiaDayAndNight.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}