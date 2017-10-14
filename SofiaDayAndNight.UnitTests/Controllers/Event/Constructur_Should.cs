using System;

using AutoMapper;
using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Event
{
    [TestFixture]
    public class Constructur_Should
    {
        [Test]
        public void ReturnsAnInstance_WhenParameterIsNotNull()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            // Act
            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);

            // Assert
            Assert.IsNotNull(eventController);
        }

        [Test]
        public void ThrowException_WhenParametersAreNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new EventController(null, null, null, null, null));
        }
    }
}
