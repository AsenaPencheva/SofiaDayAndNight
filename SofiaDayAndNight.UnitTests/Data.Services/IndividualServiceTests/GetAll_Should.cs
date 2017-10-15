using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [TestCase]
        public void ReturnCollectionWithAllModels_WhenCalled()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var individual1 = new Individual();
            var firstName1 = "first name";
            individual1.FirstName = firstName1;
            var individual2 = new Individual();
            var firstName2 = "secont name";
            individual2.FirstName = firstName2;
            var data = new List<Individual>();
            data.Add(individual1);
            data.Add(individual2);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestCase]
        public void CallWrapperAll_WhenCalled()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.SetupGet(x => x.All).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var individual1 = new Individual();
            var firstName1 = "first name";
            individual1.FirstName = firstName1;
            var individual2 = new Individual();
            var firstName2 = "secont name";
            individual2.FirstName = firstName2;
            var data = new List<Individual>();
            data.Add(individual1);
            data.Add(individual2);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            mockedEfWrappert.Verify(x => x.All, Times.Once);
        }

        [TestCase]
        public void ReturnEmptyCollection_WhenNoEntities()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = new List<Individual>();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
