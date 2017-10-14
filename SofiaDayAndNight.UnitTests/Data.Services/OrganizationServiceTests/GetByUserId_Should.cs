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
    class GetByUserId_Should
    {
        [Test]
        public void NotThrowException_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object,mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetByUser(null));
        }

        [Test]
        public void ReturnNull_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string userId = "testUserId";
            user.Id = userId;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUser(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnModel_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string userId = "testUserId";
            user.Id = userId;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUser(userId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnNull_WhenThereIsNotAModelWithThePassedUser()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string userId = "testUserId";
            user.Id = userId;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUser("otherTestUserId");

            // Assert
            Assert.IsNull(result);
        }
    }
}
