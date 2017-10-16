using System.Collections.Generic;
using System.Linq;
using System.Net;

using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using SofiaDayAndNight.Data.Services.Contracts;
using SofiaDayAndNight.Web.Areas.User.Controllers;
using SofiaDayAndNight.Web.Areas.User.Models;
using SofiaDayAndNight.Web.Helpers;

namespace SofiaDayAndNight.UnitTests.Controllers.Individual
{
    [TestFixture]
    public class EventsList_Should
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
                .WithCallTo(x => x.EventsList(null))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void CallMapMethod_WhenCollectionFound()
        {
            // Arrange
            var username = "testUsername";
            var model = new EventViewModel();

            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetCurrentEvents(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Event>() { new SofiaDayAndNight.Data.Models.Event() });
            mockedIndividualService.Setup(x => x.GetPassedEvents(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Event>() { new SofiaDayAndNight.Data.Models.Event() });
            mockedIndividualService.Setup(x => x.GetUpcomingEvents(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Event>() { new SofiaDayAndNight.Data.Models.Event() });

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<EventViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Event>()))
                .Returns(model);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            //var principal = new Mock<IPrincipal>();
            //var controllerContext = new Mock<ControllerContext>();
            //principal.SetupGet(x => x.Identity.Name).Returns(username);
            //controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);
            //controller.ControllerContext = controllerContext.Object;

            // Act
            controller.EventsList(username);

            // Assert
            mockedMapper.Verify(x => x.Map<EventViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Event>()), Times.Exactly(3));
        }

        [Test]
        public void ReturnViewWithCorrectModel_WhenUsernameMatch()
        {
            //Arrange
            var username = "testUsername";
            var viewEvent = new EventViewModel();
            var resultModel = new EventsListViewModel();
            resultModel.OngoingEvents = new List<EventViewModel>() { viewEvent };
            resultModel.UpCommingEvents = new List<EventViewModel>() { viewEvent };
            resultModel.PassedEvents = new List<EventViewModel>() { viewEvent };
            
            var mockedIndividualService = new Mock<IIndividualService>();
            mockedIndividualService.Setup(x => x.GetCurrentEvents(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Event>() { new SofiaDayAndNight.Data.Models.Event() });
            mockedIndividualService.Setup(x => x.GetPassedEvents(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Event>() { new SofiaDayAndNight.Data.Models.Event() });
            mockedIndividualService.Setup(x => x.GetUpcomingEvents(username))
                .Returns(new List<SofiaDayAndNight.Data.Models.Event>() { new SofiaDayAndNight.Data.Models.Event() });

            var mockedOrganizationService = new Mock<IOrganizationService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<EventViewModel>(
                It.IsAny<SofiaDayAndNight.Data.Models.Event>()))
                .Returns(viewEvent);
            var mockedPhotoHelper = new Mock<IPhotoHelper>();
            var mockedUserProvider = new Mock<IUserProvider>();

            var controller = new IndividualController(
                mockedIndividualService.Object,
                mockedMapper.Object,
                mockedPhotoHelper.Object,
                mockedUserProvider.Object);

            //Act & Assert
            controller
             .WithCallTo(x => x.EventsList(username))
             .ShouldRenderPartialView("_EventsListPartial")
             .WithModel<EventsListViewModel>(model =>
             {
                 Assert.AreEqual(1, model.UpCommingEvents.Count());
                 Assert.AreEqual(1, model.PassedEvents.Count());
                 Assert.AreEqual(1, model.OngoingEvents.Count());
                 Assert.AreEqual(viewEvent, model.UpCommingEvents.First());
                 Assert.AreEqual(viewEvent, model.PassedEvents.First());
                 Assert.AreEqual(viewEvent, model.OngoingEvents.First());
             });
        }
    }
}
