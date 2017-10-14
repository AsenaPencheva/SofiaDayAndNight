using System;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void NotThrowException_WhenIdIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object, 
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetById(null));
        }

        [Test]
        public void ReturnNull_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            Guid? organizationId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(organizationId.Value))
                .Returns(new Organization() { Id = organizationId.Value });

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(null);

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
            Guid? organizationId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(organizationId.Value)).Returns(new Organization() { Id = organizationId.Value });

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(organizationId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnNull_WhenThereIsNotAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            Guid? organizationId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(organizationId.Value)).Returns((Organization)null);

            var service = new OrganizationService(mockedIndividualService.Object,
                mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(organizationId);

            // Assert
            Assert.IsNull(result);
        }
    }
}

