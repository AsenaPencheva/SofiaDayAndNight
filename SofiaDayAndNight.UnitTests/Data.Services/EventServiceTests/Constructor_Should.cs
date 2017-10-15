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
    public class Constructor_Should
    {
        [Test]
        public void ThrowNewArgumentNullException_WhenetWrapperIsNull()
        {
            // Arange
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new EventService(
                null,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedDbContext.Object));
        }

        [Test]
        public void ThrowNewArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new EventService(
                mockedEfWrappert.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                null));
        }

        [Test]
        public void ThrowNewArgumentNullException_WhenIndividualServiceIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedOrganizationService = new Mock<IOrganizationService>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new EventService(
               mockedEfWrappert.Object,
               null,
               mockedOrganizationService.Object,
               mockedDbContext.Object));
        }

        [Test]
        public void ThrowNewArgumentNullException_WhenOrganizationServiceIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new EventService(
               mockedEfWrappert.Object,
               mockedIndividualService.Object,
               null,
               mockedDbContext.Object));
        }

        [Test]
        public void NotThrowException_When_PassedValuesAreCorrect()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();

            // Act & Assert 
            Assert.DoesNotThrow(() => new EventService(
               mockedEfWrappert.Object,
               mockedIndividualService.Object,
               mockedOrganizationService.Object,
               mockedDbContext.Object));
        }
    }
}
