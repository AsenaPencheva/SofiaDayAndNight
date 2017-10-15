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
    public class GetAll_Should
    {
        [TestCase]
        public void ReturnCollectionWithAllModels_WhenCalled()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var organization1 = new Organization();
            var name1 = "first name";
            organization1.Name = name1;
            var organization2 = new Organization();
            var name2 = "second name";
            organization2.Name = name2;
            var data = new List<Organization>();
            data.Add(organization1);
            data.Add(organization2);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(
                mockedIndividualService.Object,
                mockedEfWrappert.Object,
                mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestCase]
        public void CallWrapperAll_WhenCalled()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.SetupGet(x => x.All).Verifiable();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var organization1 = new Organization();
            var name1 = "first name";
            organization1.Name = name1;
            var organization2 = new Organization();
            var name2 = "second name";
            organization2.Name = name2;
            var data = new List<Organization>();
            data.Add(organization1);
            data.Add(organization2);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(
                mockedIndividualService.Object,
                mockedEfWrappert.Object,
                mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfWrappert.Verify(x => x.All, Times.Once);
        }

        [TestCase]
        public void ReturnEmptyCollection_WhenNoEntities()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = new List<Organization>();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new OrganizationService(
                mockedIndividualService.Object,
                mockedEfWrappert.Object,
                mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
