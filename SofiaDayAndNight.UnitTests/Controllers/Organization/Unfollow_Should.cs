using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Organization
{
    [TestFixture]
   public class Unfollow_Should
    {
        [Test]
        public void CallUnollow_WhenCalled()
        {
            // Arrange
            var username = "testUser";
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.Unfollow(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedMapper = new Mock<IMapper>();

            var request = new Mock<HttpRequestBase>();
            //request.SetupGet(x => x.Headers).Returns(
            //    new WebHeaderCollection() {
            //        {"X-Requested-With", "XMLHttpRequest"}
            //    }
            //);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            httpContext.SetupGet(x => x.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var controller = new OrganizationController(
              mockedOrganizationService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Unfollow(Guid.NewGuid());

            // Assert
            mockedOrganizationService.Verify(x => x.Unfollow(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallMapMethod_WhenIsAjax()
        {
            // Arrange
            var username = "testUser";
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new SofiaDayAndNight.Data.Models.Organization());
            mockedOrganizationService.Setup(x => x.Unfollow(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(It.IsAny<SofiaDayAndNight.Data.Models.Organization>())).Returns(new OrganizationViewModel());

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new WebHeaderCollection() {
                    {"X-Requested-With", "XMLHttpRequest"}
                }
            );
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            httpContext.SetupGet(x => x.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var controller = new OrganizationController(
              mockedOrganizationService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.Unfollow(Guid.NewGuid());

            // Assert
            mockedMapper.Verify(x =>
            x.Map<OrganizationViewModel>(It.IsAny<SofiaDayAndNight.Data.Models.Organization>()), Times.Once);
        }

        [Test]
        public void ReturnPartialViewwithCorrectModel_WhenIsAjax()
        {
            // Arrange
            var username = "testUser";
            var id = Guid.NewGuid();
            var viewModel = new OrganizationViewModel();
            viewModel.Id = id;
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new SofiaDayAndNight.Data.Models.Organization());
            mockedOrganizationService.Setup(x => x.Unfollow(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<OrganizationViewModel>(It.IsAny<SofiaDayAndNight.Data.Models.Organization>())).Returns(viewModel);

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new WebHeaderCollection() {
                    {"X-Requested-With", "XMLHttpRequest"}
                }
            );
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            httpContext.SetupGet(x => x.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var controller = new OrganizationController(
              mockedOrganizationService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.Unfollow(Guid.NewGuid()))
             .ShouldRenderPartialView("_OrganizationInfoPartial")
             .WithModel<OrganizationViewModel>(model =>
             {
                 Assert.AreEqual(viewModel.Id, model.Id);
             });
        }

        [Test]
        public void RedirectToCorrectAction_WhenIsNotAjax()
        {
            // Arrange
            var username = "testUser";
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService.Setup(x => x.Unfollow(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedMapper = new Mock<IMapper>();

            var request = new Mock<HttpRequestBase>();
            //request.SetupGet(x => x.Headers).Returns(
            //    new WebHeaderCollection() {
            //        {"X-Requested-With", "XMLHttpRequest"}
            //    }
            //);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var principal = new Mock<IPrincipal>();
            var controllerContext = new Mock<ControllerContext>();
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            httpContext.SetupGet(x => x.User).Returns(principal.Object);
            controllerContext.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var controller = new OrganizationController(
              mockedOrganizationService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.Unfollow(Guid.NewGuid()))
             .ShouldRedirectTo<IndividualController>(typeof(IndividualController).GetMethod("ProfileDetails"))
             .WithRouteValue("username");
        }
    }
}
