using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.EfDbSetWrappers;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Data.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SofiaDayAndNight.UnitTests.Data
{
    [TestFixture]
    public class EfDbSetWrapperTests
    {
        [Test]
        public void Constructor_Should_ThrowArgumentNullException_WithProperMessage_WhenDbContextIsNull()
        {
            // Arange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new EfDbSetWrapper<BaseModel>(null));
        }

        [Test]
        public void Constructor_Should_NotThrowArgumentNullException_WhenDbContextIsNotNull()
        {
            // Arange
            var mockedDbContext = new Mock<DbContext>();

            // Act & Assert
            Assert.DoesNotThrow(() => new EfDbSetWrapper<BaseModel>(mockedDbContext.Object));
        }

        //[Test]
        //public void Add_Should_AddEntity()
        //{
        //    // Arange
        //    var content = "TestContent";
        //    var data = new List<Comment> { }.AsQueryable();
        //    var comment = new Comment { Content = content };

        //    var mockedDbSet = new Mock<DbSet<Comment>>();
        //    mockedDbSet.Setup(x => x.Add(comment)).Verifiable();

        //    var mockedDbContext = new Mock<DbContext>();
        //    mockedDbContext.Setup(x => x.Set<Comment>()).Returns(mockedDbSet.Object);

        //    // Act
        //    var wrapper = new EfDbSetWrapper<Comment>(mockedDbContext.Object);
        //    wrapper.Add(comment);

        //    // Assert
        //    mockedDbSet.Verify(x => x.Add(comment), Times.Once);
        //}

        [Test]
        public void All_Should_ReturnCorrectSet()
        {
            // Arrange
            var content = "TestContent";
            var data = new List<Comment>
            {
                new Comment { Content = content}
            }.AsQueryable();
           

            var mockedDbSet = new Mock<DbSet<Comment>>();
            mockedDbSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockedDbContext = new Mock<DbContext>();
            mockedDbContext.Setup(x => x.Set<Comment>()).Returns(mockedDbSet.Object);

            // Act
            var wrapper = new EfDbSetWrapper<Comment>(mockedDbContext.Object);
            var result = wrapper.All;

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(content, result.First().Content);
        }
    }
}
