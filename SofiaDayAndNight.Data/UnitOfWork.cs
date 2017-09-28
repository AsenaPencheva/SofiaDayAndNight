using SofiaDayAndNight.Data.Contracts;

namespace SofiaDayAndNight.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SofiaDayAndNightDbContext context;

        public UnitOfWork(SofiaDayAndNightDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}