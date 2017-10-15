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
    public class Constructor_Should
    {
        [Test]
        public void ThrowNewArgumentNullException_WhenetWrapperIsNull()
        {
            // Arange
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() =>
            new OrganizationService(mockedIndividualService.Object, null, mockedDbContext.Object));
        }

        [Test]
        public void ThrowNewArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedIndividualService = new Mock<IIndividualService>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() =>
            new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, null));
        }

        [Test]
        public void ThrowNewArgumentNullException_WhenIndividualServiceIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();


            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() =>
            new OrganizationService(null, mockedEfWrappert.Object, mockedDbContext.Object));
        }

        [Test]
        public void NotThrowException_When_PassedValuesAreCorrect()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Organization>>();
            var mockedDbContext = new Mock<IUnitOfWork>();
            var mockedIndividualService = new Mock<IIndividualService>();


            // Act & Assert 
            Assert.DoesNotThrow(()=>
            new OrganizationService(mockedIndividualService.Object, mockedEfWrappert.Object, mockedDbContext.Object));
        }
    }
}
