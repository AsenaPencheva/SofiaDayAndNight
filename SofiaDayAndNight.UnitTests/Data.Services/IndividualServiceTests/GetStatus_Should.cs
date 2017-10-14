using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class GetStatus_Should
    {
        private static readonly string username = "testUser";
        private static readonly Guid individualId = Guid.NewGuid();
        private static readonly Guid userToCheckId = Guid.NewGuid();

        private static IEnumerable<Individual> Data()
        {
            var user = new User();
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            individual.Id = individualId;
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
            Assert.DoesNotThrow(() => service.GetStatus(null, userToCheckId));
        }

        [Test]
        public void ReturnStatusNone_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(null, userToCheckId);

            // Assert 
            Assert.AreEqual(IndividualStatus.None, result);
        }

        [Test]
        public void ReturnStatusCurrent_WhenUserToCheckIsCurrent()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = Data();
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(username, individualId);

            // Assert 
            Assert.AreEqual(IndividualStatus.IsCurrent, result);
        }

        [Test]
        public void ReturnStatusIsFriend_WhenUserToCheckIsFriend()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = Data();
            var friend = new Individual();
            friend.Id = userToCheckId;
            data.First().Friends.Add(friend);
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(username, userToCheckId);
            data.First().Friends.Remove(friend);

            // Assert 
            Assert.AreEqual(IndividualStatus.IsFriend, result);
        }

        [Test]
        public void ReturnStatusIsRequested_WhenUserToCheckIsRequested()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = Data();
            var friend = new Individual();
            friend.Id = userToCheckId;
            data.First().FriendRequested.Add(friend);
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(username, userToCheckId);
            data.First().FriendRequested.Remove(friend);

            // Assert 
            Assert.AreEqual(IndividualStatus.IsRequested, result);
        }

        [Test]
        public void ReturnStatusHasRequest_WhenUserToCheckHasRequestedCurrent()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = Data();
            var friend = new Individual();
            friend.Id = userToCheckId;
            data.First().FriendRequests.Add(friend);
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(username, userToCheckId);
            data.First().FriendRequests.Remove(friend);

            // Assert 
            Assert.AreEqual(IndividualStatus.HasRequest, result);
        }

        [Test]
        public void ReturnStatusNone_WhenUserToCheckHasNoConnectionsWithCurrent()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var data = Data();

            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(username, userToCheckId);

            // Assert 
            Assert.AreEqual(IndividualStatus.None, result);
        }
    }
}
