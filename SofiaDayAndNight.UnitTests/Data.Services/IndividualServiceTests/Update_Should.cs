using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
   public class Update_Should
    {
        [Test]
        public void CallWrapperUpdateMethod_WhanPassedVauleIsCorrect()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Update(new Individual());

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Once);
        }

        [Test]
        public void NotCallWrapperUpdateMethod_WhanPassedVauleIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Update(null);

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Never);
        }
    }
}
