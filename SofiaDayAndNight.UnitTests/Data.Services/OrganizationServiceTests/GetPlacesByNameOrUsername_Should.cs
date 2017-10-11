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
    public class GetPlacesByNameOrUsername_Should
    {
        [Test]
        public void ReturnEmptyCollection_WhenPassedValueIsNull()
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
            var result = service.GetPlacesByNameOrUsername(null);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCollectionWithFounModels_WhenPassedValueMatchesUserName()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string username = "testUser";
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetPlacesByNameOrUsername(username);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnCollectionWithFounModels_WhenPassedValueMatchesName()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string username = "testUser";
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            var name = "testName";
            organization.Name = name;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetPlacesByNameOrUsername(name);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnCollectionWithFoundModelsOnlyOnce_WhenPassedValueMatchesBothParams()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string username = "testUser";
            user.UserName = username;
            string name = "testUser";
            var organization = new Organization();
            organization.User = user;
            organization.Name = name;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetPlacesByNameOrUsername(username);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnEmptyCollection_WhenPassedValueDoesntMatch()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var user = new User();
            string username = "testUser";
            user.UserName = username;
            string name = "testUser";
            var organization = new Organization();
            organization.User = user;
            organization.Name = name;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetPlacesByNameOrUsername("otherTest");

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
