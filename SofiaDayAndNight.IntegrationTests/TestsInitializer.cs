using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Data.Entity.Migrations;

using SofiaDayAndNight.Data;

namespace SofiaDayAndNight.IntegrationTests
{
    [TestClass]
    public class TestsInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SofiaDayAndNightDbContext, TestDbConfiguration>());
        }
    }

    public sealed class TestDbConfiguration : DbMigrationsConfiguration<SofiaDayAndNightDbContext>
    {
        public TestDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
