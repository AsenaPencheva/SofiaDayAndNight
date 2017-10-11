using System.Data.Entity;

using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data;

namespace SofiaDayAndNight.UnitTests.Data
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void Commit_ShoulCall_SaveChanges()
        {
            // Arange
            var mockedDbContext = new Mock<DbContext>();
            mockedDbContext.Setup(x => x.SaveChanges()).Verifiable();
            var unitOfWork = new UnitOfWork(mockedDbContext.Object);

            // Act
            unitOfWork.Commit();

            // Assert
            mockedDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
