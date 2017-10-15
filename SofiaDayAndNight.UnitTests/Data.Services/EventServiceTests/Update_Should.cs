using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.EventServiceTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void CallWrapperUpdateMethod_WhanPassedVauleIsCorrect()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Event>())).Verifiable();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new EventService(
                mockedEfWrappert.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedDbContext.Object);

            // Act
            service.Update(new Event());

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Event>()), Times.Once);
        }

        [Test]
        public void NotCallWrapperUpdateMethod_WhanPassedVauleIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Event>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Event>())).Verifiable();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new EventService(
                           mockedEfWrappert.Object,
                           mockedIndividualService.Object,
                           mockedOrganizationService.Object,
                           mockedDbContext.Object);
            // Act
            service.Update(null);

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Event>()), Times.Never);
        }
    }
}
