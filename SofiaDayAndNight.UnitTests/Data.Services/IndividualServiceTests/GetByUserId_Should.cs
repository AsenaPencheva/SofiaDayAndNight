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
    class GetByUserId_Should
    {
        [Test]
        public void NotThrowException_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetByUser(null));
        }

        [Test]
        public void ReturnNull_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var user = new User();
            string userId = "testUserId";
            user.Id = userId;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUser(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnModel_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            string userId = "testUserId";
            var user = new User();
            user.Id = userId;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUser(userId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnNull_WhenThereIsNotAModelWithThePassedUser()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            string userId = "testUserId";
            var user = new User();
            user.Id = userId;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUser("otherTestUserId");

            // Assert
            Assert.IsNull(result);
        }
    }
}
