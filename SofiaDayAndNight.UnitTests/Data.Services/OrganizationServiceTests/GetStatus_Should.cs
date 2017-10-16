using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class GetStatus_Should
    {
        private static readonly string userId = "testUserId";
        private static readonly Guid organizationId = Guid.NewGuid();
        private static readonly Guid userToCheckId = Guid.NewGuid();

        private static IEnumerable<Organization> Data()
        {
            var user = new User();
            user.Id = userId;
            var organization = new Organization();
            organization.User = user;
            organization.Id = organizationId;
            var data = new List<Organization>();
            data.Add(organization);

            return data;
        }

        [Test]
        public void NotThrowException_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object,mockedEfWrappert.Object, mockedDbContext.Object);

            // Act & Assert 
            Assert.DoesNotThrow(() => service.GetStatus(null, userToCheckId));
        }

        [Test]
        public void ReturnStatusNone_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(null, userToCheckId);

            // Assert 
            Assert.AreEqual(OrganizationStatus.None, result);
        }

        [Test]
        public void ReturnStatusCurrent_WhenUserToCheckIsCurrent()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var user = new User();
            user.UserName = userId;
            var organization = new Organization();
            organization.User = user;
            organization.Id = organizationId;
            var data = new List<Organization>();
            data.Add(organization);

            mockedEfWrappert.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(data.First());

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(userId, organizationId);

            // Assert 
            Assert.AreEqual(OrganizationStatus.isCurrent, result);
        }

        [Test]
        public void ReturnStatusNone_WhenUserToCheckHasNoConnectionsWithCurrent()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            var user = new User();
            user.Id = userId;
            var organization = new Organization();
            organization.User = user;
            organization.Id = organizationId;

            mockedEfWrappert.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(organization);

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus("followerId", organizationId);

            // Assert 
            Assert.AreEqual(OrganizationStatus.None, result);
        }

        [Test]
        public void ReturnStatusIsFollowed_WhenUserToCheckHasFollowedCurrent()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            var user = new User();
            user.UserName = userId;
            var organization = new Organization();
            organization.User = user;
            organization.Id = organizationId;
            var follower = new Individual();
            var followerId = "followerId";
            follower.User = new User();
            follower.User.UserName = followerId;
            organization.Followers.Add(follower);

            mockedEfWrappert.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(organization);

            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act 
            var result = service.GetStatus(followerId, organizationId);

            // Assert 
            Assert.AreEqual(OrganizationStatus.IsFollowed, result);
        }
    }
}
