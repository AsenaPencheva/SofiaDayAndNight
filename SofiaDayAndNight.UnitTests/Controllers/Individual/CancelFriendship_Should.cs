using AutoMapper;
using Moq;
using NUnit.Framework;
using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace SofiaDayAndNight.UnitTests.Controllers.Individual
{
    [TestFixture]
    public class CancelFriendship_Should
    {
        [Test]
        public void CallSendRequest_WhenCalled()
        {
            // Arrange
            var username = "testUser";
            var mockedIndividualService = new Mock<IIndividualService>();
            //mockedIndividualService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.RemoveFriendship(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
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

            var controller = new IndividualController(
              mockedIndividualService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.CancelFriendship(Guid.NewGuid());

            // Assert
            mockedIndividualService.Verify(x => x.RemoveFriendship(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallMapMethod_WhenIsAjax()
        {
            // Arrange
            var username = "testUser";
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.RemoveFriendship(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(It.IsAny<SofiaDayAndNight.Data.Models.Individual>())).Returns(new IndividualViewModel());

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

            var controller = new IndividualController(
              mockedIndividualService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            // Act
            controller.CancelFriendship(Guid.NewGuid());

            // Assert
            mockedMapper.Verify(x =>
            x.Map<IndividualViewModel>(It.IsAny<SofiaDayAndNight.Data.Models.Individual>()), Times.Once);
        }

        [Test]
        public void ReturnPartialViewwithCorrectModel_WhenIsAjax()
        {
            // Arrange
            var username = "testUser";
            var id = Guid.NewGuid();
            var viewModel = new IndividualViewModel();
            viewModel.Id = id;
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.RemoveFriendship(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<IndividualViewModel>(It.IsAny<SofiaDayAndNight.Data.Models.Individual>())).Returns(viewModel);

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

            var controller = new IndividualController(
              mockedIndividualService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.CancelFriendship(Guid.NewGuid()))
             .ShouldRenderPartialView("_IndividualInfoPartial")
             .WithModel<IndividualViewModel>(model =>
             {
                 Assert.AreEqual(viewModel.Id, model.Id);
             });
        }

        [Test]
        public void RedirectToCorrectAction_WhenIsNotAjax()
        {
            // Arrange
            var username = "testUser";
            var mockedIndividualService = new Mock<IIndividualService>();
            //mockedIndividualService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new SofiaDayAndNight.Data.Models.Individual());
            mockedIndividualService.Setup(x => x.RemoveFriendship(It.IsAny<string>(), It.IsAny<Guid>())).Verifiable();
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

            var controller = new IndividualController(
              mockedIndividualService.Object,
              mockedMapper.Object,
              mockedPhotoHelper.Object,
              mockedUserProvider.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller
             .WithCallTo(x => x.CancelFriendship(Guid.NewGuid()))
             .ShouldRedirectTo(typeof(IndividualController).GetMethod("ProfileDetails"))
             .WithRouteValue("username");
        }
    }
}


