using System;

using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class AttendEvent_Should
    {
        [Test]
        public void NotCallGetByIdMethod_WhenPassedUsernameIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.AttendEvent(null, new Event());

            // Assert
            mockedEfWrappert.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public void NotCallGetByIdMethod_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.AttendEvent(Guid.NewGuid(),null);

            // Assert
            mockedEfWrappert.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenUsernameDoesntMatch()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Individual)null);
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.AttendEvent(Guid.NewGuid(), new Event());

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Never);
        }

        [Test]
        public void CallUpdateMethod_WhenValidParametersArePassed()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Individual());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.AttendEvent(Guid.NewGuid(), new Event());

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Once);
        }
    }
}
