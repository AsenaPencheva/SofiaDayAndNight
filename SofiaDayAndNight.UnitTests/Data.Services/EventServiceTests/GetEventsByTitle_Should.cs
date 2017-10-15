using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.EventServiceTests
{
    [TestFixture]
    public class GetEventsByTitle_Should
    {
        [Test]
        public void ReturnEmptyCollection_WhenPassedValueIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var eventModel = new Event();
            var title = "test title";
            eventModel.Title = title;
            var data = new List<Event>();
            data.Add(eventModel);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new EventService(
                mockedEfWrappert.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedDbContext.Object);

            // Act
            var result = service.GetEventsByName(null);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCollectionWithFoundModels_WhenPassedValueMatches()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var eventModel = new Event();
            var title = "test title";
            eventModel.Title = title;
            var data = new List<Event>();
            data.Add(eventModel);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new EventService(
                 mockedEfWrappert.Object,
                 mockedIndividualService.Object,
                 mockedOrganizationService.Object,
                 mockedDbContext.Object);

            // Act
            var result = service.GetEventsByName(title);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnEmptyCollection_WhenPassedValueDoesntMatch()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var eventModel = new Event();
            var title = "test title";
            eventModel.Title = title;
            var data = new List<Event>();
            data.Add(eventModel);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new EventService(
                 mockedEfWrappert.Object,
                 mockedIndividualService.Object,
                 mockedOrganizationService.Object,
                 mockedDbContext.Object);

            // Act
            var result = service.GetEventsByName("other title");

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
