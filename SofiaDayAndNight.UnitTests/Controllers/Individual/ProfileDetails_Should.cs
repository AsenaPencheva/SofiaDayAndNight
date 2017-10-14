using AutoMapper;
using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Common.Enums;
using System.Security.Principal;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web;

namespace SofiaDayAndNight.UnitTests.Controllers.Individual
{
    [TestFixture]
    public class ProfileDetails_Should
    {
        [Test]
        public void ReturnBadRequest_WhenUsernameIsNull()
        {
            // Arrange
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //var username = "testUsername";

            // Act & Assert
            controller
                .WithCallTo(x => x.ProfileDetails(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ReturnNotFoundRequest_WhenIdNotMatch()
        {
            var username = "testUsername";
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUsername(username)).Returns((SofiaDayAndNight.Data.Models.Individual)null);

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);


            // Act & Assert
            controller
                .WithCallTo(x => x.ProfileDetails(username))
                .ShouldGiveHttpStatus(404);
        }

        [Test]
        public void CallMapMethod_WhenUsernameMatch()
        {
            var username = "testUsername";
            var model = new IndividualViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUsername(username))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.GetStatus(It.IsAny<string>(), model.Id))
                .Returns(IndividualStatus.None);

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.ProfileDetails(username);

            // Assert
            mockedMapper.Verify(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()), Times.Once);
        }

        [Test]
        public void CallStatusMethod_WhenUsernameMatch()
        {
            var username = "testUsername";
            var model = new IndividualViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUsername(username))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.GetStatus(It.IsAny<string>(), model.Id))
                .Returns(IndividualStatus.None);

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.ProfileDetails(username);

            // Assert
            mockedIndividualService.Verify(x => x.GetStatus(It.IsAny<string>(), model.Id), Times.Once);
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenUsernameMatch()
        {
            //Arrange
            var username = "testUsername";
            var model = new IndividualViewModel();
            model.UserName = username;

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetByUsername(username))
                .Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.GetStatus(It.IsAny<string>(), model.Id))
                .Returns(IndividualStatus.None);

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Individual>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.ProfileDetails(username))
             .ShouldRenderDefaultView()
             .WithModel<IndividualViewModel>(m =>
             {
                 Assert.AreEqual(model.UserName, m.UserName);
                 Assert.AreEqual(model.Id, m.Id);
             });
        }
    }
}
