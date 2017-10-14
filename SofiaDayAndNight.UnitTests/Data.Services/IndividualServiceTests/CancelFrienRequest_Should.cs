using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class CancelFrienRequest_Should
    {
        [Test]
        public void NotCallGetByIdMethod_WhenPassedUserIdIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CancelFriendRequest(null, Guid.NewGuid());

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
            service.CancelFriendRequest("testUserId", null);

            // Assert
            mockedEfWrappert.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenParamsDoesntMatch()
        {
            // Arrange
            var data = new List<Individual>();
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Individual)null);
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CancelFriendRequest("testUserId", Guid.NewGuid());

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Never);
        }

        [Test]
        public void CallUpdateMethod_WhenParamsMatch()
        {
            // Arrange
            var userId = "testUserId";
            var user = new User();
            user.Id = userId;
            var individual = new Individual();
            individual.User = user;

            var id = Guid.NewGuid();
            var friend = new Individual();
            friend.Id = id;

            var data = new List<Individual>();
            data.Add(individual);
            data.Add(friend);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(friend);
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CancelFriendRequest(userId, id);

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Exactly(2));
        }
    }
}
