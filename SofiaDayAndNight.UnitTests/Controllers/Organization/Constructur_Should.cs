using System;

using AutoMapper;
using Moq;
using NUnit.Framework;

using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Organization
{
    [TestFixture]
    public class Constructur_Should
    {
        [Test]
        public void ReturnsAnInstance_WhenParameterIsNotNull()
        {
            // Arrange
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            // Act
            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object
                );

            // Assert
            Assert.IsNotNull(controller);
        }

        [Test]
        public void ThrowException_WhenParametersAreNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new OrganizationController(null, null, null, null));
        }
    }
}
