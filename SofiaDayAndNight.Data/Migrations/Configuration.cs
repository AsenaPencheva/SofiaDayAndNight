namespace SofiaDayAndNight.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using SofiaDayAndNight.Data.Models;
    using SofiaDayAndNight.Common.Enums;

    public sealed class Configuration : DbMigrationsConfiguration<SofiaDayAndNight.Data.SofiaDayAndNightDbContext>
    {
        private const string AdministratorUserName = "admin@test.com";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SofiaDayAndNightDbContext context)
        {
            this.SeedUsers(context);

            base.Seed(context);
        }

        private void SeedUsers(SofiaDayAndNightDbContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            //Create Role if it does not exist
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole(UserRole.Admin.ToString()));
                roleManager.Create(new IdentityRole(UserRole.Individual.ToString()));
                roleManager.Create(new IdentityRole(UserRole.Organization.ToString()));
            }

            if (!context.Users.Any())
            {
                CreateNewUser(userManager, roleManager, UserRole.Admin.ToString(), AdministratorUserName, AdministratorPassword);
            }
        }
        private static void CreateNewUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, string name, string email, string password)
        {
            //Create User with password
            var user = new User();
            user.UserName = "admin";
            user.Email = email;
            user.CreatedOn = DateTime.Now;
            var adminresult = userManager.Create(user, password);

            //Add User to Role
            if (adminresult.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, name);
            }
        }
    }
}
