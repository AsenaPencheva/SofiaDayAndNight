using System.Data.Entity;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data;
using SofiaDayAndNight.Data.Contracts;
using System;
using SofiaDayAndNight.Data.Services;
using SofiaDayAndNight.Data.Models;

namespace SofiaDayAndNight.UnitTests.Data.Services.IndividualServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowNewArgumentNullException_WhenetWrapperIsNull()
        {
            // Arange
            var mockedDbContext = new Mock<IUnitOfWork>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new IndividualService(null, mockedDbContext.Object));
        }

        [Test]
        public void ThrowNewArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();

            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() => new IndividualService( mockedEfWrappert.Object,null));
        }

        [Test]
        public void NotThrowException_When_PassedValuesAreCorrect()
        {
            // Arange
            var mockedEfWrappert = new Mock<IEfDbSetWrapper<Individual>>();
            var mockedDbContext = new Mock<IUnitOfWork>();

            // Act & Assert 
            Assert.DoesNotThrow(() => new IndividualService(mockedEfWrappert.Object, mockedDbContext.Object));
        }
    }
}
