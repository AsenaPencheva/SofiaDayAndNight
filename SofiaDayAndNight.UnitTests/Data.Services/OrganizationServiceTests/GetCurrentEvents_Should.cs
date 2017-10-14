using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Common;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class GetCurrentEvents_Should
    {
        [Test]
        public void ReturnEmptyCollection_WhenUsernameIsNull()
        {
            // Arrange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetCurrentEvents(null);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCorrectCollection_WhenUsernameMatch()
        {
            // Arrange
            var data = new List<Organization>();
            var currentDate = new DateTime(2017, 1, 1, 00, 00, 00);
            var event1 = new Event();
            event1.Begins = currentDate.AddDays(1);
            event1.Ends = currentDate.AddDays(2);
            var event2 = new Event();
            event2.Begins = currentDate.AddDays(-1);
            event2.Ends = currentDate.AddDays(1);
            var user = new User();
            var username = "testUser";
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            organization.Events.Add(event1);
            organization.Events.Add(event2);
            data.Add(organization);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedDateTime = new Mock<DateTimeProvider>();
            mockedDateTime.SetupGet(x => x.UtcNow).Returns(currentDate);
            DateTimeProvider.Current = mockedDateTime.Object;

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            var result = service.GetCurrentEvents(username);
            DateTimeProvider.ResetToDefault();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreSame(event2, result.First());
        }

        [Test]
        public void ReturnEmptyCollection_WhenUsernameDoesntMatch()
        {
            // Arrange
            var data = new List<Organization>();
            var currentDate = new DateTime(2017, 1, 1, 00, 00, 00);
            var event1 = new Event();
            event1.Begins = currentDate.AddDays(1);
            var event2 = new Event();
            event2.Begins = currentDate.AddDays(-1);
            var user = new User();
            var username = "testUser";
            user.UserName = username;
            var organization = new Organization();
            organization.User = user;
            organization.Events.Add(event1);
            organization.Events.Add(event2);
            data.Add(organization);

            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.All).Returns(data.AsQueryable());
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedDateTime = new Mock<DateTimeProvider>();
            mockedDateTime.SetupGet(x => x.UtcNow).Returns(currentDate);
            DateTimeProvider.Current = mockedDateTime.Object;

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);
            DateTimeProvider.ResetToDefault();

            // Act
            var result = service.GetCurrentEvents("anotherUser");

            // Assert
            Assert.IsEmpty(result);
        }
    }
}

