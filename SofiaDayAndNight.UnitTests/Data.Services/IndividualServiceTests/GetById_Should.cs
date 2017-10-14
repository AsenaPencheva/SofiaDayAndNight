using System;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void NotThrowException_WhenIdIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetById(null));
        }

        [Test]
        public void ReturnNull_WhenPassedIdIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            Guid? individualId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(individualId.Value)).Returns(new Individual() { Id = individualId.Value });

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnModel_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            Guid? individualId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(individualId.Value)).Returns(new Individual() { Id = individualId.Value });

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(individualId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnNull_WhenThereIsNotAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            Guid? individualId = Guid.NewGuid();

            mockedEfWrappert.Setup(m => m.GetById(individualId.Value)).Returns((Individual)null);

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetById(individualId);

            // Assert
            Assert.IsNull(result);
        }
    }
}
