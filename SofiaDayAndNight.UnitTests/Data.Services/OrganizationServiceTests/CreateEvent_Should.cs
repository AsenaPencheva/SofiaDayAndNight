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
    public class CreateEvent_Should
    {
        [Test]
        public void NotCallAllMethod_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.All).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object,mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(new Event(), null);

            // Assert
            mockedEfWrappert.Verify(x => x.All, Times.Never);
        }

        [Test]
        public void NotCallAllMethod_WhenPassedEventIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.All).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(null, "testUserId");

            // Assert
            mockedEfWrappert.Verify(x => x.All, Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenParamsDoesntMatch()
        {
            // Arrange
            var data = new List<Organization>();

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(new Event(), "testUserId");

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Never);
        }

        [Test]
        public void CallUpdateMethod_WhenParamsMatch()
        {
            // Arrange
            var user = new User();
            var username = "testUser";
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(new Event(), username);

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Once);
        }
    }
}
