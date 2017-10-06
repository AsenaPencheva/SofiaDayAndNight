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

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

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
            modelBuilder.Entity<Individual>()
                .HasKey(i => i.ImageId);
            modelBuilder.Entity<Image>()
                .HasOptional(i => i.Individual)
                .WithRequired(i => i.ProfileImage);

            modelBuilder.Entity<Organization>()
               .HasKey(i => i.ImageId);
            modelBuilder.Entity<Image>()
                .HasOptional(i => i.Organization)
                .WithRequired(o => o.ProfileImage);

            modelBuilder.Entity<Event>()
               .HasKey(i => i.ImageId);
            modelBuilder.Entity<Image>()
                .HasOptional(i => i.Event)
                .WithRequired(e => e.Cover);

            //modelBuilder.Entity<Comment>()
            //  .HasKey(c => c.ImageId);
            //modelBuilder.Entity<Image>()
            //    .HasMany(i => i.Comments)
            //    .WithRequired(c => c.Image);
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
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Image)
                .WithMany(i => i.Comments)
                .HasForeignKey(c => c.ImageId);

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

            modelBuilder.Entity<Multimedia>()
                .HasKey(m => m.EventId);
            modelBuilder.Entity<Multimedia>()
                .HasRequired(m => m.Event)
                .WithRequiredPrincipal(e => e.Multimedia);
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
