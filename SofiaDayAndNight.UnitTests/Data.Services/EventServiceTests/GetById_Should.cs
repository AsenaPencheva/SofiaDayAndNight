using System;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.EventServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void NotThrowException_WhenIdIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var service = new EventService(
               mockedEfWrappert.Object, mockedIndividualService.Object,
                mockedOrganizationService.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetById(null));
        }

        [Test]
        public void ReturnNull_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            Guid? eventId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(eventId.Value)).Returns(new Event() { Id = eventId.Value });

            var service = new EventService(
               mockedEfWrappert.Object, mockedIndividualService.Object,
                mockedOrganizationService.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnModel_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            Guid? eventId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(eventId.Value)).Returns(new Event() { Id = eventId.Value });

            var service = new EventService(
               mockedEfWrappert.Object, mockedIndividualService.Object,
                mockedOrganizationService.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(eventId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnNull_WhenThereIsNotAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            Guid? eventId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(eventId.Value)).Returns((Event)null);

            var service = new EventService(
               mockedEfWrappert.Object, mockedIndividualService.Object,
                mockedOrganizationService.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(eventId);

            // Assert
            Assert.IsNull(result);
        }
    }
}