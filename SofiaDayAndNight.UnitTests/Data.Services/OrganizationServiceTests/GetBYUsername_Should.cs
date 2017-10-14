using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class GetByUsername_Should
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
            Assert.DoesNotThrow(() => service.GetByUsername(null));
        }

        [Test]
        public void ReturnNull_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var user = new User();
            string username = "testUsername";
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUsername(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnModel_WhenThereIsAModelWithThePassedUsername()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            string username = "testUsername";
            var user = new User();
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUsername(username);

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

            string username = "testUsername";
            var user = new User();
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUsername("otherTestUser");

            // Assert
            Assert.IsNull(result);
        }
    }
}
