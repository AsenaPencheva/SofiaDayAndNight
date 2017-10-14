using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Common;
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
    public class GetUpcomingEvents_Should
    {
        [Test]
        public void ReturnEmptyCollection_WhenUsernameIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetUpcomingEvents(null);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCorrectCollection_WhenUsernameMatch()
        {
            // Arrange
            var data = new List<Individual>();
            var currentDate = new DateTime(2017, 1, 1, 00, 00, 00);
            var event1 = new Event();
            event1.Begins = currentDate.AddDays(1);
            var event2 = new Event();
            event2.Begins = currentDate.AddDays(-1);
            var user = new User();
            var username = "testUser";
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            individual.Events.Add(event1);
            individual.Events.Add(event2);
            data.Add(individual);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedDateTime = new Mock<DateTimeProvider>();
            mockedDateTime.SetupGet(x => x.UtcNow).Returns(currentDate);
            DateTimeProvider.Current = mockedDateTime.Object;

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetUpcomingEvents(username);
            DateTimeProvider.ResetToDefault();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreSame(event1, result.First());
        }

        [Test]
        public void ReturnEmptyCollection_WhenUsernameDoesntMatch()
        {
            // Arrange
            var data = new List<Individual>();
            var currentDate = new DateTime(2017, 1, 1, 00, 00, 00);
            var event1 = new Event();
            event1.Begins = currentDate.AddDays(1);
            var event2 = new Event();
            event2.Begins = currentDate.AddDays(-1);
            var user = new User();
            var username = "testUser";
            user.UserName = username;
            var individual = new Individual();
            individual.User = user;
            individual.Events.Add(event1);
            individual.Events.Add(event2);
            data.Add(individual);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedDateTime = new Mock<DateTimeProvider>();
            mockedDateTime.SetupGet(x => x.UtcNow).Returns(currentDate);
            DateTimeProvider.Current = mockedDateTime.Object;

            var service = new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetUpcomingEvents("anotherUser");
            DateTimeProvider.ResetToDefault();

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
