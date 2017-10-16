using System.Net;
using System.Security.Principal;
using System.Web.Mvc;

using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Organization
{
    [TestFixture]
    public class ProfileDetails_Should
    {
        [Test]
        public void ReturnBadRequest_WhenUsernameIsNull()
        {
            // Arrange
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object
                );

            // Act & Assert
            controller
                .WithCallTo(x => x.ProfileDetails(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ReturnNotFoundRequest_WhenIdNotMatch()
        {
            var username = "testUsername";
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetByUsername(username)).Returns((SofiaDayAndNight.Data.Models.Organization)null);

            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object
                );

            // Act & Assert
            controller
                .WithCallTo(x => x.ProfileDetails(username))
                .ShouldGiveHttpStatus(404);
        }

        [Test]
        public void CallMapMethod_WhenUsernameMatch()
        {
            var username = "testUsername";
            var model = new OrganizationViewModel();

            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetByUsername(username))
                .Returns(new SofiaDayAndNight.Data.Models.Organization());
            mockedOrganizationService.Setup(x => x.GetStatus(It.IsAny<string>(), model.Id))
                .Returns(OrganizationStatus.None);

            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object
                );
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.ProfileDetails(username);

            // Assert
            mockedMapper.Verify(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()), Times.Once);
        }

        [Test]
        public void CallStatusMethod_WhenUsernameMatch()
        {
            var username = "testUsername";
            var model = new OrganizationViewModel();

            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetByUsername(username))
                .Returns(new SofiaDayAndNight.Data.Models.Organization());
            mockedOrganizationService.Setup(x => x.GetStatus(It.IsAny<string>(), model.Id))
                .Returns(OrganizationStatus.None);

            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object
                );
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.ProfileDetails(username);

            // Assert
            mockedOrganizationService.Verify(x => x.GetStatus(It.IsAny<string>(), model.Id), Times.Once);
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenUsernameMatch()
        {
            //Arrange
            var username = "testUsername";
            var model = new OrganizationViewModel();
            model.UserName = username;

            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetByUsername(username))
                .Returns(new SofiaDayAndNight.Data.Models.Organization());
            mockedOrganizationService.Setup(x => x.GetStatus(It.IsAny<string>(), model.Id))
                .Returns(OrganizationStatus.None);
            
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Organization>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new OrganizationController(
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object
                );
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.ProfileDetails(username))
             .ShouldRenderDefaultView()
             .WithModel<OrganizationViewModel>(m =>
             {
                 Assert.AreEqual(model.UserName, m.UserName);
                 Assert.AreEqual(model.Id, m.Id);
             });
        }
    }
}
