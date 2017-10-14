using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class GetFollowers_Should
    {
        private static readonly string username = "testUsername";
        private static IEnumerable<Organization> Data()
        {
            var user = new User();
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var follower = new Individual();
            organization.Followers.Add(follower);
            var data = new List<Organization>();
            data.Add(organization);

            return data;
        }

        [Test]
        public void NotThrowException_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetFollowers(null));
        }

        [Test]
        public void ReturnEmptyCollection_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFollowers(null);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnCollection_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFollowers(username);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnEmptyCollection_WhenThereIsNotAModelWithThePassedUser()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFollowers("otherTestUser");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
