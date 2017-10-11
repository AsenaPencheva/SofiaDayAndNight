using System.Data.Entity;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data;
using SofiaDayAndNight.Data.Contracts;
using System;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Models;
using System.Linq;
using System.Collections.Generic;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class GetByUsername_Should 
    {
        [Test]
        public void NotThrowException_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetByUsername(null));
        }

        [Test]
        public void ReturnNull_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var user = new User();
            string username = "testUsername";
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUsername(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnModel_WhenThereIsAModelWithThePassedUsername()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            string username = "testUsername";
            var user = new User();
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUsername(username);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReturnNull_WhenThereIsNotAModelWithThePassedUser()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            string username = "testUsername";
            var user = new User();
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetByUsername("otherTestUser");

            // Assert
            Assert.IsNull(result);
        }
    }
}
