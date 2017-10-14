using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void CallWrapperUpdateMethod_WhanPassedVauleIsCorrect()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object,mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Update(new Organization());

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Once);
        }

        [Test]
        public void NotCallWrapperUpdateMethod_WhanPassedVauleIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Update(null);

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Never);
        }
    }
}
