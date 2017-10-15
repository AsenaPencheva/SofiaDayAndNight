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
    public class GetAll_Should
    {
        [TestCase]
        public void ReturnCollectionWithAllModels_WhenCalled()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var eventModel1 = new Event();
            var title = "first title";
            eventModel1.Title = title;
            var eventModel2 = new Event();
            var title2 = "secont title";
            eventModel2.Title = title2;
            var data = new List<Event>();
            data.Add(eventModel1);
            data.Add(eventModel2);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new EventService(
                mockedEfWrappert.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
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
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            mockedEfWrappert.SetupGet(x => x.All).Verifiable();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var eventModel1 = new Event();
            var title = "first title";
            eventModel1.Title = title;
            var eventModel2 = new Event();
            var title2 = "secont title";
            eventModel2.Title = title2;
            var data = new List<Event>();
            data.Add(eventModel1);
            data.Add(eventModel2);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new EventService(
                mockedEfWrappert.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
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
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = new List<Event>();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new EventService(
                mockedEfWrappert.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
