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
    public class CreateEvent_Should
    {
        [Test]
        public void NotCallAllMethod_WhenPassedUserIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.All).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(new Event(), null);

            // Assert
            mockedEfWrappert.Verify(x => x.All, Times.Never);
        }

        [Test]
        public void NotCallAllMethod_WhenPassedEventIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.All).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(null, "testUserId");

            // Assert
            mockedEfWrappert.Verify(x => x.All, Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenParamsDoesntMatch()
        {
            // Arrange
            var data = new List<Individual>();

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(new Event(), "testUserId");

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Never);
        }

        [Test]
        public void CallUpdateMethod_WhenParamsMatch()
        {
            // Arrange
            var user = new User();
            var username = "testUser";
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            var data = new List<Individual>();
            data.Add(individual);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.SetupGet(m => m.All).Returns(data.AsQueryable());
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Individual>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.CreateEvent(new Event(), username);

            // Assert
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Individual>()), Times.Once);
        }
    }
}
