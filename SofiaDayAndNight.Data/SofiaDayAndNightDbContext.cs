using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Models.Contracts;

namespace SofiaDayAndNight.Data
{
    public class SofiaDayAndNightDbContext : IdentityDbContext<User>
    {
        public SofiaDayAndNightDbContext()
            : base("SofiaDayAndNightDatabase", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Individual> Individuals { get; set; }

        public virtual IDbSet<Organization> Places { get; set; }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Multimedia> Multimedias { get; set; }

        //public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.OnMultimediaCreating(modelBuilder);
            this.OnEventAttended(modelBuilder);
            this.OnIndividualFriend(modelBuilder);
            this.OnIndividualPlace(modelBuilder);
            this.OnIndividualFriendRequests(modelBuilder);
            //this.OnImageComments(modelBuilder);
            //this.OnImageEvent(modelBuilder);
            //this.OnImageUser(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        private void OnIndividualFriendRequests(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>()
                .HasMany(i => i.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("IndividualRefId");
                    m.MapRightKey("RequestedFromRefId");
                    m.ToTable("IndividualFriendRequests");
                });
        }

        //private void OnImageUser(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Image>()
        //    //    .HasRequired(i => i.User)
        //    //    .WithOptional(u=>u.ProfileImage);
        //}

        //private void OnImageEvent(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Image>()
        //        .HasRequired(i => i.Event)
        //        .WithOptional(e => e.Cover);
        //}

        private void OnIndividualPlace(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>()
                .HasMany(i => i.Following)
                .WithMany(p => p.Followers)
                .Map(cs =>
                {
                    cs.MapLeftKey("IndividualRefId");
                    cs.MapRightKey("PlaceRefId");
                    cs.ToTable("IndividualPlaces");
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

        //private void OnImageComments(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Comment>()
        //        .HasRequired(c => c.Image)
        //        .WithMany(i => i.Comments);
        //}

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

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        //public void SetEntryState(object entity, EntityState entityState)
        //{
        //    var entry = this.Entry(entity);
        //    entry.State = entityState;
        //}

        //public EntityState GetState<T>(T entity) where T : class
        //{
        //    return this.Entry(entity).State;
        //}

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
