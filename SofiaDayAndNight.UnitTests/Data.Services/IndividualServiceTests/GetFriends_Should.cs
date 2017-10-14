﻿using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class GetFriends_Should
    {
        private static readonly string username = "testUsername";
        private static IEnumerable<Individual> Data()
        {
            var user = new User();
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            var friend = new Individual();
            individual.Friends.Add(friend);
            var data = new List<Individual>();
            data.Add(individual);

            return data;
        }
        [Test]
        public void NotThrowException_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetFriendsRequests(null));
        }

        [Test]
        public void ReturnEmptyCollection_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFriends(null);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnCollection_WhenThereIsAModelWithThePassedId()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFriends(username);

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ReturnEmptyCollection_WhenThereIsNotAModelWithThePassedUser()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetFriends("otherTestUser");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
