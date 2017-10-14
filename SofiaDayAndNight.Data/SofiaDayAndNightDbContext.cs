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

        public virtual DbSet<Individual> Individuals { get; set; }

        public virtual DbSet<Organization> Places { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Multimedia> Multimedias { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.OnEventCreating(modelBuilder);
            this.OnIndividualFriend(modelBuilder);
            this.OnIndividualPlace(modelBuilder);
            this.OnIndividualFriendRequests(modelBuilder);
            this.OnUserCreating(modelBuilder);
            this.OnCommentsCreating(modelBuilder);
            this.OnImageCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        private void OnUserCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasRequired(o => o.User)
                .WithOptional(u => u.Organization);

            modelBuilder.Entity<Individual>()
              .HasRequired(i => i.User)
              .WithOptional(u => u.Individual);
        }

        private void OnIndividualFriendRequests(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Individual>()
                .HasMany(i => i.FriendRequests)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("IndividualId");
                    m.MapRightKey("RequestedFromId");
                    m.ToTable("IndividualFriendRequests");
                });

            modelBuilder.Entity<Individual>()
                .HasMany(i => i.FriendRequested)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("IndividualId");
                    m.MapRightKey("RequestedToId");
                    m.ToTable("IndividualFriendRequested");
                });
        }

        private void OnImageCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
               .HasRequired(i => i.ProfileImage)
               .WithOptional(i => i.Organization)
               .Map(i => i.MapKey("ImageId"));

            modelBuilder.Entity<Individual>()
               .HasRequired(i => i.ProfileImage)
               .WithOptional(i => i.Individual)
               .Map(i => i.MapKey("ImageId"));

            modelBuilder.Entity<Event>()
                .HasRequired(i => i.Cover)
                .WithOptional(i => i.Event)
                .Map(i => i.MapKey("ImageId"));
                       
            modelBuilder.Entity<Multimedia>()
               .HasMany(m => m.Images)
               .WithMany(i => i.Multimedias)
               .Map(m =>
               {
                   m.MapLeftKey("MultimediaId");
                   m.MapRightKey("ImageId");
                   m.ToTable("MultimediaImages");
               });

            modelBuilder.Entity<Event>()
              .HasRequired(i => i.Multimedia)
              .WithRequiredDependent(i => i.Event)
              .Map(i => i.MapKey("MultimediaId"));
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
                    m.MapLeftKey("IndividualId");
                    m.MapRightKey("FriendId");
                    m.ToTable("IndividualFriends");
                });
        }

        private void OnCommentsCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Image>()
               .HasMany(i => i.Comments)
                .WithRequired(c => c.Image);

            modelBuilder.Entity<Comment>()
                 .HasRequired(c => c.Author)
                 .WithMany(i => i.Commented)
                 .HasForeignKey(c => c.IndividualId);
        }

        private void OnEventCreating(DbModelBuilder modelBuilder)
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

            modelBuilder.Entity<Event>()
                .HasOptional(e => e.Individual)
                .WithMany(i => i.Events)
                .Map(i => i.MapKey("IndividualId"));

            modelBuilder.Entity<Event>()
                .HasOptional(o => o.Organization)
                .WithMany(i => i.Events)
                .Map(i => i.MapKey("OrganizationId"));
        }

        public new DbSet<T> Set<T>()
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
