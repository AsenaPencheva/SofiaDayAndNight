using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using System;

namespace SofiaDayAndNight.Data
{
    public class SofiaDayAndNightDbContext : IdentityDbContext<ApplicationUser>, ISofiaDayAndNightDbContextSaveChanges
    {
        public SofiaDayAndNightDbContext()
            : base("SofiaDayAndNightDatabase")
        {
        }

        public IDbSet<Individual> Individuals { get; set; }

        public IDbSet<Place> Places { get; set; }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<Multimedia> Multimedias { get; set; }

        public IDbSet<Image> Images { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.OnMultimediaCreating(modelBuilder);
            this.OnEventAttended(modelBuilder);
            this.OnIndividualFriend(modelBuilder);
            this.OnIndividualPlace(modelBuilder);
            this.OnImageComments(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        private void OnIndividualPlace(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>()
                .HasMany(i => i.Following)
                .WithMany(p => p.Followers)
                .Map(cs =>
                {
                    cs.MapLeftKey("IndividualRefId");
                    cs.MapRightKey("PlaceRefId");
                    cs.ToTable("IndividualPlace");
                });
        }

        private void OnIndividualFriend(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>()
                .HasMany(i => i.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("IndividualRefId");
                    m.MapRightKey("FriendRefId");
                    m.ToTable("IndividualFriends");
                });
        }

        private void OnImageComments(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Image)
                .WithMany(i => i.Comments);
                //.WillCascadeOnDelete();
        }

        private void OnEventAttended(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(s => s.IndividualsAttended)
                .WithMany(i => i.EventsAttended)
                .Map(cs =>
                {
                    cs.MapLeftKey("EventRefId");
                    cs.MapRightKey("IndividualRefId");
                    cs.ToTable("EventAttended");
                });
        }

        private void OnMultimediaCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Multimedia>()
               .HasOptional(m => m.Event)
               .WithRequired(e => e.Multimedia);
        }

        public new IDbSet<T> Set<T>()
         where T : class
        {
            return base.Set<T>();
        }

        public static SofiaDayAndNightDbContext Create()
        {
            return new SofiaDayAndNightDbContext();
        }

        public void SetEntryState(object entity, EntityState entityState)
        {
            var entry = this.Entry(entity);
            entry.State = entityState;
        }

    }
}
