using System.Net;
using System.Security.Principal;
using System.Web;
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

namespace SofiaDayAndNight.UnitTests.Controllers.Event
{
    [TestFixture]
    public class CreatePost_Should
    {
        [Test]
        public void ReturnViewWithSameModelProperties_WhenModelStatusIsInvalid()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.AddModelError("test", "test");

            var model = new EventViewModel();
            model.Title = "1";
            var mockedUpload = new Mock<HttpPostedFileBase>();

            // Act & Assert
            eventController
                .WithCallTo(x => x.Create(model, mockedUpload.Object))
                .ShouldRenderDefaultView()
                .WithModel<EventViewModel>(m =>
                {
                    Assert.AreEqual(model.Title, m.Title);
                });
        }

        [Test]
        public void CallMapper_WhenModelStateIsValid()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService
                .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
                .Verifiable();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            var mockedUpload = new Mock<HttpPostedFileBase>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Individual.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act
            var result = eventController.Create(model, mockedUpload.Object);

            // Assert
            mockedMapper.Verify(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()), Times.Once);
        }

        [Test]
        public void CallCreateEvent_WhenUserIsInIndividualRole()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService
                .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
                .Verifiable();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            model.CreatorUserName = username;
            var mockedUpload = new Mock<HttpPostedFileBase>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Individual.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act
            var result = eventController.Create(model, mockedUpload.Object);

            // Assert
            mockedIndividualService.Verify(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), username), Times.Once);
        }

        [Test]
        public void CallCreateEvent_WhenUserIsInOrganizationRole()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService
               .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
               .Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            model.CreatorUserName = username;
            var mockedUpload = new Mock<HttpPostedFileBase>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Organization.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act
            var result = eventController.Create(model, mockedUpload.Object);

            // Assert
            mockedOrganizationService.Verify(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), username), Times.Once);
        }

        [Test]
        public void ReturnBadRequest_WhenUserIsNotInRole()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService
               .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
               .Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            model.CreatorUserName = username;
            var mockedUpload = new Mock<HttpPostedFileBase>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Organization.ToString())).Returns(false);
            principal.Setup(p => p.IsInRole(UserRole.Individual.ToString())).Returns(false);

            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act & Assert
            eventController
               .WithCallTo(x => x.Create(model, mockedUpload.Object))
               .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenPassedParamsAreCorrectInOrganizationRole()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            mockedOrganizationService
               .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
               .Verifiable();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            model.CreatorUserName = username;
            var mockedUpload = new Mock<HttpPostedFileBase>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Organization.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act & Assert
            eventController
               .WithCallTo(x => x.Create(model, mockedUpload.Object))
                .ShouldRedirectTo<OrganizationController>(typeof(OrganizationController).GetMethod("ProfileDetails"));
        }

        [Test]
        public void RedirectToTheCorrectAction_WhenPassedParamsAreCorrectInndividualRole()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService
                .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
                .Verifiable();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            model.CreatorUserName = username;
            var mockedUpload = new Mock<HttpPostedFileBase>();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Individual.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act & Assert
            eventController
               .WithCallTo(x => x.Create(model, mockedUpload.Object))
               .ShouldRedirectTo<IndividualController>(typeof(IndividualController).GetMethod("ProfileDetails"));
        }

        [Test]
        public void NotCallUploadImage_WhenModelUploadIsInValid()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService
                .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
                .Verifiable();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            var mockedUpload = new Mock<HttpPostedFileBase>();
            mockedPhotoHelper.Setup(x => x.UploadImage(mockedUpload.Object)).Verifiable();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Individual.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act
            var result = eventController.Create(model, null);

            // Assert
            mockedPhotoHelper.Verify(x => x.UploadImage(mockedUpload.Object), Times.Never);
        }

        [Test]
        public void CallUploadImage_WhenModelUploadIsValid()
        {
            // Arrange
            var mockedEventService = new Mock<IEventService>();
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService
                .Setup(x => x.CreateEvent(It.IsAny<SofiaDayAndNight.Data.Models.Event>(), It.IsAny<string>()))
                .Verifiable();
            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper
                .Setup(x => x.Map<SofiaDayAndNight.Data.Models.Event>(It.IsAny<EventViewModel>()))
                .Returns(new SofiaDayAndNight.Data.Models.Event());
            var mockedPhotoHelper = new Mock<IPhotoHelper>();

            var eventController = new EventController(mockedEventService.Object,
                mockedIndividualService.Object,
                mockedOrganizationService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object);
            eventController.ModelState.Clear();

            var username = "testUsername";
            var model = new EventViewModel();
            model.Title = "1";
            var mockedUpload = new Mock<HttpPostedFileBase>();
            mockedUpload.SetupGet(x => x.ContentLength).Returns(1);
            mockedPhotoHelper.Setup(x => x.UploadImage(mockedUpload.Object)).Verifiable();

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(UserRole.Individual.ToString())).Returns(true);
            principal.SetupGet(x => x.Identity.Name).Returns(username);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            eventController.ControllerContext = controllerContext.Object;

            // Act
            var result = eventController.Create(model, mockedUpload.Object);

            // Assert
            mockedPhotoHelper.Verify(x => x.UploadImage(mockedUpload.Object), Times.Once);
        }
    }
}
