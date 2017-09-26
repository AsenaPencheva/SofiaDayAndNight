using System.Data.Entity;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.Data.Contratcs
{
    public interface ISofiaDayAndNightDbContext
    {
        IDbSet<Comment> Comments { get; set; }
        IDbSet<Event> Events { get; set; }
        IDbSet<Image> Images { get; set; }
        IDbSet<Individual> Individuals { get; set; }
        IDbSet<Multimedia> Multimedias { get; set; }
        IDbSet<Place> Places { get; set; }

        int SaveChanges();
        IDbSet<T> Set<T>() where T : class;
        void SetEntryState(object entity, EntityState entityState);
        EntityState GetState<T>(T entity) where T : class;
    }
}