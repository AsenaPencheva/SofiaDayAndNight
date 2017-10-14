
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using NUnit.Framework;
using SofiaDayAndNight.Data;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaDayAndNight.IntegrationTests.Data.Services
{
    [TestFixture]
    public class GetById_Should
    {
        private static User dbUser = new User()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Test UserName",
            Email = "Test email",
            PasswordHash = "somePassword"
        };

        private static Image dbimage = new Image()
        {
            ContentType = "type",
            Name = "image name",
            Data = new byte[128]
        };

        private static Individual dbIndividual = new Individual()
        {
            Id = Guid.NewGuid(),
            FirstName = "Test Name",
            LastName="Test Name",
            Age=18
        };

        private static IKernel kernel;

        [SetUp]
        public void TestInit()
        {
            kernel = NinjectWebCommon.CreateKernel();
            SofiaDayAndNightDbContext dbContext = kernel.Get<SofiaDayAndNightDbContext>();

            var userManager = new UserManager<User>(new UserStore<User>(dbContext));

            dbIndividual.ProfileImage = dbimage;
            dbIndividual.User = dbUser;
            dbUser.Individual = dbIndividual;
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void TestCleanup()
        {
            SofiaDayAndNightDbContext dbContext = kernel.Get<SofiaDayAndNightDbContext>();

            dbContext.Images.Attach(dbimage);
            dbContext.Images.Remove(dbimage);
            dbContext.Individuals.Attach(dbIndividual);
            dbContext.Individuals.Remove(dbIndividual);
            dbContext.Users.Attach(dbUser);
            dbContext.Users.Remove(dbUser);

            dbContext.SaveChanges();
        }

        [TestCase]
        public void ReturnModelWithCorrectProperties_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            IIndividualService service = kernel.Get<IIndividualService>();

            // Act
            var result = service.GetById(dbIndividual.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dbIndividual.Id, result.Id);            
            Assert.AreEqual(dbIndividual.FirstName, result.FirstName);
            Assert.AreEqual(dbUser.UserName, result.User.UserName);
        }

        [TestCase]
        public void ReturnNull_WhenIdIsNull()
        {
            // Arrange
            IIndividualService service = kernel.Get<IIndividualService>();

            // Act
            Individual individual = service.GetById(null);

            // Assert
            Assert.IsNull(individual);
        }

        [TestCase]
        public void ReturnNull_WhenThereIsNoModelWithThePassedId()
        {
            // Arrange
            IIndividualService service = kernel.Get<IIndividualService>();

            // Act
            Individual individual = service.GetById(Guid.NewGuid());

            // Assert
            Assert.IsNull(individual);
        }
    }
}