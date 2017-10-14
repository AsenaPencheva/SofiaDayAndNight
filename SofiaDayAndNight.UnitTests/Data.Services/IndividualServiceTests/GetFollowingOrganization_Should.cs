using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class GetFollowingOrganization_Should
    {
        [Test]
        public void ReturnEmptyCollection_WhenPassedUsernameIsNll()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFollowingOrganization(null);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnEmptyCollection_WhenUsernameDoesntMatch()
        {
            // Arrange
            var data = new List<Individual>();

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFollowingOrganization("testUser");

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCollection_WhenUsernameMatch()
        {
            // Arrange
            var organization = new Organization();
            var user = new User();
            var username = "testUsername";
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            individual.Following.Add(organization);
            var data = new List<Individual>();
            data.Add(individual);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFollowingOrganization(username);

            // Assert
            Assert.AreEqual(1,result.Count());
        }
    }
}
