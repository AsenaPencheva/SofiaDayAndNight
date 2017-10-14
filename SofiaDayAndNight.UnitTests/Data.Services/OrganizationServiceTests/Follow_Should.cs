using System;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Services.Contracts;

namespace SofiaDayAndNight.UnitTests.Data.Services.OrganizationServiceTests
{
    [TestFixture]
    public class Follow_Should
    {
        [Test]
        public void NotCallUpdateMethod_WhenUserIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Follow(null, Guid.NewGuid());

            // Assert 
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenIdIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Follow("userId", null);

            // Assert 
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenUserNotMatch()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Organization());
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns((Individual)null);
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Follow("userId", Guid.NewGuid());

            // Assert 
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Never);
        }

        [Test]
        public void NotCallUpdateMethod_WhenIdNotMatch()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Organization)null);
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(new Individual());
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Follow("userId", Guid.NewGuid());

            // Assert 
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Never);
        }

        [Test]
        public void CallUpdateMethod_WhenBothMatch()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            mockedEfWrappert.Setup(x => x.Update(It.IsAny<Organization>())).Verifiable();
            mockedEfWrappert.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Organization());
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUser(It.IsAny<string>())).Returns(new Individual());
            var service = new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object);

            // Act
            service.Follow("userId", Guid.NewGuid());

            // Assert 
            mockedEfWrappert.Verify(x => x.Update(It.IsAny<Organization>()), Times.Once);
        }
    }
}
