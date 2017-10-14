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
    public class GetIndividualsByNameOrUsername_Should
    {
        [Test]
        public void ReturnEmptyCollection_WhenPassedValueIsNull()
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
            var result = service.GetIndividualsByNameOrUsername(null);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCollectionWithFounModels_WhenPassedValueMatchesUserName()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var user = new User();
            string username = "testUser";
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetIndividualsByNameOrUsername(username);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnCollectionWithFounModels_WhenPassedValueMatchesName()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var user = new User();
            string firstname = "testUser";
            var individual = new Individual();
            individual.User = user;
            individual.FirstName = firstname;
            individual.LastName = "LastName";
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetIndividualsByNameOrUsername(firstname);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnCollectionWithFoundModelsOnlyOnce_WhenPassedValueMatchesBothParams()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var user = new User();
            string username = "testUser";
            user.UserName = username;
            string firstname = "testUser";
            var individual = new Individual();
            individual.User = user;
            individual.FirstName = firstname;
            individual.LastName = "LastName";
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetIndividualsByNameOrUsername(username);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnEmptyCollection_WhenPassedValueDoesntMatch()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var user = new User();
            string firstname = "testUser";
            var individual = new Individual();
            individual.User = user;
            individual.FirstName = firstname;
            individual.LastName = "LastName";
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetIndividualsByNameOrUsername("otherTestName");

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
